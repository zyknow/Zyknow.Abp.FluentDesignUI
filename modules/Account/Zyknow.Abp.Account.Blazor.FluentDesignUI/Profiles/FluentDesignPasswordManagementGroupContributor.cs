using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.Timing;
using Zyknow.Abp.Account.Blazor.FluentDesignUI.Pages.ProfileManagement.Groups;
using Zyknow.Abp.GroupComponent.Abstract.FluentDesignUI;

namespace Zyknow.Abp.Account.Blazor.FluentDesignUI.Profiles;

public class FluentDesignPasswordManagementGroupContributor : AccountManageGroupComponentContributorBase
{
    public override async Task<bool> CheckPermissionsAsync(GroupComponentCreationContext context)
    {
        var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();

        return await authorizationService.IsGrantedAnyAsync();
    }

    public override Task ConfigureAsync(GroupComponentCreationContext context)
    {
        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<AccountResource>>();
        var clock = context.ServiceProvider.GetRequiredService<IClock>();
        if (clock.SupportsMultipleTimezone)
        {
            context.Groups.Add(
                new ComponentGroup(
                    "Volo.Abp.AccountPassword",
                    l["ProfileTab:Password"],
                    typeof(AccountPasswordManagementComponent)
                )
            );
        }

        return Task.CompletedTask;
    }
}