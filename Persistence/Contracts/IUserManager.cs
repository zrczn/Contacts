using Contacts.Domain.DbEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Security.Contracts
{
    public interface IUserManager
    {
        Task<User> FetchUserAsync(string login, string password);
    }
}
