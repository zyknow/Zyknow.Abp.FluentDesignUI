using Microsoft.Extensions.Localization;
using Simple.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Simple.Blazor;

[Dependency(ReplaceServices = true)]
public class SimpleBrandingProvider(IStringLocalizer<SimpleResource> localizer) : DefaultBrandingProvider
{
    public override string AppName => localizer["AppName"];
    
    public override string? LogoUrl => "~/favicon.ico";
}
