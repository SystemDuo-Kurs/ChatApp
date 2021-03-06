using ChatApp.Shared;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Services
{
    public interface IUserService
    {
        Task<Status> CreateUser(User user);

        Task<bool> AuthUser(User user);

        Task<User> GetUserByName(string name);
    }

    public class UserService : IUserService
    {
        private readonly Db _db;
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;

        public UserService(Db db, ILogger<UserService> logger, UserManager<User> userManager)
        {
            _db = db;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<User> GetUserByName(string name)
            => await _userManager.FindByNameAsync(name);

        public async Task<bool> AuthUser(User user)
        {
            _logger.LogInformation("User logging in...");
            var serverSideUser = await _userManager.FindByNameAsync(user.UserName);
            if (await _userManager.CheckPasswordAsync(serverSideUser, user.Password))
            {
                _logger.LogInformation("Login OK");
                return true;
            }
            else
            {
                _logger.LogWarning("Login failed");
                return false;
            }
        }

        public async Task<Status> CreateUser(User user)
        {
            _logger.LogInformation("Creating User");
            var rez = await _userManager.CreateAsync(user, user.Password);

            return rez.Succeeded ? new Status { Success = true } :
                    new Status
                    {
                        Success = false,
                        Errors = rez.Errors.Select(e => e.Description).ToList()
                    };
        }
    }
}