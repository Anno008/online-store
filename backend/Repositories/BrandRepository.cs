using System.Collections.Generic;
using System.Linq;
using backend.Models;

namespace backend.Repositories
{
    public class BrandRepository
    {
        public List<Brand> get()
        {
            using (var db = new DatabaseContext())
                return db.Brands.ToList();
        }
    }
}
