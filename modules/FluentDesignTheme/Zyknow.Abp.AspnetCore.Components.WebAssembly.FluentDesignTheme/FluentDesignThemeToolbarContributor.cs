using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Toolbars;
using Zyknow.Abp.AspnetCore.Components.WebAssembly.FluentDesignTheme.Themes.FluentDesignTheme;

namespace Zyknow.Abp.AspnetCore.Components.WebAssembly.FluentDesignTheme;

public class FluentDesignThemeToolbarContributor: IToolbarContributor
{
    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == StandardToolbars.Main)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitch)));

            //TODO: Can we find a different way to understand if authentication was configured or not?
            var authenticationStateProvider = context.ServiceProvider
                .GetService<AuthenticationStateProvider>();

            if (authenticationStateProvider != null)
            {
                context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginDisplay)));
            }
        }

        return Task.CompletedTask;
    }
}
