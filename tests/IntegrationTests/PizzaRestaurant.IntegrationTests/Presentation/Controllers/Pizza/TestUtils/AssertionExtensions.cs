using ErrorOr;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PizzaRestaurant.Presentation.Common.DTO;
using System.Net;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizza.TestUtils
{
    public static class AssertionExtensions
    {
        public static void AssertPizzaResponse(
                this PizzaResponse pizzaResponse,
                PizzaRequest requestedPizza
            )
        {
            pizzaResponse.Name.Should().Be(requestedPizza.Name);
            pizzaResponse.CrustType.Should().Be(requestedPizza.CrustType);
            pizzaResponse.Ingredients.Should().Be(requestedPizza.Ingredients);
            pizzaResponse.Price.Should().Be(requestedPizza.Price);
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
