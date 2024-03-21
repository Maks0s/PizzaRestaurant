using ErrorOr;
using MediatR;

namespace PizzaRestaurant.Application.Common.Interfaces.CQRS
{
    public interface IQueryHandler<TRequest, TResponse>
        : IRequestHandler<TRequest, ErrorOr<TResponse>>
        where TRequest : IQuery<TResponse>
    {
    }
}
