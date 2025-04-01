using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Caching;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.UI.Navigation.Urls;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Zyknow.Abp.Account.Blazor.Server.FluentDesignUI;

public partial class AccountController(
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