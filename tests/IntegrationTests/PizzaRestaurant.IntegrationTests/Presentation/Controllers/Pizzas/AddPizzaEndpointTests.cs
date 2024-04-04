﻿using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils;
using PizzaRestaurant.IntegrationTests.Presentation.TestUtils;
using PizzaRestaurant.Presentation.Common.DTO;
using System.Net;
using System.Net.Http.Json;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas
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