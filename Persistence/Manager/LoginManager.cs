using Contacts.Domain.DbEntites;
using Contacts.Persistence;
using Contacts.Persistence.Contracts;
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
        private readonly IPasswordService _passwordService;

        public LoginManager(IUserManager _userManager, IPasswordService passwordService)
        {
            this._userManager = _userManager;
            _passwordService = passwordService;
        }


        public async Task<(SignInResult, User)> TryToLogInAsync(AuthRequest auth)
        {
            var getUser = await _userManager.FetchUserAsync(auth.Login, auth.Password);

            if (getUser != null)
            {
                string hashPassword = await _passwordService.GenerateHash(auth.Password);

                if(hashPassword != getUser.Password)
                    return (SignInResult.Failed, null);

                return (SignInResult.Success, getUser);
            }

            return (SignInResult.Failed, null);
        }


    }
}
