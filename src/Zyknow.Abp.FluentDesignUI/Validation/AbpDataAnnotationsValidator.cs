using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web;

namespace Zyknow.Abp.FluentDesignUI.Validation;

public class AbpDataAnnotationsValidator : AbpComponentBase, IDisposable
{
    private IDisposable? _subscriptions;
    private EditContext? _originalEditContext;

    [CascadingParameter] EditContext? CurrentEditContext { get; set; }

    [Parameter] public Func<string, IEnumerable<string>, string>? Localize { get; set; }

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException($"{nameof(DataAnnotationsValidator)} requires a cascading " +
                                                $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(DataAnnotationsValidator)} " +
                                                $"inside an EditForm.");
        }

        _subscriptions = new AbpDataAnnotationsEventSubscriptions(CurrentEditContext, Localize, ScopedServices);
        //_subscriptions = CurrentEditContext.EnableDataAnnotationsValidation(ScopedServices);
        _originalEditContext = CurrentEditContext;
    }

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        if (CurrentEditContext != _originalEditContext)
        {
            // While we could support this, there's no known use case presently. Since InputBase doesn't support it,
            // it's more understandable to have the same restriction.
            throw new InvalidOperationException($"{GetType()} does not support changing the " +
                                                $"{nameof(EditContext)} dynamically.");
        }
    }

    /// <inheritdoc/>
    protected virtual void Dispose(bool disposing)
    {
    }

    void IDisposable.Dispose()
    {
        _subscriptions?.Dispose();
        _subscriptions = null;

        Dispose(disposing: true);
    }
}