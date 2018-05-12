using System.Collections.Generic;
using Backend.WebApi.Models;
using Backend.WebApi.Repositories;
using Xunit;

namespace Backend.Tests.UnitTests.Repositories
{
    public class ShoppingCartRepositoryUnitTests : RepositoryUnitTestsBase
    {
        private readonly ShoppingCartRepository shoppingCartRepository;

        public ShoppingCartRepositoryUnitTests()
        {
            shoppingCartRepository = new ShoppingCartRepository(dbContext);
            var brand = new Brand { Name = "Intel" };
            var type = new ComponentType { Name = "CPU" };

            var component1 = new Component { Id = 1, Name = "Component1", Brand = brand, ComponentType = type, Price = 40 };
            var component2 = new Component { Id = 2, Name = "Component2", Brand = brand, ComponentType = type, Price = 80 };
            var component3 = new Component { Id = 3, Name = "Component3", Brand = brand, ComponentType = type, Price = 120 };
            var component4 = new Component { Id = 4, Name = "Component4", Brand = brand, ComponentType = type, Price = 160 };

            var user1 = new User { Username = "Test1", Password = "Test1" };
            var user2 = new User { Username = "Test2", Password = "Test2" };

            var cart = new ShoppingCart
            {
                User = user1,
                Items = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem { Component = component1 },
                    new ShoppingCartItem {  Component = component4 }
                }
            };

            cart.Update(cart);
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);

            dbContext.Brands.Add(brand);
            dbContext.ComponentTypes.Add(type);

            dbContext.Components.Add(component1);
            dbContext.Components.Add(component2);
            dbContext.Components.Add(component3);
            dbContext.Components.Add(component4);

            dbContext.ShoppingCarts.Add(cart);
            dbContext.SaveChanges();
        }

        [Fact]
        public async void GetAsyncShoppingCartByUserId_UserDoesntExist_ReturnNull()
        {
            var userId = 5;

            var cart = await shoppingCartRepository.GetAsync(userId);
            Assert.Null(cart);
        }

        [Fact]
        public async void GetAsyncShoppingCartByUserId_UserExist_ReturnShoppingCart()
        {
            var user = new User { Id = 1, Username = "Test", Password = "Test" };

            var cart = await shoppingCartRepository.GetAsync(user.Id);

            Assert.NotNull(cart);
            Assert.Equal(2, cart.Items.Count);
            Assert.Equal(200, cart.TotalPrice);
        }

        [Fact]
        public void AddItem_ReturnCart_WithNewItem()
        {
            var componentId = 3;
            var username = "Test1";


            var cart = shoppingCartRepository.AddItem(username, componentId);

            Assert.NotNull(cart);
            Assert.Equal(3, cart.Items.Count);
            Assert.Equal(320, cart.TotalPrice);
        }

        [Fact]
        public void RemoveItem_ReturnCart_WithOneItemLess()
        {
            var shoppingCartItemId = 2;
            var username = "Test1";

            var cart = shoppingCartRepository.RemoveItem(username, shoppingCartItemId);

            Assert.NotNull(cart);
            Assert.Single(cart.Items);
            Assert.Equal(40, cart.TotalPrice);
        }

        [Fact]
        public void RemoveItem_ItemDoesntExist_ReturnCart()
        {
            var shoppingCartItemId = 55;
            var username = "Test1";

            var cart = shoppingCartRepository.RemoveItem(username, shoppingCartItemId);

            Assert.NotNull(cart);
            Assert.Equal(2, cart.Items.Count);
            Assert.Equal(200, cart.TotalPrice);
        }
    }
}
