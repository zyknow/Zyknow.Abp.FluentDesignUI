﻿using JetBrains.Annotations;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.Authorization;
using Volo.Abp.Localization;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Zyknow.Abp.FluentDesignUI.Components;

namespace Zyknow.Abp.FluentDesignUI;

public abstract class AbpCrudPageBase<
    TAppService,
    TEntityDto,
    TKey>
    : AbpCrudPageBase<
        TAppService,
        TEntityDto,
        TKey,
        PagedAndSortedResultRequestDto>
    where TAppService : ICrudAppService<
        TEntityDto,
        TKey>
    where TEntityDto : class, IEntityDto<TKey>, new()
{
}

public abstract class AbpCrudPageBase<
    TAppService,
    TEntityDto,
    TKey,
    TGetListInput>
    : AbpCrudPageBase<
        TAppService,
        TEntityDto,
        TKey,
        TGetListInput,
        TEntityDto>
    where TAppService : ICrudAppService<
        TEntityDto,
        TKey,
        TGetListInput>
    where TEntityDto : class, IEntityDto<TKey>, new()
    where TGetListInput : new()
{
}

public abstract class AbpCrudPageBase<
    TAppService,
    TEntityDto,
    TKey,
    TGetListInput,
    TCreateInput>
    : AbpCrudPageBase<
        TAppService,
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TCreateInput>
    where TAppService : ICrudAppService<
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateInput>
    where TEntityDto : IEntityDto<TKey>
    where TCreateInput : class, new()
    where TGetListInput : new()
{
}

public abstract class AbpCrudPageBase<
    TAppService,
    TEntityDto,
    TKey,
    TGetListInput,
    TCreateInput,
    TUpdateInput>
    : AbpCrudPageBase<
        TAppService,
        TEntityDto,
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput>
    where TAppService : ICrudAppService<
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput>
    where TEntityDto : IEntityDto<TKey>
    where TCreateInput : class, new()
    where TUpdateInput : class, new()
    where TGetListInput : new()
{
}

public abstract class AbpCrudPageBase<
    TAppService,
    TGetOutputDto,
    TGetListOutputDto,
    TKey,
    TGetListInput,
    TCreateInput,
    TUpdateInput>
    : AbpCrudPageBase<
        TAppService,
        TGetOutputDto,
        TGetListOutputDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput,
        TGetListOutputDto,
        TCreateInput,
        TUpdateInput>
    where TAppService : ICrudAppService<
        TGetOutputDto,
        TGetListOutputDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    where TCreateInput : class, new()
    where TUpdateInput : class, new()
    where TGetListInput : new()
{
}

