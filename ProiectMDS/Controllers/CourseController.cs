using Microsoft.AspNetCore.Mvc;
using ProiectMDS.DAL.Models.CourseModels;
using ProiectMDS.Services.CourseServices;
using System.ComponentModel.DataAnnotations;

namespace ProiectMDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly ICourseService _course;

        public CourseController(ICourseService course)
        {
            _course = course;
        }

        [HttpPost("Add-Courses")]
        public async Task<IActionResult> CreateCourse([FromBody][Required] CoursePostModel model, int userId)
        {
            await _course.Create(model, userId);

            return Ok("Created succesfully");
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int userId, int courseId)
        {
            var course = await _course.GetById(userId, courseId);

            return Ok(course);
        }

        [HttpPut("UpdateUserCourse")]
        public async Task<IActionResult> UpdateUserCourse([FromBody][Required] CoursePutModel model, [FromQuery] int userId, [FromQuery] int courseId)
        {
            await _course.Update(model, userId, courseId);

            return Ok("Updated succesfully");
        }

        [HttpDelete("DeleteUserCourse")]
        public async Task<IActionResult> DeleteUserCourse([FromQuery] int userId, [FromQuery] int courseId)
        {
            await _course.Delete(userId, courseId);

            return Ok("Deleted succesfully");
        }

        [HttpDelete("DeleteAllUserCourses")]
        public async Task<IActionResult> DeleteAllUserCourses([FromQuery] int userId)
        {
            await _course.DeleteAll(userId);

            return Ok("Deleted succesfully");
        }

    }
}
