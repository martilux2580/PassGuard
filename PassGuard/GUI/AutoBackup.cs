﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	/// <summary>
	/// Form to configure an AutoBackup of a selected Vault in a selected pathForBackups and with selected frequency.
	/// </summary>
	public partial class AutoBackup : Form
	{
		private readonly Dictionary<int, String> frequencies = new(); //Maps numeric mode to text description of frequencies of autobackup...
		public String AutoBackupState { get; private set; } //AutoBackup true (activated) or false
		public String PathOfVaultBackedUp { get; private set; } //Path of the Vault to be backed up.
		public String PathForBackups { get; private set; } //Path where the Backups will be saved.
		public String LastDateBackup { get; private set; } //Date when the last backup was made (more oriented to modes 3, 4, 5).
		public String FrequencyBackup { get; private set; } //Mode for the frequency
		public bool SetupSuccess { get; private set; } //Flag for checking if the setup was correct, or user clicked X or AltF4....

		public AutoBackup()
		{
			InitializeComponent();
			try
			{
				this.Icon = Properties.Resources.LogoIcon64123; //Loads Icon from Image folder.
			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not load some images.", caption: "Images not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
			SetupSuccess = false;
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

		/// <summary>
		/// Removes leading and trailing whitespaces from textboxes
		/// </summary>
		public void TrimComponents()
		{
			VaultPathTextbox.Text = VaultPathTextbox.Text.Trim();
			BackupPathFilesTextbox.Text = BackupPathFilesTextbox.Text.Trim();
			FrequencyCombobox.Text = FrequencyCombobox.Text.Trim();

		}

		/// <summary>
		/// Loads the values saved from previous configurations of AutoBackup
		/// </summary>
		private void SetupInitialValues()
		{
			if (ConfigurationManager.AppSettings["AutoBackupState"] == "false")
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

			VaultPathTextbox.Text = ConfigurationManager.AppSettings["PathVaultForAutoBackup"]; //Modify data in the config file for future executions.
			BackupPathFilesTextbox.Text = ConfigurationManager.AppSettings["dstBackupPathForSave"]; //Modify data in the config file for future executions.
			FrequencyCombobox.Text = frequencies[Int32.Parse(ConfigurationManager.AppSettings["FrequencyAutoBackup"])];

		}

		/// <summary>
		/// Add to Dictionary the equivalence of int mode - String text description of frequency, and sets data of frequency combobox
		/// </summary>
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

		/// <summary>
		/// If data inputted by user was ok, sets up variables with the information of the new configuration of autobackup and closes window....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SetupAutoBackupButton_Click(object sender, EventArgs e)
		{
			TrimComponents();

			if (ActivateBackupCheckbox.Checked == false) //Users sets deactivated autobackup
			{
				AutoBackupState = "false";
				PathOfVaultBackedUp = VaultPathTextbox.Text; //Empty string
				PathForBackups = BackupPathFilesTextbox.Text;
				LastDateBackup = DateTime.Now.ToString(); //If it is the very first time we set AutoBackup, no value will be saved in config, with this we set a value and prevent errors.
				FrequencyBackup = "0";

				SetupSuccess = true; //Everything was set correctly, activate signal for Form1

				this.Close();

			}
			else //Users sets activated autobackup
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
					PathOfVaultBackedUp = VaultPathTextbox.Text; 
					PathForBackups = BackupPathFilesTextbox.Text;
					LastDateBackup = DateTime.Now.ToString(); 
					FrequencyBackup = frequencies.FirstOrDefault(x => (x.Value == FrequencyCombobox.Text)).Key.ToString(); //Int value of string description of the frequency combobox.

					SetupSuccess = true; //Everything was set correctly, activate signal for Form1

					this.Close();

				}
			}

		}

		/// <summary>
		/// Enables or disables components whether the user activates or deactivates autobackup...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

				//Reset content, disabling component doesnt remove components text/content
				VaultPathTextbox.Text = "";
				BackupPathFilesTextbox.Text = "";
				FrequencyCombobox.Text = "";
			}
			
		}

		/// <summary>
		/// Opens the folder dialog to select the vault to be backed up....and handles if the user selects a random file or exits....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectVaultPathButton_Click(object sender, EventArgs e)
		{
			//Select and Save filepath and extension.
			string filepath = "";
			string ext = ""; //File extension
			bool cancelPathSearch = false;
			while (ext != ".encrypted" && !cancelPathSearch)
			{
				OpenFileDialog ofd = new()
				{
					Filter = "PassGuard Vaults|*.encrypted" //Type of file we are looking for...
				}; //File Selector

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

		/// <summary>
		/// Opens the folder dialog to select a path where the autobackup backups will be saved...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectVaultBackupFilesPathButton_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new(); //Folder selector

			// Show the FolderBrowserDialog.
			DialogResult result = fbd.ShowDialog();
			if (result == DialogResult.OK)
			{
				BackupPathFilesTextbox.Text = fbd.SelectedPath;
			}
		}

		/// <summary>
		/// Changes the components theme when the general theme is changed....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AutoBackup_BackColorChanged(object sender, EventArgs e)
		{
			if (this.BackColor == Color.FromArgb(230, 230, 230))
			{
				VaultPathTextbox.BackColor = SystemColors.Window;
				BackupPathFilesTextbox.BackColor = SystemColors.Window;
				FrequencyCombobox.BackColor = SystemColors.Window;

			}
			else
			{
				VaultPathTextbox.BackColor = Color.FromArgb(152, 154, 153);
				BackupPathFilesTextbox.BackColor = Color.FromArgb(152, 154, 153);
				FrequencyCombobox.BackColor = Color.FromArgb(128, 130, 129);

			}
		}

		//Mouse enters button underlines button text
		[SupportedOSPlatform("windows")]
		private void SetupAutoBackupButton_MouseEnter(object sender, EventArgs e)
		{
			SetupAutoBackupButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void SetupAutoBackupButton_MouseLeave(object sender, EventArgs e)
		{
			SetupAutoBackupButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Underline the text when mouse is in the button
		}
	}
}
