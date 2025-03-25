using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.AspNetCore.Components.Web.Configuration;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;

namespace Zyknow.Abp.FeatureManagement.Blazor.FluentDesignUI.Components;

public class FeatureManagementDialogInput
{
    public string ProviderName { get; set; }
    public string? ProviderKey { get; set; }
}

public partial class FeatureManagementDialog
{
    [Inject] protected IFeatureAppService FeatureAppService { get; set; }

    [Inject] protected IUiMessageService UiMessageService { get; set; }
    [Inject] protected IDialogService DialogService { get; set; }

    [Inject] protected IStringLocalizerFactory HtmlLocalizerFactory { get; set; }

    [Inject] protected IOptions<AbpLocalizationOptions> LocalizationOptions { get; set; }

    [Inject]
    protected ICurrentApplicationConfigurationCacheResetService CurrentApplicationConfigurationCacheResetService
    {
        get;
        set;
    }

    [CascadingParameter] public FluentDialog? Dialog { get; set; }

    [Parameter] public FeatureManagementDialogInput Content { get; set; } = new();

    protected List<FeatureGroupDto> Groups { get; set; }

    protected Dictionary<string, bool> ToggleValues;

    protected Dictionary<string, string> SelectionStringValues;

    protected bool _loading;

    protected override void OnInitialized()
    {
        Dialog?.Hide();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // when not dialog or use dialogService to show
            if (Dialog == null || Dialog.Hidden == false)
                await ShowAsync(Content.ProviderName, Content.ProviderKey);
        }
    }

    public virtual async Task ShowAsync(string providerName, string? providerKey = null)
    {
        Content.ProviderName = providerName;
        Content.ProviderKey = providerKey;
        if (Content.ProviderName.IsNullOrWhiteSpace() && Content.ProviderKey.IsNullOrWhiteSpace())
            return;


        try
        {
            _loading = true;
            await InvokeAsync(StateHasChanged);

            ToggleValues = new Dictionary<string, bool>();
            SelectionStringValues = new Dictionary<string, string>();

            Groups = (await FeatureAppService.GetAsync(Content.ProviderName, Content.ProviderKey))?.Groups;

            Groups ??= new List<FeatureGroupDto>();

            foreach (var featureGroupDto in Groups)
            {
                foreach (var featureDto in featureGroupDto.Features)
                {
                    if (featureDto.ValueType is ToggleStringValueType)
                    {
                        ToggleValues.Add(featureDto.Name, bool.Parse(featureDto.Value));
                    }

                    if (featureDto.ValueType is SelectionStringValueType)
                    {
                        SelectionStringValues.Add(featureDto.Name, featureDto.Value);
                    }
                }
            }
            Dialog?.Show();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
        finally
        {
            _loading = false;
            await InvokeAsync(StateHasChanged);
        }
    }


    protected virtual async Task SaveAsync()
    {
        try
        {
            var features = new UpdateFeaturesDto
            {
                Features = Groups.SelectMany(g => g.Features).Select(f => new UpdateFeatureDto
                {
                    Name = f.Name,
                    Value = f.ValueType is ToggleStringValueType ? ToggleValues[f.Name].ToString() :
                        f.ValueType is SelectionStringValueType ? SelectionStringValues[f.Name] : f.Value
                }).ToList()
            };

            await FeatureAppService.UpdateAsync(Content.ProviderName, Content.ProviderKey, features);

            await CurrentApplicationConfigurationCacheResetService.ResetAsync();

            await Dialog?.CloseAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual async Task CancelAsync()
    {
        InvokeAsync(() =>
        {
            Dialog.Hide();
        });
    }

    protected virtual string GetNormalizedGroupName(string name)
    {
        return "FeatureGroup_" + name.Replace(".", "_");
    }

    protected virtual string GetFeatureStyles(FeatureDto feature)
    {
        return $"margin-left: {feature.Depth * 20}px; margin-top: 10px";
    }

    protected virtual bool IsDisabled(string providerName)
    {
        return providerName != Content.ProviderName && providerName != DefaultValueFeatureValueProvider.ProviderName;
    }

    protected virtual async Task OnFeatureValueChangedAsync(string value, FeatureDto feature)
    {
        if (feature?.ValueType?.Validator.IsValid(value) == true)
        {
            feature.Value = value;
        }
        else
        {
            await UiMessageService.Warn(L["Volo.Abp.FeatureManagement:InvalidFeatureValue", feature.DisplayName]);
        }
    }

    protected virtual Task OnSelectedValueChangedAsync(bool value, FeatureDto feature)
    {
        ToggleValues[feature.Name] = value;

        if (value)
        {
            CheckParents(feature.ParentName);
        }
        else
        {
            UncheckChildren(feature.Name);
        }

        return Task.CompletedTask;
    }

    protected virtual void CheckParents(string parentName)
    {
        if (parentName.IsNullOrWhiteSpace())
        {
            return;
        }

        foreach (var featureGroupDto in Groups)
        {
            foreach (var featureDto in featureGroupDto.Features)
            {
                if (featureDto.Name == parentName && ToggleValues.ContainsKey(featureDto.Name))
                {
                    ToggleValues[featureDto.Name] = true;
                    CheckParents(featureDto.ParentName);
                }
            }
        }
    }

    protected virtual void UncheckChildren(string featureName)
    {
        foreach (var featureGroupDto in Groups)
        {
            foreach (var featureDto in featureGroupDto.Features)
            {
                if (featureDto.ParentName == featureName && ToggleValues.ContainsKey(featureDto.Name))
                {
                    ToggleValues[featureDto.Name] = false;
                    UncheckChildren(featureDto.Name);
                }
            }
        }
    }

    protected virtual IStringLocalizer CreateStringLocalizer(string resourceName)
    {
        return StringLocalizerFactory.CreateByResourceNameOrNull(resourceName) ??
               StringLocalizerFactory.CreateDefaultOrNull();
    }
}