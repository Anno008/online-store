using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Backend.WebApi.DTOs.RequestDTOs;
using Backend.WebApi.Models;
using Backend.WebApi.Repositories;
using Backend.WebApi.DTOs.ResponseDTOs;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ComponentsController : Controller
    {
        private readonly ComponentRepository _componentRepository;

        public ComponentsController(ComponentRepository componentRepository) =>
            _componentRepository = componentRepository;

        [HttpGet("{id}")]
        public Task<Component> Get(int id) =>
            _componentRepository.GetAsync(id);

        [HttpGet]
        public ComponentsResponseDTO Get(ComponentsRequestDTO dto) =>
              new ComponentsResponseDTO(_componentRepository.GetAll(
                  dto.ComponentName,
                  dto.BrandId, 
                  dto.ComponentTypeId, 
                  dto.Page, 
                  dto.PageSize));

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public Task<Component> Post([FromBody]Component component) =>
            _componentRepository.CreateAsync(component);

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public Task<Component> Put(int id, [FromBody]Component component) =>
            _componentRepository.UpdateAsync(component);

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public void Delete(int id) =>
            _componentRepository.DeleteAsync(id);
    }
}
