using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.DependencyInjection;

namespace Zyknow.Abp.FluentDesignUI;

[Dependency(ReplaceServices = true)]
public class FluentDesignUiNotificationService(IStringLocalizer<AbpUiResource> localizer)
    : IUiNotificationService, IScopedDependency
{
    [Inject] public IDialogService DialogService { get; set; }

    private readonly IStringLocalizer<AbpUiResource> _localizer = localizer;

    public async Task Info(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        DialogService.ShowInfo(message, title ?? localizer["Info"]);
    }

    public async Task Success(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        DialogService.ShowSuccess(message, title ?? localizer["Success"]);
    }

    public async Task Warn(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        DialogService.ShowWarning(message, title ?? localizer["Warn"]);
    }

    public async Task Error(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        DialogService.ShowError(message, title ?? localizer["Error"]);
    }
}