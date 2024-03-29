﻿using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using PizzaRestaurant.Application.Common.AppErrors;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizza.TestUtils;
using PizzaRestaurant.IntegrationTests.Presentation.TestUtils;
using PizzaRestaurant.Presentation.Common.DTO;
using System.Net;
using System.Net.Http.Json;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizza
{
    public class DeletePizzaEndpointTests
        : BaseApiIntegrationTests
    {
        public DeletePizzaEndpointTests(ApiFactory apiFactory)
            : base(apiFactory)
        {
        }

        [Fact]
        public async Task DeletePizza_WithExistingId_ShouldDeletePizza()
        {
            //Arrange
            var pizzaToAdd = _pizzaGenerator.Generate();
            var addResult =
                await _httpClient
                    .PostAsJsonAsync("/pizza/add", pizzaToAdd);

            var pizzaToDelete =
                await addResult.Content
                    .ReadFromJsonAsync<PizzaResponse>();

            //Act
            var deleteResult =
                await _httpClient
                    .DeleteAsync($"/pizza/delete/{pizzaToDelete!.Id}");

            var getResult =
                await _httpClient
                    .GetAsync($"/pizza/{pizzaToDelete.Id}");

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
                    .DeleteAsync($"/pizza/delete/{nonexistingId}");
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
