using ErrorOr;
using MediatR;

namespace PizzaRestaurant.Application.Common.Interfaces.CQRS
{
    public interface ICommandHandler<TRequest, TResponse>
        : IRequestHandler<TRequest, ErrorOr<TResponse>>
        where TRequest : ICommand<TResponse>
    {
    }
}
