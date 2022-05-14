using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.DAL.Entities
{
    public class BaseCourses
    {
        public int CourseId { get; set; }
        public string courseName { get; set; }
        public int credits { get; set; }
    }
}
