using ChatApp.Shared;

namespace ChatApp.Server.Services
{
    public interface IUserService
    {
        string CreateUser(User user);
        bool AuthUser(User user);
    }
    public class UserService : IUserService
    {
        private readonly Db _db;
        private readonly ILogger _logger;
        public UserService(Db db, ILogger<UserService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public bool AuthUser(User user) =>
            _db.Users.Where(u => u.Email.ToLower() == user.Email.ToLower()
                && u.Password.ToLower() == user.Password.ToLower()).Any();
           
        public string CreateUser(User user)
        {
            _logger.LogInformation("Creating User");
            if (_db.Users.Where(
                u => u.Email.ToLower() == user.Email.ToLower() 
                || u.Name.ToLower() == user.Name.ToLower()).Any()
                )
            {
                _logger.LogError("Username or Email already exists");
                return "Username or Email already exists";
            }
            _db.Add(user);
            _db.SaveChanges();

            return String.Empty;
        }
    }
}
