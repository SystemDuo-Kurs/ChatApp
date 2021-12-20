using ChatApp.Server.Services;
using ChatApp.Shared;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Hubs
{
    public class UserHub : Hub
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserHub> _logger;
        public UserHub(IUserService userService, ILogger<UserHub> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public void Register(User user)
        {
            _logger.LogInformation("User recieved");
            _userService.CreateUser(user);
        }

        public async void Login(User user)
        {
            _logger.LogInformation("User logging in");
            await Clients.Caller.SendAsync("loginfo", _userService.AuthUser(user));
        }
    }
}
