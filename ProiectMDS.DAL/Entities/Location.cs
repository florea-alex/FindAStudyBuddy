using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.DAL.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string? County { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? Number { get; set; }
        public virtual UserProfile Profile { get; set; }
    }
}
