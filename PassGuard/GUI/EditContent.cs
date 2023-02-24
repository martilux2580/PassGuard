using PassGuard.Crypto;
using PassGuard.VaultQueries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	//Form to edit the data of a Password in the Vault (Similar to other UC...)
	public partial class EditContent : Form
	{
		public String url { get; private set; }
		public String name { get; private set; }
		public String username { get; private set; }
		public String password { get; private set; }
		public String category { get; private set; }
		public String notes { get; private set; }

		private List<String> namesInDB; //Names already in the Vault
		private readonly byte[] Key;
		private readonly String decPath;
		public bool editedSuccess { get; private set; }
		public String nameToBeEdited { get; private set; }
		private readonly Dictionary<String, String> map; //No duplicate keys, EncryptedName/DecryptedName
		private ICrypt crypt = new AESAlgorithm();

		internal String getHashofName(String name)
		{
			return map.FirstOrDefault(x => x.Value == name).Key; //Return the key, given the value.
		}        

		public EditContent(List<String> names, byte[] key, String decpath)
		{
			InitializeComponent();

			decPath = decpath;
			Key = key;

			namesInDB = names;
			map = new Dictionary<string, string>();
			foreach(String enc in namesInDB) //Create dictionary with encrypted name, decrypted name.
			{
				map.Add(enc, crypt.DecryptText(key: Key, src: enc));
			}

			NameCombobox.Items.Add("");
			foreach (String name in namesInDB) //Decrypt names in DB.
			{
				NameCombobox.Items.Add(crypt.DecryptText(key: Key, src: name));
			}
			editedSuccess = false;

			try
			{
				this.Icon = Properties.Resources.LogoIcon64123; //Loads Icon from Image folder.
			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not load some images.", caption: "Images not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		private void EditButton_Click(object sender, EventArgs e)
		{
			String errorMessages = "";

			if (String.IsNullOrWhiteSpace(NameTextbox.Text) || String.IsNullOrWhiteSpace(UsernameTextbox.Text) || String.IsNullOrWhiteSpace(PasswordTextbox.Text))
			{
				errorMessages += "Non-optional fields (Name, Username, Password) cannot be left blank.";
			}

			//If the list of decrypted names contains the name in the textbox, and it is a different name from the one selected in the combobox, there is an error.
			//We can edit a password and maintain the name, but what we cannot do is select a name in the combobox and change the name to a name that is already in the Vault.
			if ((map.Values.ToList<String>().Contains(NameTextbox.Text)) && (NameTextbox.Text != NameCombobox.Text))
			{
				errorMessages += "There is already a saved password with that name in the vault.";
			}

			if (!String.IsNullOrEmpty(errorMessages)) //If any error...
			{
				MessageBox.Show(text: "The following errors have been found:\n\n" + errorMessages, caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
			}
			else //No error in params, create vault.
			{
				nameToBeEdited = NameCombobox.Text; //Get encrypted name of the values to be edited.

				url = crypt.EncryptText(key: Key, src: URLTextbox.Text);
				name = crypt.EncryptText(key: Key, src: NameTextbox.Text);
				username = crypt.EncryptText(key: Key, src: UsernameTextbox.Text);
				password = crypt.EncryptText(key: Key, src: PasswordTextbox.Text);
				category = crypt.EncryptText(key: Key, src: CategoryTextbox.Text);
				notes = crypt.EncryptText(key: Key, src: NotesTextbox.Text);

				editedSuccess = true;

				this.Close();


			}

		}

		private void NameCombobox_SelectedIndexChanged(object sender, EventArgs e)
		{
			IQuery query;
			if ((!String.IsNullOrWhiteSpace(NameCombobox.Text)) && (!String.IsNullOrEmpty(NameCombobox.Text))) //Fetch the data of the name selected.
			{
				query = new Query(decPath);
				var keyToSearch = map.FirstOrDefault(x => x.Value == NameCombobox.Text).Key;

				String[] fullResults = query.GetPassword(keyToSearch);

				//Set values in textboxes
				URLTextbox.Text = crypt.DecryptText(key: Key, src: fullResults[0]);
				NameTextbox.Text = crypt.DecryptText(key: Key, src: fullResults[1]);
				UsernameTextbox.Text = crypt.DecryptText(key: Key, src: fullResults[2]);
				PasswordTextbox.Text = crypt.DecryptText(key: Key, src: fullResults[3]);
				CategoryTextbox.Text = crypt.DecryptText(key: Key, src: fullResults[4]);
				NotesTextbox.Text = crypt.DecryptText(key: Key, src: fullResults[5]);

				//Enable elements
				URLLabel.Enabled = true;
				URLTextbox.Enabled = true;
				NameLabel.Enabled = true;
				NameTextbox.Enabled = true;
				UsernameLabel.Enabled = true;
				UsernameTextbox.Enabled = true;
				PassLabel.Enabled = true;
				PasswordTextbox.Enabled = true;
				CategoryLabel.Enabled = true;
				CategoryTextbox.Enabled = true;
				NotesLabel.Enabled = true;
				NotesTextbox.Enabled = true;
				EditButton.Enabled = true;

			}
			else
			{
				//Null textboxes
				URLTextbox.Text = null;
				NameTextbox.Text = null;
				UsernameTextbox.Text = null;
				PasswordTextbox.Text = null;
				CategoryTextbox.Text = null;
				NotesTextbox.Text = null;

				//Unable elements
				URLLabel.Enabled = false;
				URLTextbox.Enabled = false;
				NameLabel.Enabled = false;
				NameTextbox.Enabled = false;
				UsernameLabel.Enabled = false;
				UsernameTextbox.Enabled = false;
				PassLabel.Enabled = false;
				PasswordTextbox.Enabled = false;
				CategoryLabel.Enabled = false;
				CategoryTextbox.Enabled = false;
				NotesLabel.Enabled = false;
				NotesTextbox.Enabled = false;
				EditButton.Enabled = false;

			}
		}
	}
}
