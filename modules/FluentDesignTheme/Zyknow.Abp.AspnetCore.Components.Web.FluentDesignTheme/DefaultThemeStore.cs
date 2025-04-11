using Microsoft.Extensions.Options;
using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.DependencyInjection;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

public class DefaultThemeStore : IThemeStore, IScopedDependency
{
    public DefaultThemeStore(IOptions<AbpFluentDesignThemeOptions> options, ICookieService cookieService)
    {
        _themeMode = options.Value.DefaultThemeMode;
        Color = options.Value.DefaultColor;
        EnableMultipleTabs = options.Value.DefaultEnableMultipleTabs;
    }

    public event EventHandler? ThemeModeChanged;

    public string RenderMode { get; set; }

    public bool Prerender { get; set; }


    // ThemeMode
    private DesignThemeModes _themeMode;

    public DesignThemeModes ThemeMode
    {
        get => _themeMode;
        set
        {
            if (_themeMode != value)
            {
                _themeMode = value;
                ThemeModeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    // Color
    private OfficeColor _color;

    public OfficeColor Color
    {
        get => _color;
        set
        {
            if (_color != value)
            {
                _color = value;
                ThemeModeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    // EnableMultipleTabs
    private bool _enableMultipleTabs;

    public bool EnableMultipleTabs
    {
        get => _enableMultipleTabs;
        set
        {
            if (_enableMultipleTabs != value)
            {
                _enableMultipleTabs = value;
                ThemeModeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public async Task InitializeAsync()
    {
        ThemeModeChanged?.Invoke(this, EventArgs.Empty);
    }
}