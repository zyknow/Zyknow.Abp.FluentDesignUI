using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web;
using Zyknow.Abp.FluentDesignUI.Components;

namespace Zyknow.Abp.FluentDesignUI;

public abstract class AbpCreateOrUpdateDialogBase<TInput> : AbpCreateOrUpdateDialogBase<TInput, AbpUiResource>;

public abstract class AbpCreateOrUpdateDialogBase<TInput, TResource> : AbpComponentBase, IDialogContentComponent<TInput>
{
    [Parameter] public virtual RenderFragment? Header { get; set; }

    [Parameter] public virtual RenderFragment ChildContent { get; set; }
    [Parameter] public virtual RenderFragment? Footer { get; set; }

    [Parameter] public TInput Content { get; set; }

    [Inject] protected AbpBlazorMessageLocalizerHelper<TResource> LH { get; set; }

    protected virtual string? HeaderText { get; }


    [CascadingParameter] public FluentDialog? Dialog { get; set; }

    private FluentEditForm FormRef;

    protected bool SubmitBtnLoading;


    [Parameter] public EventCallback SubmitClick { get; set; }

    [Parameter] public EventCallback CancelClick { get; set; }

    public AbpCreateOrUpdateDialogBase()
    {
        LocalizationResource = typeof(TResource);
    }


    async Task SubmitAsync()
    {
        SubmitBtnLoading = true;

        try
        {
            var validate = true;
            if (FormRef != null)
            {
                validate = FormRef.EditContext.Validate();
            }

            if (validate)
            {
                await SubmitClick.InvokeAsync();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
        finally
        {
            SubmitBtnLoading = false;
        }
    }

    async Task CloseDialogAsync()
    {
        await Dialog?.CloseAsync();
        await CancelClick.InvokeAsync();
    }
}


