using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var nowUtc = DateTimeOffset.UtcNow;
            var expiration = nowUtc.AddMinutes(100);
            var exp = expiration.ToUnixTimeSeconds();
            var now = nowUtc.ToUnixTimeSeconds();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //Expires = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Expires = DateTime.UtcNow.AddDays(7),
                NotBefore = DateTime.Now.Subtract(TimeSpan.FromMinutes(30)),
                SigningCredentials = creds
                // var nowUtc = DateTimeOffset.UtcNow;
                // var expiration = nowUtc.AddMinutes(jwtOptions.ExpiryMinutes);
                // var exp = expiration.ToUnixTimeSeconds();
                // var now = nowUtc.ToUnixTimeSeconds();
                // DateTime.Now.AddDays(7)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}