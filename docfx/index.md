# nuget-music

`nuget-music` contains the dedicated music package family from `Italbytz.*`.

It is intended for developers who want shared music search contracts and concrete music-provider clients without pulling those concerns into the more general `nuget-foundation` repository.

## Packages in this repository

- `Italbytz.Music.Abstractions`
- `Italbytz.Music.ITunes.Client`

## What you can do with nuget-music

- define reusable contracts for music search scenarios and normalized results
- call the iTunes Search API through a package-level client instead of app-local integration code
- keep music-specific concerns separated from broader foundation packages

## Guide

Use `Guides > Music packages` for a quick orientation across the package split, repository boundary, and local validation flow.

## Local validation

```bash
dotnet tool restore
dotnet restore nuget-music.sln
dotnet test nuget-music.sln -v minimal
dotnet tool run docfx docfx/docfx.json
```