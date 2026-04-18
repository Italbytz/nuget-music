# nuget-music

`nuget-music` contains the dedicated music package family from `Italbytz.*`.

It is intended for developers who want shared music search contracts and concrete music-provider clients without pulling those concerns into the more general `nuget-foundation` repository.

## Packages in this repository

### `Italbytz.Music.Abstractions`
Contracts for music search scenarios, normalized results, and cover-art references.

### `Italbytz.Music.ITunes.Client`
Concrete iTunes Search API client that maps external responses to the shared music abstractions.

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

## CI and documentation

- GitHub Actions builds, tests, packs, and publishes the Docfx site.
- Tagged releases of the form `v*` publish NuGet and symbol packages when `NUGET_API_KEY` is configured.
