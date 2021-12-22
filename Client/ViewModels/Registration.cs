using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Client.ViewModels
{
    public interface IRegistration
    {
        void RegisterUser();
        Models.IUserModel UserModel { get; }
    }
    public class Registration : IRegistration
    {
        private SignalRService _signalRService;
        private readonly Models.IUserModel _userModel;
        public Models.IUserModel UserModel { get { return _userModel; } }
        public Registration(SignalRService signalRService, Models.IUserModel userModel)
        {
            _signalRService = signalRService;
            _userModel = userModel;
            _userModel.UserDTO = new();
        }

        public async void RegisterUser()
        {
            await _signalRService.UserConnection.SendAsync("Register", _userModel.UserDTO);
        }
    }
}
