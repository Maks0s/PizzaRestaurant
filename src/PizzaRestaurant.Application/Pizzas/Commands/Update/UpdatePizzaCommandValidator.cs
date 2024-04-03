using FluentValidation;

namespace PizzaRestaurant.Application.Pizzas.Commands.Update
{
    public class UpdatePizzaCommandValidator
        : AbstractValidator<UpdatePizzaCommand>
    {
        public UpdatePizzaCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .Length(4, 50);

            RuleFor(p => p.CrustType)
                .NotNull()
                .NotEmpty()
                .Length(4, 20);

            RuleFor(p => p.Ingredients)
                .NotNull()
                .NotEmpty()
                .Length(20, 255);

            RuleFor(p => p.Price)
                .ExclusiveBetween(0, 500);
        }
    }
}
