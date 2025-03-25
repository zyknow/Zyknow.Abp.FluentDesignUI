using Microsoft.FluentUI.AspNetCore.Components;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

public class AbpFluentDesignThemeOptions
{
    public DesignThemeModes ThemeMode { get; set; } = DesignThemeModes.Dark;
    public OfficeColor Color { get; set; } = OfficeColor.Default;
    public MenuPlacement Placement { get; set; } = MenuPlacement.Left;

    /// <summary>
    /// 是否启用多标签页
    /// </summary>
    public bool EnableMultipleTabs { get; set; }
}