﻿using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using PizzaRestaurant.Application.Common.AppErrors;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.BaseImplementations;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.Helpers;
using PizzaRestaurant.Presentation.Common.DTO.PizzaDto;
using System.Net;
using System.Net.Http.Json;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas
{
    public class GetPizzaByIdEndpointTests
        : BasePizzaApiIntegrationTest
    {
        public GetPizzaByIdEndpointTests(
                PizzaApiFactory apiFactory
            )
            : base(apiFactory)
        {
        }

        [Fact]
        public async Task GetPizzaById_WithexistingId_ShouldReturnExistingPizza()
        {
            //Arrange
            var pizzaToGet = _pizzaGenerator.SeededPizzas[0];
            var pizzaRequestToAssert = new PizzaRequest(
                    pizzaToGet.Name,
                    pizzaToGet.CrustType,
                    pizzaToGet.Ingredients,
                    pizzaToGet.Price
                );
            
            //Act
            var getResult =
                await _httpClient
                    .GetAsync(PizzaApiUrl.GetPizzaByIdEndpoint + pizzaToGet.Id);
            var pizzaResponse =
                await getResult.Content
                    .ReadFromJsonAsync<PizzaResponse>();

            //Assert
            using var _ = new AssertionScope();

            getResult.StatusCode.AssertStatusCode(HttpStatusCode.OK);
            pizzaResponse!.AssertPizzaResponse(pizzaRequestToAssert);
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
                    .GetAsync(PizzaApiUrl.GetPizzaByIdEndpoint + nonexistentId);
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
