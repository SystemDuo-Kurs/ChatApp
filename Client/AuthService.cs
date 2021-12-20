using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Client
{
    public class AuthService
    {
        private readonly SignalRService _signalRService;
        private readonly Models.IUserModel _userModel;
        public bool LoginState { get; set; }
        public AuthService(SignalRService signalRService, Models.IUserModel userModel)
        {
            _signalRService = signalRService;
            _signalRService.UserConnection.On<bool>("loginfo", b => LoginState = b);

            _userModel = userModel;
        }

        public void Login(Models.IUserModel userModel)
        {
            _signalRService.UserConnection.SendAsync("Login", userModel);
        }
    }
}
