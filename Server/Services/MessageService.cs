using ChatApp.Shared;

namespace ChatApp.Server.Services
{
    public interface IMessageService
    {
        void StoreMessage(Message message);
    }
    public class MessageService : IMessageService
    {
        private readonly Db _db;
        private readonly ILogger _logger;

        public MessageService(Db db, ILogger<MessageService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void StoreMessage(Message message)
        {
            _logger.LogInformation("Storing message");
            _db.Add(message);
            _db.SaveChanges();  
        }
    }
}
