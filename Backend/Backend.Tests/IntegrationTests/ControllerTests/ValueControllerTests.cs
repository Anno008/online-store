using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using Backend.Tests.IntegrationTests.TestServerSetup;

namespace Backend.Tests.IntegrationTests.ControllerTests
{
    public class ValueControllerTests : WebServer
    {
        [Fact]
        public async Task RequestWithoutAuthorizationToken_ShouldReturnUnauthorized401()
        {
            // Act
            var response = await client.GetAsync("api/values");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal("Unauthorized", response.ReasonPhrase);
        }

        [Fact]
        public async Task RequestWithAuthorizationToken_ShouldBeSuccessful()
        {
            // Act
            // Username: User123, Password: User123, Role: User
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJUZXN0MTIzIiwiaXNzIjoiQmFja2VuZCIsImF1ZCI6IlVzZXJzIiwicm9sZXMiOlsiVXNlciJdfQ.xr0A1PWVK_gtssZ_ZDkhFPRhSbIw1JURo5kNNb-P0fY";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("api/values");
           
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal("Super secret content, I hope you've got clearance for this...", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task RequestWithAuthorizationToken_InvalidRoles_ShouldReturnForbidden403()
        {
            // Act
            // Username: Admin123, Password: Admin123, Role: Admin
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJBZG1pbjEyMyIsImlzcyI6IkJhY2tlbmQiLCJhdWQiOiJVc2VycyIsInJvbGVzIjpbIkFkbWluIl19.aXFYzlaqPr24a-yB8rgCDHX7hv1gZySWsH3RBY883V0";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("api/values");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal("Forbidden", response.ReasonPhrase);
        }
    }
}
