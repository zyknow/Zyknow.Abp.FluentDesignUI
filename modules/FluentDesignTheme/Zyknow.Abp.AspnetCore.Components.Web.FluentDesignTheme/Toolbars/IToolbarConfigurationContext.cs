using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Toolbars;

public interface IToolbarConfigurationContext : IServiceProviderAccessor
{
    Toolbar Toolbar { get; }

    IAuthorizationService AuthorizationService { get; }

    IStringLocalizerFactory StringLocalizerFactory { get; }

    Task<bool> IsGrantedAsync(string policyName);

    IStringLocalizer? GetDefaultLocalizer();

    public IStringLocalizer GetLocalizer<T>();

    public IStringLocalizer GetLocalizer(Type resourceType);
}
