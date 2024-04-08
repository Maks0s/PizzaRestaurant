using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using PizzaRestaurant.Application.Common.AppErrors;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.BaseImplementations;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.Helpers;
using PizzaRestaurant.Presentation.Common.DTO.PizzaDto;
using System.Net;
using System.Net.Http.Json;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas
{
    public class GetAllPizzasEndpointTests
        : BasePizzaApiIntegrationTest
    {
        public GetAllPizzasEndpointTests(PizzaApiFactory apiFactory)
            : base(apiFactory)
        {

        }

        [Fact]
        public async Task GetAllPizzas_WithPopulatedDatabase_ShouldReturnAllPopulatedData()
        {
            //Arrange
            var dbPopulateCount = _pizzaGenerator.SeededPizzas.Count;

            //Act
            var queryResult =
                await _httpClient
                    .GetAsync(PizzaApiUrl.GetAllPizzasEndpoint);

            var requestedPizzas =
                await queryResult.Content
                    .ReadFromJsonAsync<ICollection<PizzaResponse>>();

            //Assert
            using var _ = new AssertionScope();

            queryResult.StatusCode.AssertStatusCode(HttpStatusCode.OK);
            requestedPizzas!.Count.Should().Be(dbPopulateCount);
        }

        [Fact]
        public async Task GetAllPizzas_WithNotPopulatedDatabase_ShouldReturnInternalServerError()
        {
            //Arrange
            await _asyncDbReseter.Invoke();
            var expectedError = Errors.ServerDataManipulation.NotReceived();

            //Act
            var queryResult =
                await _httpClient
                    .GetAsync(PizzaApiUrl.GetAllPizzasEndpoint);

            var problemDetails =
                await queryResult.Content
                    .ReadFromJsonAsync<ProblemDetails>();

            //Assert
            using var _ = new AssertionScope();

            queryResult.StatusCode.AssertStatusCode(HttpStatusCode.InternalServerError);
            problemDetails!.AssertError(expectedError);
        }
    }
}
