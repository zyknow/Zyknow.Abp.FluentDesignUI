using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.PageToolbars;

public class SimplePageToolbarContributor(
    Type componentType,
    Dictionary<string, object> arguments = null,
    int order = 0,
    string requiredPolicyName = null)
    : IPageToolbarContributor
{
    public Type ComponentType { get; } = componentType;

    public Dictionary<string, object> Arguments { get; set; } = arguments;

    public int Order { get; } = order;

    public string RequiredPolicyName { get; } = requiredPolicyName;

    public async Task ContributeAsync(PageToolbarContributionContext context)
    {
        if (await ShouldAddComponentAsync(context))
        {
            context.Items.Add(new PageToolbarItem(ComponentType, Arguments, Order));
        }
    }

    protected virtual async Task<bool> ShouldAddComponentAsync(PageToolbarContributionContext context)
    {
        if (RequiredPolicyName != null)
        {
            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
            if (!await authorizationService.IsGrantedAsync(RequiredPolicyName))
            {
                return false;
            }
        }

        return true;
    }
}
