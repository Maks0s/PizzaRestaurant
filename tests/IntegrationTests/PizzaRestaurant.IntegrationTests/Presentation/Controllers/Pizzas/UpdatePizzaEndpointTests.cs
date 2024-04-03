using FluentAssertions.Execution;
using PizzaRestaurant.Application.Common.AppErrors;
using Microsoft.AspNetCore.Mvc;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils;
using PizzaRestaurant.IntegrationTests.Presentation.TestUtils;
using PizzaRestaurant.Presentation.Common.DTO;
using System.Net;
using System.Net.Http.Json;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas
{
    public class UpdatePizzaEndpointTests
        : BaseApiIntegrationTests
    {
        public UpdatePizzaEndpointTests(ApiFactory apiFactory) : base(apiFactory)
        {
        }

        [Fact]
        public async Task UpdatePizza_WithValidDataAndExistingId_ShouldReturnUpdatedPizza()
        {
            //Arrange
            var pizzaToUpdate = _pizzaGenerator.SeededPizzas[0];
            var pizzaUpdateRequest = _pizzaGenerator.GeneratePizzaRequest();

            //Act
            var updateResult =
                await _httpClient
                    .PutAsJsonAsync(
                            PizzaApiUrl.UpdatePizzaEndpoint + pizzaToUpdate.Id,
                            pizzaUpdateRequest
                        );
            var updatedPizza =
                await updateResult.Content
                    .ReadFromJsonAsync<PizzaResponse>();

            //Assert
            using var _ = new AssertionScope();

            updateResult.StatusCode.AssertStatusCode(HttpStatusCode.OK);
            updatedPizza!.AssertPizzaResponse(pizzaUpdateRequest);
        }

        [Fact]
        public async Task UpdatePizza_WithNonexistingId_ShouldReturnNotFoundError()
        {
            //Arrange
            var pizzaUpdateRequest = _pizzaGenerator.GeneratePizzaRequest();
            var nonexistentId = Guid.NewGuid();
            var expectedError = Errors.Pizzas.NotFound(nonexistentId);

            //Act
            var updateResult =
                await _httpClient
                    .PutAsJsonAsync(
                            PizzaApiUrl.UpdatePizzaEndpoint + nonexistentId,
                            pizzaUpdateRequest
                        );
            var problemDetails =
                await updateResult.Content
                    .ReadFromJsonAsync<ProblemDetails>();

            //Assert
            using var _ = new AssertionScope();

            updateResult.StatusCode.AssertStatusCode(HttpStatusCode.NotFound);
            problemDetails!.AssertError(expectedError);
        }

        [Fact]
        public async Task UpdatePizza_WithInvalidDataAndExistingId_ShouldReturnBadRequest()
        {
            //Arrange
            var invalidPizzaRequest =
                _pizzaGenerator.GenerateInvalidPizzaRequest();
            var pizzaToUpdate =
                _pizzaGenerator.SeededPizzas[0];

            //Act
            var updateResult =
                await _httpClient
                    .PutAsJsonAsync(
                            PizzaApiUrl.UpdatePizzaEndpoint + pizzaToUpdate.Id,
                            invalidPizzaRequest
                        );

            //Assert
            using var _ = new AssertionScope();

            updateResult.StatusCode.AssertStatusCode(HttpStatusCode.BadRequest);
        }
    }
}
