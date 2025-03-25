using Microsoft.FluentUI.AspNetCore.Components.Icons.Regular;
using Simple.Localization;
using Simple.MultiTenancy;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Extensions;
using Zyknow.Abp.TenantManagement.Blazor.FluentDesignUI;

namespace Simple.Blazor.Client.Navigation;

public class SimpleMenuContributor(IConfiguration configuration) : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }

        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<SimpleResource>();

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 6;

        context.Menu.AddItem(new ApplicationMenuItem(
            SimpleMenus.Home,
            l["Home"],
            "/",
            order: 1
        ).SetFluentIcon(new Size24.Home()));

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        // administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        // administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);
    }

    private async Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        if (OperatingSystem.IsBrowser())
        {
            //Blazor wasm menu items

            var authServerUrl = configuration["AuthServer:Authority"] ?? "";
            var accountResource = context.GetLocalizer<AccountResource>();

            context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", accountResource["MyAccount"],
                    $"{authServerUrl.EnsureEndsWith('/')}Account/Manage", icon: "fa fa-cog", order: 900,
                    target: "_blank")
                .RequireAuthenticated());
        }
        else
        {
            //Blazor server menu items
        }

        await Task.CompletedTask;
    }
}