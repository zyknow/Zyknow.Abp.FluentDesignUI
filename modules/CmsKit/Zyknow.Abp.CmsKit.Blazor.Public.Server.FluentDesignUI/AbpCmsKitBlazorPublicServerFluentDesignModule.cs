using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.CmsKit.Public;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme.Bundling;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;
using Zyknow.Abp.CmsKit.Blazor.Common.Server.FluentDesignUI;
using Zyknow.Abp.CmsKit.Blazor.Public.FluentDesignUI;

namespace Zyknow.Abp.CmsKit.Blazor.Public.Server.FluentDesignUI;

[DependsOn(
    typeof(AbpCmsKitBlazorPublicFluentDesignModule),
    typeof(AbpCmsKitBlazorCommonServerFluentDesignModule)
)]
public class AbpCmsKitBlazorPublicServerFluentDesignModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles.Get(BlazorStandardBundles.Styles.Global)
                .AddFiles("/_content/Zyknow.Abp.CmsKit.Blazor.Public.FluentDesignUI/cms-kit/cms-public-kit.css");
        });
    }
}