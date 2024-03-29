using PizzaRestaurant.Application.Common.Interfaces.CQRS;

namespace PizzaRestaurant.Application.Pizzas.Commands.Delete
{
    public record class DeletePizzaCommand(
            Guid PizzaId
        )
        : ICommand<ErrorOr.Deleted>;
}
