using ErrorOr;
using MediatR;

namespace PizzaRestaurant.Application.Common.Interfaces.CQRS
{
    public interface IQuery<TResponse>
        : IRequest<ErrorOr<TResponse>>
    {
    }
}
