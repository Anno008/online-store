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
        private readonly ComponentTypeRepository _componentTypeRepository;

        public ComponentTypesController(ComponentTypeRepository componentTypeRepository) =>
            _componentTypeRepository = componentTypeRepository;

        [HttpGet]
        public Task<List<ComponentType>> Get() =>
            _componentTypeRepository.GetAllAsync();

        [HttpGet("{id}")]
        public Task<ComponentType> Get(int id) =>
            _componentTypeRepository.GetAsync(id);

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public Task<ComponentType> Post([FromBody]ComponentType componentType) =>
            _componentTypeRepository.CreateAsync(componentType);

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public Task<ComponentType> Put(int id, [FromBody]ComponentType componentType) =>
            _componentTypeRepository.UpdateAsync(componentType);

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public void Delete(int id) =>
            _componentTypeRepository.DeleteAsync(id);
    }
}
