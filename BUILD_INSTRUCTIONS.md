# PhotonRedirect BepInEx Plugin - Build Instructions

## Prerequisites
1. .NET SDK 7.0+ or Visual Studio with C# development tools
2. A copy of the game with BepInEx 5.4+ already installed
3. The game's managed DLLs (Photon.Pun.dll, Photon.Realtime.dll, UnityEngine.dll)

## Setup Instructions

### Step 1: Prepare the lib folder
Create a `lib` folder in the same directory as the .csproj file and copy these DLLs from your game:

**From your game's `BepInEx\core` folder:**
- BepInEx.dll
- 0Harmony.dll

**From your game's `<GameName>_Data\Managed` folder:**
- Photon.Pun.dll
- Photon.Realtime.dll
- UnityEngine.dll
- UnityEngine.CoreModule.dll

### Step 2: Build the plugin

**Option A: Using .NET CLI**
```bash
cd "c:\Users\MSI\Downloads\the photon redirect thing"
dotnet build -c Release
```

**Option B: Using Visual Studio**
1. Open Visual Studio
2. File → Open → Project/Solution
3. Select `PhotonRedirect.csproj`
4. Right-click Solution → Build Solution

### Step 3: Deploy to game
Copy the compiled `PhotonRedirect.dll` from:
- `bin/Release/net472/PhotonRedirect.dll`

To your game's:
- `BepInEx/plugins/PhotonRedirect/`

Create the `PhotonRedirect` folder if it doesn't exist.

### Step 4: Configure (Optional)
Copy the `photon-config.json` to one of these locations:
1. Same folder as the DLL: `BepInEx/plugins/PhotonRedirect/photon-config.json`
2. `<GameName>_Data/StreamingAssets/photon-config.json`
3. Game's persistent data folder

Example `photon-config.json`:
```json
{
  "overridePhotonSettings": true,
  "appIdRealtime": "your-app-id-here",
  "appIdVoice": "your-voice-app-id-here",
  "appIdChat": "your-chat-app-id-here",
  "fixedRegion": "us",
  "useNameServer": false,
  "server": "your-server-address",
  "port": 5055
}
```

## Troubleshooting

**DLL Not Found Error:** Make sure all required DLLs are in the `lib` folder

**Plugin Not Loading:** Check BepInEx console for error messages

**Wrong Version:** Ensure BepInEx version is 5.4.23+ and HarmonyLib is 2.3.3+

## Changes Made
- Fixed port validation to check upper bound (≤ 65535) in both override methods
- Ensures invalid port numbers cannot be set through config files
- Maintains consistency with JSON config validation
