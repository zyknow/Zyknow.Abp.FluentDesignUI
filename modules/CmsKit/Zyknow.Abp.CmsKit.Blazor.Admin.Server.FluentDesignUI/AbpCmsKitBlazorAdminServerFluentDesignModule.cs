using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Modularity;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme.Bundling;
using Zyknow.Abp.CmsKit.Blazor.Admin.FluentDesignUI;
using Zyknow.Abp.CmsKit.Blazor.Common.Server.FluentDesignUI;

namespace Zyknow.Abp.CmsKit.Blazor.Admin.Server.FluentDesignUI;

[DependsOn(
    typeof(AbpCmsKitBlazorAdminFluentDesignModule),
    typeof(AbpAspNetCoreComponentsServerFluentDesignThemeModule),
    typeof(AbpCmsKitBlazorCommonServerFluentDesignModule)
)]
public class AbpCmsKitBlazorAdminServerFluentDesignModule : AbpModule
{
}