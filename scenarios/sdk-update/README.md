# Scenario: SDK Update

## Intent

This scenario demonstrates how to update the .NET SDK pinned in a workspace and have every
developer and CI runner pick up the new version automatically.

The workspace pins its SDK in `global.json`. When it's time to move to a newer SDK — whether
for a security patch, a new language feature, or a major version bump —
[`dotnetup`](https://github.com/dotnet/designs/blob/main/accepted/2026/dotnetup/cli-acquisition-tool.md)
makes the rollout frictionless: edit one line in `global.json`, commit, and the next
`dotnetup install` installs the new SDK everywhere, with no manual downloads or
platform-specific steps.


## How `dotnetup` helps

Without `dotnetup`, updating an SDK across a team means every developer hunts down the right
installer for their OS, and CI pipelines need manual action steps updated or matrix entries
added. Mistakes lead to "works on my machine" problems.

With `dotnetup`, the workflow is:

1. **Edit** one line in `global.json`:
   ```json
   {
     "sdk": {
       "version": "10.0.200",
       "rollForward": "latestPatch",
       "allowPrerelease": false
     }
   }
   ```
2. **Commit** the change.
3. Every developer runs `dotnetup install` — or CI runs it automatically — and gets the new SDK.

`dotnetup` reads `global.json`, determines what version is needed, and installs it to the
local hive. The same command works on Windows, macOS, and Linux.

## Try it yourself

### Locally

1. [Install `dotnetup`](https://get.dot.net) if you haven't already.
2. Clone this repository and navigate to this scenario folder:
   ```bash
   cd scenarios/sdk-update
   ```
3. Install the SDK pinned in `global.json`:
   ```bash
   dotnetup install
   ```
4. Verify the correct SDK is active:
   ```bash
   dotnet --version
   ```
5. Run the tests to confirm everything builds and passes:
   ```bash
   dotnet test test/MyLib.Tests
   ```
6. To simulate an update, bump the `sdk.version` in `global.json` and re-run:
   ```bash
   dotnetup install
   dotnet --version   # now shows the new version
   dotnet test test/MyLib.Tests
   ```

### In CI

This scenario has a matching GitHub Actions workflow at
[`.github/workflows/sdk-update.yml`](../../.github/workflows/sdk-update.yml)
that builds on Windows, macOS, and Linux in parallel. The key step is:

```yaml
- name: Install .NET SDK via dotnetup
  run: dotnetup install --install-path ${{ env.LOCAL_INSTALL_ROOT }} --manifest-path .dotnetup.json
```

Bumping the version in `global.json` is the only change needed — no workflow edits required.
Push a change to this folder (or trigger the workflow manually) to see it in action.
