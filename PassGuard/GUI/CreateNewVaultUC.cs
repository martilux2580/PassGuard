using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Transactions;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;
using System.Runtime.Versioning;
using PassGuard.Crypto;
using PassGuard.VaultQueries;

namespace PassGuard.GUI
{
	//User Control Component to setup the data to create a new Password Vault.
	public partial class CreateNewVaultUC : UserControl
	{
		public CreateNewVaultUC()
		{
			InitializeComponent();
			VaultPathTextbox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //Set default text to Desktop folder.
			SaveEmailTooltip.SetToolTip(SaveEmailCheckbox, "If this option is checked and the new vault is created successfully, this email \nwill be saved so that the process of loading the password vault is faster. \nNote: If another vault is created and this option is checked, previously saved email \nwill be deleted and the new email will be saved.");
		}

		public void TrimComponents()
		{
			VaultEmailTextbox.Text = VaultEmailTextbox.Text.Trim();
			VaultNameTextbox.Text = VaultNameTextbox.Text.Trim();
			VaultPassTextbox.Text = VaultPassTextbox.Text.Trim();
			ConfirmPassVaultTextbox.Text = ConfirmPassVaultTextbox.Text.Trim();
			VaultPathTextbox.Text = VaultPathTextbox.Text.Trim();

		}

