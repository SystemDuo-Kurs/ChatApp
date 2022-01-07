using ChatApp.Client.Models;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Client.ViewModels
{
    public interface IMessageList
    {
        ObservableCollection<Message> Messages { get; }
        Task GetMessages();
    }
    public class MessageList : IMessageList
    {
        public ObservableCollection<Message> Messages { get; private set; } = new();

        private readonly SignalRService _signalRService;
        public MessageList(SignalRService signalRService)
        {
            _signalRService = signalRService;
            _signalRService.ChatConnection.On<List<ChatApp.Shared.Message>>
                ("RecieveMessages", lm => 
                    {
                        Messages.Clear();
                        lm.ForEach(mDTO => Messages.Add((Message)mDTO)); 
                    }
                );
        }



        public async Task GetMessages()
        {
            await _signalRService.ChatConnection.SendAsync("SendMessagesToUser");
        }
    }
}
