namespace Zyknow.Abp.FluentDesignUI;

public class FluentTableColumnDictionary : Dictionary<string, List<FluentTableColumn>>
{
    public List<FluentTableColumn> Get<T>()
    {
        return this.GetOrAdd(typeof(T).FullName!, () => new List<FluentTableColumn>());
    }
}