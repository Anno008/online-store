using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

using Backend.WebApi.DTOs.RequestDTOs;
using Backend.WebApi.DTOs.ResponseDTOs;
using Backend.WebApi.Models;
using Backend.WebApi.Repositories;
using Backend.WebApi.Services.Security;

namespace Backend.WebApi.Services
{
    public class AuthService
    {
        private readonly JwtHandler _jwtHandler;
        private readonly TokenRepository _tokenRepository;
        private readonly UserRepository _userRepository;

        public AuthService(UserRepository userRepository, JwtHandler jwtHandler, TokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _tokenRepository = tokenRepository;
        }

        public async Task<AuthResponseDTO> Login(AuthRequestDTO authDto)
        {
            var user = _userRepository.Authenticate(authDto.Username, authDto.Password);

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
                    Token = _jwtHandler.CreateRefreshToken(authDto.ClientId, user.Username, refreshTokenId)
                };
                refreshToken = (await _tokenRepository.CreateAsync(token)).Token;
            }

            return new AuthResponseDTO
            {
                AccessToken = _jwtHandler.CreateJwt(authDto.ClientId, authDto.Username, user.Role.ToString()),
                RefreshToken = refreshToken
            };
        }

        public AuthResponseDTO Register(RegisterRequestDTO registerDto) =>
            _userRepository.Register(registerDto.Username, registerDto.Password) == null ? null :
                new AuthResponseDTO { AccessToken = _jwtHandler.CreateJwt(registerDto.ClientId, registerDto.Username, Role.User.ToString()) };

        public AuthResponseDTO RefreshAccessToken(AuthRequestDTO authDto)
        {
            if (authDto.RefreshToken == null)
                return null;

            var refreshToken = new JwtSecurityTokenHandler().ReadJwtToken(authDto.RefreshToken);
            var username = refreshToken.Claims.FirstOrDefault(claim => claim.Type == "user_name")?.Value;
            var user = _userRepository.GetUserByName(username);
            var refreshTokenExists = _tokenRepository.TokenExists(refreshToken.Claims.FirstOrDefault(claim => claim.Type == "jti")?.Value);
            if (user != null && refreshTokenExists)
            { 
                return new AuthResponseDTO
                {
                    AccessToken = _jwtHandler.CreateJwt(authDto.ClientId, username, user.Role.ToString())
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
