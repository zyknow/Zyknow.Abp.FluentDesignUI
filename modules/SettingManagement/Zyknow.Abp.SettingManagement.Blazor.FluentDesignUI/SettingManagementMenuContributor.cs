using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.FluentUI.AspNetCore.Components.Icons.Regular;
using Volo.Abp.Features;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Blazor;
using Volo.Abp.SettingManagement.Localization;
using Volo.Abp.UI.Navigation;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Extensions;

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
        var settingManagementPageOptions = context.ServiceProvider.GetRequiredService<IOptions<SettingManagementComponentOptions>>().Value;
        var settingPageCreationContext = new SettingComponentCreationContext(context.ServiceProvider);
        if (!settingManagementPageOptions.Contributors.Any() ||
            !(await CheckAnyOfPagePermissionsGranted(settingManagementPageOptions, settingPageCreationContext))
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
        SettingManagementComponentOptions settingManagementComponentOptions,
        SettingComponentCreationContext settingComponentCreationContext)
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
