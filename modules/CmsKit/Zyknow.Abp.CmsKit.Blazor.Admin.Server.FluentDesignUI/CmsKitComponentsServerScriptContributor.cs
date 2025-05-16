using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Zyknow.Abp.CmsKit.Blazor.Admin.Server.FluentDesignUI;

public class CmsKitComponentsServerScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        // TODO: is this needed?
        // context.Files.AddIfNotContains(
        //     "_content/TinyMCE.Blazor/tinymce-blazor.js");


        context.Files.AddIfNotContains("_content/BlazorMonaco/jsInterop.js");
        context.Files.AddIfNotContains("_content/BlazorMonaco/lib/monaco-editor/min/vs/loader.js");
        context.Files.AddIfNotContains("_content/BlazorMonaco/lib/monaco-editor/min/vs/editor/editor.main.js");
    }
}