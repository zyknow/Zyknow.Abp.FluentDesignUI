using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web.Configuration;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Localization;

namespace Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI.Pages.SettingManagement.Groups;

public partial class EmailSettingGroupComponent
{
    [Inject]
    protected IEmailSettingsAppService EmailSettingsAppService { get; set; }

    [Inject]
    protected ICurrentApplicationConfigurationCacheResetService CurrentApplicationConfigurationCacheResetService { get; set; }

    protected EmailSettingsDto EmailSettings = new();

    public EmailSettingGroupComponent()
    {
        ObjectMapperContext = typeof(AbpSettingManagementBlazorFluentDesignModule);
        LocalizationResource = typeof(AbpSettingManagementResource);
    }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            EmailSettings = await EmailSettingsAppService.GetAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual async Task UpdateSettingsAsync()
    {
        try
        {
            await EmailSettingsAppService.UpdateAsync(ObjectMapper.Map<EmailSettingsDto, UpdateEmailSettingsDto>(EmailSettings));

            await CurrentApplicationConfigurationCacheResetService.ResetAsync();

            await Message.Success(L["SuccessfullySaved"]);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }
}
