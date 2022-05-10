using Microsoft.AspNetCore.Mvc;
using ProiectMDS.DAL.Models.LoginModels;
using ProiectMDS.DAL.Models.RegisterModels;
using ProiectMDS.DAL.Models.UserModels;
using ProiectMDS.Services.AuthService;

namespace ProiectSOFT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            await _authManager.Register(model);

            return Ok("Registration was successful");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _authManager.Login(model);

            return Ok(result);
        }
        [HttpPost("Change-Password")]
        public async Task<IActionResult> ChangePassword(ForgotPassModel forgotPassModel)
        {
            var response = await _authManager.ChangePassword(forgotPassModel);

            return Ok(response);
        }

        [HttpPost("reset-pass-token")]
        public async Task<IActionResult> ResetPassToken(ResetPassTokenModel resetPassTokenModel)
        {
            var response = await _authManager.ResetPasswordToken(resetPassTokenModel);

            return Ok(response);
        }

        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword(ResetPassModel resetPassModel)
        {
            var response = await _authManager.ResetPassword(resetPassModel);

            return Ok(response);
        }
    }
}
