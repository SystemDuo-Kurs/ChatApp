using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Client.Models
{
    public interface IUserModel
    {
        void SendUser();
        ChatApp.Shared.User UserDTO { get; set; }
    }
    public class User : IUserModel
    {
        private SignalRService _signalRService;
        public ChatApp.Shared.User UserDTO { get; set; }
        public User(SignalRService signalRService)
        {
            _signalRService = signalRService;
        }

        public async void SendUser()
        {
            await _signalRService.UserConnection.SendAsync("RegisterUser", UserDTO);
        }
    }
}
