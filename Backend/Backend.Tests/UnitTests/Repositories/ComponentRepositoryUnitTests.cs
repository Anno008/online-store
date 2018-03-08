using System.Linq;
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

        [Fact]
        public void CreateComponent_WithUnexistingBrandAndComponentType_ReturnNull()
        {
            var unexistingBrand = new Brand { Id = 5, Name = "Abc" };
            var componentType = new ComponentType { Id = 5, Name = "Abc" };

            var component = new Component
            {
                Name = "Test",
                Brand = unexistingBrand,
                Type = componentType,
                Price = 123,
            };

            var result = componentRepository.Create(component);
            Assert.Null(result);
        }

        [Fact]
        public async void CreateComponentAsync_WithUnexistingBrandAndComponentType_ReturnNull()
        {
            var unexistingBrand = new Brand { Id = 5, Name = "Abc" };
            var componentType = new ComponentType { Id = 5, Name = "Abc" };

            var component = new Component
            {
                Name = "Test",
                Brand = unexistingBrand,
                Type = componentType,
                Price = 123,
            };

            var result = await componentRepository.CreateAsync(component);
            Assert.Null(result);
        }

        [Fact]
        public void CreateComponent_WithValidBrandAndComponentType_ReturnCreatedComponent()
        {
            var brand = new Brand { Id = 1, Name = "Intel" };
            var type = new ComponentType { Id = 1, Name = "CPU" };

            var component = new Component
            {
                Name = "Test",
                Brand = brand,
                Type = type,
                Price = 123,
            };

            var result = componentRepository.Create(component);
            Assert.NotNull(result);
            Assert.Equal(component.Name, result.Name);
            Assert.Equal(component.Brand.Name, result.Brand.Name);
            Assert.Equal(component.Type.Name, result.Type.Name);
            Assert.Equal(9, dbContext.Components.Count());
        }

        [Fact]
        public async void CreateComponentAsync_WithValidBrandAndComponentType_ReturnCreatedComponent()
        {
            var brand = new Brand { Id = 1, Name = "Intel" };
            var type = new ComponentType { Id = 1, Name = "CPU" };

            var component = new Component
            {
                Name = "Test",
                Brand = brand,
                Type = type,
                Price = 123,
            };

            var result = await componentRepository.CreateAsync(component);
            Assert.NotNull(result);
            Assert.Equal(component.Name, result.Name);
            Assert.Equal(component.Brand.Name, result.Brand.Name);
            Assert.Equal(component.Type.Name, result.Type.Name);
            Assert.Equal(9, dbContext.Components.Count());
        }

        [Fact]
        public void UpdateComponent_ComponentDoesntExist_ReturnNull()
        {
            var unexistingBrand = new Brand { Id = 5, Name = "Abc" };
            var componentType = new ComponentType { Id = 5, Name = "Abc" };

            var component = new Component
            {
                Id = 111,
                Name = "Test",
                Brand = unexistingBrand,
                Type = componentType,
                Price = 123,
            };

            var result = componentRepository.Update(component);
            Assert.Null(result);
        }

        [Fact]
        public void UpdateComponent_WithUnexistingBrandAndComponentType_ReturnNull()
        {
            var unexistingBrand = new Brand { Id = 5, Name = "Abc" };
            var componentType = new ComponentType { Id = 5, Name = "Abc" };

            var component = new Component
            {
                Name = "Test",
                Brand = unexistingBrand,
                Type = componentType,
                Price = 123,
            };

            var result = componentRepository.Create(component);
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateComponentAsync_WithUnexistingBrandAndComponentType_ReturnNull()
        {
            var unexistingBrand = new Brand { Id = 5, Name = "Abc" };
            var componentType = new ComponentType { Id = 5, Name = "Abc" };

            var component = new Component
            {
                Name = "Test",
                Brand = unexistingBrand,
                Type = componentType,
                Price = 123,
            };

            var result = await componentRepository.CreateAsync(component);
            Assert.Null(result);
        }

        [Fact]
        public void UpdateComponent_WithValidBrandAndComponentType_ReturnCreatedComponent()
        {
            var brand1 = new Brand { Id = 1, Name = "Intel" };
            var brand2 = new Brand { Id = 2, Name = "AMD" };

            var type1 = new ComponentType { Id = 1, Name = "GPU" };
            var type2 = new ComponentType { Id = 2, Name = "CPU" };

            var component = new Component
            {
                Id = 1,
                Brand = brand2,
                Type = type2,
                Name = "AMD FX 6600",
                Price = 150
            };

            var result = componentRepository.Update(component);
            Assert.NotNull(result);
            Assert.Equal(component.Name, result.Name);
            Assert.Equal(component.Brand.Name, result.Brand.Name);
            Assert.Equal(component.Type.Name, result.Type.Name);
            Assert.Equal(8, dbContext.Components.Count());
        }

        [Fact]
        public async void UpdateComponentAsync_WithValidBrandAndComponentType_ReturnCreatedComponent()
        {
            var brand1 = new Brand { Id = 1, Name = "Intel" };
            var brand2 = new Brand { Id = 2, Name = "AMD" };

            var type1 = new ComponentType { Id = 1, Name = "GPU" };
            var type2 = new ComponentType { Id = 2, Name = "CPU" };

            var component = new Component
            {
                Id = 1,
                Brand = brand2,
                Type = type2,
                Name = "AMD FX 6600",
                Price = 150
            };

            var result = await componentRepository.UpdateAsync(component);
            Assert.NotNull(result);
            Assert.Equal(component.Name, result.Name);
            Assert.Equal(component.Brand.Name, result.Brand.Name);
            Assert.Equal(component.Type.Name, result.Type.Name);
            Assert.Equal(8, dbContext.Components.Count());
        }

        [Fact]
        public void Delete_ShouldDeleteTheComponentFromTheDatabase()
        {
            var componentId = 1;

            componentRepository.Delete(componentId);
            Assert.Equal(7, dbContext.Components.Count());
        }

        [Fact]
        public void DeleteAsync_ShouldDeleteTheComponentFromTheDatabase()
        {
            var componentId = 1;

            componentRepository.DeleteAsync(componentId);
            Assert.Equal(7, dbContext.Components.Count());
        }

        [Fact]
        public void Delete_AttemptToDeleteAnEntityThatDoesntExist_ShouldntChangeTheTotalCount()
        {
            var componentId = 22;

            componentRepository.Delete(componentId);
            Assert.Equal(8, dbContext.Components.Count());
        }

        [Fact]
        public void DeleteAsync_AttemptToDeleteAnEntityThatDoesntExist_ShouldntChangeTheTotalCount()
        {
            var componentId = 22;

            componentRepository.DeleteAsync(componentId);
            Assert.Equal(8, dbContext.Components.Count());
        }
    }
}
