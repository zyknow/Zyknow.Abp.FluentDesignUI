namespace Zyknow.Abp.Account.Blazor.FluentDesignUI.Pages.ProfileManagement.Groups;

public class ChangePasswordModel
{
    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }

    public string NewPasswordConfirm { get; set; }

    public void Clear()
    {
        CurrentPassword = string.Empty;
        NewPassword = string.Empty;
        NewPasswordConfirm = string.Empty;
    }
}

