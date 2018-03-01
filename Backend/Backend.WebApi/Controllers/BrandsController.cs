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
        private readonly BrandRepository brandRepository;
        public BrandsController(BrandRepository brandRepository) =>
            this.brandRepository = brandRepository;

        [HttpGet]
        public Task<List<Brand>> Get() =>
            brandRepository.GetAllAsync();

        [HttpGet("{id}")]
        public Task<Brand> Get(int id) =>
            brandRepository.GetAsync(id);

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public Task<Brand> Post([FromBody]Brand brand) =>
            brandRepository.CreateAsync(brand);

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public Task<Brand> Put(int id, [FromBody]Brand brand) =>
            brandRepository.UpdateAsync(brand);

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public void Delete(int id) =>
            brandRepository.DeleteAsync(id);
    }
}
