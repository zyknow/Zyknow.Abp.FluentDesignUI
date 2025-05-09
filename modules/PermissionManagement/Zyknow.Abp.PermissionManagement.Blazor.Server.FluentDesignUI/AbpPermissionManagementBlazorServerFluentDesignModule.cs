﻿using Volo.Abp.Modularity;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;
using Zyknow.Abp.PermissionManagement.Blazor.FluentDesignUI;

namespace Zyknow.Abp.PermissionManagement.Blazor.Server.FluentDesignUI;
[DependsOn(
    typeof(AbpPermissionManagementBlazorFluentDesignModule),
    typeof(AbpAspNetCoreComponentsServerFluentDesignThemeModule)
)]
public class AbpPermissionManagementBlazorServerFluentDesignModule : AbpModule
{
}
