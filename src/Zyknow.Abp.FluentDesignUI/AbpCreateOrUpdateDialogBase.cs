using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web;

namespace Zyknow.Abp.FluentDesignUI;
public class AbpCreateOrUpdateDialogBase<TInput, TResource> : AbpComponentBase, IDialogContentComponent<TInput>
{
    [Parameter] public virtual RenderFragment? HeaderTemplate { get; set; }

    [Parameter] public virtual RenderFragment ChildContent { get; set; }
    [Parameter] public virtual RenderFragment? FooterTemplate { get; set; }

    [Parameter] public TInput Content { get; set; }

    [Inject] protected AbpBlazorMessageLocalizerHelper<TResource> LH { get; set; }


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
