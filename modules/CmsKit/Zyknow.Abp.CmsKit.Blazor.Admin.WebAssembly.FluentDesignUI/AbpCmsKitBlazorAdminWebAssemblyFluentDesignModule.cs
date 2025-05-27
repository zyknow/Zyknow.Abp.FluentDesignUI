using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.CmsKit.Admin;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.WebAssembly.FluentDesignTheme;
using Zyknow.Abp.CmsKit.Blazor.Admin.FluentDesignUI;
using Zyknow.Abp.CmsKit.Blazor.Common.WebAssembly.FluentDesignUI;

namespace Zyknow.Abp.CmsKit.Blazor.Admin.WebAssembly.FluentDesignUI;

[DependsOn(
    typeof(AbpCmsKitBlazorAdminFluentDesignModule),
    typeof(CmsKitAdminHttpApiClientModule),
    typeof(AbpCmsKitBlazorCommonWebAssemblyFluentDesignModule)
)]
public class AbpCmsKitBlazorAdminWebAssemblyFluentDesignModule : AbpModule
{
}