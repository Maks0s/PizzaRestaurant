using PizzaRestaurant.Application.Common.Interfaces.CQRS;
using PizzaRestaurant.Domain.Entities;

namespace PizzaRestaurant.Application.Pizzas.Commands.Add
{
    public record AddPizzaCommand(
            string Name,
            string CrustType,
            string Ingredients,
            decimal Price
        )
        : ICommand<Pizza?>;
}
