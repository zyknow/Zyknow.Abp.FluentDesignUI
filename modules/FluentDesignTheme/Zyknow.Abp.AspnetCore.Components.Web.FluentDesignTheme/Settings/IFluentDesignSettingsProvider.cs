namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Settings;

public interface IFluentDesignSettingsProvider
{
    public AbpFluentDesignThemeOptions Theme { get; }

    Task TriggerSettingChanged();

    public event FluentDesignSettingsProvider.FluentDesignSettingChangedHandler SettingChanged;
}