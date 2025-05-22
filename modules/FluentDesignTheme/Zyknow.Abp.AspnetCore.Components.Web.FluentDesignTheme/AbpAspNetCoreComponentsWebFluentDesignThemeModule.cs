using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Ui.LayoutHooks;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Themes.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Toolbars;
using Zyknow.Abp.FluentDesignUI;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

[DependsOn(
    typeof(AbpFluentDesignUIModule),
    typeof(AbpUiNavigationModule)
)]
public class AbpAspNetCoreComponentsWebFluentDesignThemeModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpFluentDesignThemeOptions>(opt =>
        {
            if (opt.Layouts.IsNullOrEmpty())
            {
                opt.Layouts.Add(new AbpFluentDesignLayoutInfo
                {
                    Name = LayoutConstants.Account,
                    Type = typeof(AccountLayout),
                });

                opt.Layouts.Add(new AbpFluentDesignLayoutInfo
                {
                    Name = LayoutConstants.Application,
                    Type = typeof(DefaultLayout),
                });

                opt.Layouts.Add(new AbpFluentDesignLayoutInfo
                {
                    Name = LayoutConstants.Public,
                    Type = typeof(DefaultLayout),
                });

                opt.Layouts.Add(new AbpFluentDesignLayoutInfo
                {
                    Name = LayoutConstants.Empty,
                    Type = typeof(EmptyLayout),
                });
            }
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new FluentDesignThemeWebToolbarContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpAspNetCoreComponentsWebFluentDesignThemeModule>();
        });

        context.Services.Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AbpUiResource>()
                .AddVirtualJson("/Localization/Resources/Ui");
        });

        context.Services.AddScoped<IBlazorStore>(provider => provider.GetRequiredService<IThemeStore>());
    }
}