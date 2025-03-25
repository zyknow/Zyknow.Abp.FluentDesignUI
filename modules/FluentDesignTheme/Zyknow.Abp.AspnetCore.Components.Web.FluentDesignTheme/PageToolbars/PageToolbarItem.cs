using Volo.Abp;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.PageToolbars;

public class PageToolbarItem(
    Type componentType,
    Dictionary<string, object>? arguments = null,
    int order = 0)
{
    public Type ComponentType { get; } = Check.NotNull(componentType, nameof(componentType));

    public Dictionary<string, object>? Arguments { get; set; } = arguments;

    public int Order { get; set; } = order;
}
