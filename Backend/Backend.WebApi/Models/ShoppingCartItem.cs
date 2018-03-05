using System;

namespace Backend.WebApi.Models
{
    public class ShoppingCartItem : BaseEntity
    {
        public Component Compopnent { get; set; }

        public override void Update(BaseEntity e)
        {
            throw new NotImplementedException();
        }
    }
}
