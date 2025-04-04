using Volo.Abp.Modularity;
using Zyknow.Abp.IdentityManagement.Blazor.FluentDesignUI;
using Zyknow.Abp.IdentityManagement.Blazor.Server.FluentDesignUI;
using Zyknow.Abp.PermissionManagement.Blazor.FluentDesignUI;
using Zyknow.SharpAbp.IdentityManagement.Blazor.FluentDesignUI;

namespace Zyknow.SharpAbp.IdentityManagement.Blazor.Server.FluentDesignUI;

[DependsOn(
    typeof(AbpIdentityBlazorServerFluentDesignModule),
    typeof(SharpAbpIdentityBlazorFluentDesignModule)
)]
public class SharpAbpIdentityBlazorServerFluentDesignModule : AbpModule
{
}