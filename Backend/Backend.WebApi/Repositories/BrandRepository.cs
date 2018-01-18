using Backend.Models;

namespace Backend.Repositories
{
    public class BrandRepository : BaseRepository<Brand>
    {
        public BrandRepository(DatabaseContext databaseContext) : base(databaseContext) { }
    }
}
