﻿using Microsoft.AspNetCore.Components;
using Volo.Abp;
using Volo.Abp.AspNetCore.Components.Web.Configuration;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Localization;

namespace Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI.Pages.SettingManagement.Groups;

public partial class TimeZoneSettingGroupComponent
{
    [Inject]
    protected ITimeZoneSettingsAppService TimeZoneSettingsAppService { get; set; }

    [Inject]
    protected ICurrentApplicationConfigurationCacheResetService CurrentApplicationConfigurationCacheResetService { get; set; }

    protected UpdateTimezoneSettingsViewModel TimezoneSettings = new();

    public TimeZoneSettingGroupComponent()
    {
        ObjectMapperContext = typeof(AbpSettingManagementBlazorFluentDesignModule);
        LocalizationResource = typeof(AbpSettingManagementResource);
    }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            TimezoneSettings.Timezone = await TimeZoneSettingsAppService.GetAsync();
            TimezoneSettings.TimeZoneItems = await TimeZoneSettingsAppService.GetTimezonesAsync();
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
            await TimeZoneSettingsAppService.UpdateAsync(TimezoneSettings.Timezone);
            await CurrentApplicationConfigurationCacheResetService.ResetAsync();
            await Message.Success(L["SuccessfullySaved"]);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    public class UpdateTimezoneSettingsViewModel
    {
        public string Timezone { get; set; }

        public List<NameValue> TimeZoneItems { get; set; }
    }
}
