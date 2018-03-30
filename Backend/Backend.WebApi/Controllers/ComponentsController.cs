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
        private readonly ComponentRepository componentRepository;

        public ComponentsController(ComponentRepository componentRepository) =>
            this.componentRepository = componentRepository;

        [HttpGet("{id}")]
        public Task<Component> Get(int id) =>
            componentRepository.GetAsync(id);

        [HttpGet]
        public ComponentsResponseDTO Get(ComponentsRequestDTO dto) =>
              new ComponentsResponseDTO(componentRepository.GetAll(
                  dto.ComponentName,
                  dto.BrandId, 
                  dto.ComponentTypeId, 
                  dto.Page, 
                  dto.PageSize,
                  dto.OrderBy));

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public Task<Component> Post([FromBody]Component component) =>
            componentRepository.CreateAsync(component);

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public Task<Component> Put(int id, [FromBody]Component component) =>
            componentRepository.UpdateAsync(component);

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public void Delete(int id) =>
            componentRepository.DeleteAsync(id);
    }
}
