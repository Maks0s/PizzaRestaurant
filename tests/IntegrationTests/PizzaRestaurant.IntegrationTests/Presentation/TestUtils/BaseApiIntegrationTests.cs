using Bogus;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizza.TestUtils;
using PizzaRestaurant.IntegrationTests.TestUtils;
using PizzaRestaurant.Presentation.Common.DTO;
using Xunit.Abstractions;

namespace PizzaRestaurant.IntegrationTests.Presentation.TestUtils
{
    [Collection(SharedApiFactory.Name)]
    public class BaseApiIntegrationTests : IAsyncLifetime
    {
        private readonly Func<Task> _asyncDbReseter;
        protected HttpClient _httpClient;

        protected Faker<PizzaRequest> _pizzaGenerator =
            new Faker<PizzaRequest>()
                .WithRecord()
                .RuleFor(p => p.Name, f => f.Name.LastName())
                .RuleFor(p => p.CrustType, f => f.PickRandom(PizzaPropertiesValues.CrustTypes))
                .RuleFor(p => p.Ingredients, f => f.Lorem.Sentence())
                .RuleFor(p => p.Price, f => f.Finance.Amount(10, 499, 2));

        public BaseApiIntegrationTests(
                ApiFactory apiFactory
            )
        {
            _asyncDbReseter = apiFactory.ResetDbAsync;
            _httpClient = apiFactory.HttpClient;
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public async Task DisposeAsync()
        {
            await _asyncDbReseter.Invoke();
        }

    }
}
