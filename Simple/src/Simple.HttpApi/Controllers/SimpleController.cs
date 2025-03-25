using Simple.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Simple.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SimpleController : AbpControllerBase
{
    protected SimpleController()
    {
        LocalizationResource = typeof(SimpleResource);
    }
}
