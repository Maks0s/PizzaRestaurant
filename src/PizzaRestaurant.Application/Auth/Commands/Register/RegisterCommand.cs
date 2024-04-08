using PizzaRestaurant.Application.Auth.Common.AuthDto;
using PizzaRestaurant.Application.Common.Interfaces.CQRS;

namespace PizzaRestaurant.Application.Auth.Commands.Register
{
    public record RegisterCommand(
            string Username,
            string Email,
            string Password
        ) : ICommand<AuthResult>;
}
