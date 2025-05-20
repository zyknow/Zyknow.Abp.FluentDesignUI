using AutoMapper;
using Volo.Abp.AutoMapper;
using Volo.CmsKit.Public.Comments;

namespace Zyknow.Abp.CmsKit.Blazor.Public.FluentDesignUI;

public class AbpCmsKitBlazorPublicAutoMapperProfile : Profile
{
    public AbpCmsKitBlazorPublicAutoMapperProfile()
    {
        CreateMap<CreateCommentWithParametersInput, CreateCommentInput>()
            .Ignore(x => x.ExtraProperties);
    }
}