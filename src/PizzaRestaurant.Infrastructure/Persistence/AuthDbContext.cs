using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.Infrastructure.Persistence.Common.DbSchemas;

namespace PizzaRestaurant.Infrastructure.Persistence
{
    public class AuthDbContext
        : IdentityDbContext<AuthUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<AuthUser> AuthUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(
                    DbSchemasConstants.AuthTablesSchema
                );
        }
    }
}
