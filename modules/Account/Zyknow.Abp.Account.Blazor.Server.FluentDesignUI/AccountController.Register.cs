using Microsoft.AspNetCore.Mvc;

namespace Zyknow.Abp.Account.Blazor.Server.FluentDesignUI;

public partial class AccountController
{
    [HttpGet("api/Account/RegisterSucceededRedirect")]
    public virtual async Task<IActionResult> RegisterSucceededRedirectAsync(Guid userId, string? returnUrl = null,
        string? externalLoginAuthSchema = null)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        await signInManager.SignInAsync(user, isPersistent: true, externalLoginAuthSchema);
        // Clear the dynamic claims cache.
        await identityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);
        return Redirect(returnUrl ?? "~/");
    }
    // [HttpGet("api/Account/RegisterExternalUser")]
}