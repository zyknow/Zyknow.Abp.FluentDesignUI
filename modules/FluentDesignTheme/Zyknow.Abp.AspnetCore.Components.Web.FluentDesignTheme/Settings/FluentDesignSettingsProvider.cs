using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Settings;

public class FluentDesignSettingsProvider : IFluentDesignSettingsProvider, IScopedDependency
{
    [Inject] public IOptions<AbpFluentDesignThemeOptions> Options { get; set; }

    public AbpFluentDesignThemeOptions Theme => Options.Value;

    public delegate Task FluentDesignSettingChangedHandler();

    public event FluentDesignSettingChangedHandler SettingChanged;

    public Task TriggerSettingChanged()
    {
        return SettingChanged?.Invoke();
    }
}