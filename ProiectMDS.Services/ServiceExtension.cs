﻿using Microsoft.Extensions.DependencyInjection;
using ProiectMDS.Services.CourseServices;
using ProiectMDS.Services.EmailService;
using ProiectMDS.Services.LocationServices;
using ProiectMDS.Services.PhotosService;
using ProiectMDS.Services.ProfileService;
using ProiectMDS.Services.UserConnectionsServices;
using ProiectMDS.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.Services
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailServices, EmailServices>();
            services.AddTransient<IProfileServices, ProfileServices>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IBaseCourseService, BaseCourseService>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IUserConnService, UserConnService>();
        }
    }
}