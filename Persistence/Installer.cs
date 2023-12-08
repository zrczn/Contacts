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

namespace Contacts.Security
{
    public static class Installer
    {
        public static void AddSecServices(this IServiceCollection service,
            IConfiguration configuration)
        {
            service.Configure<JSONTokenSettingsSchema>(configuration.GetSection("JSONTokenSettings"));

            service.AddTransient<IAuthenticationService, AuthenticationService>();
            service.AddScoped<ILoginManager, LoginManager>();
            service.AddScoped<IUserManager, UserManager>();

            service.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
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
                        OnAuthenticationFailed = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "text/plain";
                            return c.Response.WriteAsync(c.Exception.ToString());
                        },
                        //OnChallenge = context =>
                        //{
                        //    context.HandleResponse();
                        //    context.Response.StatusCode = 401;
                        //    context.Response.ContentType = "application/json";
                        //    var result = JsonConvert.SerializeObject("401 Not authorized");
                        //    return context.Response.WriteAsync(result);
                        //},
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject("403 Not authorized");
                            return context.Response.WriteAsync(result);
                        },
                    };
                }
                );


        }
    }
}
