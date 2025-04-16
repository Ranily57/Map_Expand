@echo off
echo ===== Map Expand Compilation by Ranily =====

set GAME_DIR=c:\SteamLibrary\steamapps\common\REPO
set MOD_NAME=MapExpand
set MOD_AUTHOR=Ranily
set MOD_VERSION=1.0.0
set OUTPUT_DIR=%GAME_DIR%\BepInEx\plugins\%MOD_NAME%

if not exist "lib" mkdir lib

if not exist "lib\Assembly-CSharp.dll" (
    echo [INFO] Copying game DLLs...
    copy "%GAME_DIR%\REPO_Data\Managed\Assembly-CSharp.dll" lib\
    copy "%GAME_DIR%\REPO_Data\Managed\UnityEngine.dll" lib\
    copy "%GAME_DIR%\REPO_Data\Managed\UnityEngine.CoreModule.dll" lib\
)

if not exist "%GAME_DIR%\BepInEx\core\BepInEx.dll" (
    echo [ERROR] BepInEx is not installed in the game.
    echo Please install BepInEx 5.4.21 before continuing.
    echo Run setup_bepinex.bat to install it automatically.
    pause
    exit /b
)

echo [INFO] Cleaning previous files...
if exist "bin" rmdir /s /q bin
if exist "obj" rmdir /s /q obj

echo [INFO] Building the project...
dotnet restore --force
dotnet build -c Release

if not exist "bin\Release\netstandard2.1\%MOD_NAME%.dll" (
    echo [ERROR] Build failed. The file %MOD_NAME%.dll was not created.
    pause
    exit /b
)

echo [INFO] Deploying to BepInEx folder...
if not exist "%OUTPUT_DIR%" mkdir "%OUTPUT_DIR%"

copy "bin\Release\netstandard2.1\%MOD_NAME%.dll" "%OUTPUT_DIR%\"

echo [INFO] Creating distribution package...
if not exist "dist" mkdir dist
set TIMESTAMP=%date:~-4,4%%date:~-7,2%%date:~-10,2%_%time:~0,2%%time:~3,2%%time:~6,2%
set TIMESTAMP=%TIMESTAMP: =0%
set ZIP_NAME=dist\%MOD_NAME%_v%MOD_VERSION%_%TIMESTAMP%.zip

if not exist "temp_dist" mkdir temp_dist
copy "bin\Release\netstandard2.1\%MOD_NAME%.dll" "temp_dist\"
copy "README.md" "temp_dist\"

cd temp_dist
tar -a -c -f "..\%ZIP_NAME%" *
cd ..
rmdir /s /q temp_dist

echo [INFO] Package created: %ZIP_NAME%

echo.
echo ===== Compilation completed successfully! =====
echo The mod has been deployed to: %OUTPUT_DIR%
echo.
pause
