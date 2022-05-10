using ProiectMDS.DAL.Models.ProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Wrappers;

namespace ProiectMDS.Services.ProfileService
{
    public interface IProfileServices
    {
        Task<Response<ProfileGetModel>> GetById(int id);
        Task Create(ProfilePostModel model, int userId);
        Task Update(ProfilePutModel model, int id);
        Task Delete(int id);
    }
}
