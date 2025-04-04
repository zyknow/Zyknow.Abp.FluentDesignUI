using SharpAbp.Abp.Identity;
using Volo.Abp.Modularity;
using Zyknow.Abp.IdentityManagement.Blazor.WebAssembly.FluentDesignUI;
using Zyknow.SharpAbp.IdentityManagement.Blazor.FluentDesignUI;

namespace Zyknow.SharpAbp.IdentityManagement.Blazor.WebAssembly.FluentDesignUI;
[DependsOn(
    typeof(AbpIdentityBlazorWebAssemblyFluentDesignModule),
    typeof(SharpAbpIdentityBlazorFluentDesignModule),
    typeof(IdentityHttpApiClientModule)
)]
public class SharpAbpIdentityBlazorWebAssemblyFluentDesignModule : AbpModule
{
}

