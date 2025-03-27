using Microsoft.AspNetCore.Components;
using Volo.Abp.Account;
using Volo.Abp.Account.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Zyknow.Abp.Account.Blazor.FluentDesignUI;

public abstract class AccountBlazorFluentDesignComponentBase : AbpComponentBase
{
    protected AccountBlazorFluentDesignComponentBase()
    {
        LocalizationResource = typeof(AccountResource);
    }

    [Inject] protected IAccountAppService AccountAppService { get; set; }

    [Inject] protected NavigationManager NavigationManager { get; set; }
}