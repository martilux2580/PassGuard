using PassGuard.Crypto;
using PassGuard.PDF;
using PassGuard.Properties;
using PassGuard.VaultQueries;
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
	/// UC to handle exporting the vaults data as a pdf from outside the login
	/// </summary>
	public partial class ExportPdfFromSettings : UserControl
	{
		public ExportPdfFromSettings()
		{
			InitializeComponent();

		}

		/// <summary>
		/// Removes leading and trailing spaces from textboxes
		/// </summary>
		public void TrimComponents()
		{
			VaultEmailTextbox.Text = VaultEmailTextbox.Text.Trim();
			VaultPassTextbox.Text = VaultPassTextbox.Text.Trim();
			SecurityKeyTextbox.Text = SecurityKeyTextbox.Text.Trim();
			VaultPathTextbox.Text = VaultPathTextbox.Text.Trim();
		}

		/// <summary>
		/// Load saved email into email textbox...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Saves the text in the email textbox in the config file...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Shows or hides the plaintext of the password in the textbox.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Shows or hides the plaintext of the SK in the textbox.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Loads the saved Security Key in the SK Textbox...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Saves the text of the SK textbox in the config file...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Opens the file dialog to select .encrypted files...and handles if the user does other thing like exiting or selecting another file extension
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
				}; //File selector

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
		/// Decrypt the vault and its content, and export that decrypted data as PDF.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExportPDFButton_Click(object sender, EventArgs e)
		{
			TrimComponents();

			ICrypt crypt = new AESAlgorithm();
			IPDF pdf = new PDFCreator();
			IKDF kdf = new PBKDF2Function();
			IQuery query;

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

				var encVault = Path.Combine(saveEncryptedVaultPath); //Set path for encrypted vault.
				var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])); //Set path for decrypted vault.

				try
				{
					//Calculate keys to decrypt vault.
					var vKey = kdf.GetVaultKey(password: (VaultEmailTextbox.Text + VaultPassTextbox.Text), salt: Convert.FromBase64String(SecurityKeyTextbox.Text), bytes: 32);
					var keyVStr = Utils.StringUtils.Base64ToString(Convert.ToBase64String(vKey));
					var skStr = Utils.StringUtils.Base64ToString(SecurityKeyTextbox.Text);
					var cKey = kdf.GetVaultKey(password: (keyVStr + (VaultEmailTextbox.Text + VaultPassTextbox.Text)), salt: Encoding.Default.GetBytes(skStr + keyVStr), bytes: 32);

					//Obtain all its decrypted elements.
					crypt.Decrypt(key: vKey, src: encVault, dst: decVault);

					query = new Query(decVault);
					List<String[]> fullResults = query.GetAllData();

					crypt.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])), Path.Combine(saveEncryptedVaultPath));
					File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1]));

					//Decrypt data.
					foreach (String[] row in fullResults)
					{
						row[0] = crypt.DecryptText(key: cKey, src: row[0]);
						row[1] = crypt.DecryptText(key: cKey, src: row[1]);
						row[2] = crypt.DecryptText(key: cKey, src: row[2]);
						row[3] = crypt.DecryptText(key: cKey, src: row[3]);
						row[4] = crypt.DecryptText(key: cKey, src: row[4]);
						row[5] = crypt.DecryptText(key: cKey, src: row[5]);
						row[6] = crypt.DecryptText(key: cKey, src: row[6]);
					}

					//Create PDF
					pdf.CreatePDF(fullResults, lastValue[0], ConfigurationManager.AppSettings.Get("Email"), ConfigurationManager.AppSettings.Get("SecurityKey"));

					//Reset textboxes
					VaultEmailTextbox.Text = "";
					VaultPassTextbox.Text = "";
					VaultPathTextbox.Text = "";
					SecurityKeyTextbox.Text = "";

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
					//If any error, reset vault to normal state...
					if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + ".db3")))
					{
						File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + ".db3"));
					}
				}
			}
		}

		/// <summary>
		/// Change components theme when theme is changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExportPdfFromSettings_BackColorChanged(object sender, EventArgs e)
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

		//Mouse over button underlines button text
		[SupportedOSPlatform("windows")]
		private void LoadSavedEmailButton_MouseEnter(object sender, EventArgs e)
		{
			LoadSavedEmailButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline); //Dont underline the text when mouse leaves
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void LoadSavedEmailButton_MouseLeave(object sender, EventArgs e)
		{
			LoadSavedEmailButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		//Mouse over button underlines button text
		[SupportedOSPlatform("windows")]
		private void SaveEmailButton_MouseEnter(object sender, EventArgs e)
		{
			SaveEmailButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline); //Dont underline the text when mouse leaves
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void SaveEmailButton_MouseLeave(object sender, EventArgs e)
		{
			SaveEmailButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		//Mouse over button underlines button text
		[SupportedOSPlatform("windows")]
		private void LoadSavedSKButton_MouseEnter(object sender, EventArgs e)
		{
			LoadSavedSKButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline); //Dont underline the text when mouse leaves
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void LoadSavedSKButton_MouseLeave(object sender, EventArgs e)
		{
			LoadSavedSKButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		//Mouse over button underlines button text
		[SupportedOSPlatform("windows")]
		private void SaveSKButton_MouseEnter(object sender, EventArgs e)
		{
			SaveSKButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline); //Dont underline the text when mouse leaves
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void SaveSKButton_MouseLeave(object sender, EventArgs e)
		{
			SaveSKButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //Dont underline the text when mouse leaves
		}
	}
}
