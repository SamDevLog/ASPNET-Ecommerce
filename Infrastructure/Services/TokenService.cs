using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services {
    public class TokenService : ITokenService {
        private readonly IConfiguration config;
        private readonly SymmetricSecurityKey key;

        public TokenService (IConfiguration config) {
            this.config = config;
            key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (config["Token:Key"]));
        }

        public string CreateToken (AppUser user) {
            var claims = new List<Claim> {
                new Claim (ClaimTypes.Email, user.Email),
                new Claim (ClaimTypes.GivenName, user.DisplayName)
            };

            var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (7),
                SigningCredentials = creds,
                Issuer = config["Token:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler ();

            var token = tokenHandler.CreateToken (tokenDescriptor);

            return tokenHandler.WriteToken (token);
        }
    }
}