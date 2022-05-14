using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProiectMDS.DAL;
using ProiectMDS.DAL.Entities;
using ProiectMDS.DAL.Models.CourseModels;
using ProiectMDS.DAL.Models.UserModels;
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
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUriServices _uriServices;

        public CourseService(AppDbContext appDbContext, IMapper mapper, IUriServices uriServices)
        {
            _context = appDbContext;
            _mapper = mapper;
            _uriServices = uriServices;
        }
        public async Task Create(CoursePostModel model, int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"There is no user with id {userId}"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);

            if (profile == null) { throw new KeyNotFoundException($"User {userId} has no profile completed yet"); }

            var courses = _context.Courses.Where(x => x.ProfileId == profile.Id).Any(x => x.courseName == model.courseName);

            if (courses) { throw new Exception("You have already entered this course!"); }

            var course = _mapper.Map<Courses>(model);
            course.ProfileId = user.ProfileId;

            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAll(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"There is no user with id {userId}"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);

            if (profile == null) { throw new KeyNotFoundException($"User {userId} has no profile completed yet"); }

            var courses = await _context.Courses.Where(x => x.ProfileId == profile.Id).ToListAsync();

            if (courses.Count == 0) { throw new Exception("There is no course that I can delete"); }

            _context.Courses.RemoveRange(courses);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int userId, int courseId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"There is no user with id {userId}"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);

            if (profile == null) { throw new KeyNotFoundException($"User {userId} has no profile completed yet"); }

            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null) { throw new KeyNotFoundException($"There is no course with id: {courseId}"); }

            _context.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<Response<CourseGetModel>> GetById(int userId, int courseId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"There is no user with id {userId}"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);

            if (profile == null) { throw new KeyNotFoundException($"User {userId} has no profile completed yet"); }

            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null) { throw new KeyNotFoundException($"There is no course with id: {courseId}"); }

            var courseGetModel = _mapper.Map<CourseGetModel>(course);
            return new Response<CourseGetModel>(courseGetModel);
        }

        public async Task Update(CoursePutModel model, int userId, int courseId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"There is no user with id {userId}"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);

            if (profile == null) { throw new KeyNotFoundException($"User {userId} has no profile completed yet"); }

            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null) { throw new KeyNotFoundException($"There is no course with id: {courseId}"); }

            _mapper.Map<CoursePutModel, Courses>(model, course);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResponse<List<CourseGetModel>>> GetAll(int userId, CoursesFilter filter, string route)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"There is no user with id {userId}"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);

            if (profile == null) { throw new KeyNotFoundException($"User {userId} has no profile completed yet"); }

            IQueryable<Courses> courses = _context.Courses.Where(x => x.ProfileId == profile.Id);

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
                    courses = !filter.descending == false ? courses.OrderBy(s => s.Id) : courses.OrderByDescending(x => x.Id);
                    break;
            }

            var coursesModels = courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(_mapper.Map<CourseGetModel>)
                .ToList();

            var usersListCount = await _context.Users.CountAsync();

            var pagedResponse = PaginationHelper.CreatePagedReponse<CourseGetModel>(coursesModels, filter, usersListCount, _uriServices, route);

            return pagedResponse;
        }
    }
}
