namespace Backend.WebApi.Models
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }

        public override void Update(BaseEntity e) => Name = (e as Brand).Name;
    }
}
