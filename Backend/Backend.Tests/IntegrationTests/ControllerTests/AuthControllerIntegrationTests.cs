using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Xunit;
using Backend.WebApi.Controllers;
using Backend.WebApi.DTOs;
using Backend.WebApi.Models;
using Backend.WebApi.Repositories;
using Backend.WebApi.Services;
using Backend.WebApi.Services.Security;
using Backend.Tests.IntegrationTests.TestServerSetup;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Tests.IntegrationTests.Controllers
{
    public class AuthControllerIntegrationTests : WebServer
    {
        private readonly AuthService authService;

        public AuthControllerIntegrationTests()
        {
            // Setup
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new DatabaseContext(options);
            var userRepository = new UserRepository(dbContext);
            var tokenRepository = new TokenRepository(dbContext);
            var jwtOptions = Options.Create<JWTSettings>(new JWTSettings() { Issuer = "Backend", Audience = "Users", SecretKey = "myXAuthenticationSecret" });
            var jwtHandler = new JWTHandler(jwtOptions);
            authService = new AuthService(userRepository, jwtHandler, tokenRepository);

            authService.Register(new RegisterDTO { Username = "Test", Password = "Test", ClientId = "Test" });
        }

        [Fact]
        public void IfUsernameIsntTaken_CompleteRegistration()
        {
            var user = new RegisterDTO { Username = "Test1", Password = "Test1", ClientId = "Test" };
            var controller = new AuthController(authService);

            var result = (controller.Register(user) as ObjectResult).Value as AuthResponseDTO;
            var token = new JwtSecurityTokenHandler().ReadJwtToken(result.AccessToken);

            var username = token.Claims.FirstOrDefault(claim => claim.Type == "user_name").Value;
            var clientId = token.Claims.FirstOrDefault(claim => claim.Type == "sub").Value;
            var role = token.Claims.Where(claim => claim.Type == "roles").FirstOrDefault().Value;

            Assert.NotNull(result);
            Assert.Equal(user.Username, username);
            Assert.Equal(user.ClientId, clientId);
            Assert.Equal(Role.User.ToString(), role);
        }

        [Fact]
        public void IfUsernameIsTaken_ReturnBadRequest()
        {
            var user = new RegisterDTO {
                Username = "Test",
                Password = "Test",
                ClientId = "Test"
            };

            var controller = new AuthController(authService);
            var result = controller.Register(user) as ObjectResult;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("The user with the given username already exists.", result.Value);
        }

        [Fact]
        public void IfUserExists_CompleteLoginAndReturnToken()
        {
            var user = new AuthDTO
            {
                Username = "Test",
                Password = "Test",
                ClientId = "Test",
                GrantType = "password"
            };

            var controller = new AuthController(authService);
            var result = controller.Auth(user).Result as ObjectResult;
            var content = result.Value as AuthResponseDTO;

            Assert.Equal(200, result.StatusCode);
            Assert.True(content.AccessToken.Length > 10);
        }

        [Fact]
        public void IfUserExists_WithRememberMe_CompleteLoginAndReturnAccessAndRefreshToken()
        {
            var controller = new AuthController(authService);
            
            var user = new AuthDTO
            {
                Username = "Test",
                Password = "Test",
                ClientId = "Test",
                GrantType = "password",
                RememberMe = true
            };

            var result = controller.Auth(user).Result as ObjectResult;
            var content = result.Value as AuthResponseDTO;

            Assert.Equal(200, result.StatusCode);
            Assert.True(content.AccessToken.Length > 10);
            Assert.True(content.RefreshToken.Length > 10);
        }

        [Fact]
        public void UserProvidesRefreshToken_ReturnNewAccessToken()
        {
            var controller = new AuthController(authService);

            var firstLoginToCreateRefreshToken = new AuthDTO
            {
                Username = "Test",
                Password = "Test",
                ClientId = "Test",
                RememberMe = true,
                GrantType = "password"
            };

            var login = controller.Auth(firstLoginToCreateRefreshToken).Result as ObjectResult;

            var user = new AuthDTO { ClientId = "Test", GrantType = "refresh_token", RefreshToken= (login.Value as AuthResponseDTO).RefreshToken };
            var result = controller.Auth(user).Result as ObjectResult;
            var content = result.Value as AuthResponseDTO;

            Assert.Equal(200, result.StatusCode);
            Assert.True(content.AccessToken.Length > 10);
            Assert.True(content.RefreshToken == null);
        }

        [Fact]
        public void IfUserExists_RememberMeFalse_RefreshTokenNull()
        {
            var user = new AuthDTO { Username = "Test", Password = "Test", ClientId = "Test", GrantType = "password", RememberMe = false };
            var controller = new AuthController(authService);
            var result = controller.Auth(user).Result as ObjectResult;
            var content = result.Value as AuthResponseDTO;

            Assert.Equal(200, result.StatusCode);
            Assert.True(content.AccessToken.Length > 10);
            Assert.True(content.RefreshToken == null);
        }

        [Fact]
        public void IfUserDoesntExists_ReturnBadRequest()
        {
            var user = new AuthDTO { Username = "Test123", Password = "Test123", ClientId = "Test", GrantType = "password" };

            var controller = new AuthController(authService);
            var result = controller.Auth(user).Result as ObjectResult;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Incorrect credentials.", result.Value);
        }

        [Fact]
        public void RequestWithoutGrantType_ReturnsBadRequest()
        {
            var user = new AuthDTO();
            var controller = new AuthController(authService);
            var result = controller.Auth(user).Result as ObjectResult;

            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void RegisterRequestWithoutData_ReturnsBadRequest()
        {
            var user = new RegisterDTO();
            var controller = new AuthController(authService);
            var result = controller.Register(null) as StatusCodeResult;

            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task IfGrantTypeIsInvalid_Auth_ShoudReturnBadRequest()
        {
            var controller = new AuthController(authService);

            var formData = new Dictionary<string, string>
              {
                {"Username", "Test"},
                {"Password", "Test"},
                {"ClientId", "Test"},
                {"GrantType", "abc"}
              };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/auth")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var response = await client.SendAsync(postRequest);

            Assert.Equal("Bad Request", response.ReasonPhrase);
        }

        [Fact]
        public async Task IfGrantTypeIsNull_Auth_ShoudReturnBadRequest()
        {
            var controller = new AuthController(authService);

            var formData = new Dictionary<string, string>
              {
                {"Username", "Test"},
                {"Password", "Test"},
                {"ClientId", "Test"},
              };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/auth")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var response = await client.SendAsync(postRequest);

            Assert.Equal("Bad Request", response.ReasonPhrase);
        }
    }
}
