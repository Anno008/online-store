using System.Collections.Generic;
using System.Threading.Tasks;

using Backend.WebApi.Models;
using Backend.WebApi.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BrandsController : Controller
    {
        private readonly BrandRepository _brandRepository;

        public BrandsController(BrandRepository brandRepository) =>
            _brandRepository = brandRepository;

        [HttpGet]
        public Task<List<Brand>> Get() =>
            _brandRepository.GetAllAsync();

        [HttpGet("{id}")]
        public Task<Brand> Get(int id) =>
            _brandRepository.GetAsync(id);

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public Task<Brand> Post([FromBody]Brand brand) =>
            _brandRepository.CreateAsync(brand);

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public Task<Brand> Put(int id, [FromBody]Brand brand) =>
            _brandRepository.UpdateAsync(brand);

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public void Delete(int id) =>
            _brandRepository.DeleteAsync(id);
    }
}
