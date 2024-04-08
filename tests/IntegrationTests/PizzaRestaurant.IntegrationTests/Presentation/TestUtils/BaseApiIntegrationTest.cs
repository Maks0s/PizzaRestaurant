namespace PizzaRestaurant.IntegrationTests.Presentation.TestUtils
{
    public class BaseApiIntegrationTest : IAsyncLifetime
    {
        protected readonly Func<Task> _asyncDbReseter;
        protected readonly HttpClient _httpClient;

        public BaseApiIntegrationTest(
                BaseApiFactory apiFactory
            )
        {
            _asyncDbReseter = apiFactory.ResetDbAsync;
            _httpClient = apiFactory.HttpClient;
        }

        public virtual async Task InitializeAsync()
        {
            await _asyncDbReseter.Invoke();
        }

        public virtual async Task DisposeAsync() => await Task.CompletedTask;

    }
}
