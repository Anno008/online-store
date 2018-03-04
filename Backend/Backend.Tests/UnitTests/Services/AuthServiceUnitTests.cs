using System;

using Backend.WebApi.Controllers;
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
        private readonly AuthService authService;
        private readonly DatabaseContext dbContext;

        public AuthServiceUnitTests()
        {
            // Setup
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new DatabaseContext(options);
            var userRepository = new UserRepository(dbContext);
            var tokenRepository = new TokenRepository(dbContext);
            var jwtOptions = Options.Create<JWTSettings>(new JWTSettings() { Issuer = "Backend", Audience = "Users", SecretKey = "myXAuthenticationSecret" });
            var jwtHandler = new JWTHandler(jwtOptions);
            authService = new AuthService(userRepository, jwtHandler, tokenRepository);

            //var user = new User { Username = "Test", Password = "Test", Role = Role.User };
            authService.Register(new RegisterRequestDTO { Username = "Test", Password = "Test", ClientId = "Test" });
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
            var result = authService.Register(user);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);
            Assert.Equal(2, dbContext.Users.ToListAsync().Result.Count);
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
            var result = authService.Register(user);

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

            var result = authService.Login(user);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IfUserExists_WithRememberMe_CompleteLoginAndReturnAccessAndRefreshToken()
        {
            var controller = new AuthController(authService);

            var user = new AuthRequestDTO
            {
                Username = "Test",
                Password = "Test",
                ClientId = "Test",
                GrantType = "password",
                RememberMe = true
            };

            var result = authService.Login(user).Result;

            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);
            Assert.NotNull(result.RefreshToken);
        }


        [Fact]
        public void UserProvidesRefreshToken_ReturnNewAccessToken()
        {
            var controller = new AuthController(authService);

            var firstLoginToCreateRefreshToken = new AuthRequestDTO
            {
                Username = "Test",
                Password = "Test",
                ClientId = "Test",
                RememberMe = true,
                GrantType = "password"
            };

            var login = authService.Login(firstLoginToCreateRefreshToken).Result;
            var user = new AuthRequestDTO { ClientId = "Test", GrantType = "refresh_token", RefreshToken = login.RefreshToken };
            var result = authService.RefreshAccessToken(user);

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

            var result = authService.Login(user).Result;

            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);
            Assert.Null(result.RefreshToken);
        }
    }
}
