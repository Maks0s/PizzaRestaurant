using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PizzaRestaurant.Application.Common.Interfaces.Authentication;
using PizzaRestaurant.Application.Common.Interfaces.Persistence;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.Infrastructure.Authentication.Jwt;
using PizzaRestaurant.Infrastructure.Authentication.Jwt.Common;
using PizzaRestaurant.Infrastructure.Persistence;
using PizzaRestaurant.Infrastructure.Persistence.Common.DbSchemas;
using PizzaRestaurant.Infrastructure.Persistence.Repositories;
using System.Text;

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
            services.AddAuth(configuration);

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

            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(
                        configuration.GetConnectionString("DefaultDockerMsSql"),
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsHistoryTable(
                                    HistoryRepository.DefaultTableName,
                                    DbSchemasConstants.AuthTablesSchema
                                );
                        }
                    );
            });

            services.AddScoped<IPizzaRepository, PizzaRepository>();

            return services;
        }

        private static IServiceCollection AddAuth(
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            services.AddIdentity<AuthUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AuthDbContext>();

            var jwtConfig = new JwtConfig();
            configuration.Bind(JwtConfig.SectionName, jwtConfig);
            services.AddSingleton(Options.Create(jwtConfig));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters()
                        {
                            ValidIssuer = jwtConfig.Issuer,
                            ValidateIssuer = true,

                            ValidAudience = jwtConfig.Audience,
                            ValidateAudience = true,

                            ValidateLifetime = true,

                            IssuerSigningKey = new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(jwtConfig.Key)
                                ),
                            ValidateIssuerSigningKey = true
                        };
                });

            services.AddScoped<IJwtGenerator, JwtGenerator>();

            return services;
        }
    }
}
