using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;

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
            string currentRelease = "";
        }
    }
}
