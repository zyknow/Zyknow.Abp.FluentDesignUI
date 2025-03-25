namespace Zyknow.Abp.FluentDesignUI;

public class FluentEntityActionDictionary : Dictionary<string, List<FluentEntityAction>>
{
    public List<FluentEntityAction> Get<T>()
    {
        return this.GetOrAdd(typeof(T).FullName!, () => new List<FluentEntityAction>());
    }
}