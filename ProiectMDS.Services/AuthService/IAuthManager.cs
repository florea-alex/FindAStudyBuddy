using ProiectMDS.DAL.Models.LoginModels;
using ProiectMDS.DAL.Models.RegisterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.Services.AuthService
{
    public interface IAuthManager
    {
        Task<bool> Register(RegisterModel registerModel);
        Task<ResponseLogin> Login(LoginModel loginModel);
    }
}
