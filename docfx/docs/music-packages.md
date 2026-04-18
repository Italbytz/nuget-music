# Music packages

`nuget-music` contains the dedicated music package family extracted from the earlier consolidated package work.

## Package overview

| Package | Purpose | Typical entry points |
| --- | --- | --- |
| `Italbytz.Music.Abstractions` | normalized contracts for music search, result objects, and cover-art references | `IMusicSearchEngine`, `MusicSearchQuery`, `MusicSearchResult`, `CoverArtReference`, `CoverArtSize` |
| `Italbytz.Music.ITunes.Client` | concrete iTunes Search API integration built on the abstractions | `ITunesSearchClient` |

## Repository boundary

- `Italbytz.Common.Abstractions` stays in `nuget-foundation` as the shared base package.
- `nuget-music` builds on that base package for music-specific contracts and clients.
- consumer-specific CLI, download, and filesystem logic stays outside this repo.

## Local validation

```bash
dotnet tool restore
dotnet restore nuget-music.sln
dotnet test nuget-music.sln -v minimal
dotnet pack nuget-music.sln -c Release -v minimal
dotnet tool run docfx docfx/docfx.json
```