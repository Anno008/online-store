namespace Backend.WebApi.DTOs
{
    public class ComponentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BrandDto Brand { get; set; }
        public ComponentTypeDto ComponentType { get; set; }
        public double Price { get; set; }
    }
}
