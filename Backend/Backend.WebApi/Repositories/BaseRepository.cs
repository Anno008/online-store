using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.WebApi.Models;

namespace Backend.WebApi.Repositories
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext databaseContext;
        private readonly DbSet<T> entities;

        protected BaseRepository(DatabaseContext context)
        {
            databaseContext = context;
            entities = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll() =>
            entities.AsEnumerable();

        public virtual Task<List<T>> GetAllAsync() =>
            entities.ToListAsync();

        public virtual T Get(long id) =>
            entities.SingleOrDefault(s => s.Id == id);

        public virtual Task<T> GetAsync(long id) =>
            entities.SingleOrDefaultAsync(s => s.Id == id);

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
            var existingEntity = entities.FirstOrDefault(e => e.Id == entity.Id);

            if (existingEntity == null)
                return null;

            existingEntity.Update(entity);
            databaseContext.SaveChanges();
            return existingEntity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var existingEntity = entities.FirstOrDefault(e => e.Id == entity.Id);

            if (existingEntity == null)
                return null;

            existingEntity.Update(entity);
            await databaseContext.SaveChangesAsync();
            return existingEntity;
        }

        public virtual void Delete(int id)
        {
            if (!entities.Any(e => e.Id == id))
                return;

            entities.Remove(entities.Find(id));
            databaseContext.SaveChanges();
        }

        public virtual async void DeleteAsync(int id)
        {
            if (!entities.Any(e => e.Id == id))
                return;

            entities.Remove(entities.Find(id));
            await databaseContext.SaveChangesAsync();
        }
    }
}
