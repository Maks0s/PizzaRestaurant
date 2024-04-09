using FluentValidation;

namespace PizzaRestaurant.Application.Auth.Common.CustomeValidators
{
    public static class PasswordValidator
    {
        public static IRuleBuilderOptions<T, string> Password<T>(
                this IRuleBuilderInitial<T, string> ruleBuilder
            )
        {
            return ruleBuilder
                .NotEmpty()
                .NotNull()
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.\-]+").WithMessage("Your password must contain at least one special chatacter: (!?*.-)");
        }
    }
}
