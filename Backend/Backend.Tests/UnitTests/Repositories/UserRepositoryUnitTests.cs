using System;
using System.Linq;

using Backend.WebApi.Models;
using Backend.WebApi.Repositories;

using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Backend.Tests.UnitTests.Repositories
{
    public class UserRepositoryUnitTests
    {
        private readonly DatabaseContext dbContext;
        private readonly UserRepository userRepository;

        public UserRepositoryUnitTests()
        {
            // Setup
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new DatabaseContext(options);
            userRepository = new UserRepository(dbContext);

            userRepository.Register("Test", "Test");
        }

        [Fact]
        public void IfUserExists_ReturnNull()
        {
            Assert.Equal(1, dbContext.Users.ToList().Count);
            var result = userRepository.Register("Test", "Test");

            Assert.Null(result);
            Assert.Equal(1, dbContext.Users.ToList().Count);

        }

        [Fact]
        public void IfUserDoesntExist_CompleteRegistration_ReturnUser()
        {
            Assert.Equal(1, dbContext.Users.ToList().Count);
            var username = "Test1";
            var password = "Test1";

            var result = userRepository.Register(username, password);

            Assert.NotNull(result);
            Assert.Equal(username, result.Username);
            Assert.Equal(Role.User, result.Role);
            Assert.Equal(2, dbContext.Users.ToList().Count);
        }

        [Fact]
        public void AuthenticateProvided_CorrectCredentials_ReturnUser()
        {
            var username = "Test";
            var password = "Test";

            var result = userRepository.Authenticate(username, password);

            Assert.NotNull(result);
            Assert.Equal(username, result.Username);
            Assert.Equal(Role.User, result.Role);
        }

        [Fact]
        public void AuthenticateProvided_IncorrectUsername_ReturnNull()
        {
            var username = "ABC";
            var password = "Test";

            var result = userRepository.Authenticate(username, password);

            Assert.Null(result);
        }

        [Fact]
        public void AuthenticateProvided_IncorrectPassword_ReturnNull()
        {
            var username = "Test";
            var password = "ABC";

            var result = userRepository.Authenticate(username, password);

            Assert.Null(result);
        }

        [Fact]
        public void IfUserExists_ReturnTrue()
        {
            var username = "Test";

            var result = userRepository.UserExists(username);

            Assert.True(result);
        }

        [Fact]
        public void IfUserDoesntExists_ReturnTrue()
        {
            var username = "Test1";

            var result = userRepository.UserExists(username);

            Assert.False(result);
        }
    }
}
