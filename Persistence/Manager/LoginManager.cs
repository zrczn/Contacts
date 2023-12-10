using Contacts.Domain.DbEntites;
using Contacts.Persistence;
using Contacts.Security.Contracts;
using Contacts.Security.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Security.Manager
{
    internal class LoginManager : ILoginManager
    {
        //pobieranie usera z DB

        private readonly IUserManager _userManager;

        public LoginManager(IUserManager _userManager)
            => this._userManager = _userManager;

        public async Task<(SignInResult, User)> TryToLogInAsync(AuthRequest auth)
        {
            var getUser = await _userManager.FetchUserAsync(auth.Login, auth.Password);

            if (getUser is null)
                return (SignInResult.Failed, null);

            return (SignInResult.Success, getUser);
        }


    }
}
