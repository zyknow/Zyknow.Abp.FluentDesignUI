using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Volo.Abp.SettingManagement.Blazor;
using Volo.Abp.SettingManagement.Localization;
using Zyknow.Abp.FluentDesignUI;

namespace Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI.Pages.SettingManagement;

public partial class SettingManagement
{
    [Inject]
    protected IServiceProvider ServiceProvider { get; set; }

    protected SettingComponentCreationContext SettingComponentCreationContext { get; set; }

    [Inject]
    protected IOptions<SettingManagementComponentOptions> _options { get; set; }
    
    [Inject]
    protected IStringLocalizer<AbpSettingManagementResource> L { get; set; }

    protected SettingManagementComponentOptions Options => _options.Value;

    protected List<RenderFragment> SettingItemRenders { get; set; } = new();

    protected SettingComponentGroup SelectedGroup { get; set; }
    protected List<AbpBreadcrumbItem> BreadcrumbItems = new();


    protected override async Task OnInitializedAsync()
    {
        BreadcrumbItems.Add(new AbpBreadcrumbItem(L["Settings"]));
        SettingComponentCreationContext = new SettingComponentCreationContext(ServiceProvider);

        foreach (var contributor in Options.Contributors)
        {
            await contributor.ConfigureAsync(SettingComponentCreationContext);
        }

        SettingItemRenders.Clear();
        SelectedGroup = SettingComponentCreationContext.Groups.FirstOrDefault();
    }

    protected virtual string GetNormalizedString(string value)
    {
        return value.Replace('.', '_');
    }
}
