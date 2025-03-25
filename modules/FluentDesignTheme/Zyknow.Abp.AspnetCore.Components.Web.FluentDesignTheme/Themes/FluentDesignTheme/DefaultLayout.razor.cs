using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Themes.FluentDesignTheme;

public partial class DefaultLayout
{
    [Inject]
    protected IFluentDesignSettingsProvider FluentDesignSettingsProvider { get; set; }

    protected bool Collapsed { get; set; } = true;

    protected MenuPlacement MenuPlacement { get; set; }

    protected DesignThemeModes MenuTheme { get; set; }

    protected string HeaderClass { get; set; }

    // protected SiderTheme SiderTheme { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetLayout();
        FluentDesignSettingsProvider.SettingChanged += OnSettingChanged;
    }

    protected virtual async Task OnSettingChanged()
    {
        await SetLayout();
        await InvokeAsync(StateHasChanged);
    }

    private async Task SetLayout()
    {
        // SiderTheme = MenuTheme  == MenuTheme.Light ? SiderTheme.Light : SiderTheme.Dark;
        HeaderClass = MenuPlacement == MenuPlacement.Top ? "fluent-design-header-top" : "fluent-design-header-left";
        // HeaderClass = MenuTheme == MenuTheme.Light ? $"{HeaderClass} {HeaderClass}-light" : HeaderClass;
    }

    protected virtual void OnCollapse()
    {
        Collapsed = !Collapsed;
        StateHasChanged();
    }
}
