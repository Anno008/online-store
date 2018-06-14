using System.Collections.Generic;

namespace Backend.WebApi.Models
{
    public class ShoppingCart : BaseEntity
    {
        public User User { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public double TotalPrice { get; set; }
    }
}
