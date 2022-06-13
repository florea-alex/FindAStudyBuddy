using Microsoft.EntityFrameworkCore;
using ProiectMDS.DAL;
using ProiectMDS.DAL.Entities;
using ProiectMDS.DAL.Entities.Auth;
using ProiectMDS.DAL.Models.UserConnectionsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.Services.UserConnectionsServices
{
    public class UserConnService : IUserConnService
    {
        private readonly AppDbContext _context;

        public UserConnService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<string> AddFriend(AddFriendModel model)
        {
            if (model.FriendId == model.UserId)
                throw new Exception("Can't add yourself as a friend you weirdo");

            if (await _context.UserConnections.AnyAsync(x => x.UserId == model.UserId && x.FriendId == model.FriendId))
                throw new Exception("You already have this friend");

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);
            var friend = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.FriendId);

            if (user == null || friend == null)
                throw new Exception("You/your friend does not exists");

            var friend1 = new UserConnections
            {
                UserId = model.UserId,
                FriendId = model.FriendId,
                User = user
            };

            var friend2 = new UserConnections
            {
                UserId = model.FriendId,
                FriendId = model.UserId,
                User = friend
            };

            await _context.UserConnections.AddAsync(friend1);
            user.UserConnections.Add(friend1);
            await _context.UserConnections.AddAsync(friend2);
            friend.UserConnections.Add(friend2);

            await _context.SaveChangesAsync();

            return "Friend added succesfully";
        }

        public async Task<GetUserConnectionsModel> GetAllFriends(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                throw new Exception("You don't exist lmao");

            var friends = await _context.UserConnections.Where(x => x.UserId == userId).Select(x => x.FriendId).ToListAsync();

            var friendsModel = new GetUserConnectionsModel();
            friendsModel.Users = friends;

            return friendsModel;
        }

        public async Task<List<int>> GetSuggestions(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                throw new Exception("You don't exist lmao");
            
            var friends = await _context.UserConnections.Where(x => x.UserId == user.Id).Select(x => x.FriendId).ToListAsync();

            //selectez toti userii inafara de mine si de prietenii mei
            IQueryable<User> users = _context.Users.Where(x => x.Id != user.Id && !friends.Contains(x.Id));

            //selectez cursurile mele
            var courses = await _context.Courses.Where(x => x.ProfileId == user.ProfileId).ToListAsync();
            var coursesName = courses.Select(x => x.courseName).ToList();

            //acum fac selectia pe baza cursurilor
            var _users = await users.Include(x => x.Profile).Where(x => x.Profile.Courses.Any(y => coursesName.Contains(y.courseName))).ToListAsync();

            var response = new List<int>();

            foreach (var _user in _users)
            {
                var userCourses = await _context.Courses.Where(x => x.ProfileId == _user.ProfileId).ToListAsync();
                foreach (var userCourse in userCourses)
                {
                    var helper = courses.FirstOrDefault(x => x.courseName == userCourse.courseName && x.Helper != userCourse.Helper);
                    if (helper != null)
                        response.Add(_user.Id);
                }
            }

            return response.Distinct().ToList();
        }

        public async Task<string> RemoveFriend(int userId, int friendId)
        {
            var friend = await _context.UserConnections.FirstOrDefaultAsync(x => x.UserId == userId && x.FriendId == friendId);
            var user = await _context.UserConnections.FirstOrDefaultAsync(x => x.UserId == friendId && x.FriendId == userId);

            if (friend == null || user == null)
                throw new Exception("Your friend dos not exist");

            _context.Remove(friend);
            _context.Remove(user);
            await _context.SaveChangesAsync();

            return "Deleted succesfully";
        }
    }
}
