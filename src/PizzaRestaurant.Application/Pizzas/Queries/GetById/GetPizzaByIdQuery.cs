using PizzaRestaurant.Application.Common.Interfaces.CQRS;
using PizzaRestaurant.Domain.Entities;

namespace PizzaRestaurant.Application.Pizzas.Queries.GetById
{
    public record GetPizzaByIdQuery(Guid Id)
        : IQuery<Pizza?>;
}
