using System;
using System.Net;
using System.Threading.Tasks;

using Backend.Tests.IntegrationTests.TestServerSetup;
using Backend.WebApi.Controllers;
using Backend.WebApi.Models;
using Backend.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Backend.Tests.IntegrationTests.ControllerTests
{
    public class BrandsControllerTests : WebServer
    {
        private readonly BrandRepository brandRepository;
        
        public BrandsControllerTests()
        {
            // Setup
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new DatabaseContext(options);
            brandRepository = new BrandRepository(dbContext);
            brandRepository.Create(new Brand { Name = "Test" });
        }

        [Fact]
        public async Task GetAllBrands_ShouldReturnList()
        {
            // Act
            var response = await client.GetAsync("api/brands");

            var t = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var controller = new BrandsController(brandRepository);
            var result = await controller.Get();

            Assert.True(result.Count > 0);
        }

    }
}
