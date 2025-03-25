using Volo.Abp.DependencyInjection;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Bundling;

namespace Zyknow.Abp.AspnetCore.Components.WebAssembly.FluentDesignTheme.Bundling;

public class WebAssemblyComponentBundleManager : IComponentBundleManager, ITransientDependency
{
    public virtual Task<IReadOnlyList<string>> GetStyleBundleFilesAsync(string bundleName)
    {
        return Task.FromResult<IReadOnlyList<string>>(new List<string>());
    }

    public virtual Task<IReadOnlyList<string>> GetScriptBundleFilesAsync(string bundleName)
    {
        return Task.FromResult<IReadOnlyList<string>>(new List<string>());
    }
}
