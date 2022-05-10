using Microsoft.AspNetCore.Mvc;
using ProiectMDS.DAL.Models.ProfileModels;
using ProiectMDS.Services.ProfileService;
using System.ComponentModel.DataAnnotations;

namespace ProiectMDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IProfileServices _profileServices;

        public ProfileController(IProfileServices profileServices)
        {
            _profileServices = profileServices;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int userId)
        {
            var profile = await _profileServices.GetById(userId);

            return Ok(profile);
        }

        [HttpPut("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody][Required] ProfilePutModel model, [FromQuery] int userId)
        {
            await _profileServices.Update(model, userId);

            return Ok("Updated succesfully");
        }

        [HttpDelete("DeleteUserProfile")]
        public async Task<IActionResult> DeleteUserProfile([FromQuery] int userId)
        {
            await _profileServices.Delete(userId);

            return Ok("Deleted succesfully");
        }

        [HttpPost("CreateUserProfile")]
        public async Task<IActionResult> CreateUserProfile([FromBody][Required] ProfilePostModel model, int userId)
        {
            await _profileServices.Create(model, userId);

            return Ok("Created succesfully");
        }
    }
}
