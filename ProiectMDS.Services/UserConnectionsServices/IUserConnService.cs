using ProiectMDS.DAL.Entities;
using ProiectMDS.DAL.Models.UserConnectionsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.Services.UserConnectionsServices
{
    public interface IUserConnService
    {
        Task<string> AddFriend(AddFriendModel model);
        Task<string> RemoveFriend(int id1, int id2);
        Task<GetUserConnectionsModel> GetAllFriends(int id);
        Task<List<int>> GetSuggestions(int userId);
    }
}
