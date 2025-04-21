using Volo.Abp.Modularity;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;

namespace Zyknow.Abp.GroupComponent.FluentDesignUI;

[DependsOn(typeof(AbpAspNetCoreComponentsWebFluentDesignThemeModule))]
public class AbpGroupComponentAbstractFluentDesignModule : AbpModule
{
}