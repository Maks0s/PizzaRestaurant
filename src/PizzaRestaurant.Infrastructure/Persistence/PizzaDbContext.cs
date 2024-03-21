using Microsoft.EntityFrameworkCore;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.Infrastructure.Persistence.Common.DbSchemas;

namespace PizzaRestaurant.Infrastructure.Persistence
{
    public class PizzaDbContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

        public PizzaDbContext(DbContextOptions<PizzaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(DbSchemasConstants.PizzaTablesSchema);
        }
    }
}
