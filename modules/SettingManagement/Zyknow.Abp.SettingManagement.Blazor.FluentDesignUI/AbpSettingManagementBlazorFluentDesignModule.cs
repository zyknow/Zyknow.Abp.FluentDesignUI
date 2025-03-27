using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.UI.Navigation;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Routing;
using Zyknow.Abp.GroupComponent.Abstract.FluentDesignUI;
using Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI.Settings;

namespace Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI;

[DependsOn(
    typeof(AbpAutoMapperModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebFluentDesignThemeModule),
    typeof(AbpGroupComponentAbstractFluentDesignModule)
)]
public class AbpSettingManagementBlazorFluentDesignModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpSettingManagementBlazorFluentDesignModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<SettingManagementBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new SettingManagementMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpSettingManagementBlazorFluentDesignModule).Assembly);
        });

        Configure<GroupComponentOptions>(options =>
        {
            options.Contributors.Add(new FluentDesignSettingDefaultPageContributor());
            options.Contributors.Add(new FluentDesignTimeZonePageContributor());
        });
    }
}
