using FluentValidation;
using PizzaRestaurant.Application.Auth.Common.CustomeValidators;

namespace PizzaRestaurant.Application.Auth.Queries.Login
{
    public class LoginQueryValidator
        : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(lq => lq.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
