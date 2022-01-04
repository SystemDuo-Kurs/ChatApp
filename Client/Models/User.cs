using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Client.Models
{
    public interface IUserModel
    {
        ChatApp.Shared.User UserDTO { get; set;  }
        string Name { get; set; }
        string Email { get; set; }
        string Password { get; set; }
    }
    public class User : IUserModel
    {
        public ChatApp.Shared.User UserDTO { get; set; }
        public string Name {
            get => UserDTO is not null ? UserDTO.UserName : String.Empty;
            set => UserDTO.UserName = value; 
        }

        public string Email { 
            get => UserDTO is not null ? UserDTO.Email : String.Empty;
            set => UserDTO.Email = value;
        }

        public string Password
        {
            get => UserDTO is not null ? UserDTO.Password : String.Empty;
            set => UserDTO.Password = value;
        }
    }
}
