using Domain;
using Entity.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application
{
    public class Jwt()
    {

        public string GenerateToken(Account account)
        {
            string secret = Environment.GetEnvironmentVariable("SECRET");

            var claimsList = new List<Claim>
            {
                new Claim("UserName",account.UserName) ,
                new Claim("Telephone",account.Telephone) ,
                new Claim("Id",account.Id.ToString()) ,

            };
            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(3),
                claims: claimsList,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
