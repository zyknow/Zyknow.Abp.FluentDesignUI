using Microsoft.FluentUI.AspNetCore.Components;

namespace Zyknow.Abp.FluentDesignUI;

public class AbpBreadcrumbItem(string text, string url = null, Icon? icon = null)
{
    public string Text { get; set; } = text;

    public Icon? Icon { get; set; } = icon;

    public string Url { get; set; } = url;
}
