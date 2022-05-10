using ProiectMDS.DAL.Entities;
using ProiectMDS.DAL.Models.CourseModels;
using ProiectMDS.DAL.Models.LocationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.DAL.Models.ProfileModels
{
    public class ProfileGetModel
    {
        public int Id { get; set; }
        public string University { get; set; }
        public int yearOfStudy { get; set; }
        public string? Description { get; set; }
        public string? phoneNumber { get; set; }
        public LocationGetModel Address { get; set; }
        public ICollection<CourseGetModel> Courses { get; set; }
    }
}
