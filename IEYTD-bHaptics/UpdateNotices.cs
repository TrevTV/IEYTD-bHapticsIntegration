using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using MelonLoader;

namespace BHapticsSupport
{
    internal static class UpdateNotices
    {
        //private static WebClient webClient = new WebClient();
        //private static string versionUrl = "https://trevtv.github.io/ieytd/latest/";

        [DllImport("update_checker.dll")]
        public static extern string GetLatestVersion();

        public static void RunUpdateCheck()
        {
            /*string versionResponse;

            try
            {
                versionResponse = webClient.DownloadString(versionUrl);
            }
            catch (Exception e)
            {
                PrintError(e.Message);
                return;
            }*/

            string versionResponse = GetLatestVersion();

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

        public static void PrintWarning(string warning)
        {
            MelonLogger.Error("##############################");
            MelonLogger.Error(warning);
            MelonLogger.Error("##############################");
        }

        public static void PrintError(string error)
        {
            MelonLogger.Error("##############################");
            MelonLogger.Error(error);
            MelonLogger.Error("##############################");
        }
    }
}
