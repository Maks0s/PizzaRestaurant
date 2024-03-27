using PizzaRestaurant.Application.Common.Interfaces.CQRS;
using PizzaRestaurant.Domain.Entities;

namespace PizzaRestaurant.Application.Pizzas.Queries.GetAll
{
    public record GetAllPizzasQuery()
        : IQuery<ICollection<Pizza>>;
}
