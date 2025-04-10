using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.UI.Navigation;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Extensions;

public static class ApplicationMenuItemExtensions
{
    public const string FluentIconKey = "FluentIcon";
    public const string ParentKey = "Parent";
    public const string ExpandedKey = "Expanded";

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

    // get parent
    public static ApplicationMenuItem? GetParent(this ApplicationMenuItem item)
    {
        if (item.CustomData.TryGetValue(ParentKey, out var parent))
        {
            return (ApplicationMenuItem)parent;
        }

        return null;
    }

    // set parent
    public static ApplicationMenuItem SetParent(
        this ApplicationMenuItem item,
        ApplicationMenuItem parent)
    {
        item.WithCustomData(ParentKey, parent);

        return item;
    }

    // get expanded
    public static bool GetExpanded(this ApplicationMenuItem item)
    {
        if (item.CustomData.TryGetValue(ExpandedKey, out var expanded))
        {
            return (bool)expanded;
        }

        return false;
    }

    // set expanded
    public static ApplicationMenuItem SetExpanded(
        this ApplicationMenuItem item,
        bool expanded)
    {
        item.WithCustomData(ExpandedKey, expanded);

        return item;
    }
}