using Microsoft.FluentUI.AspNetCore.Components.Icons.Regular;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.UI.Navigation;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Extensions;
using IdentityPermissions = SharpAbp.Abp.Identity.IdentityPermissions;

namespace Zyknow.SharpAbp.IdentityManagement.Blazor.FluentDesignUI;

public class SharpAbpIdentityWebMainMenuContributor : IMenuContributor
{
    public virtual Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        var administrationMenu = context.Menu.GetAdministration();

        var identityMenuItem = administrationMenu.GetMenuItem(SharpAbpIdentityMenuNames.GroupName);

        var l = context.GetLocalizer<IdentityResource>();

        identityMenuItem.AddItem(new ApplicationMenuItem(
                SharpAbpIdentityMenuNames.SecurityLogs,
                l["Permission:SharpAbpIdentity.IdentitySecurityLogs"],
                url: "~/identity/security-logs")
            .RequirePermissions(IdentityPermissions.IdentitySecurityLogs.Default));

        identityMenuItem.AddItem(new ApplicationMenuItem(
                SharpAbpIdentityMenuNames.SecurityLogs,
                l["Permission:SharpAbpIdentity.IdentityClaimTypes"],
                url: "~/identity/claim-types")
            .RequirePermissions(IdentityPermissions.IdentityClaimTypes.Default));

        identityMenuItem.AddItem(new ApplicationMenuItem(
                SharpAbpIdentityMenuNames.OrganizationUnits,
                l["Permission:SharpAbpIdentity.IdentityOrganizationUnits"],
                url: "~/identity/organization-units")
            .RequirePermissions(IdentityPermissions.OrganizationUnits.Default));

        return Task.CompletedTask;
    }
}