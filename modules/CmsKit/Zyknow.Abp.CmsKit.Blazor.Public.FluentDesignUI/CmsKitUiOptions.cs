using Microsoft.FluentUI.AspNetCore.Components;

namespace Zyknow.Abp.CmsKit.Blazor.Public.FluentDesignUI;

public class CmsKitUiOptions
{
    public Dictionary<string, Emoji> ReactionIcons { get; set; } = [];

    public CmsKitUiCommentOptions CommentsOptions { get; } = new();

    // public MarkedItemIconDictionary MarkedItemIcons { get; }
}