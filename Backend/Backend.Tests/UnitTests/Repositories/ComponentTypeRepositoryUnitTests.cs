using System.Linq;

using Backend.WebApi.Models;
using Backend.WebApi.Repositories;

using Xunit;

namespace Backend.Tests.UnitTests.Repositories
{
    public class ComponentTypeRepositoryUnitTests : RepositoryUnitTestsBase
    {
        private readonly ComponentTypeRepository _componentTypeRepository;

        public ComponentTypeRepositoryUnitTests()
        {
            _componentTypeRepository = new ComponentTypeRepository(DbContext);
            DbContext.ComponentTypes.Add(new ComponentType { Name = "CPU" });
            DbContext.ComponentTypes.Add(new ComponentType { Name = "GPU" });
            DbContext.SaveChanges();
        }

        [Fact]
        public void GetAllComponentTypes_ShouldReturnExpectedResults()
        {
            var componentTypes = _componentTypeRepository.GetAll().ToList();

            Assert.Equal(2, componentTypes.Count);
            Assert.Equal(1, componentTypes.First().Id);
            Assert.Equal("CPU", componentTypes.First().Name);
        }

        [Fact]
        public async void GetAllComponentTypesAsync_ShouldReturnExpectedResults()
        {
            var componentTypes = await _componentTypeRepository.GetAllAsync();

            Assert.Equal(2, componentTypes.ToList().Count);
            Assert.Equal(1, componentTypes.First().Id);
            Assert.Equal("CPU", componentTypes.First().Name);
        }

        [Fact]
        public void GetComponentTypeById_ComponentTypeExists_ReturnComponentType()
        {
            var componentType = _componentTypeRepository.Get(1);

            Assert.NotNull(componentType);
            Assert.Equal(1, componentType.Id);
            Assert.Equal("CPU", componentType.Name);
        }

        [Fact]
        public async void GetComponentTypeByIdAsync_ComponentTypeExists_ReturnComponentType()
        {
            var componentType = await _componentTypeRepository.GetAsync(1);

            Assert.NotNull(componentType);
            Assert.Equal(1, componentType.Id);
            Assert.Equal("CPU", componentType.Name);
        }

        [Fact]
        public void GetComponentTypeById_ComponentTypeDoenstExist_ReturnNull()
        {
            var componentType = _componentTypeRepository.Get(3);

            Assert.Null(componentType);
        }

        [Fact]
        public async void GetComponentTypeByIdAsync_ComponentTypeDoenstExist_ReturnNull()
        {
            var componentType = await _componentTypeRepository.GetAsync(3);

            Assert.Null(componentType);
        }

        [Fact]
        public void CreateComponentType_ShouldReturnTheCreatedComponentType_AndIncreaseTheComponentTypesCount()
        {
            var newComponentType = new ComponentType { Name = "PSU" };
            var expectedNumberOfComponentTypes = DbContext.ComponentTypes.Count() + 1;

            var createdComponentType = _componentTypeRepository.Create(newComponentType);

            Assert.NotNull(createdComponentType);
            Assert.Equal(newComponentType.Name, createdComponentType.Name);
            Assert.True(newComponentType.Id != 0);
            Assert.Equal(expectedNumberOfComponentTypes, DbContext.ComponentTypes.Count());
        }

        [Fact]
        public async void CreateComponentTypeAsync_ShouldReturnTheCreatedComponentType_AndIncreaseTheComponentTypesCount()
        {
            var newComponentType = new ComponentType { Name = "PSU" };
            var expectedNumberOfComponentTypes = DbContext.ComponentTypes.Count() + 1;

            var createdComponentType = await _componentTypeRepository.CreateAsync(newComponentType);

            Assert.NotNull(createdComponentType);
            Assert.Equal(newComponentType.Name, createdComponentType.Name);
            Assert.True(newComponentType.Id != 0);
            Assert.Equal(expectedNumberOfComponentTypes, DbContext.ComponentTypes.Count());
        }

        [Fact]
        public void IfComponentTypeExists_UpdateComponentType_AndReturnComponentType()
        {
            var componentType = DbContext.ComponentTypes.Find(1);
            componentType.Name = "CPU123";

            var updatedComponentType = _componentTypeRepository.Update(componentType);

            Assert.NotNull(updatedComponentType);
            Assert.Equal(componentType.Name, updatedComponentType.Name);
            Assert.Equal(componentType.Id, updatedComponentType.Id);
        }

        [Fact]
        public void IfTheComponentTypeToUpdate_DoesntExist_ReturnNull()
        {
            var componentType = new ComponentType { Id = 3, Name = "Intel123" };

            var updatedComponentType = _componentTypeRepository.Update(componentType);

            Assert.Null(updatedComponentType);
        }

        [Fact]
        public async void IfComponentTypeExists_UpdateCompojnentTypeAsync_AndReturnUpdatedComponentType()
        {
            var componentType = DbContext.ComponentTypes.Find(1);
            componentType.Name = "Intel1234";
            var updatedComponentType = await _componentTypeRepository.UpdateAsync(componentType);

            Assert.NotNull(updatedComponentType);
            Assert.Equal(componentType.Name, updatedComponentType.Name);
            Assert.Equal(componentType.Id, updatedComponentType.Id);
        }

        [Fact]
        public async void IfTheComponentTypeToUpdate_DoesntExistAsync_ReturnNull()
        {
            var componentType = new ComponentType { Id = 3, Name = "Intel123" };

            var updateComponentType = await _componentTypeRepository.UpdateAsync(componentType);

            Assert.Null(updateComponentType);
        }

        [Fact]
        public void Delete_ShouldDeleteTheComponentTypeFromTheDatabase()
        {
            var componentTypeId = 1;

            _componentTypeRepository.Delete(componentTypeId);
            Assert.Equal(1, DbContext.ComponentTypes.Count());
        }

        [Fact]
        public void DeleteAsync_ShouldDeleteTheComponentTypeFromTheDatabase()
        {
            var componentTypeId = 1;

            _componentTypeRepository.DeleteAsync(componentTypeId);
            Assert.Equal(1, DbContext.ComponentTypes.Count());
        }

        [Fact]
        public void Delete_AttemptToDeleteAnEntityThatDoesntExist_ShouldntChangeTheTotalCount()
        {
            var componentTypeId = 11;

            _componentTypeRepository.Delete(componentTypeId);
            Assert.Equal(2, DbContext.ComponentTypes.Count());
        }

        [Fact]
        public void DeleteAsync_AttemptToDeleteAnEntityThatDoesntExist_ShouldntChangeTheTotalCount()
        {
            var componentTypeId = 11;

            _componentTypeRepository.DeleteAsync(componentTypeId);
            Assert.Equal(2, DbContext.ComponentTypes.Count());
        }
    }
}

