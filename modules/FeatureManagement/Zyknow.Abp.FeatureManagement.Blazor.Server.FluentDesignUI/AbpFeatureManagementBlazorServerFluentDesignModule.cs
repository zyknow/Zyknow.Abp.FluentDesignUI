using Volo.Abp.Modularity;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;
using Zyknow.Abp.FeatureManagement.Blazor.FluentDesignUI;

namespace Zyknow.Abp.FeatureManagement.Blazor.Server.FluentDesignUI;

[DependsOn(
    typeof(AbpFeatureManagementBlazorFluentDesignModule),
    typeof(AbpAspNetCoreComponentsServerFluentDesignThemeModule)
)]
public class AbpFeatureManagementBlazorServerFluentDesignModule : AbpModule
{
}