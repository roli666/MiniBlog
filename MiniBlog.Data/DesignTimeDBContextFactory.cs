using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;

namespace MiniBlog.Data
{
    public class DesignTimeDBContextFactory : IDesignTimeDbContextFactory<MiniBlogDBContext>
    {
        public MiniBlogDBContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<MiniBlogDBContext>();
            var connectionString =
                configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new MiniBlogDBContext(builder.Options, new OperationalStoreOptionsMigrations());
        }

        private class OperationalStoreOptionsMigrations : IOptions<OperationalStoreOptions>
        {
            public OperationalStoreOptions Value => new OperationalStoreOptions()
            {
                DeviceFlowCodes = new TableConfiguration("DeviceCodes"),
                EnableTokenCleanup = false,
                PersistedGrants = new TableConfiguration("PersistedGrants"),
                TokenCleanupBatchSize = 100,
                TokenCleanupInterval = 3600,
            };
        }
    }
}