using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationServise(this IServiceCollection self, string secretKey)
        {

            self.AddScoped(typeof(IAccountServiceLogic), typeof(AccountServiceLogic));
            self.AddScoped<Jwt>();
            self.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(t =>
                {
                    t.TokenValidationParameters = new TokenValidationParameters()

                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };

                });

            return self;
        }
    }
}
