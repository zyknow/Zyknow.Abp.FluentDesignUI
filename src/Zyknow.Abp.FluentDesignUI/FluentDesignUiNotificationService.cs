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
    [Inject] public IToastService ToastService { get; set; }

    private readonly IStringLocalizer<AbpUiResource> _localizer = localizer;

    public async Task Info(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        ToastService.ShowInfo(message);
    }

    public async Task Success(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        ToastService.ShowSuccess(message);
    }

    public async Task Warn(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        ToastService.ShowWarning(message);
    }

    public async Task Error(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        ToastService.ShowError(message);
    }
}