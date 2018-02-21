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
            dbContext.ResetValueGenerators();
        }

        public void Dispose() =>
            dbContext.Dispose();
    }
}
