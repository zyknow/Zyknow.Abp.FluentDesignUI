using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Public;
using Volo.CmsKit.Public.Comments;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Routing;

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
        
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new CmsKitPublicMenuContributor());
            options.MainMenuNames.Add(CmsKitMenus.Public);
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpCmsKitBlazorPublicFluentDesignModule).Assembly);
        });
    }
} 