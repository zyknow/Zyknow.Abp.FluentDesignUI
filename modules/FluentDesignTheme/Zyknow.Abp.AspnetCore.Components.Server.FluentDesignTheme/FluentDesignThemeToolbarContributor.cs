using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme.Themes.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Toolbars;

namespace Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;


public class FluentDesignThemeToolbarContributor: IToolbarContributor
{
    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == StandardToolbars.Main)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitch)));
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginDisplay)));
        }

        return Task.CompletedTask;
    }
}
