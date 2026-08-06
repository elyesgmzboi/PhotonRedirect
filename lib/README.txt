# Place Required DLLs Here

## Required DLLs

### From BepInEx\core folder:
- BepInEx.dll
- 0Harmony.dll

### From <GameName>_Data\Managed folder:
- PhotonUnityNetworking.dll
- PhotonUnityNetworking.Utilities.dll
- PhotonRealtime.dll
- UnityEngine.dll
- UnityEngine.CoreModule.dll

(Optional, only if you use Voice/Chat features and want to extend the plugin later:
 PhotonVoice.dll, PhotonVoice.API.dll, PhotonVoice.PUN.dll, PhotonChat.dll)

## Instructions

1. Locate your game installation folder
2. Copy the DLLs from the paths listed above
3. Paste them into this `lib` folder
4. Run `build.bat` to compile the plugin

If you're unsure about your game's folder structure:
- For Steam games: Right-click game in Steam → Properties → Local Files → Browse
- For other launchers: Check your game's installation directory
- BepInEx folder is in the same directory as the game's .exe file
