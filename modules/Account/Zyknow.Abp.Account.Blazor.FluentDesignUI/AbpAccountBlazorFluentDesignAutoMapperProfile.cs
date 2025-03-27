using AutoMapper;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Zyknow.Abp.Account.Blazor.FluentDesignUI.Pages.Account.Components.ProfileManagementGroup;

namespace Zyknow.Abp.Account.Blazor.FluentDesignUI;

public class AbpAccountBlazorFluentDesignAutoMapperProfile : Profile
{
    public AbpAccountBlazorFluentDesignAutoMapperProfile()
    {
        CreateMap<ProfileDto, PersonalInfoModel>()
            .MapExtraProperties()
            .Ignore(x => x.PhoneNumberConfirmed)
            .Ignore(x => x.EmailConfirmed);

        CreateMap<PersonalInfoModel, UpdateProfileDto>()
            .MapExtraProperties();
    }
}