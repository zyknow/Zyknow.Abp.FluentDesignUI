using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Account.Settings;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Caching;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.UI.Navigation.Urls;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Zyknow.Abp.Account.Blazor.Server.FluentDesignUI;

[AllowAnonymous]
public partial class LoginController(
    SignInManager<IdentityUser> signInManager,
    UserManager<IdentityUser> userManager,
    IOptions<IdentityOptions> identityOptions,
    IAppUrlProvider appUrlProvider,
    IdentitySecurityLogManager identitySecurityLogManager,
    ISettingProvider settingProvider,
    IdentityDynamicClaimsPrincipalContributorCache identityDynamicClaimsPrincipalContributorCache,
    IDistributedCache<LoginTicketCacheItem> loginTicketCache)
    : AbpController
{

}