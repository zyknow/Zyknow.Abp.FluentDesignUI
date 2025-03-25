using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Toolbars;

public class ToolbarManager(
    IOptions<AbpToolbarOptions> options,
    IServiceProvider serviceProvider)
    : IToolbarManager, ITransientDependency
{
    protected AbpToolbarOptions Options { get; } = options.Value;
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    public async Task<Toolbar> GetAsync(string name)
    {
        var toolbar = new Toolbar(name);

        using (var scope = ServiceProvider.CreateScope())
        {
            var context = new ToolbarConfigurationContext(toolbar, scope.ServiceProvider);

            foreach (var contributor in Options.Contributors)
            {
                await contributor.ConfigureToolbarAsync(context);
            }
        }

        return toolbar;
    }
}
