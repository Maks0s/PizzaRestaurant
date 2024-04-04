using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PizzaRestaurant.Infrastructure.Persistence;
using PizzaRestaurant.Infrastructure.Persistence.Common.DbSchemas;
using PizzaRestaurant.IntegrationTests.Presentation.Controllers.Pizzas.TestUtils;
using PizzaRestaurant.Presentation.Common.Markers;
using Respawn;
using System.Data.Common;
using Testcontainers.MsSql;

namespace PizzaRestaurant.IntegrationTests.Presentation.TestUtils
{
    public class ApiFactory
        : WebApplicationFactory<IApiMarker>,
        IAsyncLifetime
    {
        private readonly MsSqlContainer _msSqlContainer =
            new MsSqlBuilder().Build();

        private DbConnection _connection = default!;
        private Respawner _respawner = default!;

        public HttpClient HttpClient { get; private set; } = default!;

        public readonly PizzaGenerator PizzaGenerator = new PizzaGenerator(3);

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions<PizzaDbContext>>();
                services.RemoveAll<PizzaDbContext>();

                services.AddDbContext<PizzaDbContext>(options =>
                {
                    options.UseSqlServer(
                            _msSqlContainer.GetConnectionString(),
                            sqlOptions =>
                            {
                                sqlOptions.MigrationsHistoryTable(
                                        HistoryRepository.DefaultTableName,
                                        DbSchemasConstants.PizzaTablesSchema
                                    );
                            }
                        );
                });
            });
        }

        public async Task ResetDbAsync()
        {
            await _respawner.ResetAsync(_msSqlContainer.GetConnectionString());
        }

        public async Task ReseedDbAsync()
        {
            await ResetDbAsync();
            await SeedDatabaseAsync();
        }

        private async Task SeedDatabaseAsync()
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

        public async Task InitializeAsync()
        {
            await _msSqlContainer.StartAsync();
            HttpClient = CreateClient();
            await InitializeRespawnerAsync();
        }

        private async Task InitializeRespawnerAsync()
        {
            _connection = new SqlConnection(_msSqlContainer.GetConnectionString());
            await _connection.OpenAsync();

            _respawner =
                await Respawner.CreateAsync(
                        _connection,
                        new RespawnerOptions()
                        {
                            DbAdapter = DbAdapter.SqlServer,
                            SchemasToInclude = new[] { DbSchemasConstants.PizzaTablesSchema },
                        }
                    );
        }

        public new async Task DisposeAsync()
        {
            await _msSqlContainer.StopAsync();
        }
    }
}
