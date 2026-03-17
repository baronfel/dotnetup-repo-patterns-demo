# Scenario: Cross-Platform Webapp

## Intent

This scenario demonstrates a typical team development setup: a web application backed by a
shared class library. Team members work on Windows, macOS, and Linux, and the workspace uses
a `global.json` to ensure everyone is building with the same SDK version.

[`dotnetup`](https://github.com/dotnet/designs/blob/main/accepted/2026/dotnetup/cli-acquisition-tool.md)
(`dotnetup`) makes onboarding seamless — a new team member (or a CI runner on any OS) runs a
single command to get exactly the right tooling, with no manual downloads or version hunting.

## What's in this workspace

```
cross-platform-webapp/
├── global.json                     # Pins SDK version for the whole team
└── src/
    ├── MyApp.Core/                 # Shared class library (net10.0)
    │   ├── MyApp.Core.csproj
    │   └── Greeter.cs
    └── MyApp.Web/                  # ASP.NET Core minimal API (net10.0)
        ├── MyApp.Web.csproj
        └── Program.cs
```

## How `dotnetup` helps

The `global.json` pins the SDK to `10.0.100`. When any team member clones the repo for the
first time — regardless of their operating system — they run:

```bash
dotnetup install
```

`dotnetup` reads the `global.json`, determines that SDK `10.0.100` (or the latest patch) is needed,
and installs it to the user-local hive. No admin rights required, no platform-specific
instructions — the same command works everywhere.

This is especially powerful in CI, where the GitHub Actions workflow uses `dotnetup install` on a
matrix of `ubuntu-latest`, `windows-latest`, and `macos-latest` runners to prove the build is
truly cross-platform.

## Try it yourself

### Locally

1. Install `dotnetup` if you haven't already.
2. Clone this repository and navigate to this scenario folder:
   ```bash
   cd scenarios/cross-platform-webapp
   ```
3. Install the required SDK:
   ```bash
   dotnetup install
   ```
4. Build and run the webapp:
   ```bash
   dotnet run --project src/MyApp.Web
   ```
5. Visit `http://localhost:5000` or `http://localhost:5000/greet/YourName`.

### In CI

This scenario has a matching GitHub Actions workflow at
[`.github/workflows/cross-platform-webapp.yml`](../../.github/workflows/cross-platform-webapp.yml)
that builds on Windows, macOS, and Linux in parallel. Push a change to this folder (or trigger
the workflow manually) to see it in action.
