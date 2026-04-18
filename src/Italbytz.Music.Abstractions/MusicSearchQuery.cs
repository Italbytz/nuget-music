namespace Italbytz.Music.Abstractions;

public sealed class MusicSearchQuery
{
    public MusicSearchQuery(string term, int limit = 5)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            throw new ArgumentException("Search term must not be empty.", nameof(term));
        }

        if (limit < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(limit), "Limit must be greater than zero.");
        }

        Term = term;
        Limit = limit;
    }

    public string Term { get; }

    public int Limit { get; }
}