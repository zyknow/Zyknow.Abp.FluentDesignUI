using Volo.Abp.Modularity;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;
using Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI;

namespace Zyknow.Abp.SettingManagement.Blazor.Server.FluentDesignUI;

[DependsOn(
    typeof(AbpSettingManagementBlazorFluentDesignModule),
    typeof(AbpAspNetCoreComponentsServerFluentDesignThemeModule)
)]
public class AbpSettingManagementBlazorServerFluentDesignModule : AbpModule
{
    
}
