using Microsoft.EntityFrameworkCore;
using PizzaRestaurant.Infrastructure.Persistence;
using PizzaRestaurant.Presentation.Common.Mappers;

namespace PizzaRestaurant.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMappers();

            return services;
        }

        private static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddTransient<PizzaMapper>();
            services.AddTransient<AuthMapper>();

            return services;
        }

        public static void ApplyDbMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using PizzaDbContext pizzaDbContext =
                scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            pizzaDbContext.Database.Migrate();

            using AuthDbContext authDbContext =
                scope.ServiceProvider.GetRequiredService<AuthDbContext>();

            authDbContext.Database.Migrate();
        }
    }
}