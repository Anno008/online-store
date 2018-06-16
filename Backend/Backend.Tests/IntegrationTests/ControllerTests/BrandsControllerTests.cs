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
        private readonly BrandRepository _brandRepository;
        
        public BrandsControllerTests()
        {
            // Setup
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new DatabaseContext(options);
            _brandRepository = new BrandRepository(dbContext);
            _brandRepository.Create(new Brand { Name = "Test" });
        }

        [Fact]
        public async Task GetAllBrands_ShouldReturnList()
        {
            // Act
            var response = await Client.GetAsync("api/brands");

            await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var controller = new BrandsController(_brandRepository);
            var result = await controller.Get();

            Assert.True(result.Count > 0);
        }

    }
}
