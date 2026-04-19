# nuget-music

[![Documentation](https://img.shields.io/badge/Documentation-GitHub%20Pages-blue?style=for-the-badge)](https://italbytz.github.io/nuget-music/)

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

## Release model

- `nuget-music` starts with the stable `1.0.x` line for the extracted music packages.
- a pushed tag such as `v1.0.1` triggers the release-ready pipeline in GitHub Actions.
- if the repository secret `NUGET_API_KEY` is configured, the workflow also publishes `.nupkg` and `.snupkg` files to NuGet.

## Local validation

```bash
dotnet tool restore
dotnet restore nuget-music.sln
dotnet test nuget-music.sln -v minimal
dotnet pack nuget-music.sln -c Release -v minimal
dotnet tool run docfx docfx/docfx.json
```

## Documentation

- Product documentation: `https://italbytz.github.io/nuget-music/`

## CI and documentation

- GitHub Actions builds, tests, packs, and publishes the Docfx site.
- pushed release tags follow the `v<package-version>` pattern.
