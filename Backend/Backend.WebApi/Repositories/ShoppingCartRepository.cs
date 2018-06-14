﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.WebApi.Repositories
{
    public class ShoppingCartRepository : BaseRepository<ShoppingCart>
    {
        public ShoppingCartRepository(DatabaseContext context) : base(context) { }

        public Task<ShoppingCart> GetAsync(string username)
        {
            // Forcing eager loading on foreign tables
            databaseContext.ShoppingCarts
                .Include(c => c.User)
                .Include(c => c.Items)
                .ThenInclude(c => c.Component)
                .ThenInclude(c => c.Brand)
                .Include(c => c.Items)
                .ThenInclude(c => c.Component)
                .ThenInclude(c => c.ComponentType)
                .Load();

            return databaseContext.ShoppingCarts.FirstOrDefaultAsync(x => x.User.Username == username);
        }

        // Consider changing this to void, since the returned data doesn't have all fields mapped,
        // right now after adding/deleting I am calling get anyway. Or mapping the data
        public ShoppingCart AddItem(string username, int componentId)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var user = databaseContext.Users.FirstOrDefault(x => x.Username == username);

            // User doesn't exist
            if (user == null)
                return null;

            var component = databaseContext.Components.FirstOrDefault(x => x.Id == componentId);

            // Component doesn't exist anymore
            if (component == null)
                return null;

            var usersShoppingCart = databaseContext.ShoppingCarts.FirstOrDefault(x => x.User.Id == user.Id);
            databaseContext.ShoppingCarts
                .Include(c => c.Items)
                .ThenInclude(c => c.Component)
                .Load();

            // If the user doesn't already have a shopping cart, create one
            if (usersShoppingCart == null)
            {
                usersShoppingCart = new ShoppingCart { User = user, Items = new List<ShoppingCartItem>() };
                databaseContext.ShoppingCarts.Add(usersShoppingCart);
            }

            usersShoppingCart.Items.Add(new ShoppingCartItem { Component = component });

            databaseContext.SaveChanges();
            return usersShoppingCart;
        }

        // Consider changing this to void, since the returned data doesn't have all fields mapped,
        // right now after adding/deleting I am calling get anyway. Or mapping the data
        public ShoppingCart RemoveItem(string userName, long shoppingCartItemId)
        {
            var cart = databaseContext.ShoppingCarts.FirstOrDefault(x => x.User.Username == userName);
            databaseContext.ShoppingCarts
                .Include(c => c.User)
                .Include(c => c.Items)
                .ThenInclude(c => c.Component)
                .Load();

            if (cart == null)
                return null;

            var cartItem = cart.Items.FirstOrDefault(c => c.Id == shoppingCartItemId);

            if (cartItem == null)
                return cart;

            cart.Items.Remove(cartItem);
            databaseContext.Remove(cartItem);

            databaseContext.SaveChanges();
            return cart;
        }
    }
}
