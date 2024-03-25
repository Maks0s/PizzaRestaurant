using ErrorOr;
using PizzaRestaurant.Application.Common.AppErrors;
using PizzaRestaurant.Application.Common.Interfaces.CQRS;
using PizzaRestaurant.Application.Common.Interfaces.Persistence;
using PizzaRestaurant.Domain.Entities;

namespace PizzaRestaurant.Application.Pizzas.Commands
{
    public class AddPizzaCommandHandler
        : ICommandHandler<AddPizzaCommand, Pizza?>
    {
        private readonly IPizzaRepository _pizzaRepository;

        public AddPizzaCommandHandler(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public async Task<ErrorOr<Pizza?>> Handle(
                AddPizzaCommand command,
                CancellationToken cancellationToken
            )
        {
            var pizzaToAdd = new Pizza()
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                CrustType = command.CrustType,
                Ingredients = command.Ingredients,
                Price = command.Price,
            };

            var addedPizza =
                await _pizzaRepository.AddPizzaAsync(pizzaToAdd);

            if (addedPizza is null)
            {
                return Errors.ServerDataManipulation.NotAdded();
            }

            return addedPizza;
        }
    }
}
