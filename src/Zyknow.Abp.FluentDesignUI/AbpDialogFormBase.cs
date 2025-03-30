using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Localization;

namespace Zyknow.Abp.FluentDesignUI;

public class AbpDialogOnlyFormBase<TInput> : AbpDialogOnlyFormBase<TInput, DefaultResource>
{
}

public class AbpDialogOnlyFormBase<TInput, TResource> : AbpComponentBase, IDialogContentComponent<TInput>
{
    public AbpDialogOnlyFormBase()
    {
        LocalizationResource = typeof(TResource);
    }

    [Inject] protected AbpBlazorMessageLocalizerHelper<TResource> LH { get; set; }

    [Parameter] public TInput Content { get; set; }
    
    [Parameter] public EventCallback<TInput> ContentChanged { get; set; }
}

public class AbpDialogFormBase<TInput> : AbpDialogFormBase<TInput, DefaultResource>
{
}

public class AbpDialogFormBase<TInput, TResource> : AbpDialogOnlyFormBase<TInput, TResource>,
    IDialogContentComponent<TInput>
{
    public AbpDialogFormBase()
    {
        LocalizationResource = typeof(TResource);
    }

    [Parameter] public Func<Task<DialogResult?>> SubmitClick { get; set; }

    [Parameter] public EventCallback CancelClick { get; set; }

    [CascadingParameter] public FluentDialog? DialogRef { get; set; }
}