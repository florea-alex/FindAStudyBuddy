using ProiectMDS.DAL.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Wrappers;
using Utils.Wrappers.Filters;

namespace ProiectMDS.Services.UserServices
{
    public interface IUserService
    {
        Task<PagedResponse<List<UserGetModel>>> GetAll(UserFilter filter, string route);
        Task<Response<UserGetModel>> GetById(int id);
        Task Update(UserPutModel model, int id);
        Task Delete(int id);
        Task<Response<ResetPassModel>> ChangeEmail(ChangeEmailModel changeEmailModel);
    }
}
