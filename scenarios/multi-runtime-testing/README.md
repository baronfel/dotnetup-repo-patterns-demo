# Scenario: Multi-Runtime Testing

## Intent

This scenario demonstrates how to test a library across multiple .NET runtime versions.
The library itself targets `netstandard2.0` for broad compatibility, while the test project
multi-targets `net8.0`, `net9.0`, and `net10.0` to verify the library behaves correctly on
each runtime.

[`dotnetup`](https://github.com/dotnet/designs/blob/main/accepted/2026/dotnetup/cli-acquisition-tool.md)
makes this workflow practical — a single `dotnetup install` resolves the SDK from `global.json`,
and `dotnetup runtime install` fetches the additional runtimes needed for the older target
frameworks.

## What's in this workspace

```
multi-runtime-testing/
├── global.json                          # Pins SDK + declares required runtimes
├── src/
│   └── MyLib/                           # Shared library (netstandard2.0)
│       ├── MyLib.csproj
│       └── StringHelpers.cs
└── test/
    └── MyLib.Tests/                     # Tests multi-targeting net8.0;net9.0;net10.0
        ├── MyLib.Tests.csproj
        └── StringHelpersTests.cs
```

## How `dotnetup` helps

An SDK install only ships the runtime that matches its major version. If you build with
SDK 10.0.100, you get the `net10.0` runtime — but not `net8.0` or `net9.0`. Running tests
against those older targets will fail unless the runtimes are present.

This workspace declares all required runtimes directly in `global.json`:

```json
{
  "sdk": {
    "version": "10.0.100",
    "rollForward": "latestPatch",
    "allowPrerelease": false
  },
  "runtimes": {
    "dotnet": ["8.0", "9.0", "10.0"]
  }
}
```

The `runtimes.dotnet` array is the single source of truth for which runtimes the test
matrix needs. `dotnetup install` resolves the SDK, and then each additional runtime is
installed with `dotnetup runtime install`:

```bash
dotnetup install
dotnetup runtime install 8.0
dotnetup runtime install 9.0
```

The [CI workflow](../../.github/workflows/multi-runtime-testing.yml) automates this by
reading the `runtimes.dotnet` array with `jq`, skipping the runtime that already matches
the SDK major version, and installing the rest. Adding or removing a runtime version is
a one-line change to `global.json` — no workflow edits needed.

After this, `dotnet test` runs against all three target frameworks without any manual
runtime management.

## Try it yourself

### Locally

1. [Install `dotnetup`](https://get.dot.net) if you haven't already.
2. Clone this repository and navigate to this scenario folder:
   ```bash
   cd scenarios/multi-runtime-testing
   ```
3. Install the SDK and the runtimes declared in `global.json`:
   ```bash
   dotnetup install
   # Install each runtime from the runtimes.dotnet array
   dotnetup runtime install 8.0
   dotnetup runtime install 9.0
   ```
4. Run the tests across all target frameworks:
   ```bash
   dotnet test
   ```

### In CI

This scenario has a matching GitHub Actions workflow at
[`.github/workflows/multi-runtime-testing.yml`](../../.github/workflows/multi-runtime-testing.yml)
that automates the full setup. Push a change to this folder (or trigger the workflow manually)
to see it in action.