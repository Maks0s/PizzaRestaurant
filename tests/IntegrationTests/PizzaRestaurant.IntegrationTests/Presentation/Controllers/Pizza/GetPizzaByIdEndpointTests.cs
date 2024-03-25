using FluentAssertions;
using FluentAssertions.Execution;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizza.TestUtils;
using PizzaRestaurant.IntegrationTests.Presentation.TestUtils;
using PizzaRestaurant.Presentation.Common.DTO;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizza
{
    public class GetPizzaByIdEndpointTests : BaseApiIntegrationTests
    {
        public GetPizzaByIdEndpointTests(
                ApiFactory apiFactory,
                ITestOutputHelper testOutputHelper
            )
            : base(apiFactory, testOutputHelper)
        {
        }

        [Fact]
        public async Task GetPizzaById_WithAddedPizza_ShouldReturnExistingPizza()
        {
            //Arrange
            var pizzaToAdd = _pizzaGenerator.Generate();
            
            var postResult =
                await _httpClient
                    .PostAsJsonAsync("/pizza/add", pizzaToAdd);

            var addedPizza =
                await postResult.Content
                    .ReadFromJsonAsync<PizzaResponse>();

            //Act
            var getResult =
                await _httpClient.GetAsync($"/pizza/{addedPizza!.Id}");
            var pizzaResponse =
                await getResult.Content
                    .ReadFromJsonAsync<PizzaResponse>();

            //Assert
            using var _ = new AssertionScope();

            getResult.StatusCode.Should().Be(HttpStatusCode.OK);
            pizzaResponse!.AssertPizzaResponse(pizzaToAdd);
        }
    }
}
