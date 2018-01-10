using System.Collections.Generic;
using System.Linq;
using Backend.Models;

namespace Backend.Repositories
{
    public class BrandRepository
    {
        private readonly DatabaseContext databaseContext;

        public BrandRepository(DatabaseContext databaseContext) =>
            this.databaseContext = databaseContext;

        public List<Brand> Get() =>
            databaseContext.Brands.ToList();

        public Brand Get(int id) =>
            databaseContext.Brands.Find(id);

        public Brand Create(Brand brand)
        {
            var result = databaseContext.Brands.Add(brand);
            databaseContext.SaveChanges();
            return result.Entity;
        }

        public Brand Update(Brand brand)
        {
            var updated = databaseContext.Brands.Update(brand);
            databaseContext.SaveChanges();
            return updated.Entity;
        }

        public void Delete(int id)
        {
            databaseContext.Brands.Remove(Get(id));
            databaseContext.SaveChanges();
        }
    }
}
