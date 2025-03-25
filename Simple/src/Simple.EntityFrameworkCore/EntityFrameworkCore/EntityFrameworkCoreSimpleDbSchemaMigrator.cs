using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Simple.Data;
using Volo.Abp.DependencyInjection;

namespace Simple.EntityFrameworkCore;

public class EntityFrameworkCoreSimpleDbSchemaMigrator(IServiceProvider serviceProvider)
    : ISimpleDbSchemaMigrator, ITransientDependency
{
    public async Task MigrateAsync()
    {
        /* We intentionally resolving the SimpleDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await serviceProvider
            .GetRequiredService<SimpleDbContext>()
            .Database
            .MigrateAsync();
    }
}
