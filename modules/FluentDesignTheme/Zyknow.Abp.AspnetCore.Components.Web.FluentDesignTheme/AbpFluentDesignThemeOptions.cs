using Microsoft.FluentUI.AspNetCore.Components;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

public class AbpFluentDesignThemeOptions
{
    public DesignThemeModes DefaultThemeMode { get; set; } = DesignThemeModes.System;
    public OfficeColor DefaultColor { get; set; } = OfficeColor.Default;

    /// <summary>
    /// 是否启用多标签页
    /// </summary>
    public bool DefaultEnableMultipleTabs { get; set; }
}