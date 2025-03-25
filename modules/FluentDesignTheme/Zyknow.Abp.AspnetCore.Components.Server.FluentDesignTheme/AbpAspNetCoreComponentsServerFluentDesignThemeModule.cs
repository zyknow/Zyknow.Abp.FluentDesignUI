using Volo.Abp.AspNetCore.Components.Server;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages;
using Volo.Abp.Modularity;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme.Bundling;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Toolbars;

namespace Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerModule),
    typeof(AbpAspNetCoreMvcUiPackagesModule),
    typeof(AbpAspNetCoreMvcUiBundlingModule),
    typeof(AbpAspNetCoreComponentsWebFluentDesignThemeModule)
)]
public class AbpAspNetCoreComponentsServerFluentDesignThemeModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(BlazorStandardBundles.Styles.Global, bundle =>
                {
                    bundle.AddContributors(typeof(BlazorGlobalStyleContributor));
                });

            options
                .ScriptBundles
                .Add(BlazorStandardBundles.Scripts.Global, bundle =>
                {
                    bundle.AddContributors(typeof(BlazorGlobalScriptContributor));
                });
            
            options
                .StyleBundles
                .Add(BlazorFluentDesignThemeBundles.Styles.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorStandardBundles.Styles.Global)
                        .AddContributors(typeof(BlazorFluentDesignThemeStyleContributor));
                });

            options
                .ScriptBundles
                .Add(BlazorFluentDesignThemeBundles.Scripts.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorStandardBundles.Scripts.Global)
                        .AddContributors(typeof(BlazorFluentDesignThemeScriptContributor));
                });
        });
        
        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new FluentDesignThemeToolbarContributor());
        });
    }
}
