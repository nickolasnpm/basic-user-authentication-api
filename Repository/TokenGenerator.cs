using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAuthentication.Models;

namespace UserAuthentication.Repository
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<string> CreateTokenAsync(User user)
        {

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.GivenName, user.firstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.lastName));
            claims.Add(new Claim(ClaimTypes.Email, user.emailAddress));

            user.roles.ForEach((role) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

           return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }   
    }
}
