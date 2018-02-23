namespace Backend.WebApi.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public abstract void Update(BaseEntity e);
    }
}
