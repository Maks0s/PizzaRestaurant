using Bogus;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.IntegrationTests.TestUtils;
using PizzaRestaurant.Presentation.Common.DTO;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils
{
    public class PizzaGenerator
    {
        private readonly Faker<Pizza> _pizzaGenerator =
            new Faker<Pizza>()
                .RuleFor(p => p.Name, f => f.Name.LastName())
                .RuleFor(p => p.CrustType, f => f.PickRandom(PizzaPropertiesValues.CrustTypes))
                .RuleFor(p => p.Ingredients, f => f.Lorem.Sentence())
                .RuleFor(p => p.Price, f => f.Finance.Amount(10, 499, 2));

        private readonly Faker<PizzaRequest> _pizzaRequestGenerator =
            new Faker<PizzaRequest>()
                .WithRecord()
                .RuleFor(p => p.Name, f => f.Name.LastName())
                .RuleFor(p => p.CrustType, f => f.PickRandom(PizzaPropertiesValues.CrustTypes))
                .RuleFor(p => p.Ingredients, f => f.Lorem.Sentence())
                .RuleFor(p => p.Price, f => f.Finance.Amount(10, 499, 2));

        public List<Pizza> SeededPizzas;

        public PizzaGenerator(int pizzasCountToSeed)
        {
            SeededPizzas = _pizzaGenerator.Generate(pizzasCountToSeed);
        }

        public PizzaRequest GeneratePizzaRequest()
        {
            return _pizzaRequestGenerator.Generate();
        }
    }
}
