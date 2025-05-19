using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.CmsKit.Localization;
using Volo.CmsKit.Permissions;
using Zyknow.Abp.CmsKit.Blazor.Admin.FluentDesignUI.Pages.CmsKit.SettingManagement.Groups;
using Zyknow.Abp.GroupComponent.FluentDesignUI;

namespace Zyknow.Abp.CmsKit.Blazor.Admin.FluentDesignUI.Settings;

public class FluentDesignCmsGroupContributor : IGroupComponentContributor
{
    public string GroupKey { get; } = "Setting";

    public async Task<bool> CheckPermissionsAsync(GroupComponentCreationContext context)
    {
        return true;
    }

    public async Task ConfigureAsync(GroupComponentCreationContext context)
    {
        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<CmsKitResource>>();
        var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
        if (await authorizationService.IsGrantedAsync(CmsKitAdminPermissions.Comments.SettingManagement))
        {
            context.Groups.Add(
                new ComponentGroup(
                    "Volo.Abp.CmsKit",
                    l["Cms"],
                    typeof(CommentSettingGroupComponent)
                )
            );
        }
    }
}