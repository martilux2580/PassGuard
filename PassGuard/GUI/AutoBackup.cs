using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
    public partial class AutoBackup : Form
    {
        public AutoBackup()
        {
            InitializeComponent();
            SelectVaultPathButton.Image = Image.FromFile(@"..\..\Images\FolderIcon.ico"); //Loads Image for the Settings Icon
            SelectVaultBackupFilesPathButton.Image = Image.FromFile(@"..\..\Images\FolderIcon.ico"); //Loads Image for the Settings Icon
            this.Icon = new Icon(@"..\..\Images\LogoIcon64123.ico"); //Loads Icon from Image folder.
            SetupFrequencyCombobox();


        }

        private void SetupFrequencyCombobox()
        {
            FrequencyCombobox.Items.Add("");
            FrequencyCombobox.Items.Add("After any change on the contents of the Vault.");
            FrequencyCombobox.Items.Add("Just before closing the Aplication.");
            FrequencyCombobox.Items.Add("Every day.");
            FrequencyCombobox.Items.Add("Every week.");
            FrequencyCombobox.Items.Add("Every month.");

        }

        private void SetupAutoBackupButton_Click(object sender, EventArgs e)
        {
            String errorMessages = "";
            if (String.IsNullOrEmpty(VaultPathTextbox.Text) || String.IsNullOrEmpty(BackupPathFilesTextbox.Text) || String.IsNullOrEmpty(FrequencyCombobox.Text))
            {
                errorMessages += "There cannot be fields left in blank.";
            }
            
            if (!String.IsNullOrEmpty(errorMessages)) //If any error...
            {
                MessageBox.Show(text: "The following errors have been found:\n\n" + errorMessages, caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);

            }
            else
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                Core.Utils utils = new Core.Utils();
                /*config.AppSettings.Settings["SecurityKey"].Value = rndsalt; //Modify data in the config file for future executions.
                config.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("appSettings");*/


            }

            


            
        }
        private void ActivateBackupCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if(ActivateBackupCheckbox.Checked == true)
            {
                FrequencyCombobox.Enabled = true;
                VaultPathLabel.Enabled = true;
                VaultPathTextbox.Enabled = true;
                SelectVaultPathButton.Enabled = true;
                BackupPathLabel.Enabled = true;
                BackupPathFilesTextbox.Enabled = true;
                SelectVaultBackupFilesPathButton.Enabled = true;
                FrequencyLabel.Enabled = true;
                SetupAutoBackupButton.Enabled = true;
            }
            else if(ActivateBackupCheckbox.Checked == false)
            {
                FrequencyCombobox.Enabled = false; 
                VaultPathLabel.Enabled = false;
                VaultPathTextbox.Enabled = false;
                SelectVaultPathButton.Enabled = false;
                BackupPathLabel.Enabled = false;
                BackupPathFilesTextbox.Enabled = false;
                SelectVaultBackupFilesPathButton.Enabled = false;
                FrequencyLabel.Enabled = false;
                SetupAutoBackupButton.Enabled = false;
            }
            
        }

        private void SelectVaultPathButton_Click(object sender, EventArgs e)
        {
            //Select and Save filepath and extension.
            string filepath = "";
            string ext = ""; //File extension
            bool cancelPathSearch = false;
            while (ext != ".encrypted" && !cancelPathSearch)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "PassGuard Vaults|*.encrypted"; //Type of file we are looking for...

                var result = ofd.ShowDialog();
                filepath = ofd.FileName;
                ext = Path.GetExtension(filepath).ToLower();

                if (result == DialogResult.Cancel)
                {
                    cancelPathSearch = true; //Stop loop, as user hit cancel button.
                }
                else if (ext != ".encrypted") //If user specified file with diff extension...
                {
                    MessageBox.Show(text: "Selected file must have .encrypted extension.", caption: "Wrong File", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                }
            }
            VaultPathTextbox.Text = filepath;
        }

        private void SelectVaultBackupFilesPathButton_Click(object sender, EventArgs e)
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            // Show the FolderBrowserDialog.
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                path = fbd.SelectedPath;
                VaultPathTextbox.Text = path;
            }
        }
    }
}
