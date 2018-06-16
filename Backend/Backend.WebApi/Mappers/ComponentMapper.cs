using Backend.WebApi.DTOs;
using Backend.WebApi.Models;

namespace Backend.WebApi.Mappers
{
    public class ComponentMapper : IMapper<Component, ComponentDto>
    {
        public Component ToEntityModel(ComponentDto dto)
        {
            var brandMapper = new BrandMapper();
            var componentTypeMapper = new ComponentTypeMapper();
            return new Component
            {
                Id = dto.Id,
                Name = dto.Name,
                Brand = brandMapper.ToEntityModel(dto.Brand),
                ComponentType = componentTypeMapper.ToEntityModel(dto.ComponentType),
                Price = dto.Price
            };
        }

        public ComponentDto ToDto(Component model)
        {
            var brandMapper = new BrandMapper();
            var componentTypeMapper = new ComponentTypeMapper();
            return new ComponentDto
            {
                Id = model.Id,
                Name = model.Name,
                Brand = brandMapper.ToDto(model.Brand),
                ComponentType = componentTypeMapper.ToDto(model.ComponentType),
                Price = model.Price
            };
        }
    }
}
