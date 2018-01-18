using Backend.WebApi.Models;

namespace Backend.WebApi.Repositories
{
    public class BrandRepository : BaseRepository<Brand>
    {
        public BrandRepository(DatabaseContext databaseContext) : base(databaseContext) { }
    }
}
