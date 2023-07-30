using PassGuard.Crypto;
using PassGuard.PDF;
using PassGuard.VaultQueries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	//UC Component to obtain the credentials to login to a selected Vault.
	public partial class LoadVaultUC : UserControl
	{

		public LoadVaultUC()
		{
			InitializeComponent();

		}

		public void TrimComponents()
		{
			VaultEmailTextbox.Text = VaultEmailTextbox.Text.Trim();
			VaultPassTextbox.Text = VaultPassTextbox.Text.Trim();
			SecurityKeyTextbox.Text = SecurityKeyTextbox.Text.Trim();
			VaultPathTextbox.Text = VaultPathTextbox.Text.Trim();
		}

		private void LoadSavedSKButton_Click(object sender, EventArgs e)
		{
			try
			{
				SecurityKeyTextbox.Text = ConfigurationManager.AppSettings["SecurityKey"]; //Get data in the config file for future executions.
			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not access config file, this feature can´t be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		[SupportedOSPlatform("windows")]
		private void LoadVaultButton_Click(object sender, EventArgs e)
		{
			TrimComponents();
			IKDF kdf = new PBKDF2Function();

			//If any field is blank.
			if ((String.IsNullOrWhiteSpace(VaultEmailTextbox.Text)) || (String.IsNullOrWhiteSpace(VaultPassTextbox.Text)) || (String.IsNullOrWhiteSpace(SecurityKeyTextbox.Text)) || (String.IsNullOrWhiteSpace(VaultPathTextbox.Text)))
			{
				MessageBox.Show(text: "There cannot be fields left in blank.\n", caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
			}
			else //No error in params, load vault.
			{
				//Deal with paths for files.
				String pathforEncryptedVault = VaultPathTextbox.Text;
				String[] saveEncryptedVaultPath = pathforEncryptedVault.Split('\\');
				saveEncryptedVaultPath[0] = saveEncryptedVaultPath[0] + "\\"; //Path of the encrypted vault in a list.
				String[] lastValue = saveEncryptedVaultPath[saveEncryptedVaultPath.Length - 1].Split('.'); //[filename, filextension]
				lastValue[lastValue.Length - 1] = "db3"; //FileExtension

				try
				{
					//Calculate key to decrypt vault
					var key = kdf.GetVaultKey(password: (VaultEmailTextbox.Text + VaultPassTextbox.Text), salt: Convert.FromBase64String(SecurityKeyTextbox.Text), bytes: 32);

					//Show all the contents of the vault (UserControl).
					GUI.VaultContentUC vc = new(Path.Combine(saveEncryptedVaultPath), VaultEmailTextbox.Text, VaultPassTextbox.Text, key, SecurityKeyTextbox.Text); //Put the main panel visible.
					var ContentPanel = this.Parent;
					this.Parent.Controls.Clear(); //this.Parent.Name = ContentPanel
					ContentPanel.Controls.Add(vc);
					vc.Visible = true;
				}
				catch (Exception ex)
				{
					if ((ex is FormatException) || (ex is System.Security.Cryptography.CryptographicException))
					{
						MessageBox.Show(text: "PassGuard could not unlock your Vault. Check the input credentials.", caption: "Not correct credentials.", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
					}
					else
					{
						MessageBox.Show(text: "PassGuard could not unlock or open the Vault. Check the inserted credentials, verify the file is not corrupted and try again later.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
					}
				}
				finally
				{
					if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + ".db3")))
					{
						File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + ".db3"));
					}
				}
				

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
				OpenFileDialog ofd = new()
				{
					Filter = "PassGuard Vaults|*.encrypted" //Type of file we are looking for...
				}; //File selector

				var result = ofd.ShowDialog();
				filepath = ofd.FileName;
				ext = Path.GetExtension(filepath).ToLower();

				if (result == DialogResult.Cancel)
				{
					cancelPathSearch = true; //Stop loop, as user hit cancel button.
				}
				else if (ext!=".encrypted") //If user specified file with diff extension...
				{
					MessageBox.Show(text: "Selected file must have .encrypted extension.", caption: "Wrong File", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
				}
			}
			VaultPathTextbox.Text = filepath;
		}

		[SupportedOSPlatform("windows")]
		private void LoadVaultButton_MouseEnter(object sender, EventArgs e)
		{
			LoadVaultButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void LoadVaultButton_MouseLeave(object sender, EventArgs e)
		{
			LoadVaultButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void LoadSavedSKButton_MouseEnter(object sender, EventArgs e)
		{
			LoadSavedSKButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void LoadSavedSKButton_MouseLeave(object sender, EventArgs e)
		{
			LoadSavedSKButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //Underline the text when mouse is in the button
		}

		private void LoadSavedEmailButton_Click(object sender, EventArgs e)
		{
			try
			{
				VaultEmailTextbox.Text = ConfigurationManager.AppSettings["Email"]; //Modify data in the config file for future executions.
			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not access config file, this feature can´t be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		[SupportedOSPlatform("windows")]
		private void LoadSavedEmailButton_MouseEnter(object sender, EventArgs e)
		{
			LoadSavedEmailButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void LoadSavedEmailButton_MouseLeave(object sender, EventArgs e)
		{
			LoadSavedEmailButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //Underline the text when mouse is in the button
		}

		private void SaveEmailButton_Click(object sender, EventArgs e)
		{
			try
			{
				Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				config.AppSettings.Settings["Email"].Value = VaultEmailTextbox.Text; //Modify data in the config file for future executions.
				config.Save(ConfigurationSaveMode.Modified, true);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.
			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not access config file, this feature can´t be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		private void SaveSKButton_Click(object sender, EventArgs e)
		{
			try
			{
				Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				config.AppSettings.Settings["SecurityKey"].Value = SecurityKeyTextbox.Text; //Modify data in the config file for future executions.
				config.Save(ConfigurationSaveMode.Modified, true);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.
			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not access config file, this feature can´t be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		private void PassVisibilityButton_Click(object sender, EventArgs e)
		{
			if (VaultPassTextbox.UseSystemPasswordChar)
			{
				VaultPassTextbox.UseSystemPasswordChar = false;
				PassVisibilityButton.Image = Properties.Resources.VisibilityOff24;
			}
			else
			{
				VaultPassTextbox.UseSystemPasswordChar = true;
				PassVisibilityButton.Image = Properties.Resources.VisibilityOn24;
			}
		}

		private void SKVisibilityButton_Click(object sender, EventArgs e)
		{
			if (SecurityKeyTextbox.UseSystemPasswordChar)
			{
				SecurityKeyTextbox.UseSystemPasswordChar = false;
				SKVisibilityButton.Image = Properties.Resources.VisibilityOff24;
			}
			else
			{
				SecurityKeyTextbox.UseSystemPasswordChar = true;
				SKVisibilityButton.Image = Properties.Resources.VisibilityOn24;
			}
		}

		private void LoadVaultUC_BackColorChanged(object sender, EventArgs e)
		{
			if (this.BackColor == Color.FromArgb(230, 230, 230)) 
			{ 
				VaultEmailTextbox.BackColor = SystemColors.Window;
				VaultPassTextbox.BackColor = SystemColors.Window;
				SecurityKeyTextbox.BackColor = SystemColors.Window;
				VaultPathTextbox.BackColor = SystemColors.Window;

			}
			else 
			{
				VaultEmailTextbox.BackColor = Color.FromArgb(128, 130, 129);
				VaultPassTextbox.BackColor = Color.FromArgb(128, 130, 129);
				SecurityKeyTextbox.BackColor = Color.FromArgb(128, 130, 129);
				VaultPathTextbox.BackColor = Color.FromArgb(128, 130, 129);

			}
		}

		[SupportedOSPlatform("windows")]
		private void SaveEmailButton_MouseEnter(object sender, EventArgs e)
		{
			SaveEmailButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline); //Dont underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void SaveEmailButton_MouseLeave(object sender, EventArgs e)
		{
			SaveEmailButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void SaveSKButton_MouseEnter(object sender, EventArgs e)
		{
			SaveSKButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline); //Dont underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void SaveSKButton_MouseLeave(object sender, EventArgs e)
		{
			SaveSKButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

	}
}
