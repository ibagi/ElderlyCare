using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElderyCare.Data
{
    public static class DependencyInjection
    {
        public static void AddElderlyCareContext(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetConnectionString("ElderlyCare");
            services.AddDbContext<ElderlyCareContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), ServiceLifetime.Scoped);
        }

        public static void AddElderlyCareContextFactory(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetConnectionString("ElderlyCare");
            services.AddPooledDbContextFactory<ElderlyCareContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }

        public static void InitializeElderyCareContext(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<ElderlyCareContext>();
            dbContext!.EnsureItsMigratedAndSeeded();
        }
    }
}
