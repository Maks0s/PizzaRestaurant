using ErrorOr;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using PizzaRestaurant.Presentation.Common.DTO;
using System.Net;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizza.TestUtils
{
    public static class AssertionExtensions
    {
        public static void AssertStatusCode(
                this HttpStatusCode responseStatusCode,
                HttpStatusCode expectedStatusCode
            )
        {
            responseStatusCode.Should().Be(expectedStatusCode);
        }

        public static void AssertPizzaResponse(
                this PizzaResponse pizzaResponse,
                PizzaRequest expectedPizza
            )
        {
            pizzaResponse.Name.Should().Be(expectedPizza.Name);
            pizzaResponse.CrustType.Should().Be(expectedPizza.CrustType);
            pizzaResponse.Ingredients.Should().Be(expectedPizza.Ingredients);
            pizzaResponse.Price.Should().Be(expectedPizza.Price);
        }

        public static void AssertError(
                this ProblemDetails problemDetails,
                Error expectedError
            )
        {
            problemDetails.Status.Should().Be(expectedError.NumericType);
            problemDetails.Title.Should().Be(expectedError.Code);
            problemDetails.Detail.Should().Be(expectedError.Description);
        }
    }
}
