﻿using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web.Security;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Toolbars;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Themes.FluentDesignTheme;

public partial class NavToolbar : IDisposable
{
    [Inject]
    private IToolbarManager ToolbarManager { get; set; }

    [Inject]
    private ApplicationConfigurationChangedService ApplicationConfigurationChangedService { get; set; }

    private List<RenderFragment> ToolbarItemRenders { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await GetToolbarItemRendersAsync();
        ApplicationConfigurationChangedService.Changed += ApplicationConfigurationChanged;
    }

    private async Task GetToolbarItemRendersAsync()
    {
        var toolbar = await ToolbarManager.GetAsync(StandardToolbars.Main);

        ToolbarItemRenders.Clear();

        var sequence = 0;
        foreach (var item in toolbar.Items)
        {
            ToolbarItemRenders.Add(builder =>
            {
                builder.OpenComponent(sequence++, item.ComponentType);
                builder.CloseComponent();
            });
        }
    }

    private async void ApplicationConfigurationChanged()
    {
        await GetToolbarItemRendersAsync();
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        ApplicationConfigurationChangedService.Changed -= ApplicationConfigurationChanged;
    }
}
