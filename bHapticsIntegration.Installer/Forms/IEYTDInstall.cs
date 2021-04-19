using System;
using System.IO;
using System.Windows.Forms;

using bHapticsIntegration.Installer.Classes;

namespace bHapticsIntegration.Installer.Forms
{
    public partial class IEYTDInstall : Form
    {
        public IEYTDInstall()
        {
            InitializeComponent();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            SelectFolderButton.Enabled = false;
            FolderTextBox.Enabled = false;
            ContinueButton.Enabled = false;

            new GlobalStorage();
            TopMainText.Visible = false;

            if (string.IsNullOrEmpty(GlobalStorage.Instance.ieytdGamePath))
            {
                MessageBox.Show("Unable to find install location, please select manually.", "");
            }
            else FolderTextBox.Text = GlobalStorage.Instance.ieytdGamePath;

            SelectFolderButton.Enabled = true;
            ContinueButton.Enabled = true;
        }

        private void SelectIEYTDFolder(object sender, EventArgs e)
        {
            DialogResult result = FileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                FolderTextBox.Text = Path.GetDirectoryName(FileDialog.FileName);
            }
        }

        private void Install(object sender, EventArgs e)
        {
        }
    }
}
