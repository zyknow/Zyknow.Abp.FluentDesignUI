using Volo.Abp;

namespace Zyknow.Abp.GroupComponent.Abstract.FluentDesignUI;


public class ComponentGroup
{
    public string Id {
        get => _id;
        set => _id = Check.NotNullOrWhiteSpace(value, nameof(Id));
    }
    private string _id;

    public string DisplayName {
        get => _displayName;
        set => _displayName = Check.NotNullOrWhiteSpace(value, nameof(DisplayName));
    }
    private string _displayName;

    public Type ComponentType {
        get => _componentType;
        set => _componentType = Check.NotNull(value, nameof(ComponentType));
    }
    private Type _componentType;

    public object Parameter { get; set; }

    public ComponentGroup(string id, string displayName,Type componentType, object parameter = null)
    {
        Id = id;
        DisplayName = displayName;
        ComponentType = componentType;
        Parameter = parameter;
    }
}
