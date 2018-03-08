using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.WebApi.Repositories
{
    public class ShoppingCartRepository : BaseRepository<ShoppingCart>
    {
        public ShoppingCartRepository(DatabaseContext context) : base(context) { }

        public override Task<ShoppingCart> GetAsync(long id)
        {
            // Forcing eager loading on foreign tables
            databaseContext.ShoppingCarts
                .Include(c => c.User)
                .Include(c => c.Items)
                .ThenInclude(c => c.Component)
                .ThenInclude(c => c.Brand)
                .Include(c => c.Items)
                .ThenInclude(c => c.Component)
                .ThenInclude(c => c.Type)
                .Load();

            return databaseContext.ShoppingCarts.FirstOrDefaultAsync(x => x.User.Id == id);
        }

        public async Task<ShoppingCart> CreateAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var user = databaseContext.Users.FirstOrDefault(x => x.Username == username);

            var result = databaseContext.ShoppingCarts.Add(new ShoppingCart() { User = user });
            await databaseContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ShoppingCart> UpdateAsync(string username, List<int> componentIds)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var user = databaseContext.Users.FirstOrDefault(x => x.Username == username);
            var usersShoppingCart = databaseContext.ShoppingCarts.FirstOrDefault(x => x.User.Id == user.Id);

            if (usersShoppingCart == null || user == null)
                return null;

            var items = componentIds.Select(i => new ShoppingCartItem
            {
                Component = databaseContext.Components.FirstOrDefault(c => c.Id == i)
            }).ToList();

            usersShoppingCart.Update(items);
            await databaseContext.SaveChangesAsync();
            return usersShoppingCart;
        }

        // Honestly I can just call update and it will do the same thing plus return the 
        // updated shopping cart
        public void Delete(string userName, long shoppingCartItemId)
        {
            var cart = databaseContext.ShoppingCarts.FirstOrDefault(x => x.User.Username == userName);
            if (cart == null)
                return;

            var cartItem = cart.Items.FirstOrDefault(c => c.Id == shoppingCartItemId);

            if (cartItem == null)
                return;


            cart.Items.Remove(cartItem);
            databaseContext.SaveChanges();
        }
    }
}
