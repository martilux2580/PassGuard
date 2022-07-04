using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
    //Form to configure an AutoBackup of a selected Vault in a selected pathForBackups and with selected frequency.
    public partial class AutoBackup : Form
    {
        private Dictionary<int, String> frequencies = new Dictionary<int, String>();
        private String AutoBackupState; //AutoBackup true (activated) or false
        private String pathOfVaultBackedUp; //Path of the Vault to be backed up.
        private String pathForBackups; //Path where the Backups will be saved.
        private String lastDateBackup; //Date when the last backup was made (more oriented to modes 3, 4, 5).
        private String frequencyBackup; //Mode for the frequency
        private bool setupSuccess;

        public bool GetSetupSuccess()
        {
            return setupSuccess;
        }

        public String GetAutoBackupState()
        {
            return AutoBackupState;
        }

        public String GetPathOfVaultBackedUp()
        {
            return pathOfVaultBackedUp;
        }

        public String GetPathForBackups()
        {
            return pathForBackups;
        }

        public String GetLastDateBackup()
        {
            return lastDateBackup;
        }

        public String GetFrequencyBackup()
        {
            return frequencyBackup;
        }

        public AutoBackup(Form MainWindow)
        {
            InitializeComponent();
            try
            {
                SelectVaultPathButton.Image = Image.FromFile(@".\Images\FolderIcon.ico"); //Loads Image for the Settings Icon
                SelectVaultBackupFilesPathButton.Image = Image.FromFile(@".\Images\FolderIcon.ico"); //Loads Image for the Settings Icon
                this.Icon = new Icon(@".\Images\LogoIcon64123.ico"); //Loads Icon from Image folder.
            }
            catch (Exception)
            {
                MessageBox.Show(text: "PassGuard could not load some images.", caption: "Images not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }
            setupSuccess = false;
            SetupFrequencyCombobox();
            try
            {
                SetupInitialValues();
            }
            catch (Exception)
            {
                MessageBox.Show(text: "PassGuard could not load previous AutoBackup config.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }

        }

        //Loads the values saved from previous configurations of AutoBackup
        private void SetupInitialValues()
        {
            if (ConfigurationManager.AppSettings.Get("AutoBackupState") == "false")
            {
                ActivateBackupCheckbox.Checked = false;
                NoteLabel.Enabled = false;

                FrequencyCombobox.Enabled = false;
                VaultPathLabel.Enabled = false;
                VaultPathTextbox.Enabled = false;
                SelectVaultPathButton.Enabled = false;
                BackupPathLabel.Enabled = false;
                BackupPathFilesTextbox.Enabled = false;
                SelectVaultBackupFilesPathButton.Enabled = false;
                FrequencyLabel.Enabled = false;
            }
            else if (ConfigurationManager.AppSettings.Get("AutoBackupState") == "true")
            {
                ActivateBackupCheckbox.Checked = true;
                NoteLabel.Enabled = true;

                FrequencyCombobox.Enabled = true;
                VaultPathLabel.Enabled = true;
                SelectVaultPathButton.Enabled = true;
                BackupPathLabel.Enabled = true;
                SelectVaultBackupFilesPathButton.Enabled = true;
                FrequencyLabel.Enabled = true;

            }

            VaultPathTextbox.Text = ConfigurationManager.AppSettings.Get("PathVaultForAutoBackup"); //Modify data in the config file for future executions.
            BackupPathFilesTextbox.Text = ConfigurationManager.AppSettings.Get("dstBackupPathForSave"); //Modify data in the config file for future executions.
            FrequencyCombobox.Text = frequencies[Int32.Parse(ConfigurationManager.AppSettings.Get("FrequencyAutoBackup"))];

        }

        //Add to Dictionary the equivalence of int mode - String text description of frequency
        private void SetupFrequencyCombobox()
        {
            FrequencyCombobox.Items.Add("");
            FrequencyCombobox.Items.Add("After any change on the contents of the Vault.");
            FrequencyCombobox.Items.Add("Just before closing the Aplication.");
            FrequencyCombobox.Items.Add("Every day.");
            FrequencyCombobox.Items.Add("Every week.");
            FrequencyCombobox.Items.Add("Every month.");

            frequencies.Add(0, "");
            frequencies.Add(1, "After any change on the contents of the Vault.");
            frequencies.Add(2, "Just before closing the Aplication.");
            frequencies.Add(3, "Every day.");
            frequencies.Add(4, "Every week.");
            frequencies.Add(5, "Every month.");

        }

        private void SetupAutoBackupButton_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();
            if (ActivateBackupCheckbox.Checked == false) //It it was deactivated...
            {
                AutoBackupState = "false";
                pathOfVaultBackedUp = VaultPathTextbox.Text; //Empty string
                pathForBackups = BackupPathFilesTextbox.Text;
                lastDateBackup = DateTime.Now.ToString(); //If it is the very first time we set AutoBackup, no value will be saved in config, with this we set a value and prevent errors.
                frequencyBackup = "0";

                setupSuccess = true; //Everything was set correctly, activate signal for Form1

                this.Close();

            }
            else
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
                else //Activated Autobackup
                {
                    AutoBackupState = "true";
                    pathOfVaultBackedUp = VaultPathTextbox.Text; 
                    pathForBackups = BackupPathFilesTextbox.Text;
                    lastDateBackup = DateTime.Now.ToString(); 
                    frequencyBackup = frequencies.FirstOrDefault(x => (x.Value == FrequencyCombobox.Text)).Key.ToString(); //String value of int mode of the frequency.

                    setupSuccess = true; //Everything was set correctly, activate signal for Form1

                    this.Close();

                }
            }

        }

        private void ActivateBackupCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if(ActivateBackupCheckbox.Checked == true)
            {

                NoteLabel.Enabled = true;
                FrequencyCombobox.Enabled = true;
                VaultPathLabel.Enabled = true;
                SelectVaultPathButton.Enabled = true;
                BackupPathLabel.Enabled = true;
                SelectVaultBackupFilesPathButton.Enabled = true;
                FrequencyLabel.Enabled = true;
            }
            else if(ActivateBackupCheckbox.Checked == false)
            {
                NoteLabel.Enabled = false;
                FrequencyCombobox.Enabled = false; 
                VaultPathLabel.Enabled = false;
                VaultPathTextbox.Enabled = false;
                SelectVaultPathButton.Enabled = false;
                BackupPathLabel.Enabled = false;
                BackupPathFilesTextbox.Enabled = false;
                SelectVaultBackupFilesPathButton.Enabled = false;
                FrequencyLabel.Enabled = false;

                VaultPathTextbox.Text = "";
                BackupPathFilesTextbox.Text = "";
                FrequencyCombobox.Text = "";
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
                OpenFileDialog ofd = new OpenFileDialog(); //File Selector
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
            FolderBrowserDialog fbd = new FolderBrowserDialog(); //Folder selector

            // Show the FolderBrowserDialog.
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                path = fbd.SelectedPath;
                BackupPathFilesTextbox.Text = path;
            }
        }
    }
}
