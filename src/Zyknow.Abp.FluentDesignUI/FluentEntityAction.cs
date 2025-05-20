using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;

namespace Zyknow.Abp.FluentDesignUI;

public class FluentEntityAction : EntityAction
{
    // hidden
    // private object? Color { get; set; }

#pragma warning disable CS0108, CS0114
    public Icon? Icon { get; set; }
#pragma warning restore CS0108, CS0114

    public Appearance? Appearance { get; set; }
}