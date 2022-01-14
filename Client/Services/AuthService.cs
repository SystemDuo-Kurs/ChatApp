using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Client.Services
{
    public class AuthService
    {
        private readonly SignalRService _signalRService;
        private readonly Models.IUserModel _userModel;
        private readonly Blazored.LocalStorage.ILocalStorageService _localStorage;
        private readonly NavigationManager _navMan;
        public Models.IUserModel UserModel { get => _userModel; }

        public AuthService(SignalRService signalRService, Models.IUserModel userModel,
            Blazored.LocalStorage.ILocalStorageService localStorage,
            NavigationManager navigationManager)
        {
            _navMan = navigationManager;
            _signalRService = signalRService;
            _localStorage = localStorage;
            _signalRService.UserConnection.On<bool>
                ("loginfo", b =>
                {
                    if (b)
                        SaveData();
                });

            _userModel = userModel;
            _userModel.UserDTO = new();
        }

        private async Task SaveData()
        {
            await _localStorage.SetItemAsStringAsync("loggedin", UserModel.Name);
            _navMan.NavigateTo("/", true);
        }

        public void Login()
        {
            _signalRService.UserConnection.SendAsync("Login", UserModel.UserDTO);
        }
    }
}