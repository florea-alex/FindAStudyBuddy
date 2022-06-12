using Microsoft.AspNetCore.Mvc;
using ProiectMDS.DAL.Models.UserConnectionsModels;
using ProiectMDS.Services.UserConnectionsServices;
using System.ComponentModel.DataAnnotations;

namespace ProiectMDS.Controllers
{
    [Route("api/[controller]")]
    public class UserConnectionsController : Controller
    {
        private readonly IUserConnService _userConn;

        public UserConnectionsController(IUserConnService userConn)
        {
            _userConn = userConn;
        }

        [HttpPost("AddFriend")]
        public async Task<IActionResult> AddFriend([Required] AddFriendModel model)
        {
            var result = await _userConn.AddFriend(model);

            return Ok(result);
        }

        [HttpDelete("RemoveFriend")]
        public async Task<IActionResult> Remove([FromQuery] int friendId)
        {
            var result = await _userConn.RemoveFriend(friendId);

            return Ok(result);
        }

        [HttpGet("GetAllFriends")]
        public async Task<IActionResult> GetFriends([FromQuery] int userId)
        {
            var result = await _userConn.GetAllFriends(userId);

            return Ok(result);
        }
    }
}
