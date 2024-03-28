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
    public class GetAllPizzasEndpointTests
        : BaseApiIntegrationTests
    {
        public GetAllPizzasEndpointTests(ApiFactory apiFactory)
            : base(apiFactory)
        {

        }

        [Fact]
        public async Task GetAllPizzas_WithPopulatedDatabase_ShouldReturnAllPopulatedData()
        {
            //Arrange
            var populateCount = new Random().Next(2, 4);

            for (int i = 0; i < populateCount; i++)
            {
                await _httpClient
                    .PostAsJsonAsync(
                        "/pizza/add",
                        _pizzaGenerator.Generate()
                    );
            }

            //Act
            var queryResult =
                await _httpClient
                    .GetAsync("/pizza/all");

            var requestedPizzas =
                await queryResult.Content
                    .ReadFromJsonAsync<ICollection<PizzaResponse>>();

            //Assert
            using var _ = new AssertionScope();

            queryResult.StatusCode.AssertStatusCode(HttpStatusCode.OK);
            requestedPizzas!.Count.Should().Be(populateCount);
        }

        [Fact]
        public async Task GetAllPizzas_WithNotPopulatedDatabase_ShouldReturnInternalServerError()
        {
            //Arrange
            var expectedError = Errors.ServerDataManipulation.NotReceived();

            //Act
            var queryResult =
                await _httpClient
                    .GetAsync("/pizza/all");

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