		[SupportedOSPlatform("windows")]
		private void CreateNewVaultButton_MouseEnter(object sender, EventArgs e)
		{
			CreateNewVaultButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void CreateNewVaultButton_MouseLeave(object sender, EventArgs e)
		{
			CreateNewVaultButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Regularise the text when mouse is not in the button
		}

		private void CreateNewVaultButton_Click(object sender, EventArgs e)
		{
			TrimComponents();
			ICrypt crypt = new AESAlgorithm();
			IQuery query;
			IKDF kdf = new PBKDF2Function();
			String errorMessages = ""; //All the error messages due to input, later print it to user in just one messagebox.

			//If any field is blank.
			if ((String.IsNullOrWhiteSpace(VaultEmailTextbox.Text)) || (String.IsNullOrWhiteSpace(VaultNameTextbox.Text)) || (String.IsNullOrWhiteSpace(VaultPassTextbox.Text)) || (String.IsNullOrWhiteSpace(ConfirmPassVaultTextbox.Text)) || (String.IsNullOrWhiteSpace(VaultPathTextbox.Text)))
			{
				errorMessages += "    - There cannot be fields left in blank.\n";
			}

			//Validate the format of email.
			try
			{
				var test = new System.Net.Mail.MailAddress(VaultEmailTextbox.Text);
			}
			catch (Exception)
			{
				errorMessages += "    - Invalid Email Format.\n";
			}

			bool validName = Utils.StringUtils.Check(VaultNameTextbox.Text, "Lower") || Utils.StringUtils.Check(VaultNameTextbox.Text, "Upper") || Utils.StringUtils.Check(VaultNameTextbox.Text, "Number"); //Name not composed of symbols.
			if (!validName) //Validate name of vault.
			{
				errorMessages += "    - The new vault´s name should be composed of letters or numbers.\n";
			}

			bool validPass = Utils.StringUtils.Check(VaultPassTextbox.Text, "Lower") && Utils.StringUtils.Check(VaultPassTextbox.Text, "Upper") && Utils.StringUtils.Check(VaultPassTextbox.Text, "Number") && Utils.StringUtils.Check(VaultPassTextbox.Text, "Symbol") && (VaultPassTextbox.Text.Length >= 12);
			if (!validPass) //Valid password
			{
				errorMessages += "    - The password must have upper and lower case letters, numbers, symbols and must have a minimum length of 12 characters.\n";
			}
			
			if (!String.Equals(VaultPassTextbox.Text, ConfirmPassVaultTextbox.Text)) //Valid Confirmation of Password.
			{
				errorMessages += "    - Confirmation Password does not match actual password.\n";
			}

			//Deal with path.
			List<String> checkEncryptedPath = VaultPathTextbox.Text.Split('\\').ToList(); //Separate the path by \
			checkEncryptedPath[0] = checkEncryptedPath[0] + "\\"; //In the first place, add the \, so it is C:\ and not just C:
			checkEncryptedPath.Add(VaultNameTextbox.Text + ".encrypted"); //Add in the folder path the name of the file and its extension.
			if (File.Exists(Path.Combine(checkEncryptedPath.ToArray()))) //There is not already a vault in that location.
			{
				errorMessages += "    - In the specified path there is already created a Password Vault with the same file name.\n";
			}

			if (!String.IsNullOrEmpty(errorMessages)) //If any error...
			{
				MessageBox.Show(text: "The following errors have been found:\n\n" + errorMessages, caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
			}
			else //No error in params, create vault.
			{
				String path = VaultPathTextbox.Text + "\\" + VaultNameTextbox.Text + ".db3"; //Path for the vault.
				SQLiteConnection.CreateFile(path); //Create 0-byte file that will be modeled when it is opened, if it already exists then it is substituted.

				query = new Query(path);
				query.CreateNewVault();

				//Vault Encryption
				//Deal with paths for files.
				List<String> saveVaultPath = path.Split('\\').ToList();
				saveVaultPath[0] = saveVaultPath[0] + "\\";

				List<String> saveEncryptedVaultPath = VaultPathTextbox.Text.Split('\\').ToList();
				saveEncryptedVaultPath[0] = saveEncryptedVaultPath[0] + "\\";
				saveEncryptedVaultPath.Add(VaultNameTextbox.Text + ".encrypted");

				//Encrypt New Vault
				//Generate random salt.
				Random rnd = new();
				byte[] salt = new byte[16];
				rnd.NextBytes(salt);
				string rndsalt = Convert.ToBase64String(salt);
				//Encrypt and delete previous file.
				crypt.Encrypt(key: kdf.GetVaultKey(password: (VaultEmailTextbox.Text + VaultPassTextbox.Text), salt: Convert.FromBase64String(rndsalt), bytes: 32), Path.Combine(saveVaultPath.ToArray()), Path.Combine(saveEncryptedVaultPath.ToArray()));
				File.Delete(Path.Combine(saveVaultPath.ToArray()));

				//Save salt and maybe email.
				try
				{
					Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
					config.AppSettings.Settings["SecurityKey"].Value = rndsalt; //Modify data in the config file for future executions.
					if (SaveEmailCheckbox.Checked)
					{
						config.AppSettings.Settings["Email"].Value = VaultEmailTextbox.Text; //Modify data in the config file for future executions.
					}
					config.Save(ConfigurationSaveMode.Modified, true);
					ConfigurationManager.RefreshSection("appSettings");

					//Inform user
					var data = "\t• Vault Name: " + VaultNameTextbox.Text + "\n\t• Filename: " + VaultNameTextbox.Text + ".encrypted" + "\n\t• Email: " + VaultEmailTextbox.Text + "\n\t• Vault Password: " + VaultPassTextbox.Text + "\n\t• Security Key: " + rndsalt;
					Clipboard.SetText(data);
					GUI.InfoNewVaultCreatedForm info = new(data);
					info.ShowDialog();
				}
				catch(Exception)
				{
					MessageBox.Show(text: "PassGuard could not access config file, this feature can´t be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				//Leave everything as if the user wanted to create a new password vault...
				VaultNameTextbox.Text = VaultPassTextbox.Text = ConfirmPassVaultTextbox.Text = "";

			}

		}

		private void SelectVaultPathButton_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new(); //Folder Selector...

			// Show the FolderBrowserDialog.
			DialogResult result = fbd.ShowDialog();
			if (result == DialogResult.OK)
			{
				VaultPathTextbox.Text = fbd.SelectedPath;
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
	}

 }

