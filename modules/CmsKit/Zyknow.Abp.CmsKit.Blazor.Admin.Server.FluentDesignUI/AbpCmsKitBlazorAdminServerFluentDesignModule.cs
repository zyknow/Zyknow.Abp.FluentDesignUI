using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Modularity;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme.Bundling;
using Zyknow.Abp.CmsKit.Blazor.Admin.FluentDesignUI;

namespace Zyknow.Abp.CmsKit.Blazor.Admin.Server.FluentDesignUI;

[DependsOn(
    typeof(AbpCmsKitBlazorAdminFluentDesignModule),
    typeof(AbpAspNetCoreComponentsServerFluentDesignThemeModule)
)]
public class AbpCmsKitBlazorAdminServerFluentDesignModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Get(BlazorStandardBundles.Styles.Global)
                .AddContributors(typeof(CmsKitComponentsServerStyleContributor));
            
            options
                .ScriptBundles
                .Get(BlazorStandardBundles.Scripts.Global)
                .AddContributors(typeof(CmsKitComponentsServerScriptContributor));
        });
    }
}