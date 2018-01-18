using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext databaseContext;
        private readonly DbSet<T> entities;
        string errorMessage = string.Empty;

        public BaseRepository(DatabaseContext context)
        {
            this.databaseContext = context;
            entities = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll() =>
            entities.AsEnumerable();

        public virtual async Task<List<T>> GetAllAsync() =>
            await entities.ToListAsync();

        public virtual T Get(long id) => 
            entities.SingleOrDefault(s => s.Id == id);

        public virtual async Task<T> GetAsync(long id) =>
            await entities.SingleOrDefaultAsync(s => s.Id == id);

        public virtual T Create(T entity)
        {
            var result = entities.Add(entity);
            databaseContext.SaveChanges();
            return result.Entity;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            var result = await entities.AddAsync(entity);
            await databaseContext.SaveChangesAsync();
            return result.Entity;
        }

        public virtual T Update(T entity)
        {
            var result = databaseContext.Update(entity);
            databaseContext.SaveChanges();
            return result.Entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var result = databaseContext.Update(entity);
            await databaseContext.SaveChangesAsync();
            return result.Entity;
        }

        public virtual void Delete(T entity)
        {
            entities.Remove(entity);
            databaseContext.SaveChanges();
        }

        public virtual async void DeleteAsync(T entity)
        {
            entities.Remove(entity);
            await databaseContext.SaveChangesAsync();
        }
    }
}
