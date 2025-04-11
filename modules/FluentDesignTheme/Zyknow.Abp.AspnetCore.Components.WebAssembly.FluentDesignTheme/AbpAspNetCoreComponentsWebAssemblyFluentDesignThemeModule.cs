using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.AspNetCore.Components.WebAssembly;
using Volo.Abp.Http.Client.IdentityModel.WebAssembly;
using Volo.Abp.Modularity;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Routing;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Toolbars;

namespace Zyknow.Abp.AspnetCore.Components.WebAssembly.FluentDesignTheme;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebFluentDesignThemeModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyModule),
    typeof(AbpHttpClientIdentityModelWebAssemblyModule)
)]
public class AbpAspNetCoreComponentsWebAssemblyFluentDesignThemeModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpAspNetCoreComponentsWebAssemblyFluentDesignThemeModule)
                .Assembly);
        });

        if (!context.Services.ExecutePreConfiguredActions<AbpAspNetCoreComponentsWebOptions>().IsBlazorWebApp)
        {
            Configure<AuthenticationOptions>(options =>
            {
                options.LoginUrl = "authentication/login";
                options.LogoutUrl = "authentication/logout";
            });
        }
    }
}