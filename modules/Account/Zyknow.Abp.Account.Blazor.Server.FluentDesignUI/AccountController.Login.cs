using System.Diagnostics;
using System.Security.Claims;
using AsyncKeyedLock;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Account.Settings;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Security.Claims;
using Volo.Abp.Settings;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Zyknow.Abp.Account.Blazor.Server.FluentDesignUI;

public partial class AccountController
{
    static AsyncKeyedLocker<string> TicketLoginLocker = new AsyncKeyedLocker<string>();

    [HttpGet("api/Account/LoginSucceededRedirect")]
    public async Task<IActionResult> LoginSucceededRedirectAsync(string ticket)
    {
        var ticketCache = await loginTicketCache.GetAsync(ticket);
        if (ticketCache == null)
        {
            return BadRequest("Ticket not found!");
        }

        using (await TicketLoginLocker.LockAsync(ticket))
        {
            var user = await userManager.FindByIdAsync(ticketCache.UserId.ToString());
            await signInManager.SignInAsync(user, ticketCache.RememberMe);

            await identitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
            {
                Identity = IdentitySecurityLogIdentityConsts.Identity,
                Action = IdentitySecurityLogActionConsts.LoginSucceeded,
                UserName = ticketCache.UserNameOrEmailAddress
            });

            Debug.Assert(user != null, nameof(user) + " != null");

            await identityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);

            await loginTicketCache.RemoveAsync(ticket);

            return await RedirectSafelyAsync(ticketCache.ReturnUrl, ticketCache.ReturnUrlHash);
        }
    }

    [HttpGet("api/Account/ExternalLogin")]
    public IActionResult ExternalLogin(string provider, string returnUrl, string returnUrlHash)
    {
        var redirectUrl = Url.Page(
            "./Account",
            pageHandler: "ExternalLoginCallback",
            values: new { ReturnUrl = returnUrl, ReturnUrlHash = returnUrlHash }
        );

        var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        properties.Items["scheme"] = provider;

        return Challenge(properties, provider);
    }

    public virtual async Task<IActionResult> ExternalLoginCallbackAsync(string returnUrl = "",
        string returnUrlHash = "", string remoteError = null)
    {
        //TODO: Did not implemented Identity Server 4 sample for this method (see ExternalLoginCallback in Quickstart of IDS4 sample)
        /* Also did not implement these:
         * - Logout(string logoutId)
         */

        if (remoteError != null)
        {
            Logger.LogWarning($"External login callback error: {remoteError}");
            return RedirectToPage("./Login");
        }

        await identityOptions.SetAsync();

        var loginInfo = await signInManager.GetExternalLoginInfoAsync();
        if (loginInfo == null)
        {
            Logger.LogWarning("External login info is not available");
            return RedirectToPage("./Login");
        }

        var result = await signInManager.ExternalLoginSignInAsync(
            loginInfo.LoginProvider,
            loginInfo.ProviderKey,
            isPersistent: false,
            bypassTwoFactor: true
        );

        if (!result.Succeeded)
        {
            await identitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
            {
                Identity = IdentitySecurityLogIdentityConsts.IdentityExternal,
                Action = "Login" + result
            });
        }

        if (result.IsLockedOut)
        {
            Logger.LogWarning($"External login callback error: user is locked out!");
            throw new UserFriendlyException("Cannot proceed because user is locked out!");
        }

        if (result.IsNotAllowed)
        {
            Logger.LogWarning($"External login callback error: user is not allowed!");
            throw new UserFriendlyException("Cannot proceed because user is not allowed!");
        }

        IdentityUser user;
        if (result.Succeeded)
        {
            user = await userManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey);
            if (user != null)
            {
                // Clear the dynamic claims cache.
                await identityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);
            }

            return await RedirectSafelyAsync(returnUrl, returnUrlHash);
        }

        //TODO: Handle other cases for result!

        var email = loginInfo.Principal.FindFirstValue(AbpClaimTypes.Email) ??
                    loginInfo.Principal.FindFirstValue(ClaimTypes.Email);
        if (email.IsNullOrWhiteSpace())
        {
            return RedirectToPage("./Register", new
            {
                IsExternalLogin = true,
                ExternalLoginAuthSchema = loginInfo.LoginProvider,
                ReturnUrl = returnUrl
            });
        }

        user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return RedirectToPage("./Register", new
            {
                IsExternalLogin = true,
                ExternalLoginAuthSchema = loginInfo.LoginProvider,
                ReturnUrl = returnUrl
            });
        }

        if (await userManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey) == null)
        {
            CheckIdentityErrors(await userManager.AddLoginAsync(user, loginInfo));
        }

        await signInManager.SignInAsync(user, false);

        await identitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
        {
            Identity = IdentitySecurityLogIdentityConsts.IdentityExternal,
            Action = result.ToIdentitySecurityLogAction(),
            UserName = user.Name
        });

        // Clear the dynamic claims cache.
        await identityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);

        return await RedirectSafelyAsync(returnUrl, returnUrlHash);
    }


    protected virtual void CheckIdentityErrors(IdentityResult identityResult)
    {
        if (!identityResult.Succeeded)
        {
            throw new UserFriendlyException("Operation failed: " +
                                            identityResult.Errors.Select(e => $"[{e.Code}] {e.Description}")
                                                .JoinAsString(", "));
        }

        //identityResult.CheckErrors(LocalizationManager); //TODO: Get from old Abp
    }

    protected virtual async Task CheckLocalLoginAsync()
    {
        if (!await settingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin))
        {
            throw new UserFriendlyException(L["LocalLoginDisabledMessage"]);
        }
    }
}