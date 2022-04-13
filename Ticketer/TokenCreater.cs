using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RyGamingProvider
{
    public class TokenCreater
    {
        private static readonly JwtSecurityTokenHandler _jwtTokenHandler = new JwtSecurityTokenHandler();
        private static readonly SymmetricSecurityKey _securityKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());

        public static SymmetricSecurityKey SecurityKey
        { get { return _securityKey; } }

        public static string GenerateJwtToken(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Name is not specified.");
            }

            var claims = new[] { new Claim(ClaimTypes.Name, name) };
            var credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("ExampleServer", "ExampleClients", claims, expires: DateTime.Now.AddSeconds(60), signingCredentials: credentials);
            return _jwtTokenHandler.WriteToken(token);
        }
    }
}
