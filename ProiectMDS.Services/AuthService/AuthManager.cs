using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProiectMDS.DAL;
using ProiectMDS.DAL.Entities.Auth;
using ProiectMDS.DAL.Models.LoginModels;
using ProiectMDS.DAL.Models.RegisterModels;
using ProiectMDS.DAL.Models.UserModels;
using ProiectMDS.Services;
using ProiectMDS.Services.AuthService;
using ProiectMDS.Services.EmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Wrappers;

namespace ProiectMDS.Services.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenHelper _tokenHelper;
        private readonly AppDbContext _appDbContext;
        private readonly IEmailServices _emailServices;
        private readonly IMapper _mapper;

        public AuthManager(UserManager<User> userManager,
               SignInManager<User> signInManager,
               ITokenHelper tokenHelper,
               AppDbContext appDbContext,
               IEmailServices emailServices,
               IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHelper = tokenHelper;
            _appDbContext = appDbContext;
            _emailServices = emailServices;
            _mapper = mapper;
        }

        public async Task<ResponseLogin> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginModel.UserName);

                if (user == null)
                {
                    throw new KeyNotFoundException($"Email/Username:{loginModel.UserName} does not exist!");
                }            
            }

            var role = await _userManager.GetRolesAsync(user);

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

            if (result.Succeeded)
            {
                var token = await _tokenHelper.CreateAccesToken(user);
                var refreshToken = _tokenHelper.CreateRefreshToken();

                //user.RefreshToken = refreshToken;
                //await _userManager.UpdateAsync(user);

                var refreshTokenResult = await SetAuthenticationToken(user, "", "refreshToken", refreshToken);

                if (!refreshTokenResult) { return new ResponseLogin { Success = false }; }

                await _emailServices.SendEmailLogin(user.Email, "New login!", "<h1>Hey! \nNew login to your account noticed</h1><p>New login to your account at " + DateTime.Now + "</p>");
                //await _emailServices.SendEmailRegister(user.Email, loginModel.UserName); //e pentru test mai rapid

                return new ResponseLogin
                {
                    Success = true,
                    AccesToken = token,
                    RefreshToken = refreshToken,
                    Role = role.FirstOrDefault(),
                    Id = user.Id
                };
            }
            else
            {
                throw new Exception("Incorect password!");            
            }
        }

        public async Task Register(RegisterModel registerModel)
        {
            if (_appDbContext.Users.Any(user => user.UserName == registerModel.UserName))
            {
                throw new Exception($"{registerModel.UserName} is already used!");
            }

            if (_appDbContext.Users.Any(user => user.Email == registerModel.Email))
            {
                throw new Exception($"{registerModel.Email} is already used!");
            }

            var user = _mapper.Map<User>(registerModel); 

            user.DateCreated = DateTime.Now;
            user.DateModified = DateTime.Now;


            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                await _emailServices.SendEmailRegister(user.Email, user.UserName); // dupa ce te inregistrezi primesti un email de welcome (am luat un template gratis de pe net sa para mai dragut)

                await _userManager.AddToRoleAsync(user, registerModel.Role);
            }
            else
            {
                throw new Exception("User exists / Data not entered correctly!");
            }
        }

        public async Task<Response<ForgotPassModel>> ChangePassword(ForgotPassModel forgotPassModel)
        {   // trebuie sa pui in config unique email is required !
            var user = await _userManager.FindByEmailAsync(forgotPassModel.Email);

            if (user == null)
            {
                throw new Exception($"Email:{forgotPassModel.Email} does not exist!");
            }

            if (string.Compare(forgotPassModel.newPassword, forgotPassModel.confirmNewPassord) != 0)
            {
                throw new Exception($"Passwords doesn't match, {forgotPassModel.newPassword} and {forgotPassModel.confirmNewPassord}");
            }
           
            var result = await _userManager.ChangePasswordAsync(user, forgotPassModel.currentPassword, forgotPassModel.newPassword);
        
            if (!result.Succeeded)
            {
                /*var errors = new List<string>();
                
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                throw new Exception(string.Join(",", errors));*/

                throw new Exception("Cannot change password!");
            }

            return new Response<ForgotPassModel>(true, "Password was changed succesfully!");
        }

        public async Task<Response<ResetPassTokenModel>> ResetPasswordToken(ResetPassTokenModel resetPassModel)
        {
            var user = await _userManager.FindByEmailAsync(resetPassModel.Email);

            if (user == null)
            {
                throw new KeyNotFoundException($"Email:{resetPassModel.Email} does not exist");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            return new Response<ResetPassTokenModel>(true, token);
        }

        public async Task<Response<ResetPassModel>> ResetPassword(ResetPassModel resetPassModel)
        {
            var user = await _userManager.FindByEmailAsync(resetPassModel.Email);

            if (user == null)
            {
                throw new Exception($"Email:{resetPassModel.Email} does not exist!");
            }

            if (string.Compare(resetPassModel.newPassword, resetPassModel.confirmNewPassord) != 0)
            {
                throw new Exception($"Passwords doesn't match, {resetPassModel.newPassword} and {resetPassModel.confirmNewPassord}");
            }

            if (string.IsNullOrEmpty(resetPassModel.Token))
            {
                throw new Exception("Invalid token");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPassModel.Token, resetPassModel.newPassword);

            if (!result.Succeeded) { throw new Exception("Cannot change password!"); }

            return new Response<ResetPassModel>(true, "You have successfully reset your password!");
        }

        private async Task<bool> SetAuthenticationToken(User user, string loginProvider, string name, string value)
        {
            if (user == null || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) { return false; }

            //var existingToken = await _appDbContext.UserTokens.FirstOrDefaultAsync();

            var existingToken = await _appDbContext.UserTokens.FirstOrDefaultAsync(x => x.Name == name &&
                                                                                   x.LoginProvider == loginProvider &&
                                                                                   x.UserId == user.Id);

            if (existingToken == null)
            {
                var newToken = new IdentityUserToken<int>()
                {
                    UserId = user.Id,
                    LoginProvider = loginProvider,
                    Name = name,
                    Value = value
                };

                await _appDbContext.UserTokens.AddAsync(newToken);
            }
            else
            {
                existingToken.Value = value;
            }

            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
