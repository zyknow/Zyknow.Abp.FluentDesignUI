using Volo.Abp.Bundling;

namespace Zyknow.Abp.CmsKit.Blazor.Admin.WebAssembly.FluentDesignUI;

public class CmsKitComponentsWebAssemblyBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {
    }

    public void AddStyles(BundleContext context)
    {
        // TODO: is this needed?
        // context.Add("_content/TinyMCE.Blazor/tinymce-blazor.js");
    }
}