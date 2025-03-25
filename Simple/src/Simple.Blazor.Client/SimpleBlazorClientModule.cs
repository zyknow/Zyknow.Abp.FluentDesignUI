using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Simple.Blazor.Client.Navigation;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Routing;
using Zyknow.Abp.AspnetCore.Components.WebAssembly.FluentDesignTheme;
using Zyknow.Abp.FeatureManagement.Blazor.WebAssembly.FluentDesignUI;


namespace Simple.Blazor.Client;

[DependsOn(
    typeof(AbpAutofacWebAssemblyModule),
    typeof(SimpleHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyFluentDesignThemeModule),
    typeof(AbpFeatureManagementBlazorWebAssemblyFluentDesignModule)
)]
public class SimpleBlazorClientModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpAspNetCoreComponentsWebOptions>(options =>
        {
            options.IsBlazorWebApp = true;
        });
    }
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
        var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

        ConfigureAuthentication(builder);
        ConfigureHttpClient(context, environment);
        ConfigureRouter(context);
        ConfigureMenu(context);
        ConfigureAutoMapper(context);
        ConfigureBundles();
    }


    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(SimpleBlazorClientModule).Assembly;
            options.AdditionalAssemblies.Add(typeof(SimpleBlazorClientModule).Assembly);
        });
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new SimpleMenuContributor(context.Services.GetConfiguration()));
        });
    }


    private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddBlazorWebAppServices();
    }
    
    private static void ConfigureHttpClient(ServiceConfigurationContext context, IWebAssemblyHostEnvironment environment)
    {
        context.Services.AddTransient(sp => new HttpClient
        {
            BaseAddress = new Uri(environment.BaseAddress)
        });
    }

    private void ConfigureAutoMapper(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<SimpleBlazorClientModule>();
        });
    }

    private void ConfigureBundles()
    {
        // Configure<AbpBundlingOptions>(options =>
        // {
        //     var globalStyles = options.StyleBundles.Get(BlazorWebAssemblyStandardBundles.Styles.Global);
        //     globalStyles.AddContributors(typeof(SimpleStyleBundleContributor));
        //
        //     var globalScripts = options.ScriptBundles.Get(BlazorWebAssemblyStandardBundles.Scripts.Global);
        //     globalScripts.AddContributors(typeof(SimpleScriptBundleContributor));
        // });
    }
}
