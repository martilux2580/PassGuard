using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	/// <summary>
	/// Form to create a backup of a selected Vault in a selected dstPath
	/// </summary>
	public partial class CreateBackup : Form
	{
		public bool Success { get; private set; } //Sets whether user inputted good params and exited ok, not by X or AltF4....

		public CreateBackup()
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
			VaultBackupPathTextbox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //Set default text to Desktop folder.
			Success = false;
		}

		/// <summary>
		/// Removes leading and trailing whitespaces from textboxes
		/// </summary>
		public void TrimComponents()
		{
			VaultPathTextbox.Text = VaultPathTextbox.Text.Trim();
			VaultBackupPathTextbox.Text = VaultBackupPathTextbox.Text.Trim();
		}

		/// <summary>
		/// Opens the folder dialog to select the path where the backups of the vault will be saved....., handles also if the user doesnt select a file with requested extension or leave.....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectVaultBackupPathButton_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new(); //Folder Selector

			// Show the FolderBrowserDialog.
			DialogResult result = fbd.ShowDialog();
			if (result == DialogResult.OK)
			{
				VaultBackupPathTextbox.Text = fbd.SelectedPath;
			}
		}

		//Mouse enters button underlines button text
		[SupportedOSPlatform("windows")]
		private void SendButton_MouseEnter(object sender, EventArgs e)
		{
			SendButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void SendButton_MouseLeave(object sender, EventArgs e)
		{
			SendButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Regular the text when mouse is in the button
		}

		/// <summary>
		/// If input is OK, creates backup
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SendButton_Click(object sender, EventArgs e)
		{
			TrimComponents();

			if (String.IsNullOrEmpty(VaultPathTextbox.Text))
			{
				MessageBox.Show(text: "The path for the Vault that is going to be backed up cannot be empty.", caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
			}
			else
			{
				if(Backup.SystemBackup.CreateBackup(srcPath: VaultPathTextbox.Text, dstPath: VaultBackupPathTextbox.Text)) //If utils.CreateBackup could do its job....
				{
					MessageBox.Show(text: "Backup was created successfully.", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show(text: "There is already a Backup with that name in that directory. Please change that Backup to another folder and try again.", caption: "Backup already exists", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
				}
			}
			
		}

		/// <summary>
		/// Opens the folder dialog to select the path of the vault to be backup up, handles also if the user doesnt select a file with requested extension or leave.....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectVaultPathButton_Click(object sender, EventArgs e)
		{
			//Select and Save filepath and extension.
			string filepath = "";
			string ext = ""; //File extension
			bool cancelPathSearch = false;
			while (ext != ".encrypted" && !cancelPathSearch) //Search of a file until one with given extension is given, or the search is cancelled.
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
		/// Changes components theme when general theme changes...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CreateBackup_BackColorChanged(object sender, EventArgs e)
		{
			if (this.BackColor == Color.FromArgb(230, 230, 230))
			{
				VaultPathTextbox.BackColor = SystemColors.Window;
				VaultBackupPathTextbox.BackColor = SystemColors.Window;

			}
			else
			{
				VaultPathTextbox.BackColor = Color.FromArgb(152, 154, 153);
				VaultBackupPathTextbox.BackColor = Color.FromArgb(152, 154, 153);

			}
		}
	}
}
