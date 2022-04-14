using AutoMapper;
using ProiectMDS.DAL.Entities.Auth;
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
        }
    }
}
