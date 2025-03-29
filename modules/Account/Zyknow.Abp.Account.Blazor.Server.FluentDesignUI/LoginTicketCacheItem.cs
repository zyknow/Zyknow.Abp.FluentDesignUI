namespace Zyknow.Abp.Account.Blazor.Server.FluentDesignUI;

public class LoginTicketCacheItem
{
    public string UserNameOrEmailAddress { get; set; }
    public bool RememberMe { get; set; }
    public Guid UserId { get; set; }
    public Guid? TenantId { get; set; }
    
    public string? ReturnUrl { get; set; }
    
    public string? ReturnUrlHash { get; set; }

    
    
}