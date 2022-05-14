using AutoMapper;
using ProiectMDS.DAL.Entities;
using ProiectMDS.DAL.Entities.Auth;
using ProiectMDS.DAL.Models;
using ProiectMDS.DAL.Models.BaseCourseModel;
using ProiectMDS.DAL.Models.CourseModels;
using ProiectMDS.DAL.Models.LocationModels;
using ProiectMDS.DAL.Models.ProfileModels;
using ProiectMDS.DAL.Models.RegisterModels;
using ProiectMDS.DAL.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserGetModel>().ReverseMap();
            CreateMap<User, UserPutModel>().ReverseMap();
            CreateMap<User, RegisterModel>().ReverseMap();

            CreateMap<UserProfile, ProfileGetModel>().ReverseMap();
            CreateMap<UserProfile, ProfilePostModel>().ReverseMap();
            CreateMap<UserProfile, ProfilePutModel>().ReverseMap();

            CreateMap<Location, LocationGetModel>().ReverseMap();
            CreateMap<Location, LocationPostModel>().ReverseMap();
            CreateMap<Location, LocationPutModel>().ReverseMap();

            CreateMap<Courses, CoursePostModel>().ReverseMap();
            CreateMap<Courses, CoursePutModel>().ReverseMap();
            CreateMap<Courses, CourseGetModel>().ReverseMap();

            CreateMap<BaseCourses, BaseGetModel>().ReverseMap();
            CreateMap<BaseCourses, BasePostModel>().ReverseMap();
            CreateMap<BaseCourses, BasePutModel>().ReverseMap();
        }
    }
}
