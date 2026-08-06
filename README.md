
# PhotonRedirect

First release made by zephkek (https://github.com/Zephkek ) and the second release is fully recoded by me

PhotonRedirect is a BepInEx plugin that overrides Photon/PUN connection settings at runtime so you can redirect the game to a custom Photon server (useful for reviving games that rely on live Photon servers).


# Games Tested

The Tabung, The Tabung Reborn (prototype v0.1.3 of The Tabung)
## Quick Start

1. Build the project:

```powershell
.\build.bat
```

2. Copy the compiled DLL to your game's `BepInEx\plugins` folder:

```
<GameFolder>\BepInEx\plugins\PhotonRedirect\PhotonRedirect.dll
```

3. (Optional) Provide a `photon-config.json` alongside the DLL or in the game's `StreamingAssets` or persistent data folder to pre-fill config values.

4. Start the game and check `BepInEx\LogOutput.log` for plugin messages.

## Project Layout

- `PhotonRedirect.cs` — main plugin source
- `PhotonRedirect.csproj` — .NET project file
- `build.bat` — build helper
- `lib/` — required runtime DLLs copied from the target game

## Configuration (photon-config.json example)

```json
{
  "overridePhotonSettings": true,
  "appIdRealtime": "YOUR_APP_ID",
  "appIdVoice": "YOUR_VOICE_APP_ID",
  "appIdChat": "YOUR_CHAT_APP_ID",
  "fixedRegion": "us",
  "useNameServer": false,
  "server": "your.server.address",
  "port": 5055
}
```

## Requirements

- .NET SDK (for building)
- BepInEx 5.4+ installed in the target game
- Copies of the game's runtime assemblies in `lib/` (see `BUILD_INSTRUCTIONS.md`)

## Support

If build or runtime problems occur:

1. Ensure the required DLLs are placed in `lib/` before building.
2. Run `dotnet --version` to verify your SDK.
3. Check `BepInEx\LogOutput.log` for errors when the game runs.
4. If Harmony fails to patch, ensure the game’s PUN/Photon versions match the `lib` DLLs.

## License

This repository is licensed under the MIT License — see `LICENSE`.

