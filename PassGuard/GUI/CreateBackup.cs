﻿using System;
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
    //Form to create a backup of a selected Vault in a selected dstPath
    public partial class CreateBackup : Form
    {
        private String srcPath { get; set; }
        private String dstPath { get; set; }
        private bool success;

        public CreateBackup()
        {
            InitializeComponent();
            try
            {
                SelectVaultBackupPathButton.Image = Image.FromFile(@".\Images\FolderIcon.ico"); //Loads Image for the Settings Icon
                SelectVaultPathButton.Image = Image.FromFile(@".\Images\FolderIcon.ico"); //Loads Image for the Settings Icon
                this.Icon = new Icon(@".\Images\LogoIcon64123.ico"); //Loads Icon from Image folder.
            }
            catch (Exception)
            {
                MessageBox.Show(text: "PassGuard could not load some images.", caption: "Images not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }
            VaultBackupPathTextbox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //Set default text to Desktop folder.
            success = false;
        }

        public bool getSuccess()
        {
            return success;
        }

        private void SelectVaultBackupPathButton_Click(object sender, EventArgs e)
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FolderBrowserDialog fbd = new FolderBrowserDialog(); //Folder Selector

            // Show the FolderBrowserDialog.
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                path = fbd.SelectedPath;
                VaultBackupPathTextbox.Text = path;
            }
        }

        private void SendButton_MouseEnter(object sender, EventArgs e)
        {
            SendButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void SendButton_MouseLeave(object sender, EventArgs e)
        {
            SendButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //Regular the text when mouse is in the button
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();
            if (String.IsNullOrEmpty(VaultPathTextbox.Text))
            {
                MessageBox.Show(text: "The path for the Vault that is going to be backed up cannot be empty.", caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
            }
            else
            {
                if(utils.CreateBackup(srcPath: VaultPathTextbox.Text, dstPath: VaultBackupPathTextbox.Text)) //If utils.CreateBackup could do its job....
                {
                    MessageBox.Show(text: "Backup was created successfully.", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(text: "There is already a Backup with that name in that directory. Please change that Backup to another folder and try again.", caption: "Backup already exists", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
                }
            }
            
        }

        private void LoadSavedBackupPathButton_MouseEnter(object sender, EventArgs e)
        {
            LoadSavedBackupPathButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void LoadSavedBackupPathButton_MouseLeave(object sender, EventArgs e)
        {
            LoadSavedBackupPathButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //Underline the text when mouse is in the button
        }

        private void LoadSavedBackupPathButton_Click(object sender, EventArgs e)
        {
            VaultBackupPathTextbox.Text = ConfigurationManager.AppSettings.Get("dstBackupPathForSave"); //Modify data in the config file for future executions.
        }

        private void SelectVaultPathButton_Click(object sender, EventArgs e)
        {
            //Select and Save filepath and extension.
            string filepath = "";
            string ext = ""; //File extension
            bool cancelPathSearch = false;
            while (ext != ".encrypted" && !cancelPathSearch) //Search of a file until one with given extension is given, or the search is cancelled.
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

    }
}