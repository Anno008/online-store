using System.Threading.Tasks;

using Backend.WebApi.DTOs.ResponseDTOs;
using Backend.WebApi.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartsController : Controller
    {
        private readonly ShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartsController(ShoppingCartRepository shoppingCartRepository) =>
            _shoppingCartRepository = shoppingCartRepository;

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ShoppingCartResponseDTO> Get()
        {
            var username = User.FindFirst("username")?.Value;

            return new ShoppingCartResponseDTO(await _shoppingCartRepository.GetAsync(username));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ShoppingCartResponseDTO AddItemToShoppingCart([FromBody] int componentId)
        {
            var username = User.FindFirst("username")?.Value;

            return new ShoppingCartResponseDTO(_shoppingCartRepository.AddItem(username, componentId));
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ShoppingCartResponseDTO DeleteItemFromShoppingCart([FromBody] int componentId)
        {
            var username = User.FindFirst("username")?.Value;

            return new ShoppingCartResponseDTO(_shoppingCartRepository.RemoveItem(username, componentId));
        }
    }
}
