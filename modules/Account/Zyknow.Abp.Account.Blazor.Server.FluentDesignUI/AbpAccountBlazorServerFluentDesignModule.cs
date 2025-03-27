using Volo.Abp.Modularity;
using Zyknow.Abp.Account.Blazor.FluentDesignUI;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;

namespace Zyknow.Abp.Account.Blazor.Server.FluentDesignUI;

[DependsOn(
    typeof(AbpAccountBlazorFluentDesignModule),
    typeof(AbpAspNetCoreComponentsServerFluentDesignThemeModule))]
public class AbpAccountBlazorServerFluentDesignModule : AbpModule
{
}