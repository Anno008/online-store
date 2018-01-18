using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

using Backend.WebApi.Services.Security;
using Backend.WebApi.Repositories;
using Backend.WebApi.Models;

namespace Backend.WebApi.Services
{
    public class UserService
    {
        private readonly JWTSettings options;
        private readonly UserRepository userRepository;

        public UserService(IOptions<JWTSettings> options, UserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.options = options.Value;
        }

        public string Login(string username, string password) =>
            userRepository.Authenticate(username, password) == null ? null : GetToken(username);

        public string Register(string username, string password) =>
            userRepository.Register(username, password) == null ? null : GetToken(username);
        
        private string GetToken(string username)
        {
            var secret = options.SecretKey;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: new Claim[] {new Claim("sub", username)},
                expires: DateTime.Now.AddMinutes(options.Expiration),
                signingCredentials: creds);
            token.Payload["roles"] = new string[] { Role.User.ToString() };
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
