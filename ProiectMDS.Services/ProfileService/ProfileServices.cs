using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProiectMDS.DAL;
using ProiectMDS.DAL.Entities;
using ProiectMDS.DAL.Models.CourseModels;
using ProiectMDS.DAL.Models.LocationModels;
using ProiectMDS.DAL.Models.ProfileModels;
using ProiectMDS.DAL.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Wrappers;

namespace ProiectMDS.Services.ProfileService
{
    public class ProfileServices : IProfileServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProfileServices(AppDbContext appDbContext,
            IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }
        public async Task Create(ProfilePostModel model, int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"There is no user with id {userId}"); }

            if (user.ProfileId != null) { throw new Exception("There's aleady a profile created for this user, you can update it"); }

            var profile = _mapper.Map<UserProfile>(model);

            user.ProfileId = profile.Id;
            //user.Profile = profile;
            profile.User = user;
            await _context.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"User {userId} does not exist"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);

            if (profile == null) { throw new KeyNotFoundException($"User {userId} has no profile yet"); }

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
        }

        public async Task<Response<ProfileGetModel>> GetById(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"User {userId} does not exist"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);

            if (profile == null) { throw new KeyNotFoundException($"User {userId} has no profile yet"); }

            var location = await _context.Locations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == profile.LocationId);

            var courses = await _context.Courses.Where(x => x.ProfileId == profile.Id).AsNoTracking().ToListAsync();

            var profileGetModel = _mapper.Map<ProfileGetModel>(profile);

            if (location != null) { profileGetModel.Address = _mapper.Map<LocationGetModel>(location); }

            if (courses != null)
            {
                var coursesGetModel = new List<CourseGetModel>();
                foreach (var course in courses)
                {
                    coursesGetModel.Add(_mapper.Map<CourseGetModel>(course));
                }

                profileGetModel.Courses = coursesGetModel;
            }

            return new Response<ProfileGetModel>(profileGetModel);
        }

        public async Task Update(ProfilePutModel model, int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"There is no user profile with id:{userId}"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);

            if (profile == null) { throw new KeyNotFoundException($"User {userId} has no profile yet"); }

            _mapper.Map<ProfilePutModel, UserProfile>(model, profile);
            await _context.SaveChangesAsync();
        }
    }
}
