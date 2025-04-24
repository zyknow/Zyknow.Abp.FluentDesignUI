namespace Zyknow.Abp.CmsKit.Blazor.Admin.FluentDesignUI;

public static class LayoutConstants
{
    public const string Account = nameof(Account);
    public const string Public = nameof(Public);
    public const string Empty = nameof(Empty);
    public const string Application = nameof(Application);

    public static List<string> GetLayoutsSelectList()
    {
        return [Account, Public, Empty, Application];
    }
}