using Simple.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Simple.Blazor;

public abstract class SimpleComponentBase : AbpComponentBase
{
    protected SimpleComponentBase()
    {
        LocalizationResource = typeof(SimpleResource);
    }
}
