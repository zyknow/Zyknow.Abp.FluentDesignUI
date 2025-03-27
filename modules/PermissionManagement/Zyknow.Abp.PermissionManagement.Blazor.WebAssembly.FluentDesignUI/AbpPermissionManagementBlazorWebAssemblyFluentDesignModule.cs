using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Zyknow.Abp.AspnetCore.Components.WebAssembly.FluentDesignTheme;
using Zyknow.Abp.PermissionManagement.Blazor.FluentDesignUI;

namespace Zyknow.Abp.PermissionManagement.Blazor.WebAssembly.FluentDesignUI;
[DependsOn(
    typeof(AbpPermissionManagementBlazorFluentDesignModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyFluentDesignThemeModule),
    typeof(AbpPermissionManagementHttpApiClientModule)
)]
public class AbpPermissionManagementBlazorWebAssemblyFluentDesignModule : AbpModule
{
}
