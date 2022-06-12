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

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);
            var friend = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.FriendId);

            if (user == null || friend == null)
                throw new Exception("You/your friend does not exists");

            var _friend = new UserConnections
            {
                UserId = model.UserId,
                User = user
            };

            await _context.UserConnections.AddAsync(_friend);
            user.UserConnections.Add(_friend);

            await _context.SaveChangesAsync();

            return "Friend added succesfully";
        }

        public async Task<GetUserConnectionsModel> GetAllFriends(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                throw new Exception("You don't exist lmao");

            var friends = await _context.UserConnections.Where(x => x.UserId == userId).Select(x => x.UserId).ToListAsync();

            var test = await _context.Users.Select(x => x.UserConnections.Select(x => x.Id).ToList()).ToListAsync();

            var friendsModel = new GetUserConnectionsModel();
            friendsModel.Users = test;


            return friendsModel;
        }

        public async Task<string> RemoveFriend(int friendId)
        {
            var friend = await _context.UserConnections.FirstOrDefaultAsync(x => x.UserId == friendId);
            if (friend == null)
                throw new Exception("Your friend dos not exist");

            _context.Remove(friend);
            await _context.SaveChangesAsync();

            return "Deleted succesfully";
        }
    }
}
