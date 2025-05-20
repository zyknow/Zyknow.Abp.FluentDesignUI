using Volo.Abp.Data;

#pragma warning disable CS8604 // 引用类型参数可能为 null。

namespace Zyknow.Abp.FluentDesignUI.Components.ObjectExtending;

public partial class TextExtensionProperty<TEntity, TResourceType>
    where TEntity : IHasExtraProperties
{
    protected string Value
    {
        get { return PropertyInfo.GetTextInputValueOrNull(Entity.GetProperty(PropertyInfo.Name)); }
        set { Entity.SetProperty(PropertyInfo.Name, value, validate: false); }
    }
}