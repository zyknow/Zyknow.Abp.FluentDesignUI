# Setup Guide

## Step 1: Create a New Project

Use the ABP CLI to create a new project:

```bash
abp new BookStoreWebApp -u blazor-webapp -t app
```

> For more information, see the [ABP official documentation](https://docs.abp.io) to learn about the [ABP framework](https://github.com/abpframework/abp).

## Step 2: Update Themes and Packages

Replace `LeptonXLiteTheme` or `BasicTheme` with `FluentBlazorTheme` packages and remove `Blazorise` packages.

> Refer to [Simple.Blazor.csproj](https://github.com/zyknow/Zyknow.Abp.FluentDesignUI/blob/develop/Simple/src/Simple.Blazor/Simple.Blazor.csproj) and [Simple.Blazor.Client.csproj](https://github.com/zyknow/Zyknow.Abp.FluentDesignUI/blob/develop/Simple/src/Simple.Blazor.Client/Simple.Blazor.Client.csproj).
>
> You can still use MVC; do not remove MVC-related packages.
>
> If you want to use MVC for the login page and account-related pages, do not use the `Zyknow.Abp.Account.Blazor.FluentDesignUI` packages.

## Step 3: Update `_Imports.razor`

Open `_Imports.razor` and add the following:

```razor
@using Microsoft.FluentUI.AspNetCore.Components
@using Zyknow.Abp.FluentDesignUI
@using Zyknow.Abp.FluentDesignUI.Components
@using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Layout
@using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Bundling
```

## Step 4: Modify Modules

Open `BookStoreWebAppBlazorModule` and `BookStoreWebAppBlazorClientModule` and make the following changes:

1. Remove the `ConfigureBlazorise` method.
2. Fix incorrect using namespaces.
3. Update module dependencies:
   - For example, replace `AbpTenantManagementBlazorWebAssemblyModule` with `AbpTenantManagementBlazorWebAssemblyFluentDesignModule`.

## Step 5: Update `Routes.razor`

Open `Routes.razor` and replace it with the following:

```csharp
@using Microsoft.Extensions.Options
@using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Routing
@using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Themes.FluentDesignTheme
@using BlazorPro.BlazorSize
@inject IOptions<AbpRouterOptions> RouterOptions

<MediaQueryList>
    <Router AppAssembly="typeof(Program).Assembly" AdditionalAssemblies="RouterOptions.Value.AdditionalAssemblies">
        <Found Context="routeData">
            <AuthorizeRouteView RouteData="routeData" DefaultLayout="typeof(DefaultLayout)">
                <NotAuthorized>
                    <RedirectToLogin/>
                </NotAuthorized>
            </AuthorizeRouteView>
        </Found>
    </Router>
</MediaQueryList>
```

## Step 6: Update `App.razor`

Change `AbpStyles` and `AbpScript` to `BlazorFluentDesignThemeBundles`.

### Additional Code for WebApp Projects

If you are using a WebApp project and have included `Zyknow.Abp.Account.Blazor.Server.FluentDesignUI`, you need to add extra code in `App.razor`:

1. Add the following code:

   ```csharp
   @code{
       [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;
   
       [Inject] protected IOptions<AbpFluentDesignThemeOptions> ThemeOptions { get; set; }
   
       private IComponentRenderMode? PageRenderMode =>
           // account/* must be handled by the server
           HttpContext.Request.Path.StartsWithSegments("/Account")
               ? new InteractiveServerRenderMode()
               : (HttpContext.AcceptsInteractiveRouting()
                   ? GetDefaultRenderMode()
                   : null);
   
       protected IComponentRenderMode GetDefaultRenderMode()
       {
           var renderMode = HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "RenderMode");
           var prerenderStr = HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "Prerender");
           var prerender = prerenderStr.Value.IsNullOrEmpty() || bool.TryParse(prerenderStr.Value, out var result) && result;
   
           if (renderMode.Value == "InteractiveAutoRenderMode")
           {
               return new InteractiveAutoRenderMode(prerender);
           }
   
           if (renderMode.Value == "InteractiveServerRenderMode")
           {
               return new InteractiveServerRenderMode(prerender);
           }
   
           if (renderMode.Value == "InteractiveWebAssemblyRenderMode")
           {
               return new InteractiveWebAssemblyRenderMode(prerender);
           }
   
           // default to InteractiveAutoRenderMode
           return new InteractiveAutoRenderMode(prerender);
       }
   }
   ```

2. Replace render mode:

   ```csharp
   <HeadOutlet @rendermode="@PageRenderMode"/>
   ...
   <Routes @rendermode="InteractiveAuto"/>
   ```

## Step 7: Start Your Project and Enjoy!
