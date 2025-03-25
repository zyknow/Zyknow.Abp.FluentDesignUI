using Microsoft.FluentUI.AspNetCore.Components.Icons.Regular;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.Localization;
using Volo.Abp.UI.Navigation;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Extensions;

namespace Zyknow.Abp.TenantManagement.Blazor.FluentDesignUI;

public class TenantManagementBlazorMenuContributor : IMenuContributor
{
    public virtual Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        var administrationMenu = context.Menu.GetAdministration();
        administrationMenu.SetFluentIcon(new Size24.Wrench());

        var l = context.GetLocalizer<AbpTenantManagementResource>();

        var tenantManagementMenuItem = new ApplicationMenuItem(
            TenantManagementMenuNames.GroupName,
            l["Menu:TenantManagement"]
        ).SetFluentIcon(new Size24.PeopleTeam());
        administrationMenu.AddItem(tenantManagementMenuItem);

        tenantManagementMenuItem.AddItem(new ApplicationMenuItem(
            TenantManagementMenuNames.Tenants,
            l["Tenants"],
            url: "~/tenant-management/tenants"
        ).RequirePermissions(TenantManagementPermissions.Tenants.Default));

        return Task.CompletedTask;
    }
}