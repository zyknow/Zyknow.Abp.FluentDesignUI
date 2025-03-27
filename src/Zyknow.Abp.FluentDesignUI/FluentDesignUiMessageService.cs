using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.DependencyInjection;

namespace Zyknow.Abp.FluentDesignUI;

[Dependency(ReplaceServices = true)]
public class FluentDesignUiMessageService(IStringLocalizer<AbpUiResource> localizer)
    : IUiMessageService, IScopedDependency
{
    [Inject] public IToastService ToastService { get; set; }
    [Inject] public IDialogService DialogService { get; set; }

    public async Task Info(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        ToastService.ShowInfo(message);
    }

    public async Task Success(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        ToastService.ShowSuccess(message);
    }

    public async Task Warn(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        ToastService.ShowWarning(message);
    }

    public async Task Error(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        ToastService.ShowError(message);
    }

    public async Task<bool> Confirm(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        var res = await DialogService.ShowConfirmationAsync(
            message,
            localizer["Yes"],
            localizer["Cancel"],
            title ?? localizer["Confirm"]
        );
        var result = await res.Result;
        return !result.Cancelled;
    }
}