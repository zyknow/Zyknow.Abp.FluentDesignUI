using Volo.Abp.DependencyInjection;

namespace Simple.Data;

/* This is used if database provider does't define
 * ISimpleDbSchemaMigrator implementation.
 */
public class NullSimpleDbSchemaMigrator : ISimpleDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
