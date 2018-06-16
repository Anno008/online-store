using Backend.WebApi.DTOs;
using Backend.WebApi.Models;

namespace Backend.WebApi.Mappers
{
    public class ComponentTypeMapper : IMapper<ComponentType, ComponentTypeDto>
    {
        public ComponentType ToEntityModel(ComponentTypeDto dto)
        {
            return new ComponentType
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public ComponentTypeDto ToDto(ComponentType model)
        {
            return new ComponentTypeDto
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
