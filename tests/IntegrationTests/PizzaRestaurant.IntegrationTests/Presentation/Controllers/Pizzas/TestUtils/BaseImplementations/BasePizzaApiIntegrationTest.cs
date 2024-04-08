using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Auth.TestUtils;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.Helpers;
using PizzaRestaurant.IntegrationTests.Presentation.TestUtils;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.BaseImplementations
{
    [Collection(SharedPizzaApiFactory.Name)]
    public class BasePizzaApiIntegrationTest
        : BaseApiIntegrationTest
    {
        private readonly Func<Task> _asyncDbReseeder;

        protected PizzaGenerator _pizzaGenerator;

        public BasePizzaApiIntegrationTest(
                PizzaApiFactory apiFactory
            ) : base(apiFactory)
        {
            _asyncDbReseeder = apiFactory.ReseedDbAsync;
            _pizzaGenerator = apiFactory.PizzaGenerator;
        }

        public override async Task InitializeAsync()
        {
            await _asyncDbReseeder.Invoke();
        }
    }
}
