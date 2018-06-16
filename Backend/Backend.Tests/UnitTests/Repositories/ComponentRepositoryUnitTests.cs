using System.Linq;

using Backend.WebApi.Models;
using Backend.WebApi.Repositories;

using Xunit;

namespace Backend.Tests.UnitTests.Repositories
{
    public class ComponentRepositoryUnitTests : RepositoryUnitTestsBase
    {
        private readonly ComponentRepository _componentRepository;

        public ComponentRepositoryUnitTests()
        {
            _componentRepository = new ComponentRepository(DbContext);
            DbContext.Brands.Add(new Brand { Name = "Intel" });
            DbContext.Brands.Add(new Brand { Name = "AMD" });

            DbContext.ComponentTypes.Add(new ComponentType { Name = "GPU" });
            DbContext.ComponentTypes.Add(new ComponentType { Name = "CPU" });

            // 8 components 
            DbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 2, Name = "AMD" },
                ComponentType = new ComponentType { Id = 1, Name = "GPU" },
                Name = "RX 460",
                Price = 100
            });
            DbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 2, Name = "AMD" },
                ComponentType = new ComponentType { Id = 1, Name = "GPU" },
                Name = "RX 470",
                Price = 100
            });
            DbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 2, Name = "AMD" },
                ComponentType = new ComponentType { Id = 1, Name = "GPU" },
                Name = "RX 480",
                Price = 100
            });
            DbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 2, Name = "AMD" },
                ComponentType = new ComponentType { Id = 1, Name = "GPU" },
                Name = "Vega 64",
                Price = 100
            });

            DbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 2, Name = "AMD" },
                ComponentType = new ComponentType { Id = 2, Name = "CPU" },
                Name = "Ryzen 1700x",
                Price = 100
            });

            DbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 2, Name = "AMD" },
                ComponentType = new ComponentType { Id = 2, Name = "CPU" },
                Name = "Ryzen 1500",
                Price = 100
            });

            DbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 1, Name = "Intel" },
                ComponentType = new ComponentType { Id = 2, Name = "CPU" },
                Name = "Intel 6600k",
                Price = 100
            });

            DbContext.Components.Add(new Component
            {
                Brand = new Brand { Id = 1, Name = "Intel" },
                ComponentType = new ComponentType { Id = 2, Name = "CPU" },
                Name = "Celeron duo",
                Price = 100
            });


            DbContext.SaveChanges();
        }

        [Fact]
        public void WithoutParameters_ReturnAllComopnents()
        {
            // Filtering params
            string name = null;
            int brandId = 0;
            var typeId = 0;

            // paging params
            int currentPageIn = 0;
            int pageSize = 0;

            var (components, totalPages, totalItems, itemsOnPage, currentPage) =
                _componentRepository.GetAll(name, brandId, typeId, currentPageIn, pageSize);

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
            int brandId = 0;
            var typeId = 0;

            // paging params
            int currentPageIn = 0;
            int pageSize = 0;

            var (components, totalPages, totalItems, itemsOnPage, currentPage) =
                _componentRepository.GetAll(name, brandId, typeId, currentPageIn, pageSize);

            Assert.Equal(2, components.Count);
            Assert.Equal(2, totalItems);
            Assert.Equal(1, totalPages);
            Assert.Equal(2, itemsOnPage);
            Assert.Equal(1, currentPage);
        }

        [Fact]
        public void WithPagingParameters_WithoutFilteringParameters_ReturnAppropriateComponents()
        {
            // Filtering params
            string name = null;
            int brandId = 0;
            var typeId = 0;

            // paging params
            int currentPageIn = 0;
            int pageSize = 3;

            var (components, totalPages, totalItems, itemsOnPage, currentPage) =
                _componentRepository.GetAll(name, brandId, typeId, currentPageIn, pageSize);

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
                ComponentType = componentType,
                Price = 123,
            };

            var result = _componentRepository.Create(component);
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
                ComponentType = componentType,
                Price = 123,
            };

            var result = await _componentRepository.CreateAsync(component);
            Assert.Null(result);
        }

        [Fact]
        public void CreateComponent_WithValidBrandAndComponentType_ReturnCreatedComponent()
        {
            var brand = DbContext.Brands.Find(1);
            var type = DbContext.ComponentTypes.Find(1);

            var component = new Component
            {
                Name = "Test",
                Brand = brand,
                ComponentType = type,
                Price = 123,
            };

            var result = _componentRepository.Create(component);
            Assert.NotNull(result);
            Assert.Equal(component.Name, result.Name);
            Assert.Equal(component.Brand.Name, result.Brand.Name);
            Assert.Equal(component.ComponentType.Name, result.ComponentType.Name);
            Assert.Equal(9, DbContext.Components.Count());
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
                ComponentType = type,
                Price = 123,
            };

            var result = await _componentRepository.CreateAsync(component);
            Assert.NotNull(result);
            Assert.Equal(component.Name, result.Name);
            Assert.Equal(component.Brand.Name, result.Brand.Name);
            Assert.Equal(component.ComponentType.Name, result.ComponentType.Name);
            Assert.Equal(9, DbContext.Components.Count());
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
                ComponentType = componentType,
                Price = 123,
            };

            var result = _componentRepository.Update(component);
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
                ComponentType = componentType,
                Price = 123,
            };

            var result = _componentRepository.Create(component);
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
                ComponentType = componentType,
                Price = 123,
            };

            var result = await _componentRepository.CreateAsync(component);
            Assert.Null(result);
        }

        [Fact]
        public void UpdateComponent_WithValidBrandAndComponentType_ReturnCreatedComponent()
        {
            var brand = DbContext.Brands.Find(1);

            var type = DbContext.ComponentTypes.Find(1);

            var component = DbContext.Components.Find(1);
            component.Name = "Changed name";
            component.Price = 400;
            component.Brand = brand;
            component.ComponentType = type;

            var result = _componentRepository.Update(component);
            Assert.NotNull(result);
            Assert.Equal(component.Name, result.Name);
            Assert.Equal(brand.Name, result.Brand.Name);
            Assert.Equal(type.Name, result.ComponentType.Name);
            Assert.Equal(8, DbContext.Components.Count());
        }

        [Fact]
        public async void UpdateComponentAsync_WithValidBrandAndComponentType_ReturnCreatedComponent()
        {
            var brand = DbContext.Brands.Find(1);
            var type = DbContext.ComponentTypes.Find(1);

            var component = DbContext.Components.Find(1);
            component.Name = "Changed name";
            component.Price = 400;
            component.Brand = brand;
            component.ComponentType = type;

            var result = await _componentRepository.UpdateAsync(component);
            Assert.NotNull(result);
            Assert.Equal(component.Name, result.Name);
            Assert.Equal(brand.Name, result.Brand.Name);
            Assert.Equal(type.Name, result.ComponentType.Name);
            Assert.Equal(8, DbContext.Components.Count());
        }

        [Fact]
        public void Delete_ShouldDeleteTheComponentFromTheDatabase()
        {
            var componentId = 1;

            _componentRepository.Delete(componentId);
            Assert.Equal(7, DbContext.Components.Count());
        }

        [Fact]
        public void DeleteAsync_ShouldDeleteTheComponentFromTheDatabase()
        {
            var componentId = 1;

            _componentRepository.DeleteAsync(componentId);
            Assert.Equal(7, DbContext.Components.Count());
        }

        [Fact]
        public void Delete_AttemptToDeleteAnEntityThatDoesntExist_ShouldntChangeTheTotalCount()
        {
            var componentId = 22;

            _componentRepository.Delete(componentId);
            Assert.Equal(8, DbContext.Components.Count());
        }

        [Fact]
        public void DeleteAsync_AttemptToDeleteAnEntityThatDoesntExist_ShouldntChangeTheTotalCount()
        {
            var componentId = 22;

            _componentRepository.DeleteAsync(componentId);
            Assert.Equal(8, DbContext.Components.Count());
        }
    }
}
