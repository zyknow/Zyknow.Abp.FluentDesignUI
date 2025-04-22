using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Simple.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class SimpleDbContextFactory : IDesignTimeDbContextFactory<SimpleDbContext>
{
    public SimpleDbContext CreateDbContext(string[] args)
    {
        
        var configuration = BuildConfiguration();
        
        SimpleEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<SimpleDbContext>()
            .UseSqlite(configuration.GetConnectionString("Default"));
        
        return new SimpleDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Simple.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
