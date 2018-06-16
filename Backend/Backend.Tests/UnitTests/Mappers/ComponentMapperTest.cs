using Backend.WebApi.DTOs;
using Backend.WebApi.Mappers;
using Backend.WebApi.Models;
using Xunit;

namespace Backend.Tests.UnitTests.Mappers
{
    public class ComponentMapperTest
    {
        private readonly ComponentMapper _componentMapper;

        public ComponentMapperTest()
        {
            _componentMapper = new ComponentMapper();
        }

        [Fact]
        public void DtoToModelEntityTest()
        {
            var componentTypeDto = new ComponentTypeDto
            {
                Id = 1,
                Name = "CPU"
            };

            var brandDto = new BrandDto
            {
                Id = 1,
                Name = "Intel"
            };

            var componentDto = new ComponentDto
            {
                Id = 1,
                Name = "Intel 6600k",
                ComponentType = componentTypeDto,
                Brand = brandDto,
                Price = 100
            };

            var model = _componentMapper.ToEntityModel(componentDto);

            Assert.Equal(componentDto.Id, model.Id);
            Assert.Equal(componentDto.Name, model.Name);
            Assert.Equal(componentDto.Price, model.Price);
            Assert.Equal(componentDto.Brand.Id, model.Brand.Id);
            Assert.Equal(componentDto.Brand.Name, model.Brand.Name);
            Assert.Equal(componentDto.ComponentType.Id, model.ComponentType.Id);
            Assert.Equal(componentDto.ComponentType.Name, model.ComponentType.Name);
        }

        [Fact]
        public void ModelEntityToDtoTest()
        {
            var componentTypeModel = new ComponentType
            {
                Id = 1,
                Name = "CPU"
            };

            var brandModel = new Brand
            {
                Id = 1,
                Name = "Intel"
            };

            var componentModel = new Component
            {
                Id = 1,
                Name = "Intel 6600k",
                ComponentType = componentTypeModel,
                Brand = brandModel,
                Price = 100
            };

            var dto = _componentMapper.ToDto(componentModel);

            Assert.Equal(componentModel.Id, dto.Id);
            Assert.Equal(componentModel.Name, dto.Name);
            Assert.Equal(componentModel.Price, dto.Price);
            Assert.Equal(componentModel.Brand.Id, dto.Brand.Id);
            Assert.Equal(componentModel.Brand.Name, dto.Brand.Name);
            Assert.Equal(componentModel.ComponentType.Id, dto.ComponentType.Id);
            Assert.Equal(componentModel.ComponentType.Name, dto.ComponentType.Name);
        }
    }
}
