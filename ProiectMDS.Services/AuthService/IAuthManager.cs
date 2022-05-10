using ProiectMDS.DAL.Models.LoginModels;
using ProiectMDS.DAL.Models.RegisterModels;
using ProiectMDS.DAL.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Wrappers;

namespace ProiectMDS.Services.AuthService
{
    public interface IAuthManager
    {
        Task Register(RegisterModel registerModel);
        Task<ResponseLogin> Login(LoginModel loginModel);
        Task<Response<ForgotPassModel>> ChangePassword(ForgotPassModel forgotPassModel);
        Task<Response<ResetPassTokenModel>> ResetPasswordToken(ResetPassTokenModel resetPassModel);
        Task<Response<ResetPassModel>> ResetPassword(ResetPassModel resetPassModel);
    }
}
