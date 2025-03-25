using Simple.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Simple.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SimpleEntityFrameworkCoreModule),
    typeof(SimpleApplicationContractsModule)
)]
public class SimpleDbMigratorModule : AbpModule
{
}
