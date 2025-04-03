using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Messages;

namespace Zyknow.Abp.FluentDesignUI.Components;

public partial class EntityAction<TItem> : ComponentBase
{
    internal bool HasPermission { get; set; } = true;

    [Parameter] public bool Visible { get; set; } = true;

    [Parameter] public bool Disabled { get; set; } = false;

    [Parameter] public string Text { get; set; }

    [Parameter] public Icon? Icon { get; set; }
    
    [Parameter] public string? Color { get; set; }

    [Parameter] public Appearance Appearance { get; set; } = Appearance.Accent;

    [Parameter] public bool Primary { get; set; }

    [Parameter] public EventCallback Clicked { get; set; }

    [Parameter] public Func<string> ConfirmationMessage { get; set; }

    [Parameter] public string RequiredPolicy { get; set; }

    [Parameter] public EntityActions<TItem> ParentActions { get; set; }

    [Inject] public IAuthorizationService AuthorizationService { get; set; }

    [Inject] public IUiMessageService UiMessageService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (!RequiredPolicy.IsNullOrEmpty())
        {
            HasPermission = await AuthorizationService.IsGrantedAsync(RequiredPolicy);
        }

        ParentActions.AddAction(this);
    }

    public virtual async Task ActionClickedAsync()
    {
        if (ConfirmationMessage != null)
        {
            if (await UiMessageService.Confirm(ConfirmationMessage()))
            {
                await InvokeAsync(async () => await Clicked.InvokeAsync());
            }
        }
        else
        {
            await Clicked.InvokeAsync();
        }
    }
}