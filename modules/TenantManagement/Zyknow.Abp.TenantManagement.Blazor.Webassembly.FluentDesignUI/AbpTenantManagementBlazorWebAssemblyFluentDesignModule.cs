using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;
using Zyknow.Abp.FeatureManagement.Blazor.WebAssembly.FluentDesignUI;
using Zyknow.Abp.TenantManagement.Blazor.FluentDesignUI;

namespace Zyknow.Abp.TenantManagement.Blazor.Webassembly.FluentDesignUI;


[DependsOn(
    typeof(AbpTenantManagementBlazorFluentDesignModule),
    typeof(AbpFeatureManagementBlazorWebAssemblyFluentDesignModule),
    typeof(AbpTenantManagementHttpApiClientModule)
)]
public class AbpTenantManagementBlazorWebAssemblyFluentDesignModule : AbpModule
{
    
}
