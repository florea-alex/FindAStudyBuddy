using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProiectMDS.DAL.Models.HubModels;
using ProiectMDS.Services.Hubs;

namespace ProiectMDS.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [Route("send")]                                         
        [HttpPost]
        public IActionResult SendRequest([FromBody] MessageModel msg)
        {
            _hubContext.Clients.All.SendAsync("ReceiveOne", msg.user, msg.message);
            return Ok();
        }
    }
}
