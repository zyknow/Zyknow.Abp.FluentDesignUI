using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Zyknow.Abp.Account.Blazor.FluentDesignUI.Profiles;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Routing;
using Zyknow.Abp.GroupComponent.FluentDesignUI;

namespace Zyknow.Abp.Account.Blazor.FluentDesignUI;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebFluentDesignThemeModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpAutoMapperModule)
)]
public class AbpAccountBlazorFluentDesignModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpAccountBlazorFluentDesignModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<AbpAccountBlazorFluentDesignAutoMapperProfile>(validate: true);
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpAccountBlazorFluentDesignModule).Assembly);
        });

        Configure<GroupComponentOptions>(options =>
        {
            options.Contributors.Add(new FluentDesignPasswordManagementGroupContributor());
            options.Contributors.Add(new FluentDesignProfileManagementGroupContributor());
        });
    }
}