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
        private readonly DatabaseContext _dbContext;
        private readonly UserRepository _userRepository;

        public UserRepositoryUnitTests()
        {
            // Setup
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new DatabaseContext(options);
            _userRepository = new UserRepository(_dbContext);

            _userRepository.Register("Test", "Test");
        }

        [Fact]
        public void IfUserExists_ReturnNull()
        {
            Assert.Single(_dbContext.Users.ToList());
            var result = _userRepository.Register("Test", "Test");

            Assert.Null(result);
            Assert.Single(_dbContext.Users.ToList());

        }

        [Fact]
        public void IfUserDoesntExist_CompleteRegistration_ReturnUser()
        {
            Assert.Single(_dbContext.Users.ToList());
            var username = "Test1";
            var password = "Test1";

            var result = _userRepository.Register(username, password);

            Assert.NotNull(result);
            Assert.Equal(username, result.Username);
            Assert.Equal(Role.User, result.Role);
            Assert.Equal(2, _dbContext.Users.ToList().Count);
        }

        [Fact]
        public void AuthenticateProvided_CorrectCredentials_ReturnUser()
        {
            var username = "Test";
            var password = "Test";

            var result = _userRepository.Authenticate(username, password);

            Assert.NotNull(result);
            Assert.Equal(username, result.Username);
            Assert.Equal(Role.User, result.Role);
        }

        [Fact]
        public void AuthenticateProvided_IncorrectUsername_ReturnNull()
        {
            var username = "ABC";
            var password = "Test";

            var result = _userRepository.Authenticate(username, password);

            Assert.Null(result);
        }

        [Fact]
        public void AuthenticateProvided_IncorrectPassword_ReturnNull()
        {
            var username = "Test";
            var password = "ABC";

            var result = _userRepository.Authenticate(username, password);

            Assert.Null(result);
        }

        [Fact]
        public void IfUserExists_ReturnTrue()
        {
            var username = "Test";

            var user = _userRepository.GetUserByName(username);

            Assert.True(user != null);
        }

        [Fact]
        public void IfUserDoesntExists_ReturnTrue()
        {
            var username = "Test1";

            var user = _userRepository.GetUserByName(username);

            Assert.False(user != null);
        }
    }
}
