using Contacts.Domain.DbEntites;
using Contacts.Security.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Security.Contracts
{
    public interface ILoginManager
    {
        Task<(SignInResult, User)> TryToLogInAsync(AuthRequest auth);
    }
}
