using AutoMapper;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;

namespace Zyknow.Abp.IdentityManagement.Blazor.FluentDesignUI;

public class AbpIdentityBlazorFluentDesignAutoMapperProfile : Profile
{
    public AbpIdentityBlazorFluentDesignAutoMapperProfile()
    {
        CreateMap<IdentityUserDto, IdentityUserUpdateDto>()
            .MapExtraProperties()
            .Ignore(x => x.Password)
            .Ignore(x => x.RoleNames);

        CreateMap<IdentityRoleDto, IdentityRoleUpdateDto>()
            .MapExtraProperties();
    }
}
