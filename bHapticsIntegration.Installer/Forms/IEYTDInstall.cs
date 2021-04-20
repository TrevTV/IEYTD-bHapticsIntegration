using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO.Compression;

using EpicMorg.SteamPathsLib;
using EpicMorg.SteamPathsLib.Model;

namespace bHapticsIntegration.Installer.Forms
{
    public partial class IEYTDInstall : Form
    {
        public string ieytdGamePath;
        public const int ieytdSteamAppId = 587430;
        public const string baseUrl = "https://trevtv.github.io/ieytd";

        public WebClient webClient = new WebClient();

        public IEYTDInstall() => InitializeComponent();

        private void FormLoad(object sender, EventArgs e)
        {
            SelectFolderButton.Enabled = false;
            FolderTextBox.Enabled = false;
            ContinueButton.Enabled = false;

            FindIEYTDDirectory();
            TopMainText.Visible = false;

            if (string.IsNullOrEmpty(ieytdGamePath))
                MessageBox.Show("Unable to find install location, please select manually.", "");
            else FolderTextBox.Text = ieytdGamePath;

            SelectFolderButton.Enabled = true;
            ContinueButton.Enabled = true;
        }

        private void FindIEYTDDirectory()
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
        }

        private void SelectIEYTDFolder(object sender, EventArgs e)
        {
            DialogResult result = FileDialog.ShowDialog();

            if (result == DialogResult.OK)
                FolderTextBox.Text = Path.GetDirectoryName(FileDialog.FileName);
        }

        private void Install(object sender, EventArgs e)
        {
            // TODO: fix existing files crash
            // TODO: ml update may break this
            // TODO: move completely to github instead of using github pages
            try
            {
                string currentRelease = webClient.DownloadString(baseUrl + "/latest");
                Debug.WriteLine("Current Release: " + currentRelease);

                string downloadUrl = $"https://github.com/LavaGang/MelonLoader/releases/download/v0.3.0/MelonLoader.x86.zip";
                string downloadPath = Path.GetTempFileName();
                webClient.DownloadFile(downloadUrl, downloadPath);

                ZipFile.ExtractToDirectory(downloadPath, ieytdGamePath);
                File.Move(Path.Combine(ieytdGamePath, "version.dll"), Path.Combine(ieytdGamePath, "winhttp.dll"));

                downloadUrl = $"https://github.com/TrevTV/IEYTD-bHapticsIntegration/releases/download/{currentRelease}/{currentRelease}.zip";
                downloadPath = Path.GetTempFileName();
                webClient.DownloadFile(downloadUrl, downloadPath);

                ZipFile.ExtractToDirectory(downloadPath, ieytdGamePath);

                MessageBox.Show("Installation successful!", "Done");
            }
            catch (Exception ex)
            {
                DateTime currentTime = DateTime.Now;
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), $"Error_{currentTime.ToString("T").Replace(":", "-")}.txt"), ex.Message);
                MessageBox.Show("Installation failed, upload the created log file to #support in the IEYTD bHaptics Discord Server" +
                    "\nhttps://discord.gg/tsbFPERwjS", "Failed");
            }
        }
    }
}
