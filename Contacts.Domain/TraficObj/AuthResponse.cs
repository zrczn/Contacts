using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Security.Models
{
    public class AuthResponse
    {
        //wzór response dla pomyślnej autoryzacji
        public string Id { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }

        public SignInResult status { get; set; }
    }
}
