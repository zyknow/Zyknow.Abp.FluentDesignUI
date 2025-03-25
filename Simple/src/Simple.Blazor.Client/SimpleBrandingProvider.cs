using Volo.Abp.Ui.Branding;
using Microsoft.Extensions.Localization;
using Simple.Localization;

namespace Simple.Blazor.Client;

public class SimpleBrandingProvider(IStringLocalizer<SimpleResource> localizer) : DefaultBrandingProvider
{
    public override string AppName => localizer["AppName"];
    public override string? LogoUrl => "~/favicon.ico";
}
