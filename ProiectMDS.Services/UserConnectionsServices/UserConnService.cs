using Microsoft.EntityFrameworkCore;
using ProiectMDS.DAL;
using ProiectMDS.DAL.Entities;
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
