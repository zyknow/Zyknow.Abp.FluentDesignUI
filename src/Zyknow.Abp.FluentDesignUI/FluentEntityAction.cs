using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;

namespace Zyknow.Abp.FluentDesignUI;

public class FluentEntityAction : EntityAction
{
    // hidden
    private object? Color { get; set; }

    public Icon? Icon { get; set; }

    public Appearance? Appearance { get; set; }
}