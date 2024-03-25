namespace PizzaRestaurant.IntegrationTests.Presentation.TestUtils
{
    [CollectionDefinition(Name)]
    public class SharedApiFactory : ICollectionFixture<ApiFactory>
    {
        public const string Name = "SharedApiFactory";
    }
}
