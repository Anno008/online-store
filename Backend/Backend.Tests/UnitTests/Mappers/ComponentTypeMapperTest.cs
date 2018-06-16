using Backend.WebApi.DTOs;
using Backend.WebApi.Mappers;
using Backend.WebApi.Models;
using Xunit;

namespace Backend.Tests.UnitTests.Mappers
{
    public class ComponentTypeMapperTest
    {
        private readonly ComponentTypeMapper _componentTypeMapper;

        public ComponentTypeMapperTest()
        {
            _componentTypeMapper = new ComponentTypeMapper();
        }

        [Fact]
        public void DtoToModelEntityTest()
        {
            var dto = new ComponentTypeDto
            {
                Id = 1,
                Name = "One"
            };

            var model = _componentTypeMapper.ToEntityModel(dto);

            Assert.Equal(dto.Id, model.Id);
            Assert.Equal(dto.Name, model.Name);
        }

        [Fact]
        public void ModelEntityToDtoTest()
        {
            var model = new ComponentType
            {
                Id = 1,
                Name = "One"
            };

            var dto = _componentTypeMapper.ToDto(model);

            Assert.Equal(model.Id, dto.Id);
            Assert.Equal(model.Name, dto.Name);
        }
    }
}
