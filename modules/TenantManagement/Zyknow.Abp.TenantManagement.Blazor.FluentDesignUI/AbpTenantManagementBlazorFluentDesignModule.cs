using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.TenantManagement;
using Volo.Abp.Threading;
using Volo.Abp.UI.Navigation;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Routing;
using Zyknow.Abp.FeatureManagement.Blazor.FluentDesignUI;

namespace Zyknow.Abp.TenantManagement.Blazor.FluentDesignUI;


[DependsOn(
    typeof(AbpAutoMapperModule),
    typeof(AbpTenantManagementApplicationContractsModule),
    typeof(AbpFeatureManagementBlazorFluentDesignModule)
)]
public class AbpTenantManagementBlazorFluentDesignModule : AbpModule
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpTenantManagementBlazorFluentDesignModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<AbpTenantManagementBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new TenantManagementBlazorMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpTenantManagementBlazorFluentDesignModule).Assembly);
        });
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToUi(
                    TenantManagementModuleExtensionConsts.ModuleName,
                    TenantManagementModuleExtensionConsts.EntityNames.Tenant,
                    createFormTypes: new[] { typeof(TenantCreateDto) },
                    editFormTypes: new[] { typeof(TenantUpdateDto) }
                );
        });
    }
}
