using Volo.Abp;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.PageToolbars;

public class PageToolbarContributionContext(IServiceProvider serviceProvider)
{
    public IServiceProvider ServiceProvider { get; } = Check.NotNull(serviceProvider, nameof(serviceProvider));

    public PageToolbarItemList Items { get; } = new();
}
