using FluentAssertions;
using FluentAssertions.Execution;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizza.TestUtils;
using PizzaRestaurant.IntegrationTests.Presentation.TestUtils;
using PizzaRestaurant.Presentation.Common.DTO;
using System.Net;
using System.Net.Http.Json;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizza
{
    public class AddPizzaEndpointTests
        : BaseApiIntegrationTests
    {
        public AddPizzaEndpointTests(ApiFactory apiFactory)
            : base(apiFactory)
        {
        }

        [Fact]
        public async Task AddPizza_WithValidData_ShouldReturnCreatedPizza()
        {
            //Arrange
            var pizzaToAdd = _pizzaGenerator.Generate();

            //Act
            var creationResult =
                await _httpClient
                    .PostAsJsonAsync("/pizza/add", pizzaToAdd);
            var createdPizza =
                await creationResult.Content
                    .ReadFromJsonAsync<PizzaResponse>();

            //Assert
            using var _ = new AssertionScope();

            creationResult.StatusCode.AssertStatusCode(HttpStatusCode.Created);
            creationResult.Headers.Location.Should().Be($"http://localhost/pizza/{createdPizza!.Id}");
            createdPizza.AssertPizzaResponse(pizzaToAdd);
        }
    }
}
