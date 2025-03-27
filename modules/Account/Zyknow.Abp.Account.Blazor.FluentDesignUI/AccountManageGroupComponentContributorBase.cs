using Zyknow.Abp.GroupComponent.Abstract.FluentDesignUI;

namespace Zyknow.Abp.Account.Blazor.FluentDesignUI;

public abstract class AccountManageGroupComponentContributorBase: IGroupComponentContributor
{
    public const string SettingGroupKey = "AccountManage";
    public string GroupKey => SettingGroupKey;

    public abstract Task ConfigureAsync(GroupComponentCreationContext context);

    public abstract Task<bool> CheckPermissionsAsync(GroupComponentCreationContext context);
    
}