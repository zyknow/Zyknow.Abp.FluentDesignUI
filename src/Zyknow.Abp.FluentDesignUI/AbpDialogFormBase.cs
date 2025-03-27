using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Localization;

namespace Zyknow.Abp.FluentDesignUI;

public class AbpDialogFormBase<TInput> : AbpDialogFormBase<TInput, DefaultResource>
{
}
public class AbpDialogFormBase<TInput, TResource> : AbpComponentBase, IDialogContentComponent<TInput>
{
    public AbpDialogFormBase()
    {
        LocalizationResource = typeof(TResource);
    }

    [Inject] protected AbpBlazorMessageLocalizerHelper<TResource> LH { get; set; }

    [Parameter] public Func<Task<bool>> SubmitClick { get; set; }

    [Parameter] public EventCallback CancelClick { get; set; }

    [CascadingParameter] public FluentDialog? DialogRef { get; set; }

    [Parameter]
    public TInput Content { get; set; }

}
