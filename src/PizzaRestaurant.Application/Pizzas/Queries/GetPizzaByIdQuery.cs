using PizzaRestaurant.Application.Common.Interfaces.CQRS;
using PizzaRestaurant.Domain.Entities;

namespace PizzaRestaurant.Application.Pizzas.Queries
{
    public record GetPizzaByIdQuery(string Id)
        : IQuery<Pizza?>;
}
