using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.WebApi.Models;

namespace Backend.WebApi.Repositories
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext DatabaseContext;
        private readonly DbSet<T> _entities;

        protected BaseRepository(DatabaseContext context)
        {
            DatabaseContext = context;
            _entities = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll() =>
            _entities.AsEnumerable();

        public virtual Task<List<T>> GetAllAsync() =>
            _entities.ToListAsync();

        public virtual T Get(long id) =>
            _entities.FirstOrDefault(s => s.Id == id);

        public virtual Task<T> GetAsync(long id) =>
            _entities.FirstOrDefaultAsync(s => s.Id == id);

        public virtual T Create(T entity)
        {
            var result = _entities.Add(entity);
            DatabaseContext.SaveChanges();
            return result.Entity;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            var result = await _entities.AddAsync(entity);
            await DatabaseContext.SaveChangesAsync();
            return result.Entity;
        }

        public virtual T Update(T entity)
        {
            if (!_entities.AsNoTracking().Any(e => e.Id == entity.Id))
                return null;

            _entities.Update(entity);
            DatabaseContext.SaveChanges();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            if (!_entities.AsNoTracking().Any(e => e.Id == entity.Id))
                return null;

            _entities.Update(entity);
            await DatabaseContext.SaveChangesAsync();
            return entity;
        }

        public virtual void Delete(int id)
        {
            if (!_entities.Any(e => e.Id == id))
                return;

            _entities.Remove(_entities.Find(id));
            DatabaseContext.SaveChanges();
        }

        public virtual async void DeleteAsync(int id)
        {
            if (!_entities.Any(e => e.Id == id))
                return;

            _entities.Remove(_entities.Find(id));
            await DatabaseContext.SaveChangesAsync();
        }
    }
}
