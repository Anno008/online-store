using Backend.Models;

namespace Backend.Repositories
{
    public class ComponentTypeRepository : BaseRepository<ComponentType>
    {
        public ComponentTypeRepository(DatabaseContext databaseContext) : base(databaseContext){ }
    }
}
