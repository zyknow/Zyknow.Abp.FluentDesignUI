using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Icons.Regular;
using Volo.Abp.FeatureManagement;
using Volo.Abp.ObjectExtending;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.Localization;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.PageToolbars;
using Zyknow.Abp.FeatureManagement.Blazor.FluentDesignUI.Components;
using Zyknow.Abp.FluentDesignUI;

namespace Zyknow.Abp.TenantManagement.Blazor.FluentDesignUI.Pages;

public partial class TenantManagement
{
    protected const string FeatureProviderName = "T";

    protected bool HasManageFeaturesPermission;
    protected string ManageFeaturesPolicyName;
    protected PageToolbar Toolbar { get; } = new();


    protected bool FeatureDialogHidden = true;

    protected FeatureManagementDialog FeatureDialogRef;

    protected List<FluentTableColumn> TenantManagementTableColumns => TableColumns.Get<TenantManagement>();

    public TenantManagement()
    {
        LocalizationResource = typeof(AbpTenantManagementResource);
        ObjectMapperContext = typeof(AbpTenantManagementBlazorFluentDesignModule);

        CreatePolicyName = TenantManagementPermissions.Tenants.Create;
        UpdatePolicyName = TenantManagementPermissions.Tenants.Update;
        DeletePolicyName = TenantManagementPermissions.Tenants.Delete;

        ManageFeaturesPolicyName = TenantManagementPermissions.Tenants.ManageFeatures;
    }

    protected override ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new AbpBreadcrumbItem(L["Menu:TenantManagement"]));
        BreadcrumbItems.Add(new AbpBreadcrumbItem(L["Tenants"]));

        return base.SetBreadcrumbItemsAsync();
    }

    protected override async Task SetPermissionsAsync()
    {
        await base.SetPermissionsAsync();

        HasManageFeaturesPermission = await AuthorizationService.IsGrantedAsync(ManageFeaturesPolicyName);
    }

    protected override string GetDeleteConfirmationMessage(TenantDto entity)
    {
        return string.Format(L["TenantDeletionConfirmationMessage"], entity.Name);
    }

    protected override ValueTask SetToolbarItemsAsync()
    {
        Toolbar.AddButton(L["ManageHostFeatures"],
            async () => { await FeatureDialogRef.ShowAsync(FeatureProviderName); },
            new Size20.Settings(),
            requiredPolicyName: FeatureManagementPermissions.ManageHostFeatures);
        Toolbar.AddButton(L["NewTenant"],
            OpenCreateDialogAsync,
            new Size20.Add(),
            requiredPolicyName: CreatePolicyName);

        return base.SetToolbarItemsAsync();
    }

    protected override ValueTask SetEntityActionsAsync()
    {
        EntityActions
            .Get<TenantManagement>()
            .AddRange([
                new()
                {
                    Text = L["Edit"],
                    Visible = (data) => HasUpdatePermission,
                    Clicked = async (data) => { await OpenEditDialogAsync(data.As<TenantDto>()); }
                },
                new()
                {
                    Text = L["Features"],
                    Visible = (data) => HasManageFeaturesPermission,
                    Clicked = async (data) =>
                    {
                        var tenant = data.As<TenantDto>();
                        await FeatureDialogRef.ShowAsync(FeatureProviderName, tenant.Id.ToString());
                    }
                },
                new()
                {
                    Text = L["Delete"],
                    Visible = (data) => HasDeletePermission,
                    Clicked = async (data) => await DeleteEntityAsync(data.As<TenantDto>()),
                    ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<TenantDto>())
                }
            ]);

        return base.SetEntityActionsAsync();
    }

    protected override ValueTask SetTableColumnsAsync()
    {
        TenantManagementTableColumns
            .AddRange([
                new FluentTableColumn
                {
                    Title = L["TenantName"],
                    Sortable = true,
                    Data = nameof(TenantDto.Name),
                    PropertyName = nameof(TenantDto.Name)
                }
            ]);

        TenantManagementTableColumns.AddRange(GetExtensionTableColumns(
            TenantManagementModuleExtensionConsts.ModuleName,
            TenantManagementModuleExtensionConsts.EntityNames.Tenant));

        TenantManagementTableColumns.Add(new FluentTableColumn
        {
            Title = L["Actions"],
            Actions = EntityActions.Get<TenantManagement>()
        });

        return base.SetTableColumnsAsync();
    }
}