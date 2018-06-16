using Backend.WebApi.DTOs;
using Backend.WebApi.Mappers;
using Backend.WebApi.Models;
using Xunit;

namespace Backend.Tests.UnitTests.Mappers
{
    public class BrandMapperTest
    {
        private readonly BrandMapper _brandMapper;

        public BrandMapperTest()
        {
            _brandMapper = new BrandMapper();
        }

        [Fact]
        public void DtoToModelEntityTest()
        {
            var dto = new BrandDto
            {
                Id = 1,
                Name = "One"
            };

            var model = _brandMapper.ToEntityModel(dto);

            Assert.Equal(dto.Id, model.Id);
            Assert.Equal(dto.Name, model.Name);
        }

        [Fact]
        public void ModelEntityToDtoTest()
        {
            var model = new Brand
            {
                Id = 1,
                Name = "One"
            };

            var dto = _brandMapper.ToDto(model);

            Assert.Equal(model.Id, dto.Id);
            Assert.Equal(model.Name, dto.Name);
        }
    }
}
