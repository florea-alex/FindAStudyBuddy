using ProiectMDS.DAL.Models.BaseCourseModel;
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
    public interface IBaseCourseService
    {
        Task<Response<BaseGetModel>> GetById(int courseId);
        Task Create(BasePostModel model);
        Task Update(BasePutModel model, int courseId);
        Task DeleteAll();
        Task Delete(int courseId);
        Task<PagedResponse<List<BaseGetModel>>> GetAll(CoursesFilter filter, string route);
    }
}
