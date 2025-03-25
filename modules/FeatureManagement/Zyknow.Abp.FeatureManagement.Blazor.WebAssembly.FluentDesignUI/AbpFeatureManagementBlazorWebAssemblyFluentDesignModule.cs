using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Zyknow.Abp.AspnetCore.Components.WebAssembly.FluentDesignTheme;
using Zyknow.Abp.FeatureManagement.Blazor.FluentDesignUI;

namespace Zyknow.Abp.FeatureManagement.Blazor.WebAssembly.FluentDesignUI;

[DependsOn(
    typeof(AbpFeatureManagementBlazorFluentDesignModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyFluentDesignThemeModule),
    typeof(AbpFeatureManagementHttpApiClientModule)
)]
public class AbpFeatureManagementBlazorWebAssemblyFluentDesignModule : AbpModule
{
}