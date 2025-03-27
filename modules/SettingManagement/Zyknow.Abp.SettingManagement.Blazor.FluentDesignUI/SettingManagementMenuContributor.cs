using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.FluentUI.AspNetCore.Components.Icons.Regular;
using Volo.Abp.Features;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Localization;
using Volo.Abp.UI.Navigation;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Extensions;
using Zyknow.Abp.GroupComponent.Abstract.FluentDesignUI;

namespace Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI;

public class SettingManagementMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var groupOptions = context.ServiceProvider.GetRequiredService<IOptions<GroupComponentOptions>>().Value;
        var settingManagementContributors = groupOptions.Contributors
            .Where(x => x.GroupKey == SettingGroupComponentContributorBase.SettingGroupKey).ToList();
        var settingPageCreationContext = new GroupComponentCreationContext(context.ServiceProvider);
        if (!settingManagementContributors.Any() ||
            !(await CheckAnyOfPagePermissionsGranted(groupOptions, settingPageCreationContext))
           )
        {
            return;
        }

        var l = context.GetLocalizer<AbpSettingManagementResource>();

        context.Menu.GetAdministration().TryRemoveMenuItem(SettingManagementMenus.GroupName);

        context.Menu
            .GetAdministration()
            .AddItem(
                new ApplicationMenuItem(
                        SettingManagementMenus.GroupName,
                        l["Settings"],
                        "~/setting-management"
                    )
                    .SetFluentIcon(new Size24.WrenchSettings())
                    .RequireFeatures(SettingManagementFeatures.Enable)
            );
    }

    protected virtual async Task<bool> CheckAnyOfPagePermissionsGranted(
        GroupComponentOptions settingManagementComponentOptions,
        GroupComponentCreationContext settingComponentCreationContext)
    {
        foreach (var contributor in settingManagementComponentOptions.Contributors)
        {
            if (await contributor.CheckPermissionsAsync(settingComponentCreationContext))
            {
                return true;
            }
        }

        return false;
    }
}