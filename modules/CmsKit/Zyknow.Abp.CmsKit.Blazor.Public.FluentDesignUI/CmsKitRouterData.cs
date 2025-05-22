using Microsoft.AspNetCore.Components;

namespace Zyknow.Abp.CmsKit.Blazor.Public.FluentDesignUI;

public class CmsKitRouterData(RouteData routeData, Type layout)
{
    public RouteData RouteData { get; set; } = routeData;
    public Type Layout { get; set; } = layout;
}