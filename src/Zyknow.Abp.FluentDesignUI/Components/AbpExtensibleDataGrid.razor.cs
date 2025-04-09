using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Text.RegularExpressions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;

namespace Zyknow.Abp.FluentDesignUI.Components;

public partial class AbpExtensibleDataGrid<TItem, TKey> : ComponentBase where TItem : IEntityDto<TKey>
{
    protected const string DataFieldAttributeName = "Data";

    protected Regex ExtensionPropertiesRegex = new Regex(@"ExtraProperties\[(.*?)\]");

    protected Dictionary<string, TableEntityActionsColumn<TItem>> ActionColumns = new();

    [Parameter] public ActionType ActionType { get; set; } = ActionType.Dropdown;

    [Parameter] public AbpFluentPaginationState Pagination { get; set; } = new();

    [Parameter] public IEnumerable<FluentTableColumn>? Columns { get; set; }


    [Parameter] public bool Loading { get; set; }

    [Parameter] public Func<GridItemsProviderRequest<TItem>, Task>? OnChange { get; set; }

    [Parameter] public IReadOnlyList<TItem> Entities { get; set; }

    [Parameter] public List<TItem> SelectEntities { get; set; } = [];


    [Parameter] public EventCallback<List<TItem>> SelectEntitiesChanged { get; set; }

    [Parameter] public RenderFragment? SelectToolbar { get; set; }
    [Parameter] public RenderFragment? SelectToolbarEnd { get; set; }

    [Parameter] public bool EnableSelected { get; set; } = false;

    [Parameter] public EventCallback<IEnumerable<TItem>> OnDeleteSelected { get; set; }

    [Parameter] public Func<TItem, string>? DeleteSelectedDisplayPropertyConfirmExpression { get; set; }

    [Parameter] public bool PrimarySelectedDeletesBtnVisible { get; set; } = false;

    [Inject] public IDialogService DialogService { get; set; }


    [Inject] public IStringLocalizerFactory StringLocalizerFactory { get; set; }

    [Inject] public IUiMessageService UiMessageService { get; set; }
    
    public FluentDataGrid<TItem> FluentDataGridRef { get; set; }

    protected bool ToggleColumnPopoverVisible { get; set; }

    protected virtual string ToggleColumnPopoverVisibleBtnId { get; set; } = Guid.NewGuid().ToString();

    protected bool? SelectedAll
    {
        get
        {
            var entityIds = Entities.Select(x => x.Id).ToList();
            if (!SelectEntities.Any(x => entityIds.Contains(x.Id)))
            {
                return false;
            }

            var selectedIds = SelectEntities.Select(x => x.Id).ToList();
            if (entityIds.All(x => selectedIds.Contains(x)))
            {
                return true;
            }

            return null;
        }
    }

    protected  EntityActions<TItem> EntityActionsRef;

    protected virtual RenderFragment RenderCustomTableColumnComponent(Type type, object data)
    {
        return (builder) =>
        {
            builder.OpenComponent(0, type);
            builder.AddAttribute(0, DataFieldAttributeName, data);
            builder.CloseComponent();
        };
    }

    protected virtual object GetColumnValue(object data, string fieldName)
    {
        return data.GetType().GetProperty(fieldName)?.GetValue(data);
    }

    protected virtual string GetConvertedFieldValue(TItem item, TableColumn columnDefinition)
    {
        var convertedValue = columnDefinition.ValueConverter.Invoke(item);
        if (!columnDefinition.DisplayFormat.IsNullOrEmpty())
        {
            return string.Format(columnDefinition.DisplayFormatProvider, columnDefinition.DisplayFormat,
                convertedValue);
        }

        return convertedValue;
    }

    protected async Task OnSelectAllChanged(bool? obj)
    {
        if (obj == null)
        {
            return;
        }

        if (obj.Value)
        {
            var entities = Entities.Where(x => !SelectEntities.Any(y => y.Id.Equals(x.Id)));
            SelectEntities.AddIfNotContains(entities);
        }
        else
        {
            var entityIds = Entities.Select(x => x.Id).ToList();
            SelectEntities.RemoveAll(x => entityIds.Contains(x.Id));
        }

        await SelectEntitiesChanged.InvokeAsync(SelectEntities);
        await InvokeAsync(StateHasChanged);
    }


    protected async Task OnSelect((TItem Item, bool Selected) obj)
    {
        if (obj.Selected)
        {
            if (SelectEntities.Any(x => x.Id.Equals(obj.Item.Id)))
            {
                return;
            }

            SelectEntities.AddIfNotContains(obj.Item);
        }
        else
        {
            SelectEntities.RemoveAll(x => x.Id.Equals(obj.Item.Id));
        }

        await SelectEntitiesChanged.InvokeAsync(SelectEntities);
        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnDeleteSelectedClick()
    {
        if (SelectEntities.IsNullOrEmpty())
        {
            return;
        }

        if (OnDeleteSelected.HasDelegate)
        {
            var input = new MultiItemDeleteConfirmDialogInput<TItem>(SelectEntities,
                DeleteSelectedDisplayPropertyConfirmExpression);
            var res = await DialogService.ShowDialogAsync<MultiItemDeleteConfirmDialog<TItem>>(input,
                new DialogParameters());
            var result = await res.Result;
            if (!result.Cancelled)
            {
                await OnDeleteSelected.InvokeAsync(SelectEntities);
                SelectEntities.Clear();
            }
        }
    }

    protected async Task OnClearSelectedClick()
    {
        if (SelectEntities.IsNullOrEmpty())
        {
            return;
        }

        SelectEntities.Clear();
    }

    protected Task RefreshItemsAsync(GridItemsProviderRequest<TItem> request)
    {
        var res = OnChange.Invoke(request);
        if (res != null && res is Task task)
        {
            return task;
        }
        
        return Task.CompletedTask;
    }
}