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

            var component1 = new Component { Name = "Component1", Brand = brand, Type = type, Price = 40 };
            var component2 = new Component { Name = "Component2", Brand = brand, Type = type, Price = 80 };
            var component3 = new Component { Name = "Component3", Brand = brand, Type = type, Price = 120 };
            var component4 = new Component { Name = "Component4", Brand = brand, Type = type, Price = 160 };

            var user1 = new User { Username = "Test", Password = "Test" };
            var user2 = new User { Username = "Test", Password = "Test" };


            var cart = new ShoppingCart
            {
                User = user1,
                Items = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem {Component = component1 },
                    new ShoppingCartItem {Component = component4 }
                }
            };

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
        }

        [Fact]
        public async void UpdateShoppingCart_UserDoesntExist_ReturnNull()
        {
            var userName = "Something";
            var componentIds = new List<int>();

            var cart = await shoppingCartRepository.UpdateAsync(userName, componentIds);
            Assert.Null(cart);
        }

        [Fact]
        public async void UpdateShoppingCart_UserExists_ReturnCart()
        {
            var userName = "Test";

            // Component { Id = 1, Name = "Component1", Brand = brand, Type = type, Price = 40 }
            var componentIds = new List<int> { 1, 1, 1 };

            var cart = await shoppingCartRepository.UpdateAsync(userName, componentIds);

            Assert.NotNull(cart);
            Assert.Equal(3, cart.Items.Count);
            Assert.Equal(120, cart.TotalPrice);
        }
    }
}
