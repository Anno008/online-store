using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Backend.WebApi.Services.Security
{
    public class JwtHandler
    {
        private readonly JwtSettings _options;

        public JwtHandler(IOptions<JwtSettings> options) =>
            _options = options.Value;

        public string CreateJwt(string clientId, string username, string role)
        {
            var secret = _options.SecretKey;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                new Claim("username", username),
                new Claim("clientId", clientId),
            };

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(_options.Expiration)),
                signingCredentials: creds)
            {
                Payload = { ["roles"] = new string[] { role } }
            };

            return new JwtSecurityTokenHandler().WriteToken(token);

            // var jwtHeader = new JwtHeader(creds);
            // var jwtPayload = new JwtPayload
            // {
            //     {"sub", clientId },
            //     {"iss", options.Issuer},
            //     {"aud", options.Audience },
            //     {"exp", DateTime.Now.Add(TimeSpan.FromMinutes(options.Expiration)) },
            //     {"user_name", username },
            //     {"roles", new string[] { Role.User.ToString() } }
            // };

            // return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(jwtHeader, jwtPayload));
        }

        public string CreateRefreshToken(string clientId, string username, string refreshTokenId)
        {
            var secret = _options.SecretKey;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, clientId),
                new Claim(JwtRegisteredClaimNames.Jti, refreshTokenId),
                new Claim("user_name", username)
            };

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
