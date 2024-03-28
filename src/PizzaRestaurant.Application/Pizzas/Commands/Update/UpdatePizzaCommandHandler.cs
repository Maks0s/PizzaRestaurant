using ErrorOr;
using PizzaRestaurant.Application.Common.AppErrors;
using PizzaRestaurant.Application.Common.Interfaces.CQRS;
using PizzaRestaurant.Application.Common.Interfaces.Persistence;
using PizzaRestaurant.Domain.Entities;

namespace PizzaRestaurant.Application.Pizzas.Commands.Update
{
    public class UpdatePizzaCommandHandler
        : ICommandHandler<UpdatePizzaCommand, Pizza?>
    {
        private readonly IPizzaRepository _pizzaRepository;

        public UpdatePizzaCommandHandler(
                IPizzaRepository pizzaRepository
            )
        {
            _pizzaRepository = pizzaRepository;
        }

        public async Task<ErrorOr<Pizza?>> Handle(
                UpdatePizzaCommand command,
                CancellationToken cancellationToken
            )
        {
            var updatedPizza = new Pizza()
            {
                Id = command.PizzaId,
                Name = command.Name,
                CrustType = command.CrustType,
                Ingredients = command.Ingredients,
                Price = command.Price
            };

            var updatedPizzasCount = 
                await _pizzaRepository
                    .UpdatePizzaAsync(updatedPizza);

            if(updatedPizzasCount < 1)
            {
                return Errors.Pizzas.NotFound(updatedPizza.Id);
            }

            return updatedPizza;
        }
    }
}
