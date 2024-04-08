namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.BaseImplementations
{
    [CollectionDefinition(Name)]
    public class SharedPizzaApiFactory
        : ICollectionFixture<PizzaApiFactory>
    {
        public const string Name = "SharedPizzaApiFactory";
    }
}
