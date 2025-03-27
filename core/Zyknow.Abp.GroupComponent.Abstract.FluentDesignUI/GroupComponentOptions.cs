namespace Zyknow.Abp.GroupComponent.Abstract.FluentDesignUI;

public class GroupComponentOptions
{
    public List<IGroupComponentContributor> Contributors { get; } = [];

    public List<IGroupComponentContributor> GetContributors(string groupKey)
    {
        return Contributors
            .Where(x => x.GroupKey == groupKey)
            .ToList() ?? [];
    }
}