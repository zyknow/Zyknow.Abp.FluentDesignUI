using System.Linq.Expressions;

namespace Zyknow.Abp.FluentDesignUI.Components;

public class MultiItemDeleteConfirmDialogInput<TItem>(
    IEnumerable<TItem> items,
    Func<TItem, string>? deleteSelectedDisplayPropertyConfirmExpression = null)
{
    public Func<TItem, string>? DeleteSelectedDisplayPropertyConfirmExpression { get; set; } =
        deleteSelectedDisplayPropertyConfirmExpression;

    public IEnumerable<TItem> Items { get; set; } = items;
}