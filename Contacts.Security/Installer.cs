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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contacts.Security.Services;

namespace Contacts.Security
{
    public static class Installer
    {
        public static void AddSecServices(this IServiceCollection service,
            IConfiguration configuration)
        {
            service.Configure<JSONTokenSettingsSchema>(configuration.GetSection("JSONTokenSettings"));

            service.AddTransient<IAuthenticationService, AuthenticationService>();
            service.AddSingleton<ILoginManager, LoginManager>();
            service.AddSingleton<IUserManager, UserManager>();

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = false;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JSONTokenSettings:Issuer"],
                        ValidAudience = configuration["JSONTokenSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JSONTokenSettings:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };

                    opt.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = x =>
                        {
                            x.NoResult();
                            x.Response.StatusCode = 500;
                            x.Response.ContentType = "text/plain";

                            return x.Response.WriteAsync(x.Exception.ToString());
                        },
                        OnChallenge = y =>
                        {
                            y.HandleResponse();
                            y.Response.StatusCode = 401;
                            y.Response.ContentType = "text/plain";

                            return y.Response.WriteAsync("onchallenge");
                        },
                        OnForbidden = z =>
                        {
                            z.Response.StatusCode = 403;
                            z.Response.ContentType = "text/plain";

                            return z.Response.WriteAsync("access denied");
                        }
                    };
                }
                );


        }
    }
}
