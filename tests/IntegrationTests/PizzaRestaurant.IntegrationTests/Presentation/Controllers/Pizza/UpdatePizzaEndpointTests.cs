using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizza.TestUtils;
using PizzaRestaurant.IntegrationTests.Presentation.TestUtils;
using PizzaRestaurant.Presentation.Common.DTO;
using System.Net;
using System.Net.Http.Json;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizza
{
    public class UpdatePizzaEndpointTests
        : BaseApiIntegrationTests
    {
        public UpdatePizzaEndpointTests(ApiFactory apiFactory) : base(apiFactory)
        {
        }

        [Fact]
        public async Task UpdatePizza_WithValidData_ShouldReturnUpdatedPizza()
        {
            //Arrange
            var pizzaToAdd = _pizzaGenerator.Generate();
            var addResult =
                await _httpClient
                    .PostAsJsonAsync("/pizza/add", pizzaToAdd);
            var addedPizza =
                await addResult.Content
                    .ReadFromJsonAsync<PizzaResponse>();

            //Act
            var pizzaToUpdate = _pizzaGenerator.Generate();
            var updateResult =
                await _httpClient
                    .PutAsJsonAsync($"/pizza/update/{addedPizza!.Id}", pizzaToUpdate);
            var updatedPizza =
                await updateResult.Content
                    .ReadFromJsonAsync<PizzaResponse>();

            //Assert
            using var _ = new AssertionScope();

            updateResult.StatusCode.AssertStatusCode(HttpStatusCode.OK);
            updatedPizza!.AssertPizzaResponse(pizzaToUpdate);
        }

        [Fact]
        public async Task UpdatePizza_UpdatingNonexistentPizza_ShouldReturnNotFoundError()
        {
            //Arrange
            var pizzaToUpdate = _pizzaGenerator.Generate();
            var nonexistentId = Guid.NewGuid();
            var expectedError = Application.Common.AppErrors.Errors.Pizzas.NotFound(nonexistentId);

            //Act
            var updateResult =
                await _httpClient
                    .PutAsJsonAsync($"/pizza/update/{nonexistentId}", pizzaToUpdate);
            var problemDetails =
                await updateResult.Content
                    .ReadFromJsonAsync<ProblemDetails>();

            //Assert
            using var _ = new AssertionScope();

            updateResult.StatusCode.AssertStatusCode(HttpStatusCode.NotFound);
            problemDetails!.AssertError(expectedError);

        }
    }
}
