using Volo.Abp.Settings;

namespace Simple.Settings;

public class SimpleSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SimpleSettings.MySetting1));
    }
}
