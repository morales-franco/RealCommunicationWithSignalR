using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RealTimeCommunication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeCommunication.Hubs
{
    /// <summary>
    /// Interface that represents methods implementing in the client (front-end)
    /// These methods can be called by front-end
    /// </summary>
    public interface IMessageClient
    {
        Task MessageReceived(MessageDto message);
    }

    /// <summary>
    /// info: Strongly typed hub
    /// </summary>
    public class MessageHub: Hub<IMessageClient>
    {
        private readonly ILogger<MessageHub> _logger;

        public MessageHub(ILogger<MessageHub> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Receive new message from one client and hub sends a broadcast
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task NewMessage(MessageDto message)
        {
            return Clients.All.MessageReceived(message);
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"New client connected - ConenctionId: { Context.ConnectionId }");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation($"Client disconnected - ConenctionId: { Context.ConnectionId }");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
