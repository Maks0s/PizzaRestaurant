using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils;

namespace PizzaRestaurant.IntegrationTests.Presentation.TestUtils
{
    [Collection(SharedApiFactory.Name)]
    public class BaseApiIntegrationTests : IAsyncLifetime
    {
        private readonly Func<Task> _asyncDbReseeder;
        protected readonly Func<Task> _asyncDbReseter;
        protected readonly HttpClient _httpClient;

        protected PizzaGenerator _pizzaGenerator;

        public BaseApiIntegrationTests(
                ApiFactory apiFactory
            )
        {
            _asyncDbReseter = apiFactory.ResetDbAsync;
            _asyncDbReseeder = apiFactory.ReseedDbAsync;
            _httpClient = apiFactory.HttpClient;
            _pizzaGenerator = apiFactory.PizzaGenerator;
        }

        public async Task InitializeAsync()
        {
            await _asyncDbReseeder.Invoke();
        }

        public async Task DisposeAsync() => await Task.CompletedTask;

    }
}
