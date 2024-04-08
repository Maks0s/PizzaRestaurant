using FluentAssertions;
using FluentAssertions.Execution;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.BaseImplementations;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.Helpers;
using PizzaRestaurant.Presentation.Common.DTO.PizzaDto;
using System.Net;
using System.Net.Http.Json;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas
{
    public class AddPizzaEndpointTests
        : BasePizzaApiIntegrationTest
    {
        public AddPizzaEndpointTests(PizzaApiFactory apiFactory)
            : base(apiFactory)
        {
        }

        [Fact]
        public async Task AddPizza_WithValidData_ShouldReturnCreatedPizza()
        {
            //Arrange
            var pizzaToAdd = _pizzaGenerator.GeneratePizzaRequest();

            //Act
            var creationResult =
                await _httpClient
                    .PostAsJsonAsync(PizzaApiUrl.AddPizzaEndpoint, pizzaToAdd);
            var createdPizza =
                await creationResult.Content
                    .ReadFromJsonAsync<PizzaResponse>();

            //Assert
            using var _ = new AssertionScope();

            creationResult.StatusCode.AssertStatusCode(HttpStatusCode.Created);
            creationResult.Headers.Location.Should().Be($"http://localhost/pizza/{createdPizza!.Id}");
            createdPizza.AssertPizzaResponse(pizzaToAdd);
        }

        [Fact]
        public async Task AddPizza_WithInvalidData_ShouldReturnValidationError()
        {
            //Arrange
            var invalidPizzaRequest =
                _pizzaGenerator.GenerateInvalidPizzaRequest();

            //Act
            var creationResult =
                await _httpClient
                    .PostAsJsonAsync(
                        PizzaApiUrl.AddPizzaEndpoint,
                        invalidPizzaRequest
                    );
            //Assert
            using var _ = new AssertionScope();

            creationResult.StatusCode.AssertStatusCode(HttpStatusCode.BadRequest);
        }
    }
}
