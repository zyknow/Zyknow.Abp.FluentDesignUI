using System.Reflection;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Routing;

public class AbpRouterOptions
{
    public Assembly AppAssembly { get; set; }

    public RouterAssemblyList AdditionalAssemblies { get; } = new();
}
