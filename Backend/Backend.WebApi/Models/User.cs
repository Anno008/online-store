namespace Backend.WebApi.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public override void Update(BaseEntity e) =>
            throw new System.NotImplementedException();
    }
}
