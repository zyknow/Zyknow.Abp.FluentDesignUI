using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.PageToolbars;

public class PageToolbarManager(IServiceScopeFactory serviceScopeFactory) : IPageToolbarManager, ITransientDependency
{
    protected IServiceScopeFactory ServiceScopeFactory { get; } = serviceScopeFactory;

    public virtual async Task<PageToolbarItem[]> GetItemsAsync(PageToolbar? toolbar)
    {
        if (toolbar == null || !toolbar.Contributors.Any())
        {
            return [];
        }

        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var context = new PageToolbarContributionContext(scope.ServiceProvider);

            foreach (var contributor in toolbar.Contributors)
            {
                await contributor.ContributeAsync(context);
            }

            return context.Items.OrderBy(i => i.Order).ToArray();
        }
    }
}
