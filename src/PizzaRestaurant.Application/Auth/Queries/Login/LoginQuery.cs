using PizzaRestaurant.Application.Auth.Common.AuthDto;
using PizzaRestaurant.Application.Common.Interfaces.CQRS;

namespace PizzaRestaurant.Application.Auth.Queries.Login
{
    public record LoginQuery(
            string Email,
            string Password
        ) : IQuery<AuthResult>;
}
