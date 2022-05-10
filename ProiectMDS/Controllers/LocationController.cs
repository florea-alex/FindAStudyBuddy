using Microsoft.AspNetCore.Mvc;
using ProiectMDS.DAL.Models;
using ProiectMDS.DAL.Models.LocationModels;
using ProiectMDS.Services.LocationServices;
using System.ComponentModel.DataAnnotations;

namespace ProiectMDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationServices;

        public LocationController(ILocationService locationServices)
        {
            _locationServices = locationServices;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int userId)
        {
            var location = await _locationServices.GetById(userId);

            return Ok(location);
        }

        [HttpPost("AddLocation")]
        public async Task<IActionResult> AddLocation([FromBody][Required] LocationPostModel model, int userId)
        {
            await _locationServices.Create(model, userId);

            return Ok("Created succesfully");
        }

        [HttpPut("UpdateLocation")]
        public async Task<IActionResult> UpdateLocation([FromBody][Required] LocationPutModel model, [FromQuery] int userId)
        {
            await _locationServices.Update(model, userId);

            return Ok("Updated succesfully");
        }

        [HttpDelete("DeleteLocation")]
        public async Task<IActionResult> DeleteLocation([FromQuery] int userId)
        {
            await _locationServices.Delete(userId);

            return Ok("Deleted succesfully");
        }
    }
}
