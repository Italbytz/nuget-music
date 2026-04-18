namespace Italbytz.Music.Abstractions;

public sealed class MusicSearchResult
{
    public MusicSearchResult(long id, string artistName, string collectionName, string trackName, int trackNumber, int discNumber, CoverArtReference coverArt)
    {
        if (string.IsNullOrWhiteSpace(artistName))
        {
            throw new ArgumentException("Artist name must not be empty.", nameof(artistName));
        }

        if (string.IsNullOrWhiteSpace(collectionName))
        {
            throw new ArgumentException("Collection name must not be empty.", nameof(collectionName));
        }

        if (string.IsNullOrWhiteSpace(trackName))
        {
            throw new ArgumentException("Track name must not be empty.", nameof(trackName));
        }

        Id = id;
        ArtistName = artistName;
        CollectionName = collectionName;
        TrackName = trackName;
        TrackNumber = trackNumber;
        DiscNumber = discNumber;
        CoverArt = coverArt ?? throw new ArgumentNullException(nameof(coverArt));
    }

    public long Id { get; }

    public string ArtistName { get; }

    public string CollectionName { get; }

    public string TrackName { get; }

    public int TrackNumber { get; }

    public int DiscNumber { get; }

    public CoverArtReference CoverArt { get; }
}