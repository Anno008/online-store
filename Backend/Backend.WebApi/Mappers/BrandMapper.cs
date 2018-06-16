using Backend.WebApi.DTOs;
using Backend.WebApi.Models;

namespace Backend.WebApi.Mappers
{
    public class BrandMapper : IMapper<Brand, BrandDto>
    {
        public Brand ToEntityModel(BrandDto dto)
        {
            return new Brand
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public BrandDto ToDto(Brand model)
        {
            return new BrandDto
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
