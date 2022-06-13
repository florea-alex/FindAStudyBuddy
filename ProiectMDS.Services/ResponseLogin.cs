﻿using ProiectMDS.DAL.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.Services
{
    public class ResponseLogin
    {
        public bool Success { get; set; }
        public string AccesToken { get; set; }
        public string RefreshToken { get; set; }
        public string? Role { get; set; }
        public int Id { get; set; }
        public List<int>? Users { get; set; } 
    }
}