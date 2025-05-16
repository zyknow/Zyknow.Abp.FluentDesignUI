using Volo.Abp.Bundling;

namespace Zyknow.Abp.CmsKit.Blazor.Admin.WebAssembly.FluentDesignUI;

public class CmsKitComponentsWebAssemblyBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {
        context.Add("_content/BlazorMonaco/jsInterop.js");
        context.Add("_content/BlazorMonaco/lib/monaco-editor/min/vs/loader.js");
        context.Add("_content/BlazorMonaco/lib/monaco-editor/min/vs/editor/editor.main.js");
    }

    public void AddStyles(BundleContext context)
    {
        // TODO: is this needed?
        // context.Add("_content/TinyMCE.Blazor/tinymce-blazor.js");
    }
}