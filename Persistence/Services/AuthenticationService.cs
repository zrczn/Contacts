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

namespace Contacts.Security.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILoginManager _loginManager;
    private readonly JSONTokenSettingsSchema _jsonSettings;

    //serwis do generowania JWT tokenu
    //pobierz request z loginem i hasłem
    //jeżeli logowanie udane zwróć token wdłg modelu response

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

        if (fetchUser.Item2 is null)
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
        var issuer = _jsonSettings.Issuer;
        var audience = _jsonSettings.Audience;
        var key = Encoding.ASCII.GetBytes(_jsonSettings.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return (JwtSecurityToken)token;
    }
}


