using Volo.Abp.Modularity;
using Zyknow.Abp.FeatureManagement.Blazor.Server.FluentDesignUI;
using Zyknow.Abp.TenantManagement.Blazor.FluentDesignUI;

namespace Zyknow.Abp.TenantManagement.Blazor.Server.FluentDesignUI;

[DependsOn(
    typeof(AbpTenantManagementBlazorFluentDesignModule),
    typeof(AbpFeatureManagementBlazorServerFluentDesignModule)
)]
public class AbpTenantManagementBlazorServerFluentDesignModule : AbpModule
{
}