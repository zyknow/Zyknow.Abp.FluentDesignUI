using Volo.Abp.Data;
#pragma warning disable CS8604 // 引用类型参数可能为 null。

namespace Zyknow.Abp.FluentDesignUI.Components.ObjectExtending;

public partial class TimeExtensionProperty<TEntity, TResourceType>
    where TEntity : IHasExtraProperties
{
    protected TimeOnly? Value
    {
        get { return PropertyInfo.GetInputValueOrDefault<TimeOnly?>(Entity.GetProperty(PropertyInfo.Name)); }
        set { Entity.SetProperty(PropertyInfo.Name, value, false); }
    }
}