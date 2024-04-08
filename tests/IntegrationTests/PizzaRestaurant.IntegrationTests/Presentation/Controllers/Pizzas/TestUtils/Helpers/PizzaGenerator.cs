using Bogus;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.IntegrationTests.TestUtils;
using PizzaRestaurant.Presentation.Common.DTO.PizzaDto;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.Helpers
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

        private readonly Faker<PizzaRequest> _invalidPizzaRequestGenerator =
            new Faker<PizzaRequest>()
                .WithRecord()
                .RuleFor(p => p.Name, f => f.Random.AlphaNumeric(3))
                .RuleFor(p => p.CrustType, f => f.Lorem.Paragraph(3))
                .RuleFor(p => p.Ingredients, f => f.Lorem.Word())
                .RuleFor(p => p.Price, f => f.Finance.Amount(-500, -10, 2));

        public List<Pizza> SeededPizzas;

        public PizzaGenerator(int pizzasCountToSeed)
        {
            SeededPizzas = _pizzaGenerator.Generate(pizzasCountToSeed);
        }

        public PizzaRequest GeneratePizzaRequest()
        {
            return _pizzaRequestGenerator.Generate();
        }

        public PizzaRequest GenerateInvalidPizzaRequest()
        {
            return _invalidPizzaRequestGenerator.Generate();
        }
    }
}
