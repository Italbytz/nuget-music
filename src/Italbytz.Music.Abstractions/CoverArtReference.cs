namespace Italbytz.Music.Abstractions;

public sealed class CoverArtReference
{
    public CoverArtReference(Uri smallUri, Uri mediumUri, Uri largeUri)
    {
        SmallUri = smallUri ?? throw new ArgumentNullException(nameof(smallUri));
        MediumUri = mediumUri ?? throw new ArgumentNullException(nameof(mediumUri));
        LargeUri = largeUri ?? throw new ArgumentNullException(nameof(largeUri));
    }

    public Uri SmallUri { get; }

    public Uri MediumUri { get; }

    public Uri LargeUri { get; }

    public Uri GetUri(CoverArtSize size)
    {
        return size switch
        {
            CoverArtSize.Small => SmallUri,
            CoverArtSize.Medium => MediumUri,
            CoverArtSize.Large => LargeUri,
            _ => MediumUri,
        };
    }
}