using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ElderyCare.Data
{
    public class ElderlyCareContextFactory : IDesignTimeDbContextFactory<ElderlyCareContext>
    {
        public ElderlyCareContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost;Database=elderly_care_local;Uid=root;Pwd=root;";

            var optionsBuilder = new DbContextOptionsBuilder<ElderlyCareContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mySqlOptionsAction: o => o.MigrationsAssembly("ElderyCare.Data"));

            return new ElderlyCareContext(optionsBuilder.Options);
        }
    }
}
