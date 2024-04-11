using ErrorOr;
using PizzaRestaurant.Application.Common.AppErrors;
using PizzaRestaurant.Application.Common.Interfaces.CQRS;
using PizzaRestaurant.Application.Common.Interfaces.Persistence;
using PizzaRestaurant.Domain.Entities;

namespace PizzaRestaurant.Application.Pizzas.Queries.GetById
{
    public class GetPizzaByIdQueryHandler
        : IQueryHandler<GetPizzaByIdQuery, Pizza?>
    {
        private readonly IPizzaRepository _pizzaRepository;

        public GetPizzaByIdQueryHandler(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public async Task<ErrorOr<Pizza?>> Handle(
                GetPizzaByIdQuery query,
                CancellationToken cancellationToken
            )
        {
            var pizzaId = query.Id;

            var pizza =
                await _pizzaRepository.GetPizzaAsync(pizzaId);

            if (pizza is null)
            {
                return Errors.Pizzas.NotFound(pizzaId);
            }

            return pizza;
        }
    }
}
