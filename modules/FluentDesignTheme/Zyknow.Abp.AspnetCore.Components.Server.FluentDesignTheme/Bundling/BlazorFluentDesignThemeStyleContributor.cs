using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme.Bundling;

public class BlazorFluentDesignThemeStyleContributor: BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_content/Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme/libs/abp/css/theme.css");
    }
}
