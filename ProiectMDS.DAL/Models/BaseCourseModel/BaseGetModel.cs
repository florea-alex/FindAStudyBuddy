﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.DAL.Models.BaseCourseModel
{
    public class BaseGetModel
    {
        public int courseId { get; set; }
        public string courseName { get; set; }
        public int credits { get; set; }
    }
}