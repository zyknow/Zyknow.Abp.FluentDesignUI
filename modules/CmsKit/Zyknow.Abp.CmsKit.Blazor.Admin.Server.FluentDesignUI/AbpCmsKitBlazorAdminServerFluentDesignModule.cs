using Volo.Abp.Modularity;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;
using Zyknow.Abp.CmsKit.Blazor.Admin.FluentDesignUI;

namespace Zyknow.Abp.CmsKit.Blazor.Admin.Server.FluentDesignUI;

[DependsOn(
    typeof(AbpCmsKitBlazorAdminFluentDesignModule),
    typeof(AbpAspNetCoreComponentsServerFluentDesignThemeModule)
)]
public class AbpCmsKitBlazorAdminServerFluentDesignModule : AbpModule
{
}