using Microsoft.Extensions.DependencyInjection;
using PizzaRestaurant.Infrastructure.Persistence;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.Helpers;
using PizzaRestaurant.IntegrationTests.Presentation.TestUtils;

namespace PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils.BaseImplementations
{
    public class PizzaApiFactory
        : BaseApiFactory
    {
        public readonly PizzaGenerator PizzaGenerator = new PizzaGenerator(3);

        public async Task ReseedDbAsync()
        {
            await ResetDbAsync();
            await SeedPizzaTablesAsync();
        }

        private async Task SeedPizzaTablesAsync()
        {
            using var scope = Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<PizzaDbContext>();

            await context.Pizzas
                .AddRangeAsync(
                        PizzaGenerator.SeededPizzas
                    );

            await context.SaveChangesAsync();
        }
    }
}
