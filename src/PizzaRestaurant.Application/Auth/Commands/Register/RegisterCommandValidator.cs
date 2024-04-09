using FluentValidation;
using PizzaRestaurant.Application.Auth.Common.CustomeValidators;

namespace PizzaRestaurant.Application.Auth.Commands.Register
{
    public class RegisterCommandValidator
        : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(rc => rc.Username)
                .NotNull()
                .NotEmpty();

            RuleFor(rc => rc.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(rc => rc.Password)
                .Password();
        }
    }
}
