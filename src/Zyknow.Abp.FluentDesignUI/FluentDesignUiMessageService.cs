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
    [Inject] public IDialogService DialogService { get; set; }

    public async Task Info(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        DialogService.ShowInfo(message, title ?? localizer["Info"]);
    }

    public async Task Success(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        DialogService.ShowSuccess(message, title ?? localizer["Success"]);
    }

    public async Task Warn(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        DialogService.ShowWarning(message, title ?? localizer["Warn"]);
    }

    public async Task Error(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        DialogService.ShowError(message, title ?? localizer["Error"]);
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