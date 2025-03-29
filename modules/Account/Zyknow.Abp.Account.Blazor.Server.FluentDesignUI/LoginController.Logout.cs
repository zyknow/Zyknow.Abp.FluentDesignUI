using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Account.Settings;
using Volo.Abp.Identity;
using Volo.Abp.Settings;

namespace Zyknow.Abp.Account.Blazor.Server.FluentDesignUI;

public partial class LoginController
{
    // [HttpGet("api/Account/Logout")]
    // public async Task<IActionResult> Logout(string? returnUrl = null, string? returnUrlHash = null)
    // {
    //     await identitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
    //     {
    //         Identity = IdentitySecurityLogIdentityConsts.Identity,
    //         Action = IdentitySecurityLogActionConsts.Logout
    //     });
    //
    //     await signInManager.SignOutAsync();
    //     
    //     if (returnUrl != null)
    //     {
    //         return await RedirectSafelyAsync(returnUrl, returnUrlHash);
    //     }
    //
    //     if (await settingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin))
    //     {
    //         return RedirectToPage("/Account/Login");
    //     }
    //
    //     return RedirectToPage("/");
    // }
}