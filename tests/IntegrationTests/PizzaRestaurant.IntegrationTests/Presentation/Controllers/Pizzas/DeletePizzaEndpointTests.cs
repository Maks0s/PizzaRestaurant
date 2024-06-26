﻿using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using PizzaRestaurant.Application.Common.AppErrors;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.BaseImplementations;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.Helpers;
using System.Net;
using System.Net.Http.Json;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas
{
    public class DeletePizzaEndpointTests
        : BasePizzaApiIntegrationTest
    {
        public DeletePizzaEndpointTests(PizzaApiFactory apiFactory)
            : base(apiFactory)
        {
        }

        [Fact]
        public async Task DeletePizza_WithExistingId_ShouldDeletePizza()
        {
            //Arrange
            var pizzaToDelete = _pizzaGenerator.SeededPizzas[0];

            //Act
            var deleteResult =
                await _httpClient
                    .DeleteAsync(PizzaApiUrl.DeletePizzaEndpoint + pizzaToDelete.Id);

            var getResult =
                await _httpClient
                    .GetAsync(PizzaApiUrl.GetPizzaByIdEndpoint + pizzaToDelete.Id);

            //Assert
            using var _ = new AssertionScope();

            deleteResult.StatusCode.AssertStatusCode(HttpStatusCode.NoContent);
            getResult.StatusCode.AssertStatusCode(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeletePizza_WithNonexistingId_ShouldReturnNotFound()
        {
            //Arrange
            var nonexistingId = Guid.NewGuid();
            var expectedError = Errors.Pizzas.NotFound(nonexistingId);

            //Act
            var deleteResult =
                await _httpClient
                    .DeleteAsync(PizzaApiUrl.DeletePizzaEndpoint + nonexistingId);
            var problemDetails =
                await deleteResult.Content
                    .ReadFromJsonAsync<ProblemDetails>();

            //Assert
            using var _ = new AssertionScope();

            deleteResult.StatusCode.AssertStatusCode(HttpStatusCode.NotFound);
            problemDetails!.AssertError(expectedError);
        }
    }
}
