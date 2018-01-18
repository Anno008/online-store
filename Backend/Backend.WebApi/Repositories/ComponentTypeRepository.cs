using Backend.WebApi.Models;

namespace Backend.WebApi.Repositories
{
    public class ComponentTypeRepository : BaseRepository<ComponentType>
    {
        public ComponentTypeRepository(DatabaseContext databaseContext) : base(databaseContext){ }
    }
}
