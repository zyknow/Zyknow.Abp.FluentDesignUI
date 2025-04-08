using AutoMapper;
using SharpAbp.Abp.Identity;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;

namespace Zyknow.SharpAbp.IdentityManagement.Blazor.FluentDesignUI;

public class SharpAbpIdentityBlazorFluentDesignAutoMapperProfile : Profile
{
    public SharpAbpIdentityBlazorFluentDesignAutoMapperProfile()
    {
        CreateMap<IdentityClaimTypeDto, UpdateIdentityClaimTypeDto>();
        CreateMap<OrganizationUnitDto, UpdateOrganizationUnitDto>();
    }
}