using ProiectMDS.DAL.Models;
using ProiectMDS.DAL.Models.LocationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Wrappers;

namespace ProiectMDS.Services.LocationServices
{
    public interface ILocationService
    {
        Task<Response<LocationGetModel>> GetById(int userId);
        Task Create(LocationPostModel model, int userId);
        Task Update(LocationPutModel model, int userId);
        Task Delete(int userId);
    }
}
