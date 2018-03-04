using System.Collections.Generic;

namespace Backend.WebApi.DTOs.RequestDTOs
{
    public class ShoppingCartRequestDTO
    {
        public string Username { get; set; }
        public List<int> ComponentIds{ get; set; }
    }
}
