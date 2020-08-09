using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTimeCommunication.Dtos;
using RealTimeCommunication.Hubs;

namespace RealTimeCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        /// <summary>
        /// info: Injecting strongly type hub
        /// </summary>
        private readonly IHubContext<MessageHub, IMessageClient> _hubContext;

        public MessagesController(IHubContext<MessageHub, IMessageClient> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody]MessageDto message)
        {
            await _hubContext.Clients.All.MessageReceived(message);
            return Ok();
        }
    }
}