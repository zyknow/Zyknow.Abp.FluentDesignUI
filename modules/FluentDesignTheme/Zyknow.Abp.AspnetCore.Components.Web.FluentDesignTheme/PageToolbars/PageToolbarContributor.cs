namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.PageToolbars;

public abstract class PageToolbarContributor : IPageToolbarContributor
{
    public abstract Task ContributeAsync(PageToolbarContributionContext context);
}
