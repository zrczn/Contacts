using Contacts.Domain.DbEntites;
using Contacts.Persistence;
using Contacts.Security.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Security.Manager
{
    internal class UserManager : IUserManager
    {
        private readonly ContactsDatabaseContext _dbCon;

        public UserManager(ContactsDatabaseContext contactsDatabaseContext)
        {
            _dbCon = contactsDatabaseContext;
        }

        public async Task<User> FetchUserAsync(string login, string password)
            => await _dbCon.Users.Include(y => y.Role).FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

    }
}
