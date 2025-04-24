using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.CmsKit.Admin;

namespace Zyknow.Abp.CmsKit.Blazor.Admin.FluentDesignUI;

[DependsOn(
    typeof(CmsKitAdminApplicationContractsModule),
    typeof(AbpAutoMapperModule)
)]
public class AbpCmsKitBlazorAdminFluentDesignModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpCmsKitBlazorAdminFluentDesignModule>();
    }
}