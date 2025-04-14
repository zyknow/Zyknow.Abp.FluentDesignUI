using Microsoft.FluentUI.AspNetCore.Components;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

public interface IThemeStore : IBlazorStore
{
    DesignThemeModes ThemeMode { get; set; }
    OfficeColor Color { get; set; }
    bool EnableMultipleTabs { get; set; }
    FluentLayoutTheme LayoutTheme { get; set; }
    event EventHandler? ThemeModeChanged;
}