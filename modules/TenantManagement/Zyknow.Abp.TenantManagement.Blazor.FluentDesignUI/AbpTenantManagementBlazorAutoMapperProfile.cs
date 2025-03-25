using AutoMapper;
using Volo.Abp.TenantManagement;

namespace Zyknow.Abp.TenantManagement.Blazor.FluentDesignUI;

public class AbpTenantManagementBlazorAutoMapperProfile : Profile
{
    public AbpTenantManagementBlazorAutoMapperProfile()
    {
        CreateMap<TenantDto, TenantUpdateDto>()
            .MapExtraProperties();
    }
}
