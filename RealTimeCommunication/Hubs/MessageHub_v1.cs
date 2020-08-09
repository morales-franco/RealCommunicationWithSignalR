using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RealTimeCommunication.Dtos;
using System;
using System.Threading.Tasks;

namespace RealTimeCommunication.Hubs
{
    /// <summary>
    /// info: Message Hub v1 - No strongly type
    /// </summary>
    public class MessageHubV1: Hub
    {
        private readonly ILogger<MessageHubV1> _logger;

        public MessageHubV1(ILogger<MessageHubV1> logger)
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
            return Clients.All.SendAsync("messageReceived", message);
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
