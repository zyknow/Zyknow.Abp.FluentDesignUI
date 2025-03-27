using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Zyknow.Abp.AspnetCore.Components.WebAssembly.FluentDesignTheme;
using Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI;

namespace Zyknow.Abp.SettingManagement.Blazor.WebAssembly.FluentDesignUI;

[DependsOn(
    typeof(AbpSettingManagementBlazorFluentDesignModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyFluentDesignThemeModule),
    typeof(AbpSettingManagementHttpApiClientModule)
)]
public class AbpSettingManagementBlazorWebAssemblyFluentDesignModule : AbpModule
{
    
}
