using Zyknow.Abp.GroupComponent.Abstract.FluentDesignUI;

namespace Zyknow.Abp.SettingManagement.Blazor.FluentDesignUI;

public abstract class SettingGroupComponentContributorBase : IGroupComponentContributor
{
    public const string SettingGroupKey = "Setting";
    public string GroupKey => SettingGroupKey;

    public abstract Task ConfigureAsync(GroupComponentCreationContext context);

    public abstract Task<bool> CheckPermissionsAsync(GroupComponentCreationContext context);
}