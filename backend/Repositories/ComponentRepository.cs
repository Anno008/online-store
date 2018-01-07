using System.Collections.Generic;
using System.Linq;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ComponentRepository
    {
        public List<Component> get()
        {
            using (var db = new DatabaseContext())
            {
                // Forcing eager loading on foreign tables
                db.Components.Include(c => c.Type).Load();
                db.Components.Include(c => c.Brand).Load();
                return db.Components.ToList();
            }
        }
    }
}
