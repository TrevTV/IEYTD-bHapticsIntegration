# I Expect You To Die - bHaptics Integration

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/S6S244CYE)
[![discord](https://trevtv.github.io/assets/images/discordbutton_sm.svg)](https://discord.gg/tsbFPERwjS)

## How To Install
### Video Tutorial
[![Video Tutorial](https://i.imgur.com/YKbmnef.png)](http://www.youtube.com/watch?v=XtIIeOzcORs "I Expect You To Die - bHaptics Integration Mod Installation")

### Text Tutorial
1. Download the latest MelonLoader installer from [here](https://github.com/LavaGang/MelonLoader.Installer/releases/)
2. Open the installer, go to settings, and make sure that `Show ALPHA Pre-Releases` is checked
3. In Steam, right click the game, and under Manage, select `Browse Local Files` ![](https://i.imgur.com/fK4N0SF.png)
4. In the explorer window that appears, copy the path by right clicking the address bar and press `Copy address as text`
5. In the installer, press the SELECT button in the Automated section.
6. In that selection window, paste the address you copied before into the bar and press enter.
7. Then select `IEYTD.exe` and press Open.
8. The installer should have v0.3.0 selected and auto-detect x86 game arch, if not, set them now, then press `INSTALL`
9.  After the installer gives a prompt saying "INSTALL was Successful!" you may close it.
10. Reopen the game's folder, either by using Steam or through the path you copied, and find `version.dll`
11. Once found, rename it to `winhttp.dll` or MelonLoader will not be loaded.
12. Take the files from the downloaded zip (should contain the folders Mods, Plugins, and UserData) and drag them into the explorer window.
13. Start the game through Steam and the MelonLoader console should start and load the mods into the game.

## Project Hierarchy
This repo uses a relatively simple heirarchy
```
Root
├───bHapticsIntegration.Installer
│   └─TBD
└───bHapticsIntegration.Mod
    ├───Properties
    │   └─ Contains Assembly Info
    ├───Sections
    |   └─ Contains each level's patches as separate files to stay organized
    └─ Mod root contains the Main class and other useful classes
```

## Todo
 - The GenericGameEvents class could probably be improved, it currently contains code for other levels due to how damage is handled

## Thanks
 - Everyone at bHaptics
 - Herp Derpinstine
 - Chromium