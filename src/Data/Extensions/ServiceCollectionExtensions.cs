using ElderlyCare.Domain.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElderyCare.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddElderlyCareContext(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var dbConfig = configuration.Get<DatabaseConfiguration>();
            var connectionString = dbConfig.GetConnectionString();
            services.AddDbContext<ElderlyCareContext>(opts => opts.UseSqlServer(connectionString), ServiceLifetime.Scoped);
        }

        public static void AddElderlyCareContextFactory(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var dbConfig = configuration.Get<DatabaseConfiguration>();
            var connectionString = dbConfig.GetConnectionString();
            services.AddPooledDbContextFactory<ElderlyCareContext>(opts => opts.UseSqlServer(connectionString));
        }
    }
}
