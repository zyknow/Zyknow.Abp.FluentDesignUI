using Volo.Abp.Account;
using Volo.Abp.Modularity;
using Zyknow.Abp.Account.Blazor.FluentDesignUI;
using Zyknow.Abp.AspnetCore.Components.WebAssembly.FluentDesignTheme;

namespace Zyknow.Abp.Account.Blazor.WebAssembly.FluentDesignUI;

[DependsOn(
    typeof(AbpAccountBlazorFluentDesignModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyFluentDesignThemeModule),
    typeof(AbpAccountHttpApiClientModule)
)]
public class AbpAccountBlazorWebAssemblyFluentDesignModule : AbpModule
{
    
}