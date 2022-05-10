using ProiectMDS.DAL.Models.CourseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Wrappers;
using Utils.Wrappers.Filters;

namespace ProiectMDS.Services.CourseServices
{
    public interface ICourseService
    {
        Task<Response<CourseGetModel>> GetById(int userId, int courseId);
        Task Create(CoursePostModel model, int userId);
        Task Update(CoursePutModel model, int userId, int courseId);
        Task DeleteAll(int userId);
        Task Delete(int userId, int courseId);
        Task<PagedResponse<List<CourseGetModel>>> GetAll(int userId, CoursesFilter filter, string route);
    }
}
