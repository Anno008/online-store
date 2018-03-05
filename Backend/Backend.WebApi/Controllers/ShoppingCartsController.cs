using System.Threading.Tasks;
using Backend.WebApi.DTOs.RequestDTOs;
using Backend.WebApi.DTOs.ResponseDTOs;
using Backend.WebApi.Models;
using Backend.WebApi.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartsController : Controller
    {
        private readonly ShoppingCartRepository repo;
        public ShoppingCartsController(ShoppingCartRepository repo) =>
            this.repo = repo;

        [HttpGet("{userId}")]
        public ShoppingCart Get(int userId) =>
            repo.Get(userId);

        [HttpPost]
        public async Task<ShoppingCartResponseDTO> Post([FromBody]ShoppingCartRequestDTO cart) =>
            new ShoppingCartResponseDTO(await repo.CreateAsync(cart.Username));

        [HttpPut]
        public async Task<ShoppingCartResponseDTO> Put([FromBody]ShoppingCartRequestDTO cart) =>
           new ShoppingCartResponseDTO(await repo.UpdateAsync(cart.Username, cart.ComponentIds));
    }
}
