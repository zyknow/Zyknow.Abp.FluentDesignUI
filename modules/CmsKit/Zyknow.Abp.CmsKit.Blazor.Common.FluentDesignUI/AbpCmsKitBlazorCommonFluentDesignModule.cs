using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit;
using Volo.CmsKit.Localization;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

namespace Zyknow.Abp.CmsKit.Blazor.Common.FluentDesignUI;

[DependsOn(
    typeof(AbpAutoMapperModule),
    typeof(AbpAspNetCoreComponentsWebFluentDesignThemeModule)
)]
public class AbpCmsKitBlazorCommonFluentDesignModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpCmsKitBlazorCommonFluentDesignModule>();
        });

        context.Services.Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CmsKitResource>()
                .AddVirtualJson("/Localization/Resources/CmsKit");
        });
    }
}