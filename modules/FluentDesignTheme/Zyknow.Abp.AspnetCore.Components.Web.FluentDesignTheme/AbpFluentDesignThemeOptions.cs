using Microsoft.FluentUI.AspNetCore.Components;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

public class AbpFluentDesignThemeOptions
{
    public FluentLayoutTheme DefaultFluentLayoutTheme { get; set; } = FluentLayoutTheme.Default;
    public DesignThemeModes DefaultThemeMode { get; set; } = DesignThemeModes.System;
    public OfficeColor DefaultColor { get; set; } = OfficeColor.Default;
    public bool RenderModeChangeEnabled { get; set; } = true;
    public bool DefaultEnableMultipleTabs { get; set; }
}