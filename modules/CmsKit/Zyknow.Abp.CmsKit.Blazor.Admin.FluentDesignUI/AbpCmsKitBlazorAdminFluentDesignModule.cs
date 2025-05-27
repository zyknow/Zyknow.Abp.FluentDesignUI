using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.CmsKit.Admin;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Routing;
using Zyknow.Abp.CmsKit.Blazor.Admin.FluentDesignUI.Settings;
using Zyknow.Abp.CmsKit.Blazor.Common.FluentDesignUI;
using Zyknow.Abp.GroupComponent.FluentDesignUI;

namespace Zyknow.Abp.CmsKit.Blazor.Admin.FluentDesignUI;

[DependsOn(
    typeof(CmsKitAdminApplicationContractsModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpAspNetCoreComponentsWebFluentDesignThemeModule),
    typeof(AbpCmsKitBlazorCommonFluentDesignModule),
    typeof(AbpGroupComponentAbstractFluentDesignModule)
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

        Configure<GroupComponentOptions>(options =>
        {
            options.Contributors.Add(new FluentDesignCmsGroupContributor());
        });
    }
}