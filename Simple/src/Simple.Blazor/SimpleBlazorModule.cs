using Microsoft.AspNetCore.Extensions.DependencyInjection;
using OpenIddict.Validation.AspNetCore;
using OpenIddict.Server.AspNetCore;
using Simple.Blazor.Client.Navigation;
using Simple.EntityFrameworkCore;
using Simple.MultiTenancy;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Simple.Blazor.Client;
using Volo.Abp;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Security.Claims;
// using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
// using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.OpenIddict;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.Libs;
using Volo.Abp.Timing;
using Zyknow.Abp.Account.Blazor.Server.FluentDesignUI;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Server.FluentDesignTheme.Bundling;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Routing;
using Zyknow.Abp.IdentityManagement.Blazor.Server.FluentDesignUI;
using Zyknow.Abp.SettingManagement.Blazor.Server.FluentDesignUI;
using Zyknow.Abp.TenantManagement.Blazor.FluentDesignUI;
using Zyknow.SharpAbp.IdentityManagement.Blazor.Server.FluentDesignUI;
using App = Simple.Blazor.Components.App;

namespace Simple.Blazor;

[DependsOn(
    typeof(SimpleApplicationModule),
    typeof(SimpleEntityFrameworkCoreModule),
    typeof(SimpleHttpApiModule),
    typeof(AbpAutofacModule),
    // typeof(AbpAspNetCoreMvcUiBasicThemeModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAspNetCoreComponentsServerFluentDesignThemeModule),
    typeof(AbpTenantManagementBlazorFluentDesignModule),
    typeof(AbpIdentityBlazorServerFluentDesignModule),
    typeof(AbpSettingManagementBlazorServerFluentDesignModule),
    typeof(AbpAccountBlazorServerFluentDesignModule),
    typeof(SharpAbpIdentityBlazorServerFluentDesignModule)
)]
public class SimpleBlazorModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        // context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        // {
        //     options.AddAssemblyResource(
        //         typeof(SimpleResource),
        //         typeof(SimpleDomainModule).Assembly,
        //         typeof(SimpleDomainSharedModule).Assembly,
        //         typeof(SimpleApplicationModule).Assembly,
        //         typeof(SimpleApplicationContractsModule).Assembly,
        //         typeof(SimpleBlazorModule).Assembly
        //     );
        // });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("Simple");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });

        if (!hostingEnvironment.IsDevelopment())
        {
            PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
            {
                options.AddDevelopmentEncryptionAndSigningCertificate = false;
            });

            PreConfigure<OpenIddictServerBuilder>(serverBuilder =>
            {
                serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx",
                    configuration["AuthServer:CertificatePassPhrase"]!);
                serverBuilder.SetIssuer(new Uri(configuration["AuthServer:Authority"]!));
            });
        }

        PreConfigure<AbpAspNetCoreComponentsWebOptions>(options => { options.IsBlazorWebApp = true; });
        
        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        // Add services to the container.
        context.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        if (!configuration.GetValue<bool>("App:DisablePII"))
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.LogCompleteSecurityArtifact = true;
        }

        if (!configuration.GetValue<bool>("AuthServer:RequireHttpsMetadata"))
        {
            Configure<OpenIddictServerAspNetCoreOptions>(options =>
            {
                options.DisableTransportSecurityRequirement = true;
            });
        }

        ConfigureAuthentication(context);
        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureSwaggerServices(context.Services);
        ConfigureAutoApiControllers();
        ConfigureBlazorise(context);
        ConfigureRouter(context);
        ConfigureMenu(context);
        
        Configure<AbpMvcLibsOptions>(options =>
        {
            options.CheckLibs = false;
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults
            .AuthenticationScheme);
        context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        // Configure<AppUrlOptions>(options =>
        // {
        //     options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        //     options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ??
        //                                          Array.Empty<string>());
        // });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            // Blazor Web App
            // options.Parameters.InteractiveAuto = true;

            // MVC UI
            // options.StyleBundles.Configure(
            //     BasicThemeBundles.Styles.Global,
            //     bundle =>
            //     {
            //         bundle.AddFiles("/global-styles.css");
            //         bundle.AddFiles("/global-scripts.js");
            //     }
            // );

            // Blazor UI
            options.StyleBundles.Configure(
                BlazorFluentDesignThemeBundles.Styles.Global,
                bundle =>
                {
                    //You can remove the following line if you don't use Blazor CSS isolation for components
                    bundle.AddFiles("/Simple.Blazor.styles.css");
                    bundle.AddFiles("/Simple.Blazor.Client.styles.css");
                    bundle.AddFiles("/blazor-global-styles.css");
                }
            );
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<SimpleDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Simple.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<SimpleDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Simple.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<SimpleApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Simple.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<SimpleApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Simple.Application"));
                options.FileSets.ReplaceEmbeddedByPhysical<SimpleBlazorModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Simple API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }


    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        // context.Services
        //     .AddBootstrap5Providers()
        //     .AddFontAwesomeIcons();
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new SimpleMenuContributor(context.Services.GetConfiguration()));
        });
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(SimpleBlazorModule).Assembly;
            options.AdditionalAssemblies.Add(typeof(SimpleBlazorClientModule).Assembly);
        });
    }

    private void ConfigureAutoApiControllers()
    {
        // Configure<AbpAspNetCoreMvcOptions>(options =>
        // {
        //     options.ConventionalControllers.Create(typeof(SimpleApplicationModule).Assembly);
        // });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<SimpleBlazorModule>(); });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var env = context.GetEnvironment();
        var app = context.GetApplicationBuilder();

        app.Use(async (ctx, next) =>
        {
            /* Converting to https to be able to include https URLs in `/.well-known/openid-configuration` endpoint.
             * This should only be done if the request is coming outside of the cluster.  */
            if (ctx.Request.Headers.ContainsKey("from-ingress"))
            {
                ctx.Request.Scheme = "https";
            }

            await next();
        });

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
            app.UseHsts();
        }

        app.UseCorrelationId();

        app.MapAbpStaticAssets();
        app.UseRouting();
        app.UseAbpSecurityHeaders();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAntiforgery();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple API"); });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();

        app.UseConfiguredEndpoints(builder =>
        {
            builder.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(builder.ServiceProvider.GetRequiredService<IOptions<AbpRouterOptions>>().Value
                    .AdditionalAssemblies.ToArray());
        });
    }
}