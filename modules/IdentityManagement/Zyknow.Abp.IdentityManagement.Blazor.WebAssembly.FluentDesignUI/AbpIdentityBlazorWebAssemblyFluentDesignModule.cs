using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Zyknow.Abp.IdentityManagement.Blazor.FluentDesignUI;
using Zyknow.Abp.PermissionManagement.Blazor.WebAssembly.FluentDesignUI;

namespace Zyknow.Abp.IdentityManagement.Blazor.WebAssembly.FluentDesignUI;
[DependsOn(
    typeof(AbpIdentityBlazorFluentDesignModule),
    typeof(AbpPermissionManagementBlazorWebAssemblyFluentDesignModule),
    typeof(AbpIdentityHttpApiClientModule)
)]
public class AbpIdentityBlazorWebAssemblyFluentDesignModule : AbpModule
{
}

