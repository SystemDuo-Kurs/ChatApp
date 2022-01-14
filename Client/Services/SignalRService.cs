using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Client.Services
{
    public class SignalRService
    {
        public HubConnection ChatConnection { get; set; }
        public HubConnection UserConnection { get; set; }

        public SignalRService(NavigationManager nm)
        {
            ChatConnection = new HubConnectionBuilder()
                .WithUrl(nm.BaseUri + "chathub")
                .Build();

            UserConnection = new HubConnectionBuilder()
                .WithUrl(nm.BaseUri + "userhub")
                .Build();

            StartHubs();
        }

        private async void StartHubs()
        {
            await UserConnection.StartAsync();
            await ChatConnection.StartAsync();
        }
    }
}