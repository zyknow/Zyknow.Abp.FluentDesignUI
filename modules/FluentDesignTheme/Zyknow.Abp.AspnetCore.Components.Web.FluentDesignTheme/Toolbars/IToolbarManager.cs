namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Toolbars;

public interface IToolbarManager
{
    Task<Toolbar> GetAsync(string name);
}
