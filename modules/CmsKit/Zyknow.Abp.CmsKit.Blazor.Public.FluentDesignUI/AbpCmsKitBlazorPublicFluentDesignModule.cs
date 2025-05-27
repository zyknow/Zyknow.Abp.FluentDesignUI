using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.FluentUI.AspNetCore.Components.Emojis.PeopleBody.Color;
using Volo.Abp;
using Volo.Abp.AutoMapper;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Public;
using Volo.CmsKit.Public.Comments;
using Volo.CmsKit.Reactions;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme;
using Zyknow.Abp.AspnetCore.Components.Web.FluentDesignTheme.Routing;
using Zyknow.Abp.CmsKit.Blazor.Common.FluentDesignUI;

namespace Zyknow.Abp.CmsKit.Blazor.Public.FluentDesignUI;

[DependsOn(
    typeof(CmsKitPublicApplicationContractsModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpAspNetCoreComponentsWebFluentDesignThemeModule),
    typeof(AbpCmsKitBlazorCommonFluentDesignModule)
)]
public class AbpCmsKitBlazorPublicFluentDesignModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpCmsKitBlazorPublicFluentDesignModule>();

        Configure<CmsKitUiOptions>(options =>
        {
            options.ReactionIcons[StandardReactions.Smile] =
                new Microsoft.FluentUI.AspNetCore.Components.Emojis.SmileysEmotion.Color.Default.SlightlySmilingFace();
            options.ReactionIcons[StandardReactions.ThumbsUp] = new Default.ThumbsUp();
            options.ReactionIcons[StandardReactions.Confused] =
                new Microsoft.FluentUI.AspNetCore.Components.Emojis.SmileysEmotion.Color.Default.FaceWithOpenMouth();
            new Microsoft.FluentUI.AspNetCore.Components.Emojis.SmileysEmotion.Color.Default.ConfusedFace();
            options.ReactionIcons[StandardReactions.Eyes] =
                options.ReactionIcons[StandardReactions.Heart] =
                    new Microsoft.FluentUI.AspNetCore.Components.Emojis.SmileysEmotion.Color.Default.RedHeart();
            options.ReactionIcons[StandardReactions.HeartBroken] =
                new Microsoft.FluentUI.AspNetCore.Components.Emojis.SmileysEmotion.Color.Default.BrokenHeart();
            options.ReactionIcons[StandardReactions.Wink] =
                new Microsoft.FluentUI.AspNetCore.Components.Emojis.SmileysEmotion.Color.Default.WinkingFace();
            options.ReactionIcons[StandardReactions.Pray] =
                new Microsoft.FluentUI.AspNetCore.Components.Emojis.PeopleBody.Color.Default.FoldedHands();
            options.ReactionIcons[StandardReactions.Rocket] =
                new Microsoft.FluentUI.AspNetCore.Components.Emojis.TravelPlaces.Color.Default.Rocket();
            options.ReactionIcons[StandardReactions.ThumbsDown] = new Default.ThumbsDown();
            options.ReactionIcons[StandardReactions.Victory] = new Default.VictoryHand();
            options.ReactionIcons[StandardReactions.Rock] = new Default.RaisedFist();
        });


        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new CmsKitPublicMenuContributor());
            options.MainMenuNames.Add(CmsKitMenus.Public);
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpCmsKitBlazorPublicFluentDesignModule).Assembly);
        });
    }
}