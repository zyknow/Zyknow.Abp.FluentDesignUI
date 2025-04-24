using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.CmsKit.Admin;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

namespace Zyknow.Abp.CmsKit.Blazor.Admin.FluentDesignUI;

[DependsOn(
    typeof(CmsKitAdminApplicationContractsModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpAspNetCoreComponentsWebFluentDesignThemeModule)
)]
public class AbpCmsKitBlazorAdminFluentDesignModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpCmsKitBlazorAdminFluentDesignModule>();
    }
}