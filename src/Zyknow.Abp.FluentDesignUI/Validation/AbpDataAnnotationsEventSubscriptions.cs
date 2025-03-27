using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Metadata;

namespace Zyknow.Abp.FluentDesignUI.Validation;

internal class AbpDataAnnotationsEventSubscriptions : IDisposable
{
    private static readonly ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo?> _propertyInfoCache =
        new();

    private readonly EditContext _editContext;
    private readonly IServiceProvider? _serviceProvider;
    private readonly ValidationMessageStore _messages;
    private readonly EditContextValidator _contextValidator;
    private readonly Func<string, IEnumerable<string>, string>? _localize;
    private static event Action? OnClearCache;

    public AbpDataAnnotationsEventSubscriptions(EditContext editContext,
        Func<string, IEnumerable<string>, string>? localize, IServiceProvider serviceProvider)
    {
        _editContext = editContext ?? throw new ArgumentNullException(nameof(editContext));
        _serviceProvider = serviceProvider;
        _messages = new ValidationMessageStore(_editContext);
        _localize = localize;
        _editContext.OnFieldChanged += OnFieldChanged;
        _editContext.OnValidationRequested += OnValidationRequested;

        _contextValidator =
            new EditContextValidator(new ValidationMessageLocalizerAttributeFinder(), _serviceProvider);

        if (MetadataUpdater.IsSupported)
        {
            OnClearCache += ClearCache;
        }
    }

    [UnconditionalSuppressMessage("Trimming", "IL2026",
        Justification = "Model types are expected to be defined in assemblies that do not get trimmed.")]
    private void OnFieldChanged(object? sender, FieldChangedEventArgs eventArgs)
    {
        var fieldIdentifier = eventArgs.FieldIdentifier;
        _editContext.ClearValidationMessages(fieldIdentifier);
        _contextValidator.ValidateField(_editContext, _messages, fieldIdentifier, _localize);
        _editContext.NotifyValidationStateChanged();
    }

    [UnconditionalSuppressMessage("Trimming", "IL2026",
        Justification = "Model types are expected to be defined in assemblies that do not get trimmed.")]
    private void OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
    {
        _contextValidator.Validate(_editContext, _messages, _localize);
    }

    public void Dispose()
    {
        _messages.Clear();
        _editContext.OnFieldChanged -= OnFieldChanged;
        _editContext.OnValidationRequested -= OnValidationRequested;
        _editContext.NotifyValidationStateChanged();

        if (MetadataUpdater.IsSupported)
        {
            OnClearCache -= ClearCache;
        }
    }

    [UnconditionalSuppressMessage("Trimming", "IL2080",
        Justification = "Model types are expected to be defined in assemblies that do not get trimmed.")]
    private static bool TryGetValidatableProperty(in FieldIdentifier fieldIdentifier,
        [NotNullWhen(true)] out PropertyInfo? propertyInfo)
    {
        var cacheKey = (ModelType: fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);
        if (!_propertyInfoCache.TryGetValue(cacheKey, out propertyInfo))
        {
            // DataAnnotations only validates public properties, so that's all we'll look for
            // If we can't find it, cache 'null' so we don't have to try again next time
            propertyInfo = cacheKey.ModelType.GetProperty(cacheKey.FieldName);

            // No need to lock, because it doesn't matter if we write the same value twice
            _propertyInfoCache[cacheKey] = propertyInfo;
        }

        return propertyInfo != null;
    }

    internal void ClearCache()
    {
        _propertyInfoCache.Clear();
    }
}