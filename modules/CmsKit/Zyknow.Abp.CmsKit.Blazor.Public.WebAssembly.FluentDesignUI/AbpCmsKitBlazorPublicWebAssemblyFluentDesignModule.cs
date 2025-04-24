using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.CmsKit.Public;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.WebAssembly.FluentDesignTheme;
using Zyknow.Abp.CmsKit.Blazor.Public.FluentDesignUI;

namespace Zyknow.Abp.CmsKit.Blazor.Public.WebAssembly.FluentDesignUI;

[DependsOn(
    typeof(AbpCmsKitBlazorPublicFluentDesignModule),
    typeof(CmsKitPublicHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyFluentDesignThemeModule)
)]
public class AbpCmsKitBlazorPublicWebAssemblyFluentDesignModule : AbpModule
{
} 