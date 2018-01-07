using System.Collections.Generic;
using System.Linq;
using backend.Models;

namespace backend.Repositories
{
    public class ComponentTypeRepository
    {
        public List<ComponentType> Get()
        {
            using (var db = new DatabaseContext())
                return db.ComponentTypes.ToList();
        }

        public ComponentType Get(int id)
        {
            using (var db = new DatabaseContext())
                return db.ComponentTypes.Find(id);
        }

        public ComponentType Create(ComponentType type)
        {
            using (var db = new DatabaseContext())
            {
                var result = db.ComponentTypes.Add(type);
                db.SaveChanges();
                return result.Entity;
            }
        }

        public ComponentType Update(ComponentType type)
        {
            using (var db = new DatabaseContext())
            {
                var updated = db.ComponentTypes.Update(type);
                db.SaveChanges();
                return updated.Entity;
            }
        }

        public void Delete(int id)
        {
            using (var db = new DatabaseContext())
            {
                db.ComponentTypes.Remove(Get(id));
                db.SaveChanges();
            }
        }
    }
}
