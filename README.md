# dotnetup Repo Patterns Demo

Showcase for several situations where
[`dotnetup`](https://github.com/dotnet/designs/blob/main/accepted/2026/dotnetup/cli-acquisition-tool.md)
(`dotnetup`) can help manage .NET SDK and Runtime installations.

Each scenario lives in its own folder under `scenarios/`, with its own `global.json`,
source code, GitHub Actions workflow, and README explaining the intent and how to try it.

## Scenarios

| Scenario | What it demonstrates |
|---|---|
| [**Multi-Runtime Testing**](scenarios/multi-runtime-testing/) | A `netstandard2.0` library with a test project that multi-targets `net8.0`, `net9.0`, and `net10.0`. `dotnetup install` acquires the SDK **and** all required runtimes so tests can run against every target. |
| [**Cross-Platform Webapp**](scenarios/cross-platform-webapp/) | An ASP.NET Core webapp + class library used by a team on Windows, macOS, and Linux. The `global.json` pins the SDK, and `dotnetup install` ensures every developer and CI runner gets exactly the right version — no manual downloads, no platform-specific steps. |

## Getting started

1. [Install `dotnetup`](#installing-dotnetup) if you haven't already.
2. Navigate into any scenario folder.
3. Run `dotnetup install` to acquire the required tooling.
4. Follow the scenario-specific README for next steps.


## Installing `dotnetup`

Today there is no public release of dotnetup, but you can build it from source:
1. Clone the [dotnet/sdk repo](https://github.com/dotnet/sdk)
2. Check out the `release/dnup` branch, which contains the latest dotnetup code.
3. Publish the `src/Installer/dotnetup` project via `dotnet publish`. This will create a Native AOT, platform-specific executable in `<repo root>/artifacts/bin/dotnetup/Release/net11.0/<platform RID>/publish/dotnetup`.
4. Add this binary to your PATH.

## Learn more

- [dotnetup design document](https://github.com/dotnet/designs/blob/main/accepted/2026/dotnetup/cli-acquisition-tool.md)
- [dotnetup development branch](https://github.com/dotnet/sdk/tree/release/dnup)