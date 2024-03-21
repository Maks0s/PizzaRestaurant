using ErrorOr;
using MediatR;

namespace PizzaRestaurant.Application.Common.Interfaces.CQRS
{
    public interface ICommand<TResponse>
        : IRequest<ErrorOr<TResponse>>
    {
    }
}
