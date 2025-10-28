using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application
{
    public static class JwtModule
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection self, string secretKey)
        {

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
