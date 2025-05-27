using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.CmsKit;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

namespace Zyknow.Abp.CmsKit.Blazor.Common.FluentDesignUI;

[DependsOn(
    typeof(AbpAutoMapperModule),
    typeof(AbpAspNetCoreComponentsWebFluentDesignThemeModule)
)]
public class AbpCmsKitBlazorCommonFluentDesignModule : AbpModule
{
}