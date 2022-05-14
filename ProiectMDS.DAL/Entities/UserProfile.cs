using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProiectMDS.DAL.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.DAL.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string University { get; set; }
        public int yearOfStudy { get; set; }
        public string? Description { get; set; }
        public string? phoneNumber { get; set; }
        public int ? LocationId { get; set; }
        public string? Photo { get; set; }
        public virtual User User { get; set; }
        public virtual Location Address { get; set; }
        public virtual ICollection<Courses> Courses { get; set; }
    }
}
