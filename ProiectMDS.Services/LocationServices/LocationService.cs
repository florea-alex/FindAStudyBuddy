using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProiectMDS.DAL;
using ProiectMDS.DAL.Entities;
using ProiectMDS.DAL.Models;
using ProiectMDS.DAL.Models.LocationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Wrappers;

namespace ProiectMDS.Services.LocationServices
{
    public class LocationService : ILocationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LocationService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(LocationPostModel model, int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"There is no user with id {userId}"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);

            if (profile == null) { throw new KeyNotFoundException($"User {userId} has no profile yet"); }

            var location = _mapper.Map<Location>(model);

            profile.LocationId = location.Id;
            //profile.Address = location;
            location.Profile = profile;
            await _context.AddAsync(location);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"There is no user with id {userId}"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);

            if (profile == null) { throw new KeyNotFoundException($"User {userId} has no profile yet"); }

            var location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == profile.LocationId);

            if (location == null) { throw new KeyNotFoundException($"There is no location inserted for user {userId}"); }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }

        public async Task<Response<LocationGetModel>> GetById(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"There is no user with id {userId}"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);

            if (profile == null) { throw new KeyNotFoundException($"User {userId} has no profile yet"); }

            var location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == profile.LocationId);

            if (location == null) { throw new KeyNotFoundException($"There is no location inserted for user {userId}"); }

            var locationGetModel = _mapper.Map<LocationGetModel>(location);

            return new Response<LocationGetModel>(locationGetModel);
        }

        public async Task Update(LocationPutModel model, int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) { throw new KeyNotFoundException($"There is no user with id {userId}"); }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == user.ProfileId);

            if (profile == null) { throw new KeyNotFoundException($"User {userId} has no profile yet"); }

            var location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == profile.LocationId);

            if (location == null) { throw new KeyNotFoundException($"There is no location inserted for user {userId}"); }

            _mapper.Map<LocationPutModel, Location>(model, location);

            await _context.SaveChangesAsync();
        }
    }
}
