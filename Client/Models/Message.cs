namespace ChatApp.Client.Models
{
    public interface IMessageModel
    {
        string Content { get; set; }
        DateTime Sent { get; set; }
        User User { get; set; }
    }
    public class Message : IMessageModel
    {
        public string Content { get; set; }
        public DateTime Sent { get; set; }

        public User User { get; set; }

        public static explicit operator Message(ChatApp.Shared.Message m)
        {
            var message = new Message
            {
                Content = m.Content,
                Sent = m.Sent
            };

            message.User = new User { UserDTO = m.User };

            return message;
        }

        public static explicit operator ChatApp.Shared.Message(Message m)
            => new ChatApp.Shared.Message
            {
                Content = m.Content,
                Sent = m.Sent,
                User = m.User?.UserDTO
            };
        
    

    }
}
