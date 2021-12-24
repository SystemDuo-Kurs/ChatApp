using ChatApp.Client.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Client.ViewModels
{
    public interface IMessageSend
    {
        Message Message { get; set; }
        void SendMessage();
    }
    public class MessageSend : IMessageSend
    {
        public Message Message { get; set; } = new();
        
        private SignalRService _signalRService;
        public MessageSend(SignalRService signalRService)
        {
            _signalRService = signalRService;
            Message.User = new();
            Message.User.UserDTO = new ChatApp.Shared.User
            {
                TempName = "sklj"
            };
        }

        public async void SendMessage()
        {
            await _signalRService.ChatConnection
                .SendAsync("StoreMessage", (ChatApp.Shared.Message)Message);
        }
    }
}
