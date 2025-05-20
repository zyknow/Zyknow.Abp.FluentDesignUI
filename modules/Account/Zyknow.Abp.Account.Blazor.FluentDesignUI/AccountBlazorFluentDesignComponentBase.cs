using Microsoft.AspNetCore.Components;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.ExceptionHandling;
using Zyknow.Abp.FluentDesignUI;

namespace Zyknow.Abp.Account.Blazor.FluentDesignUI;

public abstract class AccountBlazorFluentDesignComponentBase : FluentAbpComponentBase<AccountResource>
{
    [Inject] protected IAccountAppService AccountAppService { get; set; }

    [Inject] protected IUiMessageService UiMessageService { get; set; }

    [Inject] protected IUiNotificationService UiNotificationService { get; set; }

    [Inject] protected IExceptionToErrorInfoConverter ExceptionToErrorInfoConverter { get; set; }


    protected virtual string GetLocalizeExceptionMessage(Exception exception)
    {
        if (exception is ILocalizeErrorMessage || exception is IHasErrorCode)
        {
#pragma warning disable CS0618 // 类型或成员已过时
            return ExceptionToErrorInfoConverter.Convert(exception, false).Message;
#pragma warning restore CS0618 // 类型或成员已过时
        }

        return exception.Message;
    }
}