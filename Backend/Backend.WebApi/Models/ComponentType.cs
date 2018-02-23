namespace Backend.WebApi.Models
{
    public class ComponentType : BaseEntity
    {
        public string Name { get; set; }

        public override void Update(BaseEntity e) => Name = (e as ComponentType).Name;
    }
}
