using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProiectMDS.DAL;
using ProiectMDS.DAL.Entities.Auth;
using ProiectMDS.DAL.Models.LoginModels;
using ProiectMDS.DAL.Models.RegisterModels;
using ProiectMDS.Services;
using ProiectMDS.Services.AuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.Services.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenHelper _tokenHelper;
        private readonly AppDbContext _appDbContext;

        public AuthManager(UserManager<User> userManager,
               SignInManager<User> signInManager,
               ITokenHelper tokenHelper,
               AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHelper = tokenHelper;
            _appDbContext = appDbContext;
        }

        public async Task<ResponseLogin> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            var role = await _userManager.GetRolesAsync(user);

            if (user == null)
            {
                return new ResponseLogin
                {
                    Success = false
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

            if (result.Succeeded)
            {
                var token = await _tokenHelper.CreateAccesToken(user);
                var refreshToken = _tokenHelper.CreateRefreshToken();

                //user.RefreshToken = refreshToken;
                //await _userManager.UpdateAsync(user);

                var refreshTokenResult = await SetAuthenticationToken(user, "", "refreshToken", refreshToken);

                if (!refreshTokenResult) { return new ResponseLogin { Success = false }; }

                return new ResponseLogin
                {
                    Success = true,
                    AccesToken = token,
                    RefreshToken = refreshToken,
                    Role = role.FirstOrDefault()
                };
            }
            else
            {
                return new ResponseLogin
                {
                    Success = false
                };            
            }
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {

            var user = new User
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Email = registerModel.Email,
                UserName = registerModel.UserName,
                Faculty = registerModel.Faculty,
                DateModified = DateTime.Now,
                DateCreated = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, registerModel.Role);

                return true;
            }

            return false;
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
