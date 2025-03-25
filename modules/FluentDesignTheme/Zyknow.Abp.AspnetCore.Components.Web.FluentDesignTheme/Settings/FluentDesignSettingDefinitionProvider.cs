using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.Settings;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Settings;

public class FluentDesignSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(FluentDesignSettingNames.MenuPlacement, MenuPlacement.Left.ToString()),
            // TODO: Theme?
            new SettingDefinition(FluentDesignSettingNames.MenuTheme, DesignThemeModes.Dark.ToString())
        );
    }
}
