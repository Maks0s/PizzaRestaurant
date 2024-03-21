using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaRestaurant.Application.Common.Interfaces.Persistence;
using PizzaRestaurant.Infrastructure.Persistence;
using PizzaRestaurant.Infrastructure.Persistence.Common.DbSchemas;
using PizzaRestaurant.Infrastructure.Persistence.Repositories;

namespace PizzaRestaurant.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            services.AddPersistence(configuration);

            return services;
        }

        private static IServiceCollection AddPersistence(
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            services.AddDbContext<PizzaDbContext>(options =>
            {
                options.UseSqlServer(
                        configuration.GetConnectionString("DefaultDockerMsSql"),
                        sqlConfig =>
                        {
                            sqlConfig.MigrationsHistoryTable(
                                    HistoryRepository.DefaultTableName,
                                    DbSchemasConstants.PizzaTablesSchema
                                );
                        }
                    );
            });

            services.AddScoped<IPizzaRepository, PizzaRepository>();

            return services;
        }
    }
}
