using ErrorOr;
using PizzaRestaurant.Application.Common.AppErrors;
using PizzaRestaurant.Application.Common.Interfaces.CQRS;
using PizzaRestaurant.Application.Common.Interfaces.Persistence;

namespace PizzaRestaurant.Application.Pizzas.Commands.Delete
{
    public class DeletePizzaCommandHandler
        : ICommandHandler<DeletePizzaCommand, ErrorOr.Deleted>
    {
        private readonly IPizzaRepository _pizzaRepository;

        public DeletePizzaCommandHandler(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(
                DeletePizzaCommand command, 
                CancellationToken cancellationToken
            )
        {
            var deletedPizzasCount =
                await _pizzaRepository
                    .DeletePizzaAsync(command.PizzaId);

            if(deletedPizzasCount < 1)
            {
                return Errors.Pizzas.NotFound(command.PizzaId);
            }

            return Result.Deleted;
        }
    }
}
