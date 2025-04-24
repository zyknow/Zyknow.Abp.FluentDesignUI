using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.CmsKit.Public;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;
using Zyknow.Abp.CmsKit.Blazor.Public.FluentDesignUI;

namespace Zyknow.Abp.CmsKit.Blazor.Public.Server.FluentDesignUI;

[DependsOn(
    typeof(AbpCmsKitBlazorPublicFluentDesignModule),
    typeof(AbpAspNetCoreComponentsServerFluentDesignThemeModule)
)]
public class AbpCmsKitBlazorPublicServerFluentDesignModule : AbpModule
{
}