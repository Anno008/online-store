using System.Collections.Generic;
using System.Linq;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ComponentRepository
    {
        public List<Component> Get()
        {
            using (var db = new DatabaseContext())
            {
                // Forcing eager loading on foreign tables
                db.Components.Include(c => c.Type).Load();
                db.Components.Include(c => c.Brand).Load();
                return db.Components.ToList();
            }
        }

        public Component Get(int id)
        {
            using (var db = new DatabaseContext())
            {
                // Forcing eager loading on foreign tables
                db.Components.Include(c => c.Type).Load();
                db.Components.Include(c => c.Brand).Load();
                return db.Components.Find(id);
            }
        }

        public Component Create(Component component)
        {
            using (var db = new DatabaseContext())
            {
                var result = db.Components.Add(component);
                db.SaveChanges();
                return result.Entity;
            }
        }

        public Component Update(Component component)
        {
            using (var db = new DatabaseContext())
            {
                var updated = db.Components.Update(component);
                db.SaveChanges();
                return updated.Entity;
            }
        }

        public void Delete(int id)
        {
            using (var db = new DatabaseContext())
            {
                db.Components.Remove(Get(id));
                db.SaveChanges();
            }
        }
    }
}
