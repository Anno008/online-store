﻿using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Backend.WebApi.Models;
using System.Threading.Tasks;

namespace Backend.WebApi.Repositories
{
    public class ComponentRepository : BaseRepository<Component>
    {
        public ComponentRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        public (List<Component> components, int totalPages, int totalItems, int itemsOnPage, int currentPage) GetAll(
            string name, int[] brandIds, int typeId, int currentPage, int pageSize)
        {
            // Forcing eager loading on foreign tables
            databaseContext.Components.Include(c => c.Type).Load();
            databaseContext.Components.Include(c => c.Brand).Load();

            var all = databaseContext.Components.AsQueryable();
            var totalItems = all.Count();

            // filtering by name
            if(!string.IsNullOrWhiteSpace(name))
                all = all.Where(x => x.Name.Contains(name));

            // filtering by brands
            if (brandIds?.Length > 0)
                all = all.Where(x => brandIds.Any(y => y == x.Brand.Id));

            // filtering by component type
            if (typeId != 0)
                all = all.Where(x => x.Type.Id == typeId);

            // Get only the required page
            if (pageSize != 0)
            {
                all = all
                       .Skip((currentPage - 1) * pageSize)
                       .Take(pageSize);
            }

            return (
                components: all.ToList(),
                totalPages: pageSize == 0 ? 1 : CalculateTotalPages(totalItems, pageSize),
                totalItems: totalItems,
                itemsOnPage: all.Count(),
                currentPage: currentPage == 0 ? 1 : currentPage
                );
        }

        private int CalculateTotalPages(int totalItems, int itemsPerPage)
        {
            var totalPages = totalItems / itemsPerPage;

            if (totalItems % itemsPerPage != 0)
                totalPages++;

            return totalPages;
        }

        public override Component Create(Component entity)
        {
            var brand = databaseContext.Brands.FirstOrDefault(b => b.Id == entity.Brand.Id);
            var componentType = databaseContext.ComponentTypes.FirstOrDefault(c => c.Id == entity.Brand.Id);

            if (brand == null || componentType == null)
                return null;

            entity.Brand = brand;
            entity.Type = componentType;

            return base.Create(entity);
        }

        public override Task<Component> CreateAsync(Component entity)
        {
            var brand = databaseContext.Brands.FirstOrDefault(b => b.Id == entity.Brand.Id);
            var componentType = databaseContext.ComponentTypes.FirstOrDefault(c => c.Id == entity.Brand.Id);

            if (brand == null || componentType == null)
                return Task.FromResult<Component>(null);

            entity.Brand = brand;
            entity.Type = componentType;

            return base.CreateAsync(entity);
        }

        public override Task<Component> GetAsync(long id)
        {
            // Forcing eager loading on foreign tables
            databaseContext.Components.Include(c => c.Type).Load();
            databaseContext.Components.Include(c => c.Brand).Load();
            return base.GetAsync(id);
        }

        public override Component Get(long id)
        {
            // Forcing eager loading on foreign tables
            databaseContext.Components.Include(c => c.Type).Load();
            databaseContext.Components.Include(c => c.Brand).Load();
            return base.Get(id);
        }

        public override Component Update(Component entity)
        {
            var brand = databaseContext.Brands.FirstOrDefault(b => b.Id == entity.Brand.Id);
            var componentType = databaseContext.ComponentTypes.FirstOrDefault(c => c.Id == entity.Brand.Id);

            if (brand == null || componentType == null)
                return null;

            entity.Brand = brand;
            entity.Type = componentType;

            return base.Update(entity);
        }

        public override Task<Component> UpdateAsync(Component entity)
        {
            var brand = databaseContext.Brands.FirstOrDefault(b => b.Id == entity.Brand.Id);
            var componentType = databaseContext.ComponentTypes.FirstOrDefault(c => c.Id == entity.Brand.Id);

            if (brand == null || componentType == null)
                return Task.FromResult<Component>(null);

            entity.Brand = brand;
            entity.Type = componentType;

            return base.UpdateAsync(entity);
        }
    }
}
