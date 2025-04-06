using JetBrains.Annotations;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
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
    : AbpCustomCrudMethodPageBase<
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
    protected override Task<TGetOutputDto> AppServiceGetAsync(TKey id) =>
        AppService.GetAsync(id);

    protected override Task AppServiceCreateAsync(TCreateInput input) =>
        AppService.CreateAsync(input);

    protected override Task AppServiceUpdateAsync(TKey id, TUpdateInput input) =>
        AppService.UpdateAsync(id, input);

    protected override Task AppServiceDeleteAsync(TKey id) =>
        AppService.DeleteAsync(id);
}