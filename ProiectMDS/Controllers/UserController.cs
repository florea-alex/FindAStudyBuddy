using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProiectMDS.DAL.Entities.Auth;
using ProiectMDS.DAL.Models.UserModels;
using ProiectMDS.Services.UserServices;
using System.ComponentModel.DataAnnotations;
using Utils.Wrappers.Filters;

namespace ProiectMDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userServices;

        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] UserFilter filter)
        {
            var route = Request.Path.Value;

            var users = await _userServices.GetAll(filter, route);

            return Ok(users);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var user = await _userServices.GetById(id);

            return Ok(user);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody][Required] UserPutModel model, [FromQuery] int id)
        {
            await _userServices.Update(model, id);

            return Ok("Updated succesfully");
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            await _userServices.Delete(id);

            return Ok("Deleted succesfully");
        }

        [HttpPost("Change-Email")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailModel changeEmailModel)
        {
            var result = await _userServices.ChangeEmail(changeEmailModel);

            return Ok(result);
        }   
    }
}
