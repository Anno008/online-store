using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Backend.WebApi.Models;

namespace Backend.WebApi.Repositories
{
    public class ComponentRepository : BaseRepository<Component>
    {

        public ComponentRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        public override IEnumerable<Component> GetAll()
        {
            // Forcing eager loading on foreign tables
            databaseContext.Components.Include(c => c.Type).Load();
            databaseContext.Components.Include(c => c.Brand).Load();
            return base.GetAll();
        }

        public override Component Get(long id)
        {
            // Forcing eager loading on foreign tables
            databaseContext.Components.Include(c => c.Type).Load();
            databaseContext.Components.Include(c => c.Brand).Load();
            return base.Get(id);
        }
    }
}
