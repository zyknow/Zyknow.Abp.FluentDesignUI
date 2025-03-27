using Volo.Abp.ObjectExtending;

namespace Zyknow.Abp.Account.Blazor.FluentDesignUI.Pages.ProfileManagement.Groups;

public class PersonalInfoModel : ExtensibleObject
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool EmailConfirmed { get; set; }

    public string ConcurrencyStamp { get; set; }
}