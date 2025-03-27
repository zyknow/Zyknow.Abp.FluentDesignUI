namespace Zyknow.Abp.GroupComponent.Abstract.FluentDesignUI;

public interface IGroupComponentContributor
{
    /// <summary>
    /// AccountManage Or Setting , or you can add your own
    /// </summary>
    public string GroupKey { get; }

    Task ConfigureAsync(GroupComponentCreationContext context);

    Task<bool> CheckPermissionsAsync(GroupComponentCreationContext context);
}