public abstract class AbpCrudPageBase<
    TAppService,
    TGetOutputDto,
    TGetListOutputDto,
    TKey,
    TGetListInput,
    TCreateInput,
    TUpdateInput,
    TListViewModel,
    TCreateViewModel,
    TUpdateViewModel>
    : AbpComponentBase
    where TAppService : ICrudAppService<
        TGetOutputDto,
        TGetListOutputDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    where TCreateInput : class
    where TUpdateInput : class
    where TGetListInput : new()
    where TListViewModel : IEntityDto<TKey>
    where TCreateViewModel : class, new()
    where TUpdateViewModel : class, new()
{
    [Inject] protected TAppService AppService { get; set; }

    [Inject] protected IStringLocalizer<AbpUiResource> UiLocalizer { get; set; }

    [Inject] protected IAbpEnumLocalizer AbpEnumLocalizer { get; set; }

    [Inject] protected IDialogService DialogService { get; set; }

    protected bool Loading = false;
    protected TGetListInput GetListInput = new();
    protected IReadOnlyList<TListViewModel> Entities = Array.Empty<TListViewModel>();
    protected TCreateViewModel NewEntity = new();
    protected TKey EditingEntityId;
    protected TUpdateViewModel EditingEntity = new();

    protected List<AbpBreadcrumbItem> BreadcrumbItems = new();
    protected TableEntityActionsColumn<TListViewModel> EntityActionsColumn;
    protected FluentEntityActionDictionary EntityActions { get; set; } = new();
    protected FluentTableColumnDictionary TableColumns { get; set; } = new();

    public AbpExtensibleDataGrid<TListViewModel> AbpExtensibleDataGridRef { get; set; }

    protected bool CreateDialogHidden = true;
    protected bool EditDialogHidden = true;

    protected AbpFluentPaginationState Pagination { get; set; } = new();

    protected string CreatePolicyName { get; set; }
    protected string UpdatePolicyName { get; set; }
    protected string DeletePolicyName { get; set; }

    public bool HasCreatePermission { get; set; }
    public bool HasUpdatePermission { get; set; }
    public bool HasDeletePermission { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await SetEntityActionsAsync();
        await SetTableColumnsAsync();
        await SetToolbarItemsAsync();
        await SetBreadcrumbItemsAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task SetPermissionsAsync()
    {
        if (CreatePolicyName != null)
        {
            HasCreatePermission = await AuthorizationService.IsGrantedAsync(CreatePolicyName);
        }

        if (UpdatePolicyName != null)
        {
            HasUpdatePermission = await AuthorizationService.IsGrantedAsync(UpdatePolicyName);
        }

        if (DeletePolicyName != null)
        {
            HasDeletePermission = await AuthorizationService.IsGrantedAsync(DeletePolicyName);
        }
    }

    protected virtual async Task<IPagedResult<TGetListOutputDto>> GetEntitiesAsync()
    {
        try
        {
            Loading = true;
            await InvokeAsync(StateHasChanged);
            await UpdateGetListInputAsync();
            var result = await AppService.GetListAsync(GetListInput);
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

    private IReadOnlyList<TListViewModel> MapToListViewModel(IReadOnlyList<TGetListOutputDto> dtos)
    {
        if (typeof(TGetListOutputDto) == typeof(TListViewModel))
        {
            return dtos.As<IReadOnlyList<TListViewModel>>();
        }

        return ObjectMapper.Map<IReadOnlyList<TGetListOutputDto>, List<TListViewModel>>(dtos);
    }

    protected virtual Task UpdateGetListInputAsync()
    {
        Pagination.SetPageRequest(GetListInput);
        return Task.CompletedTask;
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

    protected virtual async Task OpenCreateDialogAsync()
    {
        try
        {
            await CheckCreatePolicyAsync();

            NewEntity = new TCreateViewModel();

            await InvokeAsync(async () =>
            {
                CreateDialogHidden = false;
                StateHasChanged();
            });
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }
    protected virtual async Task OpenEditDialogAsync(TListViewModel entity)
    {
        try
        {
            await CheckUpdatePolicyAsync();

            var entityDto = await AppService.GetAsync(entity.Id);

            EditingEntityId = entity.Id;
            EditingEntity = MapToEditingEntity(entityDto);

            await InvokeAsync(async () =>
            {
                EditDialogHidden = false;
                StateHasChanged();
            });
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual TUpdateViewModel MapToEditingEntity(TGetOutputDto entityDto)
    {
        return ObjectMapper.Map<TGetOutputDto, TUpdateViewModel>(entityDto);
    }

    protected virtual TCreateInput MapToCreateInput(TCreateViewModel createViewModel)
    {
        if (typeof(TCreateInput) == typeof(TCreateViewModel))
        {
            return createViewModel.As<TCreateInput>();
        }

        return ObjectMapper.Map<TCreateViewModel, TCreateInput>(createViewModel);
    }

    protected virtual TUpdateInput MapToUpdateInput(TUpdateViewModel updateViewModel)
    {
        if (typeof(TUpdateInput) == typeof(TUpdateViewModel))
        {
            return updateViewModel.As<TUpdateInput>();
        }

        return ObjectMapper.Map<TUpdateViewModel, TUpdateInput>(updateViewModel);
    }

    protected virtual async Task CreateEntityAsync()
    {
        try
        {
            var createInput = MapToCreateInput(NewEntity);
            await AppService.CreateAsync(createInput);
            await OnCreatedEntityAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual Task OnCreatingEntityAsync(FluentEditForm form)
    {
        return Task.CompletedTask;
    }

    protected virtual async Task OnCreatedEntityAsync()
    {
        NewEntity = new TCreateViewModel();
        CreateDialogHidden = true;
        await GetEntitiesAsync();

    }

    protected virtual async Task UpdateEntityAsync()
    {
        try
        {
            await OnUpdatingEntityAsync();

            await CheckUpdatePolicyAsync();
            var updateInput = MapToUpdateInput(EditingEntity);
            await AppService.UpdateAsync(EditingEntityId, updateInput);

            await OnUpdatedEntityAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual Task OnUpdatingEntityAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual async Task OnUpdatedEntityAsync()
    {
        EditDialogHidden = true;
        await GetEntitiesAsync();
    }

    protected virtual async Task DeleteEntityAsync(TListViewModel entity)
    {
        try
        {
            await CheckDeletePolicyAsync();
            await OnDeletingEntityAsync();
            await AppService.DeleteAsync(entity.Id);
            await OnDeletedEntityAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual Task OnDeletingEntityAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual async Task OnDeletedEntityAsync()
    {
        await GetEntitiesAsync();
        await InvokeAsync(StateHasChanged);
        await Notify.Success(L["SuccessfullyDeleted"]);
    }

    protected virtual string GetDeleteConfirmationMessage(TListViewModel entity)
    {
        return UiLocalizer["ItemWillBeDeletedMessage"];
    }

    protected virtual async Task CheckCreatePolicyAsync()
    {
        await CheckPolicyAsync(CreatePolicyName);
    }

    protected virtual async Task CheckUpdatePolicyAsync()
    {
        await CheckPolicyAsync(UpdatePolicyName);
    }

    protected virtual async Task CheckDeletePolicyAsync()
    {
        await CheckPolicyAsync(DeletePolicyName);
    }

    /// <summary>
    /// Calls IAuthorizationService.CheckAsync for the given <paramref name="policyName"/>.
    /// Throws <see cref="AbpAuthorizationException"/> if given policy was not granted for the current user.
    ///
    /// Does nothing if <paramref name="policyName"/> is null or empty.
    /// </summary>
    /// <param name="policyName">A policy name to check</param>
    protected virtual async Task CheckPolicyAsync([CanBeNull] string policyName)
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