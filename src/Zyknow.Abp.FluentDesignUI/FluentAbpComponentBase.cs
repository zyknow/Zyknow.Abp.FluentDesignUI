using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Localization;

namespace Zyknow.Abp.FluentDesignUI;

public abstract class FluentAbpComponentBase : AbpComponentBase
{
    protected IAbpEnumLocalizer AbpEnumLocalizer => LazyGetNonScopedRequiredService(ref _abpEnumLocalizer)!;
    private IAbpEnumLocalizer? _abpEnumLocalizer;

    protected IDialogService DialogService => LazyGetNonScopedRequiredService(ref _dialogService)!;
    private IDialogService? _dialogService;

    protected NavigationManager NavigationManager => LazyGetNonScopedRequiredService(ref _navigationManager)!;
    private NavigationManager? _navigationManager;
}

public abstract class FluentAbpComponentBase<TResource> : FluentAbpComponentBase
{
    protected AbpBlazorMessageLocalizerHelper<TResource> LH => LazyGetNonScopedRequiredService(ref _lh)!;
    private AbpBlazorMessageLocalizerHelper<TResource>? _lh;

    public FluentAbpComponentBase()
    {
        LocalizationResource = typeof(TResource);
    }
}