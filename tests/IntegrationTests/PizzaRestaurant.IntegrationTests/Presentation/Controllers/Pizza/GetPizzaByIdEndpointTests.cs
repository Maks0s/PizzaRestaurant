using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using PizzaRestaurant.Application.Common.AppErrors;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizza.TestUtils;
using PizzaRestaurant.IntegrationTests.Presentation.TestUtils;
using PizzaRestaurant.Presentation.Common.DTO;
using System.Net;
using System.Net.Http.Json;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizza
{
    public class GetPizzaByIdEndpointTests : BaseApiIntegrationTests
    {
        public GetPizzaByIdEndpointTests(
                ApiFactory apiFactory
            )
            : base(apiFactory)
        {
        }

        [Fact]
        public async Task GetPizzaById_WithexistingId_ShouldReturnExistingPizza()
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
                await _httpClient
                    .GetAsync($"/pizza/{addedPizza!.Id}");
            var pizzaResponse =
                await getResult.Content
                    .ReadFromJsonAsync<PizzaResponse>();

            //Assert
            using var _ = new AssertionScope();

            getResult.StatusCode.AssertStatusCode(HttpStatusCode.OK);
            pizzaResponse!.AssertPizzaResponse(pizzaToAdd);
        }

        [Fact]
        public async Task GetPizzaById_WithNonexistingId_ShouldReturnNotFoundError()
        {
            //Arrange
            var nonexistentId = Guid.NewGuid();
            var expectedError = Errors.Pizzas.NotFound(nonexistentId);

            //Act
            var getResult =
                await _httpClient
                    .GetAsync($"/pizza/{nonexistentId}");
            var error =
                await getResult.Content
                    .ReadFromJsonAsync<ProblemDetails>();

            //Assert
            using var _ = new AssertionScope();

            getResult.StatusCode.AssertStatusCode(HttpStatusCode.NotFound);
            error!.AssertError(expectedError);
        }
    }
}
