using System.Collections.Generic;
using System.Linq;
using Backend.Models;

namespace Backend.Repositories
{
    public class ComponentTypeRepository
    {
        private readonly DatabaseContext databaseContext;

        public ComponentTypeRepository(DatabaseContext databaseContext) =>
            this.databaseContext = databaseContext;

        public List<ComponentType> Get() =>
            databaseContext.ComponentTypes.ToList();

        public ComponentType Get(int id) =>
            databaseContext.ComponentTypes.Find(id);

        public ComponentType Create(ComponentType type)
        {
            var result = databaseContext.ComponentTypes.Add(type);
            databaseContext.SaveChanges();
            return result.Entity;
        }

        public ComponentType Update(ComponentType type)
        {
            var updated = databaseContext.ComponentTypes.Update(type);
            databaseContext.SaveChanges();
            return updated.Entity;
        }

        public void Delete(int id)
        {
            databaseContext.ComponentTypes.Remove(Get(id));
            databaseContext.SaveChanges();
        }
    }
}
