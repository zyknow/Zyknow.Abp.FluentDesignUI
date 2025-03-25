namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Toolbars;

public interface IToolbarContributor
{
    Task ConfigureToolbarAsync(IToolbarConfigurationContext context);
}
