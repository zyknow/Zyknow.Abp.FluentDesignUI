using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.Features;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Localization;
using Zyknow.Abp.GroupComponent.Abstract.FluentDesignUI;
using Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI.Pages.SettingManagement.Groups;

namespace Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI.Settings;

public class FluentDesignSettingGroupContributor : SettingGroupComponentContributorBase
{
    public override async Task ConfigureAsync(GroupComponentCreationContext context)
    {
        if (!await CheckPermissionsInternalAsync(context))
        {
            return;
        }

        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<AbpSettingManagementResource>>();
        context.Groups.Add(
            new ComponentGroup(
                "Volo.Abp.SettingManagement",
                l["Menu:Emailing"],
                typeof(EmailSettingGroupComponent)
            )
        );
    }

    public override async Task<bool> CheckPermissionsAsync(GroupComponentCreationContext context)
    {
        return await CheckPermissionsInternalAsync(context);
    }

    private async Task<bool> CheckPermissionsInternalAsync(GroupComponentCreationContext context)
    {
        if (!await CheckFeatureAsync(context))
        {
            return false;
        }

        var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();

        return await authorizationService.IsGrantedAsync(SettingManagementPermissions.Emailing);
    }

    private async Task<bool> CheckFeatureAsync(GroupComponentCreationContext context)
    {
        var currentTenant = context.ServiceProvider.GetRequiredService<ICurrentTenant>();

        if (!currentTenant.IsAvailable)
        {
            return true;
        }

        var featureCheck = context.ServiceProvider.GetRequiredService<IFeatureChecker>();

        return await featureCheck.IsEnabledAsync(SettingManagementFeatures.AllowChangingEmailSettings);
    }
}