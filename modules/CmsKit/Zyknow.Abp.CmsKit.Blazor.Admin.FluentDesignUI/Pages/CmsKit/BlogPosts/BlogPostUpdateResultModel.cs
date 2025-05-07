using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Blogs;

namespace Zyknow.Abp.CmsKit.Blazor.Admin.FluentDesignUI.Pages.CmsKit.BlogPosts;

public class BlogPostUpdateResultModel : IDisposable
{
    public UpdateBlogPostDto Dto { get; set; }
    public string TagNameString { get; set; }
    public FileStream? CoverImageStream { get; set; }
    public string? FileName { get; set; }

    public void Dispose()
    {
        CoverImageStream?.Dispose();
    }
}