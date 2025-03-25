using Volo.Abp.Bundling;

namespace Zyknow.Abp.AspnetCore.Components.WebAssembly.FluentDesignTheme;

public class ComponentsComponentsBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {
        context.Add("_content/Microsoft.AspNetCore.Components.WebAssembly.Authentication/AuthenticationService.js");
        context.Add("_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/abp.js");
        context.Add("_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/lang-utils.js");
        // context.Add("/_content/Microsoft.FluentUI.AspNetCore.Components/Microsoft.FluentUI.AspNetCore.Components.lib.module.js");
    }

    public void AddStyles(BundleContext context)
    {
        context.Add("/_content/Microsoft.FluentUI.AspNetCore.Components/css/reboot.css");
        context.Add("_content/Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme/libs/abp/css/theme.css");
    }
}
