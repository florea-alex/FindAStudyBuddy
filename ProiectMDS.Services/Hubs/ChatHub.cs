using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.Services.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(string user, string message)               // Two parameters accepted
        {
            await Clients.All.ReceiveMessage(user, message);   // Note this 'ReceiveOne' 
        }
        
        public Task SendMessageToCaller(string user, string message)
        {
            return Clients.Caller.ReceiveMessage(user, message);
        }
    }
}
