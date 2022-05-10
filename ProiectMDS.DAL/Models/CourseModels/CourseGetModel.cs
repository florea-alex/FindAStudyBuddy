using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.DAL.Models.CourseModels
{
    public class CourseGetModel
    {
        public int Id { get; set; }
        public string courseName { get; set; }
        public int Credit { get; set; }
    }
}
