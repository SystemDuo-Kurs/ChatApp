using ChatApp.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using System.ComponentModel;

namespace ChatApp.Client.ViewModels
{
    public interface IRegistration
    {
        Task RegisterUser();

        Status Status { get; }

        event PropertyChangedEventHandler? PropertyChanged;

        Models.IUserModel UserModel { get; }
    }

    public class Registration : IRegistration, INotifyPropertyChanged
    {
        private SignalRService _signalRService;
        private readonly Models.IUserModel _userModel;

        public Models.IUserModel UserModel
        { get { return _userModel; } }

        private Status _status;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Status Status
        {
            get => _status;
            private set
            {
                _status = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Status)));
            }
        }

        public Registration(SignalRService signalRService, Models.IUserModel userModel)
        {
            _signalRService = signalRService;
            _userModel = userModel;
            _userModel.UserDTO = new();
            _signalRService.UserConnection.On<Status>("getStatus",
                s => Status = s);
        }

        public async Task RegisterUser()
        {
            await _signalRService.UserConnection.SendAsync("Register", _userModel.UserDTO);
        }
    }
}