namespace Backend.WebApi.Models
{
    public class Component : BaseEntity
    {
        public string Name { get; set; }
        public Brand Brand { get; set; }
        public ComponentType ComponentType { get; set; }
        public double Price { get; set; }

        public override void Update(BaseEntity e)
        {
            var updateComponent = e as Component;
            Name = updateComponent.Name;
            Brand = updateComponent.Brand;
            ComponentType = updateComponent.ComponentType;
            Price = updateComponent.Price;
        }
    }
}
