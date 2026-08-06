@echo off
REM PhotonRedirect Build Script for BepInEx

echo.
echo ========================================
echo PhotonRedirect Build Script
echo ========================================
echo.

REM Check if .NET SDK is installed
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo Error: .NET SDK not found. Please install .NET SDK 7.0 or later.
    echo Download from: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

echo Step 1: Checking for required lib folder...
if not exist "lib" (
    echo.
    echo ERROR: lib folder not found!
    echo.
    echo Please create a 'lib' folder in this directory and copy the following DLLs:
    echo.
    echo From game's BepInEx\core:
    echo   - BepInEx.dll
    echo   - 0Harmony.dll
    echo.
    echo From game's ^<GameName^>_Data\Managed - Photon/Unity DLLs, not on NuGet:
    echo   - PhotonUnityNetworking.dll
    echo   - PhotonUnityNetworking.Utilities.dll
    echo   - PhotonRealtime.dll
    echo   - UnityEngine.dll
    echo   - UnityEngine.CoreModule.dll
    echo.
    echo BepInEx.dll and 0Harmony.dll are now pulled automatically via NuGet.
    echo.
    pause
    exit /b 1
)

echo Step 2: Verifying required DLLs...
if not exist "lib\PhotonUnityNetworking.dll" (
    echo Error: lib\PhotonUnityNetworking.dll not found
    pause
    exit /b 1
)
if not exist "lib\PhotonUnityNetworking.Utilities.dll" (
    echo Error: lib\PhotonUnityNetworking.Utilities.dll not found
    pause
    exit /b 1
)
if not exist "lib\PhotonRealtime.dll" (
    echo Error: lib\PhotonRealtime.dll not found
    pause
    exit /b 1
)
if not exist "lib\UnityEngine.dll" (
    echo Error: lib\UnityEngine.dll not found
    pause
    exit /b 1
)
if not exist "lib\UnityEngine.CoreModule.dll" (
    echo Error: lib\UnityEngine.CoreModule.dll not found
    pause
    exit /b 1
)

echo Step 3: Building project...
dotnet build PhotonRedirect.csproj -c Release

if errorlevel 1 (
    echo.
    echo Build failed! Check errors above.
    pause
    exit /b 1
)

echo.
echo ========================================
echo Build completed successfully!
echo ========================================
echo.
echo Output DLL: bin\Release\net472\PhotonRedirect.dll
echo.
echo Next steps:
echo 1. Copy PhotonRedirect.dll to your game's BepInEx\plugins\PhotonRedirect\ folder
echo 2. Optional: copy photon-config.json to the same folder or game's StreamingAssets folder
echo 3. Run the game!
echo.
pause
