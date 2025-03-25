using Volo.Abp.Bundling;

namespace Simple.Blazor.Client;

public class SimpleBundleContributor  : IBundleContributor
{

    public void AddScripts(BundleContext context)
    {
    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}