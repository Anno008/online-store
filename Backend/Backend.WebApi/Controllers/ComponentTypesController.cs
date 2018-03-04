using System.Collections.Generic;
using System.Threading.Tasks;

using Backend.WebApi.Models;
using Backend.WebApi.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ComponentTypesController : Controller
    {
        private readonly ComponentTypeRepository componentTypeRepository;

        public ComponentTypesController(ComponentTypeRepository componentTypeRepository) =>
            this.componentTypeRepository = componentTypeRepository;

        [HttpGet]
        public Task<List<ComponentType>> Get() =>
            componentTypeRepository.GetAllAsync();

        [HttpGet("{id}")]
        public Task<ComponentType> Get(int id) =>
            componentTypeRepository.GetAsync(id);

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public Task<ComponentType> Post([FromBody]ComponentType ComponentType) =>
            componentTypeRepository.CreateAsync(ComponentType);

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public Task<ComponentType> Put(int id, [FromBody]ComponentType ComponentType) =>
            componentTypeRepository.UpdateAsync(ComponentType);

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public void Delete(int id) =>
            componentTypeRepository.DeleteAsync(id);
    }
}
