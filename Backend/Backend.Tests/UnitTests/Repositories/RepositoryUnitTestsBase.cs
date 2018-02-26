using System;
using Backend.Tests.UnitTests.Helpers;
using Backend.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Tests.UnitTests.Repositories
{
    public abstract class RepositoryUnitTestsBase : IDisposable
    {
        protected readonly DatabaseContext dbContext;

        public RepositoryUnitTestsBase()
        {
            // Setup
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new DatabaseContext(options);
            // a work around to reset the auto generation after each test,
            // far from ideal since it occasionally causes the tests to fail
            dbContext.ResetValueGenerators();
        }

        public void Dispose() =>
            dbContext.Dispose();
    }
}
