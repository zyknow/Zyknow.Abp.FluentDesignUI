using Microsoft.AspNetCore.Components;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.AspNetCore.Components.Notifications;

namespace Zyknow.Abp.Account.Blazor.FluentDesignUI;

public abstract class AccountBlazorFluentDesignComponentBase : AbpComponentBase
{
    protected AccountBlazorFluentDesignComponentBase()
    {
        LocalizationResource = typeof(AccountResource);
    }

    [Inject] protected IAccountAppService AccountAppService { get; set; }

    [Inject] protected NavigationManager NavigationManager { get; set; }
    
    [Inject] protected IUiMessageService UiMessageService { get; set; }
    
    [Inject] protected IUiNotificationService UiNotificationService { get; set; }
     
}