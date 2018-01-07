using System.Collections.Generic;
using System.Linq;
using backend.Models;

namespace backend.Repositories
{
    public class BrandRepository
    {
        public List<Brand> Get()
        {
            using (var db = new DatabaseContext())
                return db.Brands.ToList();
        }

        public Brand Get(int id)
        {
            using (var db = new DatabaseContext())
                return db.Brands.Find(id);
        }

        public Brand Create(Brand brand)
        {
            using (var db = new DatabaseContext())
            {
                var result = db.Brands.Add(brand);
                db.SaveChanges();
                return result.Entity;
            }
        }

        public Brand Update(Brand brand)
        {
            using (var db = new DatabaseContext())
            {
                var updated = db.Brands.Update(brand);
                db.SaveChanges();
                return updated.Entity;
            }
        }

        public void Delete(int id)
        {
            using (var db = new DatabaseContext())
            {
                db.Brands.Remove(Get(id));
                db.SaveChanges();
            }
        }
    }
}
