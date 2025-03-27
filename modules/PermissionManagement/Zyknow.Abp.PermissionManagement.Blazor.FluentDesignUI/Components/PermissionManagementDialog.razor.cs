using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web.Configuration;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Localization;

namespace Zyknow.Abp.PermissionManagement.Blazor.FluentDesignUI.Components;

public class PermissionManagementDialogInput(string providerName, string? providerKey = null, string entityDisplayName = null)
{
    public string ProviderName { get; set; } = providerName;
    public string? ProviderKey { get; set; } = providerKey;
    public string? EntityDisplayName { get; set; } = entityDisplayName;
}

public partial class PermissionManagementDialog
{
    [Inject]
    protected IPermissionAppService PermissionAppService { get; set; }

    [Inject]
    protected ICurrentApplicationConfigurationCacheResetService CurrentApplicationConfigurationCacheResetService { get; set; }

    private List<PermissionGroupDto> _groups;
    private List<PermissionGrantInfoDto> _disabledPermissions = new();
    private string _selectedTabName;



    private bool _loading = false;

    public PermissionManagementDialog()
    {
        LocalizationResource = typeof(AbpPermissionManagementResource);
    }

    protected override async Task OnInitializedAsync()
    {
        await ShowAsync(Content.ProviderName, Content.ProviderKey, Content.EntityDisplayName);
    }

    private async Task<bool> SaveAsync()
    {
        try
        {
            var updateDto = new UpdatePermissionsDto
            {
                Permissions = _groups
                    .SelectMany(g => g.Permissions)
                    .Select(p => new UpdatePermissionDto { IsGranted = p.IsGranted, Name = p.Name })
                    .ToArray()
            };

            await PermissionAppService.UpdateAsync(Content.ProviderName, Content.ProviderKey, updateDto);

            await CurrentApplicationConfigurationCacheResetService.ResetAsync();

            return true;
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
            return false;
        }
    }

    protected virtual async Task CancelAsync()
    {

    }

    public async Task ShowAsync(string providerName, string providerKey, string entityDisplayName = null)
    {
        _loading = true;
        await InvokeAsync(StateHasChanged);
        try
        {
            Content.ProviderName = providerName;
            Content.ProviderKey = providerKey;
            Content.EntityDisplayName = entityDisplayName;

            var result = await PermissionAppService.GetAsync(Content.ProviderName, Content.ProviderKey);

            Content.EntityDisplayName = entityDisplayName ?? result.EntityDisplayName;
            _groups = result.Groups;

            foreach (var permission in _groups.SelectMany(x => x.Permissions))
            {
                if (permission.IsGranted &&
                    permission.GrantedProviders.All(x => x.ProviderName != Content.ProviderName))
                {
                    _disabledPermissions.Add(permission);
                }
            }

            _selectedTabName = GetNormalizedGroupName(_groups.First().Name);
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

    private string GetNormalizedGroupName(string name)
    {
        return "PermissionGroup_" + name.Replace(".", "_");
    }

    private void GrantAllChanged(bool value)
    {
        foreach (var groupDto in _groups)
        {
            foreach (var permission in groupDto.Permissions)
            {
                if (!IsDisabledPermission(permission))
                {
                    SetPermissionGrant(permission, value);
                }
            }
        }
    }

    private void GroupGrantAllChanged(bool value, PermissionGroupDto permissionGroup)
    {
        foreach (var permission in permissionGroup.Permissions)
        {
            if (!IsDisabledPermission(permission))
            {
                SetPermissionGrant(permission, value);
            }
        }
    }

    private bool IsDisabledPermission(PermissionGrantInfoDto permissionGrantInfo)
    {
        return _disabledPermissions.Any(x => x == permissionGrantInfo);
    }

    private void PermissionChanged(bool value, PermissionGroupDto permissionGroup, PermissionGrantInfoDto permission)
    {
        SetPermissionGrant(permission, value);

        if (value && permission.ParentName != null)
        {
            var parentPermission = GetParentPermission(permissionGroup, permission);

            SetPermissionGrant(parentPermission, true);
        }
        else if (value == false)
        {
            var childPermissions = GetChildPermissions(permissionGroup, permission);

            foreach (var childPermission in childPermissions)
            {
                SetPermissionGrant(childPermission, false);
            }
        }
    }

    private void SetPermissionGrant(PermissionGrantInfoDto permission, bool value)
    {
        if (permission.IsGranted == value)
        {
            return;
        }

        permission.IsGranted = value;
    }

    private PermissionGrantInfoDto GetParentPermission(PermissionGroupDto permissionGroup, PermissionGrantInfoDto permission)
    {
        return permissionGroup.Permissions.First(x => x.Name == permission.ParentName);
    }

    private List<PermissionGrantInfoDto> GetChildPermissions(PermissionGroupDto permissionGroup, PermissionGrantInfoDto permission)
    {
        return permissionGroup.Permissions.Where(x => x.Name.StartsWith(permission.Name)).ToList();
    }

    private string GetShownName(PermissionGrantInfoDto permissionGrantInfo)
    {
        if (!IsDisabledPermission(permissionGrantInfo))
        {
            return permissionGrantInfo.DisplayName;
        }

        return string.Format(
            "{0} ({1})",
            permissionGrantInfo.DisplayName,
            permissionGrantInfo.GrantedProviders
                .Where(p => p.ProviderName != Content.ProviderName)
                .Select(p => p.ProviderName)
                .JoinAsString(", ")
        );
    }
}
