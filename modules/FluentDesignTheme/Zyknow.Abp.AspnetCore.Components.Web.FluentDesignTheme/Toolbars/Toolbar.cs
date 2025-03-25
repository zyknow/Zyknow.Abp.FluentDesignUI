using Volo.Abp;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Toolbars;

public class Toolbar(string name)
{
    public string Name { get; } = Check.NotNull(name, nameof(name));

    public List<ToolbarItem> Items { get; } = new();
}
