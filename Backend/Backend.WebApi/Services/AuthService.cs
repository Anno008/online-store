using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

using Backend.WebApi.DTOs;
using Backend.WebApi.Models;
using Backend.WebApi.Repositories;
using Backend.WebApi.Services.Security;

namespace Backend.WebApi.Services
{
    public class AuthService
    {
        private readonly JWTHandler jwtHandler;
        private readonly TokenRepository tokenRepository;
        private readonly UserRepository userRepository;

        public AuthService(UserRepository userRepository, JWTHandler jwtHandler, TokenRepository tokenRepository)
        {
            this.userRepository = userRepository;
            this.jwtHandler = jwtHandler;
            this.tokenRepository = tokenRepository;
        }

        public async Task<AuthResponseDTO> Login(AuthDTO authDto)
        {
            var user = userRepository.Authenticate(authDto.Username, authDto.Password);

            if (user == null)
            {
                return null;
                //Message = "Incorrect credentials." };
            }


            string refreshToken = null;
            if (authDto.RememberMe)
            {
                var refreshTokenId = Guid.NewGuid().ToString();
                var token = new RefreshToken
                {
                    TokenId = refreshTokenId,
                    Token = jwtHandler.CreateRefreshToken(authDto.ClientId, user.Username, refreshTokenId)
                };
                refreshToken = (await tokenRepository.CreateAsync(token)).Token;
            }

            return new AuthResponseDTO
            {
                AccessToken = jwtHandler.CreateJWT(authDto.ClientId, authDto.Username),
                RefreshToken = refreshToken
            };
        }

        public AuthResponseDTO Register(RegisterDTO registerDto) =>
            userRepository.Register(registerDto.Username, registerDto.Password) == null ? null :
                new AuthResponseDTO { AccessToken = jwtHandler.CreateJWT(registerDto.ClientId, registerDto.Username) };

        public AuthResponseDTO RefreshAccessToken(AuthDTO authDto)
        {
            if (authDto.RefreshToken == null)
                return null;

            var refreshToken = new JwtSecurityTokenHandler().ReadJwtToken(authDto.RefreshToken);
            var username = refreshToken.Claims.FirstOrDefault(claim => claim.Type == "user_name").Value;
            var userExists = userRepository.UserExists(username);
            var refreshTokenExists = tokenRepository.TokenExists(refreshToken.Claims.FirstOrDefault(claim => claim.Type == "jti").Value);
            if (userExists && refreshTokenExists)
            { 
                return new AuthResponseDTO
                {
                    AccessToken = jwtHandler.CreateJWT(authDto.ClientId, username)
                };
            }
            else
            {
                // this could mean:
                // 1: the user doesn't exist anymore.
                // 2. The refresh token doesn't exist anymore, consider creating a revoked flag.
                return null;
            }
        }
    }
}
