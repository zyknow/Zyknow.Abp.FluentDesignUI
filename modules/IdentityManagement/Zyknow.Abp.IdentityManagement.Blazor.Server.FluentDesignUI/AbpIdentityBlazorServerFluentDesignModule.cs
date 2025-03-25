using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
