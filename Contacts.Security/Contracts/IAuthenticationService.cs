using Contacts.Domain.DbEntites;
using Contacts.Security.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Security.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthResponse> AuthenticateAsync(AuthRequest req);
    }
}
