using System.Net;
using System.Text;
using Italbytz.Music.Abstractions;
using Italbytz.Music.ITunes.Client;

namespace Italbytz.Music.ITunes.Client.Tests;

[TestClass]
public class ITunesSearchClientTests
{
    [TestMethod]
    public async Task Execute_MapsITunesResponseToMusicSearchResults()
    {
        var json = """
        {
          "results": [
            {
              "trackId": 617154366,
              "artistName": "Daft Punk, Pharrell Williams & Nile Rodgers",
              "collectionName": "Random Access Memories",
              "trackName": "Get Lucky",
              "trackNumber": 8,
              "discNumber": 1,
              "artworkUrl100": "https://example.test/artwork/100x100bb.jpg"
            }
          ]
        }
        """;

        using var httpClient = new HttpClient(new StubHttpMessageHandler(json));
        var client = new ITunesSearchClient(httpClient);

        var result = await client.Execute(new MusicSearchQuery("Daft Punk", 1));

        Assert.HasCount(1, result.Value);
        var track = result.Value[0];
        Assert.AreEqual(617154366L, track.Id);
        Assert.AreEqual("Get Lucky", track.TrackName);
        Assert.AreEqual("https://example.test/artwork/60x60bb.jpg", track.CoverArt.SmallUri.AbsoluteUri);
        Assert.AreEqual("https://example.test/artwork/100x100bb.jpg", track.CoverArt.MediumUri.AbsoluteUri);
        Assert.AreEqual("https://example.test/artwork/600x600bb.jpg", track.CoverArt.LargeUri.AbsoluteUri);
    }

    [TestMethod]
    public async Task Execute_FiltersIncompleteResults()
    {
        var json = """
        {
          "results": [
            {
              "trackId": 1,
              "artistName": "Artist",
              "collectionName": "Collection",
              "trackName": null,
              "trackNumber": 1,
              "discNumber": 1,
              "artworkUrl100": "https://example.test/artwork/100x100bb.jpg"
            }
          ]
        }
        """;

        using var httpClient = new HttpClient(new StubHttpMessageHandler(json));
        var client = new ITunesSearchClient(httpClient);

        var result = await client.Execute(new MusicSearchQuery("Daft Punk", 1));

        Assert.IsEmpty(result.Value);
    }

    private sealed class StubHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _content;

        public StubHttpMessageHandler(string content)
        {
            _content = content;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_content, Encoding.UTF8, "application/json"),
            };

            return Task.FromResult(response);
        }
    }
}