using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Text.RegularExpressions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;

namespace Zyknow.Abp.FluentDesignUI.Components;

public partial class AbpExtensibleDataGrid<TItem> : ComponentBase
{
    protected const string DataFieldAttributeName = "Data";

    protected Regex ExtensionPropertiesRegex = new Regex(@"ExtraProperties\[(.*?)\]");

    protected Dictionary<string, TableEntityActionsColumn<TItem>> ActionColumns = new();

    [Parameter] public AbpFluentPaginationState Pagination { get; set; } = new();

    [Parameter] public IEnumerable<FluentTableColumn>? Columns { get; set; }

    [Parameter] public bool Loading { get; set; }

    [Parameter] public Func<GridItemsProviderRequest<TItem>, Task>? OnChange { get; set; }

    [Parameter] public IReadOnlyList<TItem> Entities { get; set; }



    [Inject] public IStringLocalizerFactory StringLocalizerFactory { get; set; }

    EntityActions<TItem> EntityActionsRef;

    protected virtual RenderFragment RenderCustomTableColumnComponent(Type type, object data)
    {
        return (builder) =>
        {
            builder.OpenComponent(0, type);
            builder.AddAttribute(0, DataFieldAttributeName, data);
            builder.CloseComponent();
        };
    }

    protected virtual object GetColumnValue(object data, string fieldName)
    {
        return data.GetType().GetProperty(fieldName)?.GetValue(data);
    }

    protected virtual string GetConvertedFieldValue(TItem item, TableColumn columnDefinition)
    {
        var convertedValue = columnDefinition.ValueConverter.Invoke(item);
        if (!columnDefinition.DisplayFormat.IsNullOrEmpty())
        {
            return string.Format(columnDefinition.DisplayFormatProvider, columnDefinition.DisplayFormat,
                convertedValue);
        }

        return convertedValue;
    }
}