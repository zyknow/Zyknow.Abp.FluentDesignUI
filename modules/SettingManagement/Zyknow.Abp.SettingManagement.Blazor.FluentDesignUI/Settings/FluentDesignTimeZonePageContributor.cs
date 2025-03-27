using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Blazor;
using Volo.Abp.SettingManagement.Localization;
using Volo.Abp.Timing;
using Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI.Pages.SettingManagement.TimeZoneSettingGroup;

namespace Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI.Settings;

public class FluentDesignTimeZonePageContributor : ISettingComponentContributor
{
    public virtual async Task<bool> CheckPermissionsAsync(SettingComponentCreationContext context)
    {
        var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();

        return await authorizationService.IsGrantedAsync(SettingManagementPermissions.TimeZone);
    }

    public virtual Task ConfigureAsync(SettingComponentCreationContext context)
    {
        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<AbpSettingManagementResource>>();
        var clock = context.ServiceProvider.GetRequiredService<IClock>();
        if (clock.SupportsMultipleTimezone)
        {
            context.Groups.Add(
                new SettingComponentGroup(
                    "Volo.Abp.TimeZone",
                    l["Menu:TimeZone"],
                    typeof(TimeZoneSettingGroupViewComponent)
                )
            );
        }

        return Task.CompletedTask;
    }
}
