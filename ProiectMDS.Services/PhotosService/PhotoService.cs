using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProiectMDS.DAL;
using ProiectMDS.DAL.Entities;
using ProiectMDS.DAL.Models.PhotoModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.Wrappers;

namespace ProiectMDS.Services.PhotosService
{
    public class PhotoService : IPhotoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PhotoService(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task AddPhoto(PhotoPostModel photoData, int userId)
        {
            string[] extentions = { "jpeg", "jpg", "png" };
            var extention = photoData.Files.FileName.Split(".");
            if (!extentions.Contains(extention[1]))
            {
                throw new Exception("Upload only photos!");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) { throw new KeyNotFoundException("User doesn't exist"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);
            if (profile == null) { throw new KeyNotFoundException("There is no profile setup"); }

            var connection = _configuration.GetSection("AZURE_CONNECTION_STRING").Get<String>();
            BlobServiceClient blobServiceClient = new BlobServiceClient(connection);
           
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(user.UserName);
            if (!containerClient.Exists())
            {
                containerClient = await blobServiceClient.CreateBlobContainerAsync(user.UserName, Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }

            var blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpeg" };

            using (Stream stream = new MemoryStream())
            {
                photoData.Files.CopyTo(stream);
                stream.Position = 0;
                BlobClient blob = containerClient.GetBlobClient(user.UserName);
                if (!blob.Exists())
                {
                    await blob.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeader });
                    profile.Photo = blob.Uri.AbsoluteUri;
                    await _context.SaveChangesAsync();
                }          
            }
        }

        public async Task DeletePhoto(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) { throw new KeyNotFoundException("User doesn't exist"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);
            if (profile == null) { throw new KeyNotFoundException("There is no profile setup"); }

            var connection = _configuration.GetSection("AZURE_CONNECTION_STRING").Get<String>();
            BlobServiceClient blobServiceClient = new BlobServiceClient(connection);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(user.UserName);
            if (!containerClient.Exists())
            {
                throw new Exception("There is no photo uploaded on your profile");
            }

            BlobClient blob = containerClient.GetBlobClient(user.UserName);
            if (blob.Exists())
            {
                blob.Delete();
                profile.Photo = String.Empty;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("There is no photo uploaded on your profile");
            }
        }

        public async Task<Response<string>> GetById(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) { throw new KeyNotFoundException("User doesn't exist"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);
            if (profile == null) { throw new KeyNotFoundException("There is no profile setup"); }

            var connection = _configuration.GetSection("AZURE_CONNECTION_STRING").Get<String>();
            BlobServiceClient blobServiceClient = new BlobServiceClient(connection);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(user.UserName);
            if (!containerClient.Exists())
            {
                throw new Exception("There is no photo uploaded on your profile");
            }

            BlobClient blob = containerClient.GetBlobClient(user.UserName);
            var properties = "";
            if (blob.Exists())
            {
                properties = blob.Uri.AbsoluteUri;
            }

            return new Response<string>(properties);
        }

        public async Task UpdatePhoto(PhotoPostModel photoData, int userId)
        {
            string[] extentions = { "jpeg", "jpg", "png" };
            var extention = photoData.Files.FileName.Split(".");
            if (!extentions.Contains(extention[1]))
            {
                throw new Exception("Upload only photos!");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) { throw new KeyNotFoundException("User doesn't exist"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);
            if (profile == null) { throw new KeyNotFoundException("There is no profile setup"); }

            var connection = _configuration.GetSection("AZURE_CONNECTION_STRING").Get<String>();
            BlobServiceClient blobServiceClient = new BlobServiceClient(connection);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(user.UserName);
            if (!containerClient.Exists())
            {
                containerClient = await blobServiceClient.CreateBlobContainerAsync(user.UserName, Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }

            var blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpeg" };

            using (Stream stream = new MemoryStream())
            {
                photoData.Files.CopyTo(stream);
                stream.Position = 0;
                BlobClient blob = containerClient.GetBlobClient(user.UserName);
                if (blob.Exists())
                {
                    blob.Delete();
                }

                await blob.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeader });
                profile.Photo = blob.Uri.AbsoluteUri;
                await _context.SaveChangesAsync();
            }
        }
    }
}
