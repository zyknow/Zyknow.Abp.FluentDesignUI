using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;

namespace Zyknow.Abp.FluentDesignUI;

public class FluentTableColumn : TableColumn
{
#pragma warning disable CS0108, CS0114
    public List<FluentEntityAction> Actions { get; set; } = [];
#pragma warning restore CS0108, CS0114

    public bool CanHidden { get; set; } = true;

    public bool IsCheckIcon { get; set; }
}