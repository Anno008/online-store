using System.Collections.Generic;
using Backend.WebApi.Models;

namespace Backend.Tests.IntegrationTests.TestServerSetup
{
    public class TestDataSet
    {
        public List<User> Users { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
