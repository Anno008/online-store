namespace Backend.WebApi.Models
{
    public class RefreshToken : BaseEntity
    {
        public string TokenId { get; set; }
        public string Token { get; set; }

        public override void Update(BaseEntity e) =>
            throw new System.NotImplementedException();
    }
}
