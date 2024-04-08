using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PizzaRestaurant.Infrastructure.Persistence;
using PizzaRestaurant.Infrastructure.Persistence.Common.DbSchemas;
using PizzaRestaurant.Presentation.Common.Markers;
using Respawn;
using System.Data.Common;
using Testcontainers.MsSql;

namespace PizzaRestaurant.IntegrationTests.Presentation.TestUtils
{
    public class BaseApiFactory
        : WebApplicationFactory<IApiMarker>,
        IAsyncLifetime
    {
        private readonly MsSqlContainer _msSqlContainer =
            new MsSqlBuilder().Build();

        private DbConnection _connection = default!;
        protected Respawner _respawner = default!;

        public HttpClient HttpClient { get; private set; } = default!;

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

                services.RemoveAll<DbContextOptions<AuthDbContext>>();
                services.RemoveAll<AuthDbContext>();

                services.AddDbContext<AuthDbContext>(options =>
                {
                    options.UseSqlServer(
                            _msSqlContainer.GetConnectionString(),
                            sqlOptions =>
                            {
                                sqlOptions.MigrationsHistoryTable(
                                        HistoryRepository.DefaultTableName,
                                        DbSchemasConstants.AuthTablesSchema
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
                            SchemasToInclude = new[] 
                            { 
                                DbSchemasConstants.PizzaTablesSchema,
                                DbSchemasConstants.AuthTablesSchema
                            }
                        }
                    );
        }

        public new async Task DisposeAsync()
        {
            await _msSqlContainer.StopAsync();
        }
    }
}
