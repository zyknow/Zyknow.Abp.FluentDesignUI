using Volo.Abp.Data;
#pragma warning disable CS8604 // 引用类型参数可能为 null。

namespace Zyknow.Abp.FluentDesignUI.Components.ObjectExtending;

public partial class DateTimeExtensionProperty<TEntity, TResourceType>
    where TEntity : IHasExtraProperties
{
    protected DateTime? Value {
        get {
            return PropertyInfo.GetInputValueOrDefault<DateTime?>(Entity.GetProperty(PropertyInfo.Name));
        }
        set {
            Entity.SetProperty(PropertyInfo.Name, value, false);
        }
    }
}
