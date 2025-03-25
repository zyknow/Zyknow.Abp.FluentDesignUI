using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Toolbars;

public class ToolbarConfigurationContext : IToolbarConfigurationContext
{
    public IServiceProvider ServiceProvider { get; }

    private readonly IAbpLazyServiceProvider _lazyServiceProvider;

    public IAuthorizationService AuthorizationService => _lazyServiceProvider.LazyGetRequiredService<IAuthorizationService>();

    public IStringLocalizerFactory StringLocalizerFactory => _lazyServiceProvider.LazyGetRequiredService<IStringLocalizerFactory>();

    public Toolbar Toolbar { get; }

    public ToolbarConfigurationContext(Toolbar toolbar, IServiceProvider serviceProvider)
    {
        Toolbar = toolbar;
        ServiceProvider = serviceProvider;
        _lazyServiceProvider = ServiceProvider.GetRequiredService<IAbpLazyServiceProvider>();
    }

    public Task<bool> IsGrantedAsync(string policyName)
    {
        return AuthorizationService.IsGrantedAsync(policyName);
    }

    public IStringLocalizer? GetDefaultLocalizer()
    {
        return StringLocalizerFactory.CreateDefaultOrNull();
    }

    public IStringLocalizer GetLocalizer<T>()
    {
        return StringLocalizerFactory.Create<T>();
    }

    public IStringLocalizer GetLocalizer(Type resourceType)
    {
        return StringLocalizerFactory.Create(resourceType);
    }
}
