# Map Expand - Mod for R.E.P.O.

This mod adds an expanded view of the map in the game R.E.P.O.

## Prerequisites

- [R.E.P.O.](https://store.steampowered.com/app/3241660/REPO/)
- [BepInEx 5.4.21](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.21)

## Installation

1. Download and install BepInEx 5.4.21 in your R.E.P.O. game folder.
    - Use the `setup_bepinex.bat` script for automatic installation
    - Or manually extract the contents of BepInEx into the main game folder where REPO.exe is located
    - Launch the game once to allow BepInEx to create its configuration folders

2. Download the latest version of the mod from the releases page
    - Extract the `REPOMod.dll` file into the `BepInEx/plugins/` folder
    - Or compile the mod yourself using `build.bat`

3. Launch the game and enjoy the new features!

## Features

- **Expanded Map View**: Increases the size of the map camera ("Dirt Finder Map Camera") to 5, allowing you to see a much larger portion of the map without excessive scrolling.
- This modification only affects the map camera in the main game scene.

## Configuration

After launching the game once with the mod, a configuration file is created at:
`BepInEx/config/com.ranily.repo.mapexpand.cfg`

You can modify the following settings:
- `MapCamera.Size`: Size of the map camera (default: 10, game value: ~5)

## Development Resources

### Essential Tools to Download
- [BepInEx 5.4.21](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.21) - Modding framework
- [dnSpy](https://github.com/dnSpy/dnSpy/releases) or [ILSpy](https://github.com/icsharpcode/ILSpy/releases) - To decompile and explore the game's code
- [Unity Explorer](https://github.com/sinai-dev/UnityExplorer/releases) - To explore Unity objects during runtime

### GitHub Search Terms
To find other existing mods, search for:
- "REPO game mod"
- "REPO BepInEx"
- "R.E.P.O modding"

### Communities to Join
- [Official R.E.P.O Discord](https://discord.gg/semiwork) - Check if there is a dedicated modding channel
- Look for communities on Reddit or Nexus Mods

## Compilation

If you want to compile the mod yourself:

1. Ensure you have installed the .NET 6.0 SDK or higher
2. Clone this repository
3. Modify the game path in `build.bat` if necessary
4. Run `build.bat` to compile the mod
5. The compiled DLL will be copied to the game's BepInEx folder

## Troubleshooting

- If the mod does not load, check the log files in `BepInEx/logs/`
- If the map is not expanded after installing the mod:
  - Press F8 in-game to force the map expansion
  - Check that the `MapCamera.Size` value in the configuration file is not too small
  - Check the logs to see if the camera was correctly identified
- Ensure the BepInEx version is compatible (5.4.21 recommended)

## Development Environment Setup

### Installing BepInEx for Development

1. **Download BepInEx 5.4.21**:
    - Visit [the BepInEx releases page](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.21)
    - Download `BepInEx_x64_5.4.21.0.zip` for 64-bit Windows games

2. **Install in the Game**:
    - Extract all contents of the ZIP into the main R.E.P.O. game folder (where REPO.exe is located)
    - Launch the game once and close it to allow BepInEx to create its configuration folders

3. **Setup for Development**:
    - Create a `libs` folder in your development directory (`c:\Users\raphi\Downloads\Fullmap REPO\libs`)
    - Copy the following files from the BepInEx installation folder to your `libs` folder:
      - `BepInEx/core/BepInEx.dll`
      - `BepInEx/core/0Harmony.dll`
      - `BepInEx/core/MonoMod.Utils.dll`
      - `BepInEx/core/MonoMod.RuntimeDetour.dll`

4. **Reference the DLLs in Your Project**:
    - Add references to these DLLs in your `.csproj` file
    - Your `.csproj` file should already include these references via NuGet packages

### File Organization

Recommended project structure:
```
c:\Users\raphi\Downloads\Fullmap REPO\
├── REPOMod.csproj          # Project file
├── build.bat               # Build script
├── README.md               # Documentation
├── libs\                   # External DLLs for reference (if needed)
├── lib\                    # Game DLLs for reference
│   ├── Assembly-CSharp.dll
│   ├── UnityEngine.dll
│   └── UnityEngine.CoreModule.dll
└── src\                    # Mod source code
     ├── Plugin.cs           # Mod entry point
     ├── PluginInfo.cs       # Mod information
     └── Utils\              # Utility classes
          └── GameExplorer.cs
```

## Credits

- Developed by Ranily
- Based on the game R.E.P.O. by semiwork
