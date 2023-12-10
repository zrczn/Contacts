using Contacts.Security.Contracts;
using Contacts.Security.Manager;
using Contacts.Security.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contacts.Security.Services;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Contacts.Persistence.Contracts;
using Contacts.Persistence.Services;

namespace Contacts.Security
{
    public static class Installer
    {
        //dodanie kontenera DI dla ustawień obsługujących warstwę logowania z autentykacją usera
        //konfiguracja parametrów JWT (waliduj przychodzące tokeny wdłg reguł poniżej)

        public static void AddSecServices(this IServiceCollection service,
            IConfiguration configuration)
        {
            service.Configure<JSONTokenSettingsSchema>
                (configuration.GetSection("JSONTokenSettings"));

            service.AddTransient<IAuthenticationService, AuthenticationService>();
            service.AddScoped<ILoginManager, LoginManager>();
            service.AddScoped<IUserManager, UserManager>();
            service.AddSingleton<IPasswordService, PasswordService>();

            service.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JSONTokenSettings:Issuer"],
                    ValidAudience = configuration["JSONTokenSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JSONTokenSettings:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

        }
    }
}
