using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.UI.Navigation;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Extensions;

public static class ApplicationMenuItemExtensions
{
    const string FluentIconKey = "FluentIcon";

    public static ApplicationMenuItem SetFluentIcon(
        this ApplicationMenuItem item,
        Icon icon)
    {
        item.WithCustomData(FluentIconKey, icon);

        return item;
    }

    public static Icon? GetFluentIcon(this ApplicationMenuItem item)
    {
        if (item.CustomData.TryGetValue(FluentIconKey, out var icon))
        {
            return (Icon)icon;
        }

        return null;
    }
}