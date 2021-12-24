using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string TempName { get; set; }
        public string Password { get; set; }    
        public List<Message> Messages { get; set; }
    }
}
