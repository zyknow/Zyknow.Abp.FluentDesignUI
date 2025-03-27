using AutoMapper;
using Volo.Abp.SettingManagement;

namespace Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI;

public class SettingManagementBlazorAutoMapperProfile : Profile
{
    public SettingManagementBlazorAutoMapperProfile()
    {
        CreateMap<EmailSettingsDto, UpdateEmailSettingsDto>();
    }
}
