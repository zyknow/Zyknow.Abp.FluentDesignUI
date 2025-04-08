using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Zyknow.Abp.FluentDesignUI.Components;

public partial class EntityActions<TItem> : ComponentBase
{
    protected readonly List<EntityAction<TItem>> Actions = new();

    protected bool HasPrimaryAction => Actions.Any(t => t.Primary);

    protected EntityAction<TItem> PrimaryAction => Actions.FirstOrDefault(t => t.Primary);

    [Parameter] public ActionType Type { get; set; } = ActionType.Dropdown;

    [Parameter] public bool Disabled { get; set; } = false;

    [Parameter] public Appearance ToggleAppearance { get; set; } = Appearance.Accent;

    [Parameter] public string ToggleText { get; set; }
    
    [Parameter] public Icon? Icon { get; set; }
    [Parameter] public bool OnlyIcon { get; set; }

    [Parameter] public RenderFragment ChildContent { get; set; }

    // [Parameter]
    // public TableEntityActionsColumn<TItem> EntityActionsColumn { get; set; }
    //
    // [CascadingParameter]
    // public TableEntityActionsColumn<TItem> ParentEntityActionsColumn { get; set; }

    [Inject] public IStringLocalizer<AbpUiResource> UiLocalizer { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        ToggleText = UiLocalizer["Actions"];
    }

    // protected override async Task OnAfterRenderAsync(bool firstRender)
    // {
    //     if (firstRender)
    //     {
    //         if (ParentEntityActionsColumn != null)
    //         {
    //             ParentEntityActionsColumn.Hidden = !Actions.Any(t => t.Visible && t.HasPermission);
    //         }
    //
    //         await InvokeAsync(StateHasChanged);
    //     }
    //
    //     await base.OnAfterRenderAsync(firstRender);
    // }

    internal void AddAction(EntityAction<TItem> action)
    {
        Actions.Add(action);
    }
}