using Microsoft.Extensions.Options;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.DependencyInjection;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

public class DefaultThemeStore(IOptions<AbpFluentDesignThemeOptions> options, ICookieService cookieService)
    : IThemeStore, IScopedDependency
{
    protected string StorePrefix { get; set; } = "Theme_";

    public event EventHandler? ThemeModeChanged;


    // ThemeMode
    private DesignThemeModes _themeMode = options.Value.DefaultThemeMode;

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
    private OfficeColor _color = options.Value.DefaultColor;

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

    // FluentLayoutTheme
    private FluentLayoutTheme _layoutTheme = options.Value.DefaultFluentLayoutTheme;

    public FluentLayoutTheme LayoutTheme
    {
        get => _layoutTheme;
        set
        {
            if (_layoutTheme != value)
            {
                _layoutTheme = value;
                cookieService.SetAsync(GetStoreKey(nameof(LayoutTheme)), _layoutTheme.ToString());
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
                cookieService.SetAsync(GetStoreKey(nameof(EnableMultipleTabs)), _enableMultipleTabs.ToString());
                ThemeModeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public async Task InitializeAsync()
    {
        ThemeModeChanged?.Invoke(this, EventArgs.Empty);
        _enableMultipleTabs = bool.TryParse(await cookieService.GetAsync(GetStoreKey(nameof(EnableMultipleTabs))),
            out var enableMultipleTabs) && enableMultipleTabs;
        _layoutTheme =
            Enum.TryParse<FluentLayoutTheme>(await cookieService.GetAsync(GetStoreKey(nameof(LayoutTheme))),
                out var layoutTheme)
                ? layoutTheme
                : options.Value.DefaultFluentLayoutTheme;
    }


    string GetStoreKey(string key)
    {
        return $"{StorePrefix}{key}";
    }
}