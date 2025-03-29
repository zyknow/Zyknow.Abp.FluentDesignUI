using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Zyknow.Abp.Account.Blazor.Server.FluentDesignUI.Pages.Components;

public class LoginInput
{
    [Required]
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
    public string UserNameOrEmailAddress { get; set; }

    [Required]
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
    [DataType(DataType.Password)]
    [DisableAuditing]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
    
    public Guid? TenantId { get; set; }

    public string? ReturnUrl { get; set; }
    
    public string? ReturnUrlHash { get; set; }
}