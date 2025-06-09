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


        CreateMap<CommentDto, UpdateCommentInput>()
            .ForMember(dest => dest.CaptchaToken, opt => opt.Ignore())
            .ForMember(dest => dest.CaptchaAnswer, opt => opt.Ignore());
        ;
        CreateMap<CommentWithDetailsDto, UpdateCommentInput>()
            .ForMember(dest => dest.CaptchaToken, opt => opt.Ignore())
            .ForMember(dest => dest.CaptchaAnswer, opt => opt.Ignore());
        ;
    }
}