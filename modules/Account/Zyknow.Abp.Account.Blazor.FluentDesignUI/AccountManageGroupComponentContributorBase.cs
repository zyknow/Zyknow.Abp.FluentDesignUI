using Zyknow.Abp.GroupComponent.FluentDesignUI;

namespace Zyknow.Abp.Account.Blazor.FluentDesignUI;

public abstract class AccountManageGroupComponentContributorBase: IGroupComponentContributor
{
    public const string AccountGroupKey = "AccountManage";
    public string GroupKey => AccountGroupKey;

    public abstract Task ConfigureAsync(GroupComponentCreationContext context);

    public abstract Task<bool> CheckPermissionsAsync(GroupComponentCreationContext context);
    
}