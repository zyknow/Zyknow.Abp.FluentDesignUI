using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Zyknow.Abp.FluentDesignUI;

namespace Zyknow.Abp.PermissionManagement.Blazor.FluentDesignUI;

[DependsOn(
    typeof(AbpFluentDesignUIModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpPermissionManagementApplicationContractsModule)
)]
public class AbpPermissionManagementBlazorFluentDesignModule : AbpModule
{

}
