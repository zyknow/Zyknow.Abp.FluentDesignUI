﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Volo.Abp.UI.Navigation;

namespace Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme.Themes.FluentDesignTheme;

public partial class LoginDisplay : IDisposable
{
    [Inject]
    protected IMenuManager MenuManager { get; set; }

    protected ApplicationMenu Menu { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Menu = await MenuManager.GetAsync(StandardMenus.User);

        Navigation.LocationChanged += OnLocationChanged;
    }

    protected virtual void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        Navigation.LocationChanged -= OnLocationChanged;
    }
}
