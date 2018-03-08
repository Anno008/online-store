﻿using System;
using Backend.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Tests.UnitTests.Repositories
{
    public abstract class RepositoryUnitTestsBase : IDisposable
    {
        protected readonly DatabaseContext dbContext;

        protected RepositoryUnitTestsBase()
        {
            // Setup
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            dbContext = new DatabaseContext(options);
        }

        public void Dispose() =>
            dbContext.Dispose();
    }
}
