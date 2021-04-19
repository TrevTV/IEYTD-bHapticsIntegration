using System;
using System.Diagnostics;
using System.Collections.Generic;

using EpicMorg.SteamPathsLib;
using EpicMorg.SteamPathsLib.Model;

namespace bHapticsIntegration.Installer.Classes
{
    public class GlobalStorage
    {
        public static GlobalStorage Instance { get; set; }

        public string ieytdGamePath;
        public const int ieytdSteamAppId = 587430;

        public GlobalStorage()
        {
            SteamAppRegistryData registry = SteamPathsUtil.GetSteamAppDataById(ieytdSteamAppId);
            SteamAppManifestData manifest = SteamPathsUtil.GetSteamAppManifestDataById(ieytdSteamAppId);

            bool installed = registry?.Installed ?? false;

            Debug.WriteLine("Installed: " + installed);
            if (installed)
            {
                Debug.WriteLine("Install Directory: " + manifest?.Path);
                ieytdGamePath = manifest?.Path;
            }

            Instance = this;
        }
    }
}
