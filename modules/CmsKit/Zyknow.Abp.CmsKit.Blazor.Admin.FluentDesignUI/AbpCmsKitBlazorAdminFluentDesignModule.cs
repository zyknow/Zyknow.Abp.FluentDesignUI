using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.CmsKit.Admin;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Routing;

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
        
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<CmsKitAdminBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options => { options.MenuContributors.Add(new CmsKitAdminMenuContributor()); });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpCmsKitBlazorAdminFluentDesignModule).Assembly);
        });
    }
}