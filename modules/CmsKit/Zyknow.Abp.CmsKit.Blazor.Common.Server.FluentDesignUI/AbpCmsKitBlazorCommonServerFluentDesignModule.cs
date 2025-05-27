using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Modularity;
using Volo.CmsKit;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme.Bundling;
using Zyknow.Abp.CmsKit.Blazor.Common.FluentDesignUI;

namespace Zyknow.Abp.CmsKit.Blazor.Common.Server.FluentDesignUI;

[DependsOn(
    typeof(AbpCmsKitBlazorCommonFluentDesignModule),
    typeof(AbpAspNetCoreComponentsServerFluentDesignThemeModule),
    typeof(CmsKitCommonApplicationContractsModule)
)]
public class AbpCmsKitBlazorCommonServerFluentDesignModule : AbpModule
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