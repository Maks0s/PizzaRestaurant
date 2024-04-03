using PizzaRestaurant.Application.Common.Interfaces.CQRS;
using PizzaRestaurant.Domain.Entities;

namespace PizzaRestaurant.Application.Pizzas.Commands.Update
{
    public record UpdatePizzaCommand(
            string Name,
            string CrustType,
            string Ingredients,
            decimal Price
        )
        : ICommand<Pizza?>
    {
        public Guid PizzaId { get; set; }
    }
}
