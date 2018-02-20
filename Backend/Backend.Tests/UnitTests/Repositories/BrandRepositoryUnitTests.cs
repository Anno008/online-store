using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backend.WebApi.Models;
using Backend.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Backend.Tests.UnitTests.Repositories
{
    public class BrandRepositoryUnitTests
    {
        private readonly DatabaseContext dbContext;
        private readonly BrandRepository brandRepository;

        public BrandRepositoryUnitTests()
        {
            // Setup
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new DatabaseContext(options);
            brandRepository = new BrandRepository(dbContext);

            dbContext.Add(new Brand { Id = 1, Name = "Intel" });
            dbContext.Add(new Brand { Id = 2, Name = "AMD" });
            dbContext.SaveChanges();
        }
    }
}
