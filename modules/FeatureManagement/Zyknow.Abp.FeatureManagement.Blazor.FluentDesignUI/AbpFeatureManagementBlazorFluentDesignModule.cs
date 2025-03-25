using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Features;
using Volo.Abp.Modularity;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

namespace Zyknow.Abp.FeatureManagement.Blazor.FluentDesignUI;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebFluentDesignThemeModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpFeaturesModule)
)]
public class AbpFeatureManagementBlazorFluentDesignModule : AbpModule
{
}