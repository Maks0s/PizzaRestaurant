using Microsoft.Extensions.DependencyInjection;
using PizzaRestaurant.Application.Common.Behaviors;
using PizzaRestaurant.Application.Common.Interfaces;

namespace PizzaRestaurant.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
                this IServiceCollection services
            )
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            return services;
        }
    }
}
