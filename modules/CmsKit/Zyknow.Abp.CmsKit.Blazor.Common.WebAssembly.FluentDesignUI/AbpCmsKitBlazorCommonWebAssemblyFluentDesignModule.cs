using Volo.Abp.Modularity;
using Volo.CmsKit;
using Zyknow.Abp.AspnetCore.Components.WebAssembly.FluentDesignTheme;
using Zyknow.Abp.CmsKit.Blazor.Common.FluentDesignUI;

namespace Zyknow.Abp.CmsKit.Blazor.Common.WebAssembly.FluentDesignUI;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebAssemblyFluentDesignThemeModule),
    typeof(AbpCmsKitBlazorCommonFluentDesignModule),
    typeof(CmsKitCommonHttpApiClientModule)
)]
public class AbpCmsKitBlazorCommonWebAssemblyFluentDesignModule : AbpModule
{
}