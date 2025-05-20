using Volo.Abp.AspNetCore.Bundling;
using Volo.Abp.DependencyInjection;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Bundling;

namespace Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme.Bundling;

public class BlazorServerComponentBundleManager(IBundleManager bundleManager)
    : IComponentBundleManager, ITransientDependency
{
    protected IBundleManager BundleManager { get; } = bundleManager;

    public virtual async Task<IReadOnlyList<string>> GetStyleBundleFilesAsync(string bundleName)
    {
        return (await BundleManager.GetStyleBundleFilesAsync(bundleName)).Select(f => f.FileName).ToList();
    }

    public virtual async Task<IReadOnlyList<string>> GetScriptBundleFilesAsync(string bundleName)
    {
        return (await BundleManager.GetScriptBundleFilesAsync(bundleName)).Select(f => f.FileName).ToList();
    }
}