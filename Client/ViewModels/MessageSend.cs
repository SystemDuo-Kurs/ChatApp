using Blazored.LocalStorage;
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

        private readonly SignalRService _signalRService;
        private ILocalStorageService LocalStorage { init; get; }
        public MessageSend(SignalRService signalRService,
            ILocalStorageService localStorage)
        {
            _signalRService = signalRService;
            LocalStorage = localStorage;
        }

        public async void SendMessage()
        {
            string user = await LocalStorage.GetItemAsStringAsync("loggedin");
            ChatApp.Shared.Message msg = (ChatApp.Shared.Message)Message;
            msg.Sent = DateTime.Now;
            msg.User = new();
            msg.User.UserName = user;
            await _signalRService.ChatConnection
                .SendAsync("StoreMessage", msg);
            Message.Content = String.Empty;
        }
    }
}
