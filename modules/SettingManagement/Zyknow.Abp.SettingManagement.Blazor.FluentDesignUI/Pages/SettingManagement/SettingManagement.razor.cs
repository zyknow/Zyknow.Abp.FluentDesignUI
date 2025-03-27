using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Volo.Abp.SettingManagement.Localization;
using Zyknow.Abp.FluentDesignUI;
using Zyknow.Abp.GroupComponent.Abstract.FluentDesignUI;
namespace Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI.Pages.SettingManagement;

public partial class SettingManagement
{
    [Inject]
    protected IServiceProvider ServiceProvider { get; set; }

    protected GroupComponentCreationContext SettingComponentCreationContext { get; set; }

    [Inject]
    protected IOptions<GroupComponentOptions> _options { get; set; }
    
    [Inject]
    protected IStringLocalizer<AbpSettingManagementResource> L { get; set; }

    protected GroupComponentOptions Options => _options.Value;

    protected List<RenderFragment> SettingItemRenders { get; set; } = new();

    protected ComponentGroup SelectedComponentGroup { get; set; }
    protected List<AbpBreadcrumbItem> BreadcrumbItems = new();


    protected override async Task OnInitializedAsync()
    {
        BreadcrumbItems.Add(new AbpBreadcrumbItem(L["Settings"]));
        SettingComponentCreationContext = new GroupComponentCreationContext(ServiceProvider);

        foreach (var contributor in Options.Contributors)
        {
            await contributor.ConfigureAsync(SettingComponentCreationContext);
        }

        SettingItemRenders.Clear();
        SelectedComponentGroup = SettingComponentCreationContext.Groups.FirstOrDefault();
    }

    protected virtual string GetNormalizedString(string value)
    {
        return value.Replace('.', '_');
    }
}
