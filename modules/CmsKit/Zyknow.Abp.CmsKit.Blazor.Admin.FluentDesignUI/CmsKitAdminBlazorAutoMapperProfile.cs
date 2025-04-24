using AutoMapper;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Admin.Comments;
using Volo.CmsKit.Admin.GlobalResources;
using Volo.CmsKit.Admin.Menus;
using Volo.CmsKit.Admin.Pages;
using Volo.CmsKit.Admin.Tags;
using Volo.CmsKit.Menus;
using Volo.CmsKit.Tags;

namespace Zyknow.Abp.CmsKit.Blazor.Admin.FluentDesignUI;

public class CmsKitAdminBlazorAutoMapperProfile : Profile
{
    public CmsKitAdminBlazorAutoMapperProfile()
    {
        CreateMap<BlogDto, UpdateBlogDto>()
            .MapExtraProperties();

        CreateMap<BlogPostDto, UpdateBlogPostDto>()
            .MapExtraProperties();

        CreateMap<MenuItemDto, MenuItemUpdateInput>()
            .MapExtraProperties();

        CreateMap<PageDto, UpdatePageInputDto>()
            .MapExtraProperties();

        CreateMap<TagDto, TagUpdateDto>()
            .MapExtraProperties();
    }
}