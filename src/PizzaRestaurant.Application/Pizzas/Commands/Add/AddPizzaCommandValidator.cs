using FluentValidation;

namespace PizzaRestaurant.Application.Pizzas.Commands.Add
{
    public class AddPizzaCommandValidator
        : AbstractValidator<AddPizzaCommand>
    {
        public AddPizzaCommandValidator()
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
