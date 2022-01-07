using ChatApp.Shared;

namespace ChatApp.Server.Services
{
    public interface IMessageService
    {
        Task StoreMessage(Message message);
        List<Message> GetAllMessages();
    }
    public class MessageService : IMessageService
    {
        private readonly Db _db;
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public MessageService(Db db, ILogger<MessageService> logger,
            IUserService userService)
        {
            _db = db;
            _logger = logger;
            _userService = userService;

        }

        public List<Message> GetAllMessages()
        {
            _db.Users.ToList();
            return _db.Messages.ToList(); 
        } 

        public async Task StoreMessage(Message message)
        {
            _logger.LogInformation("Storing message");
            var dbUser = await _userService.GetUserByName(message.User.UserName);
            message.User = dbUser;
            _db.Add(message);
            _db.SaveChanges();  
        }
    }
}
