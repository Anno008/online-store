namespace Backend.WebApi.DTOs
{
    public class ComponentDto : BaseDto
    {
        public string Name { get; set; }
        public BrandDto Brand { get; set; }
        public ComponentTypeDto ComponentType { get; set; }
        public double Price { get; set; }
    }
}
