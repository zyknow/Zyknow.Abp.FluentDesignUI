using Microsoft.FluentUI.AspNetCore.Components;

namespace Zyknow.Abp.FluentDesignUI;

public class TreeViewItem<TData> : TreeViewItem
{
    public TData Data { get; set; }

    public TreeViewItem<TData>? Parent { get; set; }
}