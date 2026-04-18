using Italbytz.Common.Abstractions;

namespace Italbytz.Music.Abstractions;

public interface IMusicSearchEngine : IAsyncService<MusicSearchQuery, Result<List<MusicSearchResult>>>
{
}