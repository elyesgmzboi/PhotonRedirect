# PhotonRedirect Plugin - Summary

## What Was Fixed

### 1. **Port Validation Consistency**
   - **Issue**: The `ApplyOverridesNow()` method only checked if port > 0, but didn't validate the upper bound
   - **Fix**: Added upper bound check (≤ 65535) to match the `TryLoadJsonConfig()` validation
   - **Impact**: Prevents invalid port numbers from being set through BepInEx config
   - **Locations Fixed**: 
     - Line 167: `ApplyOverridesNow()` method
     - Line 329: `BeforeConnectUsingSettings()` method

## Project Structure

```
the photon redirect thing/
├── PhotonRedirect.cs           # Main plugin code (FIXED)
├── PhotonRedirect.csproj       # Project file for compilation
├── build.bat                   # Batch script to build the plugin
├── BUILD_INSTRUCTIONS.md       # Detailed build guide
├── photon-config.json          # Configuration file (optional)
└── lib/                        # Required DLLs go here
    └── README.txt             # Instructions for DLL placement
```

## Quick Start

### Step 1: Gather Required DLLs
Run `build.bat` - it will tell you exactly which DLLs are missing

### Step 2: Place DLLs in lib/ folder
Copy these from your game:
- From `BepInEx\core`: BepInEx.dll, 0Harmony.dll
- From `<GameName>_Data\Managed`: Photon.Pun.dll, Photon.Realtime.dll, UnityEngine.dll, UnityEngine.CoreModule.dll

### Step 3: Compile
Double-click `build.bat` or run: `dotnet build -c Release`

### Step 4: Deploy
Copy compiled DLL to: `<GameFolder>\BepInEx\plugins\PhotonRedirect\PhotonRedirect.dll`

## Files Included

- **PhotonRedirect.cs** - Main plugin source code
- **PhotonRedirect.csproj** - .NET project configuration
- **build.bat** - Automated build script
- **photon-config.json** - Sample configuration file
- **BUILD_INSTRUCTIONS.md** - Detailed step-by-step guide

## Requirements

- .NET SDK 6.0+ (download from https://dotnet.microsoft.com/download)
- Your game with BepInEx 5.4+ already installed
- Game installation folder for DLL extraction

## Configuration

Edit `photon-config.json` to override Photon settings:

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

## Support

If compilation fails:
1. Check that all DLLs are in the `lib` folder
2. Verify .NET SDK is installed: `dotnet --version`
3. Check BepInEx console for runtime errors
4. Ensure BepInEx 5.4.23+ is installed in your game
