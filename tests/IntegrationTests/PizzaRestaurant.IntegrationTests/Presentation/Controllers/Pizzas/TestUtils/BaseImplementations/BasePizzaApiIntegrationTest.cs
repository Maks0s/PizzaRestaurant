using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.Helpers;
using PizzaRestaurant.IntegrationTests.Presentation.TestUtils;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.BaseImplementations
{
    [Collection(SharedPizzaApiFactory.Name)]
    public class BasePizzaApiIntegrationTest
        : BaseApiIntegrationTest
    {
        protected PizzaGenerator _pizzaGenerator;

        private readonly Func<Task> _asyncDbReseeder;

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
