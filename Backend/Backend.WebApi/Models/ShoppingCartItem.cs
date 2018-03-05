using System;

namespace Backend.WebApi.Models
{
    public class ShoppingCartItem : BaseEntity
    {
        public Component Component { get; set; }

        public override void Update(BaseEntity e)
        {
            throw new NotImplementedException();
        }
    }
}
