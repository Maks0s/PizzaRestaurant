using FluentAssertions;
using PizzaRestaurant.Presentation.Common.DTO;

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
    }
}
