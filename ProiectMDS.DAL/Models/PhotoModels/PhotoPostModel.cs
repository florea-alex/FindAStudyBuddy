using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.DAL.Models.PhotoModels
{
    public class PhotoPostModel
    {
        [FromForm]
        [NotMapped]
        public IFormFile Files { get; set; }
    }
}
