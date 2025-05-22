using BlazorPro.BlazorSize;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

public class AbpFluentDesignThemeOptions
{
    public Type? DefaultLayoutFooterType { get; set; }
    public string MobileBreakpoint { get; set; } = Breakpoints.MediumDown;
    public FluentLayoutTheme DefaultFluentLayoutTheme { get; set; } = FluentLayoutTheme.Default;
    public DesignThemeModes DefaultThemeMode { get; set; } = DesignThemeModes.System;
    public OfficeColor DefaultColor { get; set; } = OfficeColor.Default;
    public bool RenderModeChangeEnabled { get; set; } = true;

    public bool DefaultEnableMultipleTabs { get; set; }

    public List<AbpFluentDesignLayoutInfo> Layouts { get; set; } = [];

    public List<string> GetLayoutsSelectList()
    {
        return Layouts.Select(x => x.Name).ToList();
    }

    public AbpFluentDesignLayoutInfo GetLayout(string name)
    {
        return Layouts.FirstOrDefault(x => x.Name == name) ?? Layouts.First(x => x.Name == LayoutConstants.Application);
    }
}