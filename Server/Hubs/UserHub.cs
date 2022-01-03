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

        public async Task Register(User user)
        {
            _logger.LogInformation("User recieved");
            await _userService.CreateUser(user);
        }

        public async Task Login(User user)
        {
            _logger.LogInformation("User logging in");
            await Clients.Caller.SendAsync("loginfo", await _userService.AuthUser(user));
        }
    }
}
