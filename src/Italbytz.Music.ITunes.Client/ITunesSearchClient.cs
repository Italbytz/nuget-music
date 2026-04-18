using System.Text.Json;
using Italbytz.Common.Abstractions;
using Italbytz.Music.Abstractions;

namespace Italbytz.Music.ITunes.Client;

public sealed class ITunesSearchClient : IMusicSearchEngine
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public ITunesSearchClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Result<List<MusicSearchResult>>> Execute(MusicSearchQuery query)
    {
        ArgumentNullException.ThrowIfNull(query);

        using var response = await _httpClient.GetAsync(BuildSearchUri(query));
        response.EnsureSuccessStatusCode();

        await using var responseStream = await response.Content.ReadAsStreamAsync();
        var payload = await JsonSerializer.DeserializeAsync<ITunesSearchResponse>(responseStream, JsonOptions)
            ?? throw new InvalidOperationException("Received an empty response from the iTunes Search API.");

        var tracks = payload.Results
            .Where(static item => !string.IsNullOrWhiteSpace(item.ArtistName)
                && !string.IsNullOrWhiteSpace(item.TrackName)
                && !string.IsNullOrWhiteSpace(item.CollectionName)
                && !string.IsNullOrWhiteSpace(item.ArtworkUrl100))
            .Select(static item => new MusicSearchResult(
                item.TrackId,
                item.ArtistName!,
                item.CollectionName!,
                item.TrackName!,
                item.TrackNumber,
                item.DiscNumber,
                CreateCoverArtReference(item.ArtworkUrl100!)))
            .ToList();

        return new Result<List<MusicSearchResult>>(tracks);
    }

    private static Uri BuildSearchUri(MusicSearchQuery query)
    {
        var encodedTerm = Uri.EscapeDataString(query.Term);
        return new Uri($"https://itunes.apple.com/search?term={encodedTerm}&entity=song&limit={query.Limit}", UriKind.Absolute);
    }

    private static CoverArtReference CreateCoverArtReference(string mediumArtworkUrl)
    {
        var mediumUri = new Uri(mediumArtworkUrl, UriKind.Absolute);
        return new CoverArtReference(
            ResizeArtworkUri(mediumUri, "60x60bb"),
            ResizeArtworkUri(mediumUri, "100x100bb"),
            ResizeArtworkUri(mediumUri, "600x600bb"));
    }

    private static Uri ResizeArtworkUri(Uri originalUri, string replacement)
    {
        var resized = originalUri.AbsoluteUri.Replace("100x100bb", replacement, StringComparison.OrdinalIgnoreCase);
        return new Uri(resized, UriKind.Absolute);
    }

    private sealed record ITunesSearchResponse(List<ITunesTrackDto> Results);

    private sealed record ITunesTrackDto(
        long TrackId,
        string? ArtistName,
        string? CollectionName,
        string? TrackName,
        int TrackNumber,
        int DiscNumber,
        string? ArtworkUrl100);

}