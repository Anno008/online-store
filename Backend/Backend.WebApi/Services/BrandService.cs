using Backend.WebApi.DTOs;
using Backend.WebApi.Mappers;
using Backend.WebApi.Models;
using Backend.WebApi.Repositories;

namespace Backend.WebApi.Services
{
    public class BrandService : ServiceBase<Brand, BrandDto>
    {
        public BrandService(BrandRepository repository, IMapper<Brand, BrandDto> mapper) : base(repository, mapper)
        {

        }
    }
}
