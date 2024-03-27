using ErrorOr;
using PizzaRestaurant.Application.Common.AppErrors;
using PizzaRestaurant.Application.Common.Interfaces.CQRS;
using PizzaRestaurant.Application.Common.Interfaces.Persistence;
using PizzaRestaurant.Domain.Entities;

namespace PizzaRestaurant.Application.Pizzas.Queries.GetAll
{
    public class GetAllPizzasQueryHandler
        : IQueryHandler<GetAllPizzasQuery, ICollection<Pizza>>
    {
        private readonly IPizzaRepository _pizzaRepository;

        public GetAllPizzasQueryHandler(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public async Task<ErrorOr<ICollection<Pizza>>> Handle(
                GetAllPizzasQuery query,
                CancellationToken cancellationToken
            )
        {
            var allPizzas =
                await _pizzaRepository.
                    GetAllPizzasAsync();

            if(allPizzas.Count <= 0)
            {
                return Errors.ServerDataManipulation.NotReceived();
            }

            return allPizzas;
        }
    }
}
