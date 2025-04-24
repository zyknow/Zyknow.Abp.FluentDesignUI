using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.CmsKit.Public;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

namespace Zyknow.Abp.CmsKit.Blazor.Public.FluentDesignUI;

[DependsOn(
    typeof(CmsKitPublicApplicationContractsModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpAspNetCoreComponentsWebFluentDesignThemeModule)
)]
public class AbpCmsKitBlazorPublicFluentDesignModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpCmsKitBlazorPublicFluentDesignModule>();
    }
} 