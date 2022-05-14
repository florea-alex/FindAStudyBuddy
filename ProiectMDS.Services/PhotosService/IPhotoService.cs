using Microsoft.AspNetCore.Mvc;
using ProiectMDS.DAL.Entities;
using ProiectMDS.DAL.Models.PhotoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Wrappers;

namespace ProiectMDS.Services.PhotosService
{
    public interface IPhotoService
    {
        Task AddPhoto(PhotoPostModel photoData, int userId);
        Task DeletePhoto(int userId);
        Task<Response<string>> GetById(int userId);
        Task UpdatePhoto(PhotoPostModel photoData, int userId);
    }
}
