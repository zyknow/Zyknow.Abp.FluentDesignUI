using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.Localization;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Zyknow.Abp.FluentDesignUI.Components;

namespace Zyknow.Abp.FluentDesignUI;

public abstract class
    AbpGetListPageBase<TGetListOutputDto, TKey, TGetListInput> : AbpGetListPageBase<TGetListOutputDto, TKey,
    TGetListInput, TGetListOutputDto>
    where TGetListInput : new()
{
}

public abstract class AbpGetListPageBase<TGetListOutputDto, TKey, TGetListInput, TListViewModel> : AbpComponentBase
    where TGetListInput : new()
{
    [Inject] protected IStringLocalizer<AbpUiResource> UiLocalizer { get; set; }

    [Inject] protected IAbpEnumLocalizer AbpEnumLocalizer { get; set; }

    [Inject] protected IDialogService DialogService { get; set; }

    protected bool Loading = false;
    protected TGetListInput GetListInput = new();
    protected IReadOnlyList<TListViewModel> Entities = Array.Empty<TListViewModel>();

    protected List<AbpBreadcrumbItem> BreadcrumbItems = new();
    protected TableEntityActionsColumn<TListViewModel> EntityActionsColumn;
    protected FluentEntityActionDictionary EntityActions { get; set; } = new();
    protected FluentTableColumnDictionary TableColumns { get; set; } = new();

    protected AbpFluentPaginationState Pagination { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await SetEntityActionsAsync();
        await SetTableColumnsAsync();
        await SetToolbarItemsAsync();
        await SetBreadcrumbItemsAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected abstract Task<IPagedResult<TGetListOutputDto>> AppServiceGetListAsync(TGetListInput input);

    protected virtual async Task<IPagedResult<TGetListOutputDto>> GetEntitiesAsync()
    {
        try
        {
            Loading = true;
            await InvokeAsync(StateHasChanged);
            await UpdateGetListInputAsync();
            var result = await AppServiceGetListAsync(GetListInput);
            Entities = MapToListViewModel(result.Items);
            await Pagination.SetTotalItemCountAsync((int)result.TotalCount);

            return result;
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
            return null;
        }
        finally
        {
            Loading = false;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected virtual Task UpdateGetListInputAsync()
    {
        Pagination.SetPageRequest(GetListInput);
        return Task.CompletedTask;
    }

    protected IReadOnlyList<TListViewModel> MapToListViewModel(IReadOnlyList<TGetListOutputDto> dtos)
    {
        if (typeof(TGetListOutputDto) == typeof(TListViewModel))
        {
            return dtos.As<IReadOnlyList<TListViewModel>>();
        }

        return ObjectMapper.Map<IReadOnlyList<TGetListOutputDto>, List<TListViewModel>>(dtos);
    }

    protected virtual async Task SearchEntitiesAsync()
    {
        await Pagination.SetCurrentPageIndexAsync(0);

        await GetEntitiesAsync();

        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task<IPagedResult<TGetListOutputDto>> OnDataGridReadAsync(
        GridItemsProviderRequest<TGetListOutputDto> e)
    {
        Pagination.Sorting = e.GetSortByProperties()
            .Select(c => c.PropertyName + (c.Direction == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");

        var res = await GetEntitiesAsync();
        return res;
    }

    protected virtual async Task CheckPolicyAsync(string? policyName)
    {
        if (string.IsNullOrEmpty(policyName))
        {
            return;
        }

        await AuthorizationService.CheckAsync(policyName);
    }

    protected virtual ValueTask SetBreadcrumbItemsAsync()
    {
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetEntityActionsAsync()
    {
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetTableColumnsAsync()
    {
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetToolbarItemsAsync()
    {
        return ValueTask.CompletedTask;
    }


    protected virtual IEnumerable<FluentTableColumn> GetExtensionTableColumns(string moduleName, string entityType)
    {
        var properties = ModuleExtensionConfigurationHelper.GetPropertyConfigurations(moduleName, entityType);
        foreach (var propertyInfo in properties)
        {
            if (propertyInfo.IsAvailableToClients && propertyInfo.UI.OnTable.IsVisible)
            {
                if (propertyInfo.Name.EndsWith("_Text"))
                {
                    var lookupPropertyName = propertyInfo.Name.RemovePostFix("_Text");
                    var lookupPropertyDefinition = properties.SingleOrDefault(t => t.Name == lookupPropertyName);
                    yield return new FluentTableColumn
                    {
                        Title = lookupPropertyDefinition.GetLocalizedDisplayName(StringLocalizerFactory),
                        Data = $"ExtraProperties[{propertyInfo.Name}]"
                    };
                }
                else
                {
                    var column = new FluentTableColumn
                    {
                        Title = propertyInfo.GetLocalizedDisplayName(StringLocalizerFactory),
                        Data = $"ExtraProperties[{propertyInfo.Name}]"
                    };

                    if (propertyInfo.IsDate() || propertyInfo.IsDateTime())
                    {
                        column.DisplayFormat = propertyInfo.GetDateEditInputFormatOrNull();
                    }

                    if (propertyInfo.Type.IsEnum)
                    {
                        column.ValueConverter = (val) =>
                            AbpEnumLocalizer.GetString(propertyInfo.Type,
                                val.As<ExtensibleObject>().ExtraProperties[propertyInfo.Name]);
                    }

                    yield return column;
                }
            }
        }
    }
}