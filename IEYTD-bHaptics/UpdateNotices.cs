using System;
using System.Net;
using MelonLoader;

namespace BHapticsSupport
{
    internal static class UpdateNotices
    {
        private static WebClient webClient = new WebClient();
        private static string versionUrl = "https://trevtv.github.io/ieytd/latest/";

        public static void RunUpdateCheck()
        {
            string versionResponse = "if you're reading this, 1. why, 2. it doesn't work due to mono blocking all certificates by default";

            try
            {
                versionResponse = webClient.DownloadString(versionUrl);
            }
            catch (Exception e)
            {
                MelonLogger.Msg(e.Message);
                return;
            }

            MelonLogger.Msg("Current version: " + BuildInfo.Version);
            MelonLogger.Msg("Remote version: " + versionResponse);

            Version remoteVersion = new Version(versionResponse);
            Version localVersion = new Version(BuildInfo.Version);

            if (remoteVersion > localVersion)
            {
                MelonLogger.Msg("Out of date!");
            }
            else if (remoteVersion <= localVersion)
            {
                MelonLogger.Msg("Up to date!");
            }
        }
    }
}
