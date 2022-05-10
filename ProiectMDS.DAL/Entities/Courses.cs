using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.DAL.Entities
{
    public class Courses
    {
        public int Id { get; set; }
        public string courseName { get; set; }
        public int Credit { get; set; }
        public int? ProfileId { get; set; }
        public virtual UserProfile Profile { get; set; }
    }
}
