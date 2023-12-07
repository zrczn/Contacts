using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Security.Models
{
    public class AuthRequest
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
