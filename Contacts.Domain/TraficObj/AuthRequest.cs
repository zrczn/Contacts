using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Security.Models
{
    public class AuthRequest
    {
        //wzór requestu dla autoryzacji użytkownika
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
