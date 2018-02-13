using System.Collections.Generic;
using System.Net.Http;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Backend.WebApi.Models;

namespace Backend.Tests.IntegrationTests.TestServerSetup
{
    public class WebServer
    {
        protected readonly HttpClient client;

        // Initializing our TestServer
        public WebServer()
        {
            // Arrange
            var server = new TestServer(new WebHostBuilder()
                .ConfigureServices(services => services.AddSingleton(TestData()))
                .UseStartup<TestStartup>());

            client = server.CreateClient();
        }

        /// <summary>
        /// This is the initial data that will be
        /// used to populate our in memory database
        /// </summary>
        /// <returns></returns>
        private TestDataSet TestData()
        {
            var testData = new TestDataSet
            {
                Users = new List<User>()
            };

            // Users
            testData.Users.Add(new User { Username = "User", Password = "User", Role = Role.User });
            

            return testData;
        }

    }
}
