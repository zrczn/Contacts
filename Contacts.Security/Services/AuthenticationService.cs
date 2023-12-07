using Contacts.Domain.DbEntites;
using Contacts.Security.Contracts;
using Contacts.Security.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Security.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILoginManager _loginManager;
        private readonly JSONTokenSettingsSchema _jsonSettings;

        public AuthenticationService(ILoginManager _loginManager,
            IOptions<JSONTokenSettingsSchema> options)
        {
            this._loginManager = _loginManager;
            _jsonSettings = options.Value;
        }

        public async Task<AuthResponse> AuthenticateAsync(AuthRequest req)
        {
            AuthResponse response = new();

            var fetchUser = await _loginManager.TryToLogInAsync(req);

            if(fetchUser.Item2 is null)
            {
                response.status = fetchUser.Item1;
                return response;
            }

            JwtSecurityToken token = await CreateToken(fetchUser.Item2);
            JwtSecurityTokenHandler hndlr = new JwtSecurityTokenHandler();

            response.Id = fetchUser.Item2.Id.ToString();
            response.Login = fetchUser.Item2.Login;
            response.status = fetchUser.Item1;
            response.Token = hndlr.WriteToken(token);

            return response;

        }

        private async Task<JwtSecurityToken> CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Iss, "PhoneBook.API"),
                new Claim(JwtRegisteredClaimNames.Sub, user.Login),
                new Claim(JwtRegisteredClaimNames.Aud, "PhoneBook.API.users"),
                new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddMinutes(_jsonSettings.ExpireDuration).ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", user.Role.RoleType)
            };

            var serverKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jsonSettings.Key));
            var publicKey = new SigningCredentials(serverKey, SecurityAlgorithms.HmacSha256);

            var assembleKey = new JwtSecurityToken(
                claims: claims,
                signingCredentials: publicKey);

            return assembleKey;
        }
    }
}
