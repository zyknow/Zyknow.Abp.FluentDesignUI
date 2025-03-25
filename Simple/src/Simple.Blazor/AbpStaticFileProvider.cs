using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Primitives;

namespace Simple.Blazor;

public class AbpStaticFileProvider : IFileProvider
{
    private readonly Matcher _matcher;
    private readonly IFileProvider _fileProvider;

    /// <param name="fileProvider">The file provider to be used to get the files.</param>
    /// <param name="fileNames">The file names to get from the file provider. Supports glob patterns. See https://learn.microsoft.com/en-us/dotnet/core/extensions/file-globbing.</param>
    public AbpStaticFileProvider(IReadOnlyList<string> fileNames, IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
        _matcher = new Matcher(StringComparison.OrdinalIgnoreCase);
        _matcher.AddIncludePatterns(fileNames);
    }

    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        return new NotFoundDirectoryContents();
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        return _matcher.Match(Path.GetFileName(subpath)).HasMatches ?
            _fileProvider.GetFileInfo(subpath) :
            new NotFoundFileInfo(subpath);
    }

    public IChangeToken Watch(string filter)
    {
        return NullChangeToken.Singleton;
    }
}