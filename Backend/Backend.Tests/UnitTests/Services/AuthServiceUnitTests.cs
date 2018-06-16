using System;

using Backend.WebApi.DTOs.RequestDTOs;
using Backend.WebApi.Repositories;
using Backend.WebApi.Services;
using Backend.WebApi.Services.Security;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Xunit;

namespace Backend.Tests.UnitTests.Services
{
    public class AuthServiceUnitTests
    {
        private readonly AuthService _authService;
        private readonly DatabaseContext _dbContext;

        public AuthServiceUnitTests()
        {
            // Setup
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new DatabaseContext(options);
            var userRepository = new UserRepository(_dbContext);
            var tokenRepository = new TokenRepository(_dbContext);
            var jwtOptions = Options.Create(new JwtSettings() { Issuer = "Backend", Audience = "Users", SecretKey = "myXAuthenticationSecret" });
            var jwtHandler = new JwtHandler(jwtOptions);
            _authService = new AuthService(userRepository, jwtHandler, tokenRepository);

            //var user = new User { Username = "Test", Password = "Test", Role = Role.User };
            _authService.Register(new RegisterRequestDTO { Username = "Test", Password = "Test", ClientId = "Test" });
        }

        [Fact]
        public void IfUsernameIsntTaken_CompleteRegistration_ReturnAccessToken()
        {
            // Arrange
            var user = new RegisterRequestDTO
            {
                Username = "Test1",
                Password = "Test1",
                ClientId = "Test"
            };

            // Act
            var result = _authService.Register(user);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);
            Assert.Equal(2, _dbContext.Users.ToListAsync().Result.Count);
        }

        [Fact]
        public void IfUsernameIsTaken_ReturnNull()
        {
            // Arrange
            var user = new RegisterRequestDTO
            {
                Username = "Test",
                Password = "Test",
                ClientId = "Test"
            };

            // Act
            var result = _authService.Register(user);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void IfUserExists_CompleteLogin_ReturnToken()
        {
            var user = new AuthRequestDTO
            {
                Username = "Test",
                Password = "Test",
                ClientId = "Test",
                GrantType = "password"
            };

            var result = _authService.Login(user);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IfUserExists_WithRememberMe_CompleteLoginAndReturnAccessAndRefreshToken()
        {
            var user = new AuthRequestDTO
            {
                Username = "Test",
                Password = "Test",
                ClientId = "Test",
                GrantType = "password",
                RememberMe = true
            };

            var result = _authService.Login(user).Result;

            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);
            Assert.NotNull(result.RefreshToken);
        }


        [Fact]
        public void UserProvidesRefreshToken_ReturnNewAccessToken()
        {
            var firstLoginToCreateRefreshToken = new AuthRequestDTO
            {
                Username = "Test",
                Password = "Test",
                ClientId = "Test",
                RememberMe = true,
                GrantType = "password"
            };

            var login = _authService.Login(firstLoginToCreateRefreshToken).Result;
            var user = new AuthRequestDTO { ClientId = "Test", GrantType = "refresh_token", RefreshToken = login.RefreshToken };
            var result = _authService.RefreshAccessToken(user);

            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);
            Assert.Null(result.RefreshToken);
            Assert.False(login.AccessToken == result.AccessToken);
        }

        [Fact]
        public void IfUserExists_RememberMeFalse_RefreshTokenNull()
        {
            var user = new AuthRequestDTO
            {
                Username = "Test",
                Password = "Test",
                ClientId = "Test",
                GrantType = "password",
                RememberMe = false
            };

            var result = _authService.Login(user).Result;

            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);
            Assert.Null(result.RefreshToken);
        }
    }
}
