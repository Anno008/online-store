namespace backend.Models
{
    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Brand Brand { get; set; }
        public ComponentType Type { get; set; }
        public double Price { get; set; }
    }
}
