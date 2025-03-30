using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Zyknow.Abp.Account.Blazor.FluentDesignUI;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Routing;

namespace Zyknow.Abp.Account.Blazor.Server.FluentDesignUI;

[DependsOn(
    typeof(AbpAccountBlazorFluentDesignModule),
    typeof(AbpAspNetCoreComponentsServerFluentDesignThemeModule),
    typeof(AbpIdentityAspNetCoreModule),
    typeof(AbpMultiTenancyModule),
    typeof(AbpAspNetCoreMultiTenancyModule)
    )]
public class AbpAccountBlazorServerFluentDesignModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAccountBlazorServerFluentDesignModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        
        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpAccountBlazorServerFluentDesignModule).Assembly);
        });
    }
}