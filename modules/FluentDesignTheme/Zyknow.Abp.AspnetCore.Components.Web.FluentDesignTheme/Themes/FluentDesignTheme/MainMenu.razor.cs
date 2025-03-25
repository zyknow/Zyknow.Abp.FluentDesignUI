using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web.Security;
using Volo.Abp.UI.Navigation;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Themes.FluentDesignTheme;

public partial class MainMenu : IDisposable
{
    protected ApplicationMenu Menu { get; set; }
    
    [Inject]
    protected IMenuManager MenuManager { get; set; }
    
    [Inject]
    protected ApplicationConfigurationChangedService ApplicationConfigurationChangedService { get; set; }
    
    [Parameter]
    public MenuPlacement Placement { get; set; }

    [Parameter] public bool Collapsed { get; set; }
    


    protected override async Task OnInitializedAsync()
    {
        await GetMenuAsync();
        ApplicationConfigurationChangedService.Changed += ApplicationConfigurationChanged;
    }

    private async Task GetMenuAsync()
    {
        Menu = await MenuManager.GetMainMenuAsync();
    }

    private async void ApplicationConfigurationChanged()
    {
        await GetMenuAsync();
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        ApplicationConfigurationChangedService.Changed -= ApplicationConfigurationChanged;
    }
}
