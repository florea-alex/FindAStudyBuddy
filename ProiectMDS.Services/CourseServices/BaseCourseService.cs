using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProiectMDS.DAL;
using ProiectMDS.DAL.Entities;
using ProiectMDS.DAL.Models.BaseCourseModel;
using ProiectMDS.DAL.Models.CourseModels;
using ProiectMDS.Services.UriServicess;
using ProiectSoft.BLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Wrappers;
using Utils.Wrappers.Filters;

namespace ProiectMDS.Services.CourseServices
{
    public class BaseCourseService : IBaseCourseService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUriServices _uriService;

        public BaseCourseService(AppDbContext context, IMapper mapper, IUriServices uriServices)
        {
            _context = context;
            _mapper = mapper;
            _uriService = uriServices;
        }

        public async Task Create(BasePostModel model)
        {
            var doesThisCourseExist = await _context.BaseCourses.AnyAsync(x => x.courseName == model.courseName);

            if (doesThisCourseExist) { throw new Exception("This course already exists"); }

            var course = _mapper.Map<BaseCourses>(model);

            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int courseId)
        {
            var course = await _context.BaseCourses.FirstOrDefaultAsync(x => x.CourseId == courseId);
            if (course == null) { throw new KeyNotFoundException($"There is no course with id: {courseId}"); }

            _context.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAll()
        {
            var courses = await _context.BaseCourses.ToListAsync();

            if (courses.Count == 0) { throw new Exception("There is no course that I can delete"); }

            _context.BaseCourses.RemoveRange(courses);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResponse<List<BaseGetModel>>> GetAll(CoursesFilter filter, string route)
        {
            IQueryable<BaseCourses> courses = _context.BaseCourses;

            if (!string.IsNullOrEmpty(filter.searchName))
            {
                courses = courses.Where(x => x.courseName!.Contains(filter.searchName));
            }

            switch (filter.orderBy)
            {
                case "Name":
                    courses = !filter.descending == false ? courses.OrderBy(x => x.courseName) : courses.OrderByDescending(x => x.courseName);
                    break;
                default:
                    courses = !filter.descending == false ? courses.OrderBy(s => s.CourseId) : courses.OrderByDescending(x => x.CourseId);
                    break;
            }

            var coursesModels = courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(_mapper.Map<BaseGetModel>)
                .ToList();

            var usersListCount = await _context.Users.CountAsync();

            var pagedResponse = PaginationHelper.CreatePagedReponse<BaseGetModel>(coursesModels, filter, usersListCount, _uriService, route);

            return pagedResponse;
        }

        public async Task<Response<BaseGetModel>> GetById(int courseId)
        {
            var course = await _context.BaseCourses.FirstOrDefaultAsync(x => x.CourseId == courseId);

            if (course == null) { throw new KeyNotFoundException($"There is no course with id: {courseId}"); }

            var courseGetModel = _mapper.Map<BaseGetModel>(course);
            return new Response<BaseGetModel>(courseGetModel);
        }

        public async Task Update(BasePutModel model, int courseId)
        {
            var course = await _context.BaseCourses.FirstOrDefaultAsync(x => x.CourseId == courseId);

            if (course == null) { throw new KeyNotFoundException($"There is no course with id: {courseId}"); }

            _mapper.Map<BasePutModel, BaseCourses>(model, course);
            await _context.SaveChangesAsync();
        }
    }
}
