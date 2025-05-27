using Volo.Abp.Bundling;

namespace Zyknow.Abp.CmsKit.Blazor.Public.WebAssembly.FluentDesignUI;

public class CmsKitComponentsBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {
    }

    public void AddStyles(BundleContext context)
    {
        context.Add(
            "/_content/Zyknow.Abp.CmsKit.Blazor.Public.FluentDesignUI/cms-kit/cms-public-kit.css");
    }
}