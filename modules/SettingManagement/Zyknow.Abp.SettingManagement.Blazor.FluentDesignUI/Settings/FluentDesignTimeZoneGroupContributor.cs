using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Localization;
using Volo.Abp.Timing;
using Zyknow.Abp.GroupComponent.FluentDesignUI;
using Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI.Pages.SettingManagement.Groups;

namespace Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI.Settings;

public class FluentDesignTimeZoneGroupContributor : SettingGroupComponentContributorBase
{
    public override async Task<bool> CheckPermissionsAsync(GroupComponentCreationContext context)
    {
        var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();

        return await authorizationService.IsGrantedAsync(SettingManagementPermissions.TimeZone);
    }

    public override Task ConfigureAsync(GroupComponentCreationContext context)
    {
        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<AbpSettingManagementResource>>();
        var clock = context.ServiceProvider.GetRequiredService<IClock>();
        if (clock.SupportsMultipleTimezone)
        {
            context.Groups.Add(
                new ComponentGroup(
                    "Volo.Abp.TimeZone",
                    l["Menu:TimeZone"],
                    typeof(TimeZoneSettingGroupComponent)
                )
            );
        }

        return Task.CompletedTask;
    }
}