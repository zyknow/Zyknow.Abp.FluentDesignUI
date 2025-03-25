namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.PageToolbars;

public interface IPageToolbarManager
{
    Task<PageToolbarItem[]> GetItemsAsync(PageToolbar toolbar);
}
