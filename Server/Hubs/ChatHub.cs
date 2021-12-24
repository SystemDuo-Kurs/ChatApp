using ChatApp.Server.Services;
using ChatApp.Shared;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly ILogger _logger;
        public ChatHub(IMessageService messageService, ILogger<ChatHub> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }
        public void StoreMessage(Message message)
        {
            _logger.LogInformation("Recieved message");
            _messageService.StoreMessage(message);
        }
    }
}
