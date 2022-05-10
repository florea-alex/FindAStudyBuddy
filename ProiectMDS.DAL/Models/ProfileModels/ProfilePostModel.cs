using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.DAL.Models.ProfileModels
{
    public class ProfilePostModel
    {
        public string University { get; set; }
        public int yearOfStudy { get; set; }
        public string? Description { get; set; }
        public string? phoneNumber { get; set; }
    }
}
