using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.Application.Dtos;

namespace Zyknow.Abp.FluentDesignUI;

public class AbpFluentPaginationState : PaginationState
{
    public AbpFluentPaginationState()
    {
        ItemsPerPage = LimitedResultRequestDto.DefaultMaxResultCount;
    }

    public string Sorting { get; set; }

    public void SetPageRequest(object getListInput)
    {
        if (getListInput is ISortedResultRequest sortedResultRequestInput)
        {
            sortedResultRequestInput.Sorting = Sorting;
        }

        if (getListInput is IPagedResultRequest pagedResultRequestInput)
        {
            pagedResultRequestInput.SkipCount = CurrentPageIndex * ItemsPerPage;
        }

        if (getListInput is ILimitedResultRequest limitedResultRequestInput)
        {
            limitedResultRequestInput.MaxResultCount = ItemsPerPage;
        }
    }
}