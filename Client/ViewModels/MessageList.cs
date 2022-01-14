using ChatApp.Client.Models;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.SignalR.Client;
using ChatApp.Client.Services;

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
                ("ReceiveMessages", lm =>
                    {
                        Messages.Clear();
                        lm.ForEach(mDTO => Messages.Add((Message)mDTO));
                    }
                );
            _signalRService.ChatConnection.On<ChatApp.Shared.Message>
                ("ReceiveMessage", m => Messages.Add((Message)m));
        }

        public async Task GetMessages()
        {
            await _signalRService.ChatConnection.SendAsync("SendMessagesToUser");
        }
    }
}