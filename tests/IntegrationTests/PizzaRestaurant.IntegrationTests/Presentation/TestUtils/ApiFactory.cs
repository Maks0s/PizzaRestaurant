using Meziantou.Extensions.Logging.Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using PizzaRestaurant.Infrastructure.Persistence;
using PizzaRestaurant.Infrastructure.Persistence.Common.DbSchemas;
using PizzaRestaurant.Presentation.Common.Markers;
using Respawn;
using System.Data.Common;
using Testcontainers.MsSql;
using Xunit.Abstractions;

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
        public ITestOutputHelper testOutputHelper { get; set; } = default!;

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

            builder.ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Information);

                loggingBuilder.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(testOutputHelper));
            });
        }

        public async Task ResetDbAsync()
        {
            await _respawner.ResetAsync(_msSqlContainer.GetConnectionString());
        }

        public async Task InitializeAsync()
        {
            await _msSqlContainer.StartAsync();
            HttpClient = CreateClient();
            await InitializeRespawner();
        }

        private async Task InitializeRespawner()
        {
            _connection = new SqlConnection(_msSqlContainer.GetConnectionString());
            await _connection.OpenAsync();

            _respawner =
                await Respawner.CreateAsync(
                        _connection,
                        new RespawnerOptions()
                        {
                            SchemasToInclude = new[] { DbSchemasConstants.PizzaTablesSchema }
                        }
                    );
        }

        public new async Task DisposeAsync()
        {
            await _msSqlContainer.StopAsync();
        }
    }
}
