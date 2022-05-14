using Microsoft.AspNetCore.Mvc;
using ProiectMDS.DAL.Models.BaseCourseModel;
using ProiectMDS.Services.CourseServices;
using System.ComponentModel.DataAnnotations;
using Utils.Wrappers.Filters;

namespace ProiectMDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseCoursesController : Controller
    {
        private readonly IBaseCourseService _course;

        public BaseCoursesController(IBaseCourseService course)
        {
            _course = course;
        }

        [HttpPost("Add-Courses")]
        public async Task<IActionResult> CreateCourse([FromBody][Required] BasePostModel model)
        {
            await _course.Create(model);

            return Ok("Created succesfully");
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int courseId)
        {
            var course = await _course.GetById(courseId);

            return Ok(course);
        }

        [HttpPut("UpdateUserCourse")]
        public async Task<IActionResult> UpdateUserCourse([FromBody][Required] BasePutModel model, [FromQuery] int courseId)
        {
            await _course.Update(model, courseId);

            return Ok("Updated succesfully");
        }

        [HttpDelete("DeleteUserCourse")]
        public async Task<IActionResult> DeleteUserCourse([FromQuery] int courseId)
        {
            await _course.Delete(courseId);

            return Ok("Deleted succesfully");
        }

        [HttpDelete("DeleteAllUserCourses")]
        public async Task<IActionResult> DeleteAllUserCourses()
        {
            await _course.DeleteAll();

            return Ok("Deleted succesfully");
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] CoursesFilter filter)
        {
            var route = Request.Path.Value;

            var users = await _course.GetAll(filter, route);

            return Ok(users);
        }
    }
}
