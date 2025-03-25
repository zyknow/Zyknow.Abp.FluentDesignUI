using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Themes.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Toolbars;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

public class FluentDesignThemeWebToolbarContributor : IToolbarContributor
{
    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == StandardToolbars.Main)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(SettingPanelButton), order: 1));
        }

        return Task.CompletedTask;
    }
}