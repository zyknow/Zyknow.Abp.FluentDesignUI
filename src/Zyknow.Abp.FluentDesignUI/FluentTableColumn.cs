using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;

namespace Zyknow.Abp.FluentDesignUI;

public class FluentTableColumn : TableColumn
{
    public List<FluentEntityAction> Actions { get; set; } = [];

    public bool CanHidden { get; set; } = true;
}