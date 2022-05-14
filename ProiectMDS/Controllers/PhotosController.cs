using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectMDS.DAL;
using ProiectMDS.DAL.Entities;
using ProiectMDS.DAL.Models;
using ProiectMDS.DAL.Models.PhotoModels;
using ProiectMDS.Services.PhotosService;

namespace ProiectMDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : Controller
    {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost("Add-Image")]
        public async Task<IActionResult> AddImage([FromForm] PhotoPostModel photoData, int userId)
        {
            if (!ModelState.IsValid) //habar n-am ce face
                return (IActionResult)Task.FromResult(photoData);

            await _photoService.AddPhoto(photoData, userId);

            return Ok("Added succesfully");
        }

        [HttpDelete("Delete-Image")]
        public async Task<IActionResult> DeleteImage(int userId)
        {
            await _photoService.DeletePhoto(userId);

            return Ok("Deleted succesfully");
        }

        [HttpGet("Get-Image")]
        public async Task<IActionResult> GetImage(int userId)
        {
            var photo = await _photoService.GetById(userId);

            return Ok(photo);
        }

        [HttpPut("Update-Image")]
        public async Task<IActionResult> UpdateImage([FromForm] PhotoPostModel photoData, int userId)
        {
            await _photoService.UpdatePhoto(photoData, userId);

            return Ok("Updated succesfully");
        }
    }
}
