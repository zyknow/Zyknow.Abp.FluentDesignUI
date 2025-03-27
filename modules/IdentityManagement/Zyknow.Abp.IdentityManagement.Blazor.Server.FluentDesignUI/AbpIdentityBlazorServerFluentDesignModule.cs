using Volo.Abp.Modularity;
using Zyknow.Abp.IdentityManagement.Blazor.FluentDesignUI;
using Zyknow.Abp.PermissionManagement.Blazor.FluentDesignUI;

namespace Zyknow.Abp.IdentityManagement.Blazor.Server.FluentDesignUI;
[DependsOn(
    typeof(AbpIdentityBlazorFluentDesignModule),
    typeof(AbpPermissionManagementBlazorFluentDesignModule)
)]
public class AbpIdentityBlazorServerFluentDesignModule : AbpModule
{

}
