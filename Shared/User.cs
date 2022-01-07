using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatApp.Shared
{
    public class User : IdentityUser
    {
        public string Password { get; set; }   
        [JsonIgnore]
        public List<Message> Messages { get; set; }
    }
}
