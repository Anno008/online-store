using Backend.WebApi.DTOs;
using Backend.WebApi.Models;
using Backend.WebApi.Repositories;
using Xunit;

namespace Backend.Tests.UnitTests.Repositories
{
    public class ComponentRepositoryUnitTests : RepositoryUnitTestsBase
    {
        private readonly ComponentRepository componentRepository;

        public ComponentRepositoryUnitTests()
        {
            componentRepository = new ComponentRepository(dbContext);
            dbContext.Brands.Add(new Brand { Name = "Intel" });
            dbContext.Brands.Add(new Brand { Name = "AMD" });

            dbContext.ComponentTypes.Add(new ComponentType { Name = "GPU" });
            dbContext.ComponentTypes.Add(new ComponentType { Name = "CPU" });

            // 8 components 
            dbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 2, Name = "AMD" },
                Type = new ComponentType { Id = 1, Name = "GPU" },
                Name = "RX 460",
                Price = 100
            });
            dbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 2, Name = "AMD" },
                Type = new ComponentType { Id = 1, Name = "GPU" },
                Name = "RX 470",
                Price = 100
            });
            dbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 2, Name = "AMD" },
                Type = new ComponentType { Id = 1, Name = "GPU" },
                Name = "RX 480",
                Price = 100
            });
            dbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 2, Name = "AMD" },
                Type = new ComponentType { Id = 1, Name = "GPU" },
                Name = "Vega 64",
                Price = 100
            });

            dbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 2, Name = "AMD" },
                Type = new ComponentType { Id = 2, Name = "CPU" },
                Name = "Ryzen 1700x",
                Price = 100
            });

            dbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 2, Name = "AMD" },
                Type = new ComponentType { Id = 2, Name = "CPU" },
                Name = "Ryzen 1500",
                Price = 100
            });

            dbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 1, Name = "Intel" },
                Type = new ComponentType { Id = 2, Name = "CPU" },
                Name = "Intel 6600k",
                Price = 100
            });

            dbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 1, Name = "Intel" },
                Type = new ComponentType { Id = 2, Name = "CPU" },
                Name = "Celeron duo",
                Price = 100
            });


            dbContext.SaveChanges();
        }

        [Fact]
        public void WithoutParameters_ReturnAllComopnents()
        {
            // Filtering params
            string name = null;
            int[] brandIds = null;
            var typeId = 0;

            // paging params
            int currentPageIn = 0;
            int pageSize = 0;

            // Ordering params
            var orderBy = OrderComponentsBy.Nothing;

            var (components, totalPages, totalItems, itemsOnPage, currentPage) =
                componentRepository.GetAll(name, brandIds, typeId, currentPageIn, pageSize, orderBy);

            Assert.Equal(8, components.Count);
            Assert.Equal(8, totalItems);
            Assert.Equal(1, totalPages);
            Assert.Equal(8, itemsOnPage);
            Assert.Equal(1, currentPage);
        }

        [Fact]
        public void WithoutPagingParameters_WithFilteringParameters_ReturnAppropriateComponents()
        {
            // Filtering params
            string name = "Ryzen";
            int[] brandIds = null;
            var typeId = 0;

            // paging params
            int currentPageIn = 0;
            int pageSize = 0;

            // Ordering params
            var orderBy = OrderComponentsBy.Nothing;

            var (components, totalPages, totalItems, itemsOnPage, currentPage) =
                componentRepository.GetAll(name, brandIds, typeId, currentPageIn, pageSize, orderBy);

            Assert.Equal(2, components.Count);
            Assert.Equal(8, totalItems);
            Assert.Equal(1, totalPages);
            Assert.Equal(2, itemsOnPage);
            Assert.Equal(1, currentPage);
        }

        [Fact]
        public void WithPagingParameters_WithoutFilteringParameters_ReturnAppropriateComponents()
        {
            // Filtering params
            string name = null;
            int[] brandIds = null;
            var typeId = 0;

            // paging params
            int currentPageIn = 0;
            int pageSize = 3;

            // Ordering params
            var orderBy = OrderComponentsBy.Nothing;

            var (components, totalPages, totalItems, itemsOnPage, currentPage) =
                componentRepository.GetAll(name, brandIds, typeId, currentPageIn, pageSize, orderBy);

            Assert.Equal(3, components.Count);
            Assert.Equal(8, totalItems);
            Assert.Equal(3, totalPages);
            Assert.Equal(3, itemsOnPage);
            Assert.Equal(1, currentPage);
        }
    }
}
