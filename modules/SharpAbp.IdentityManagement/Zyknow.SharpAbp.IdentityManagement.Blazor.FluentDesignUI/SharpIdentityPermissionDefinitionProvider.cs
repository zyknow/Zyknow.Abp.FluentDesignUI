using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;
using SharpAbpIdentityPermissions = SharpAbp.Abp.Identity.IdentityPermissions;

namespace Zyknow.SharpAbp.IdentityManagement.Blazor.FluentDesignUI;

public class SharpIdentityPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var sharpAbpIdentityGroup = context.GetGroup(SharpAbpIdentityPermissions.GroupName);

        if (sharpAbpIdentityGroup == null)
            return;

        // remove sharp identity group
        context.RemoveGroup(sharpAbpIdentityGroup.Name);

        var voloAbpIdentityGroup = context.GetGroup(IdentityPermissions.GroupName);
        if (voloAbpIdentityGroup == null)
        {
            return;
        }

        //identityClaimType
        var identityClaimTypePermission = voloAbpIdentityGroup.AddPermission(
            SharpAbpIdentityPermissions.IdentityClaimTypes.Default,
            L($"Permission:{SharpAbpIdentityPermissions.IdentityClaimTypes.Default}"));

        identityClaimTypePermission.AddChild(
            SharpAbpIdentityPermissions.IdentityClaimTypes.Create,
            L($"Permission:{SharpAbpIdentityPermissions.IdentityClaimTypes.Create}"));

        identityClaimTypePermission.AddChild(
            SharpAbpIdentityPermissions.IdentityClaimTypes.Update,
            L($"Permission:{SharpAbpIdentityPermissions.IdentityClaimTypes.Update}"));

        identityClaimTypePermission.AddChild(
            SharpAbpIdentityPermissions.IdentityClaimTypes.Delete,
            L($"Permission:{SharpAbpIdentityPermissions.IdentityClaimTypes.Delete}"));

        //IdentitySecurityLogs
        var identitySecurityLogsPermission = voloAbpIdentityGroup.AddPermission(
            SharpAbpIdentityPermissions.IdentitySecurityLogs.Default,
            L($"Permission:{SharpAbpIdentityPermissions.IdentitySecurityLogs.Default}"));

        identitySecurityLogsPermission.AddChild(
            SharpAbpIdentityPermissions.IdentitySecurityLogs.Create,
            L($"Permission:{SharpAbpIdentityPermissions.IdentitySecurityLogs.Create}"));

        identitySecurityLogsPermission.AddChild(
            SharpAbpIdentityPermissions.IdentitySecurityLogs.Update,
            L($"Permission:{SharpAbpIdentityPermissions.IdentitySecurityLogs.Update}"));

        identitySecurityLogsPermission.AddChild(
            SharpAbpIdentityPermissions.IdentitySecurityLogs.Delete,
            L($"Permission:{SharpAbpIdentityPermissions.IdentitySecurityLogs.Delete}"));

        //OrganizationUnits
        var organizationUnitsPermission = voloAbpIdentityGroup.AddPermission(
            SharpAbpIdentityPermissions.OrganizationUnits.Default,
            L($"Permission:{SharpAbpIdentityPermissions.OrganizationUnits.Default}"));

        organizationUnitsPermission.AddChild(
            SharpAbpIdentityPermissions.OrganizationUnits.Create,
            L($"Permission:{SharpAbpIdentityPermissions.OrganizationUnits.Create}"));

        organizationUnitsPermission.AddChild(
            SharpAbpIdentityPermissions.OrganizationUnits.Update,
            L($"Permission:{SharpAbpIdentityPermissions.OrganizationUnits.Update}"));

        organizationUnitsPermission.AddChild(
            SharpAbpIdentityPermissions.OrganizationUnits.Delete,
            L($"Permission:{SharpAbpIdentityPermissions.OrganizationUnits.Delete}"));

        //Identity settings
        var settingsPermission = voloAbpIdentityGroup.AddPermission(
            SharpAbpIdentityPermissions.Settings.Default,
            L($"Permission:{SharpAbpIdentityPermissions.Settings.Default}"));

        settingsPermission.AddChild(
            SharpAbpIdentityPermissions.Settings.Update,
            L($"Permission:{SharpAbpIdentityPermissions.Settings.Update}"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IdentityResource>(name);
    }
}