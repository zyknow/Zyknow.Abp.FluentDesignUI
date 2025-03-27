using System.Runtime.CompilerServices;
using Volo.Abp.Account;
using Volo.Abp.Modularity;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

namespace Zyknow.Abp.Account.Blazor.FluentDesignUI;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebFluentDesignThemeModule),
    typeof(AbpAccountApplicationContractsModule)
)]
public class AbpAccountBlazorFluentDesignModule : AbpModule
{
    
}