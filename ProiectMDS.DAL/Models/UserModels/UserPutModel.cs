using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.DAL.Models.UserModels
{
    public class UserPutModel
    {
        public string FirstName { get; set; } //momentan doar atat poate sa modifice
        public string LastName { get; set; }
        public string Faculty { get; set; }
    }
}
