using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Backend.Services.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Backend.Repositories;
using Backend.Models;
using System.Security.Claims;

namespace Backend.Services
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

        public string Login(string username, string password)
        {
            var user = userRepository.Authenticate(username, password);
            return user == null ? null : GetToken(username);
        }

        internal string Register(string username, string password)
        {
            var user = userRepository.Create(username, password);
            return user == null ? null : GetToken(username);
        }

        private string GetToken(string username)
        {
            var secret = options.SecretKey;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: new Claim[] {new Claim("sub", username)},
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            token.Payload["roles"] = new string[] { Role.User.ToString() };
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
