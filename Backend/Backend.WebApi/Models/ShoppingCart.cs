using System.Collections.Generic;
using System.Linq;

namespace Backend.WebApi.Models
{
    public class ShoppingCart : BaseEntity
    {
        public User User { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public double TotalPrice { get; set; }

        public override void Update(BaseEntity e)
        {
            var shoppingCart = e as ShoppingCart;
            User = shoppingCart.User;
            Items = shoppingCart.Items;
            TotalPrice = shoppingCart.Items?.Sum(x => x.Component.Price) ?? 0;
        }
    }
}
