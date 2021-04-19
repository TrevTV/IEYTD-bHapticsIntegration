
namespace bHapticsIntegration.Installer.Forms
{
    partial class IEYTDInstall
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopMainText = new System.Windows.Forms.Label();
            this.FolderTextBox = new System.Windows.Forms.TextBox();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.FileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // TopMainText
            // 
            this.TopMainText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TopMainText.AutoSize = true;
            this.TopMainText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopMainText.Location = new System.Drawing.Point(110, 9);
            this.TopMainText.Name = "TopMainText";
            this.TopMainText.Size = new System.Drawing.Size(208, 25);
            this.TopMainText.TabIndex = 0;
            this.TopMainText.Text = "Finding game path...";
            // 
            // FolderTextBox
            // 
            this.FolderTextBox.Location = new System.Drawing.Point(128, 55);
            this.FolderTextBox.Name = "FolderTextBox";
            this.FolderTextBox.Size = new System.Drawing.Size(275, 20);
            this.FolderTextBox.TabIndex = 1;
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Location = new System.Drawing.Point(47, 53);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(75, 23);
            this.SelectFolderButton.TabIndex = 2;
            this.SelectFolderButton.Text = "Select";
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectIEYTDFolder);
            // 
            // ContinueButton
            // 
            this.ContinueButton.Location = new System.Drawing.Point(137, 95);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(136, 36);
            this.ContinueButton.TabIndex = 3;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.Install);
            // 
            // FileDialog
            // 
            this.FileDialog.FileName = "IEYTD.exe";
            this.FileDialog.Filter = "Executable Files (*.exe)|*.exe";
            // 
            // IEYTDInstall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 158);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.SelectFolderButton);
            this.Controls.Add(this.FolderTextBox);
            this.Controls.Add(this.TopMainText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "IEYTDInstall";
            this.ShowIcon = false;
            this.Text = "Select IEYTD Directory";
            this.Load += new System.EventHandler(this.FormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TopMainText;
        private System.Windows.Forms.TextBox FolderTextBox;
        private System.Windows.Forms.Button SelectFolderButton;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.OpenFileDialog FileDialog;
    }
}