using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class ComponentRepository
    {
        private readonly DatabaseContext databaseContext;

        public ComponentRepository(DatabaseContext databaseContext) =>
            this.databaseContext = databaseContext;

        public List<Component> Get()
        {
            // Forcing eager loading on foreign tables
            databaseContext.Components.Include(c => c.Type).Load();
            databaseContext.Components.Include(c => c.Brand).Load();
            return databaseContext.Components.ToList();
        }

        public Component Get(int id)
        {
            // Forcing eager loading on foreign tables
            databaseContext.Components.Include(c => c.Type).Load();
            databaseContext.Components.Include(c => c.Brand).Load();
            return databaseContext.Components.Find(id);
        }

        public Component Create(Component component)
        {
            var result = databaseContext.Components.Add(component);
            databaseContext.SaveChanges();
            return result.Entity;
        }

        public Component Update(Component component)
        {
            var updated = databaseContext.Components.Update(component);
            databaseContext.SaveChanges();
            return updated.Entity;
        }

        public void Delete(int id)
        {
            databaseContext.Components.Remove(Get(id));
            databaseContext.SaveChanges();
        }
    }
}
