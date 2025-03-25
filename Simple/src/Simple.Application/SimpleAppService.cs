using Simple.Localization;
using Volo.Abp.Application.Services;

namespace Simple;

/* Inherit your application services from this class.
 */
public abstract class SimpleAppService : ApplicationService
{
    protected SimpleAppService()
    {
        LocalizationResource = typeof(SimpleResource);
    }
}
