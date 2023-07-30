using PassGuard.Crypto;
using PassGuard.VaultQueries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	//Form to edit the data of a Password in the Vault (Similar to other UC...)
	public partial class EditContent : Form
	{
		public String Url { get; private set; }
		public String name { get; private set; }
		public String Username { get; private set; }
		public String Password { get; private set; }
		public String Category { get; private set; }
		public String Notes { get; private set; }
		public String Important { get; private set; }

		private readonly List<String> namesInDB; //Names already in the Vault
		private List<String> categories; 
		private readonly byte[] Key;
		private readonly String decPath;
		public bool EditedSuccess { get; private set; }
		public String NameToBeEdited { get; private set; }
		private readonly Dictionary<String, String> map; //No duplicate keys, EncryptedName/DecryptedName
		private readonly ICrypt crypt = new AESAlgorithm();

		internal String GetHashofName(String name)
		{
			return map.FirstOrDefault(x => x.Value == name).Key; //Return the key, given the value.
		}

		public EditContent(List<String> names, byte[] key, String decpath, List<String> rawCategories)
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
			EditedSuccess = false;
			categories = new();

			LoadCategoryCombobox(rawCategories);

			try
			{
				this.Icon = Properties.Resources.LogoIcon64123; //Loads Icon from Image folder.
			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not load some images.", caption: "Images not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		public void TrimComponents()
		{
			NameCombobox.Text = NameCombobox.Text.Trim();
			URLTextbox.Text = URLTextbox.Text.Trim();
			NameTextbox.Text = NameTextbox.Text.Trim();
			UsernameTextbox.Text = UsernameTextbox.Text.Trim();
			PasswordTextbox.Text = PasswordTextbox.Text.Trim();
			CategoryCombobox.Text = CategoryCombobox.Text.Trim();
			NotesTextbox.Text = NotesTextbox.Text.Trim();
		}

		private void LoadCategoryCombobox(List<String> categorias)
		{
			var rawCategories = new List<String>();
			for (int i = 0; i < categorias.Count; i++)
			{
				rawCategories.Add(crypt.DecryptText(key: Key, src: categorias[i]));
			}

			categories = new HashSet<String>(rawCategories).ToList<String>(); //Remove dups

			foreach (String category in categories)
			{
				CategoryCombobox.Items.Add(category);
			}
		}

		private void EditButton_Click(object sender, EventArgs e)
		{
			TrimComponents();

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
				NameToBeEdited = NameCombobox.Text; //Get encrypted name of the values to be edited.

				Url = crypt.EncryptText(key: Key, src: URLTextbox.Text);
				name = crypt.EncryptText(key: Key, src: NameTextbox.Text);
				Username = crypt.EncryptText(key: Key, src: UsernameTextbox.Text);
				Password = crypt.EncryptText(key: Key, src: PasswordTextbox.Text);
				Category = crypt.EncryptText(key: Key, src: CategoryCombobox.Text);
				Notes = crypt.EncryptText(key: Key, src: NotesTextbox.Text);
				if (ImportantCheckbox.Checked) { Important = crypt.EncryptText(key: Key, src: "1"); }
				else { Important = crypt.EncryptText(key: Key, src: "0"); }

				EditedSuccess = true;

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
				CategoryCombobox.Text = crypt.DecryptText(key: Key, src: fullResults[4]);
				NotesTextbox.Text = crypt.DecryptText(key: Key, src: fullResults[5]);
				if(Convert.ToBoolean(Int32.Parse(crypt.DecryptText(key: Key, src: fullResults[6])))){ ImportantCheckbox.Checked = true; }
				else { ImportantCheckbox.Checked = false; }

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
				CategoryCombobox.Enabled = true;
				NotesLabel.Enabled = true;
				NotesTextbox.Enabled = true;
				EditButton.Enabled = true;
				ImportantCheckbox.Enabled = true;

			}
			else
			{
				//Null textboxes
				URLTextbox.Text = null;
				NameTextbox.Text = null;
				UsernameTextbox.Text = null;
				PasswordTextbox.Text = null;
				CategoryCombobox.Text = null;
				NotesTextbox.Text = null;
				ImportantCheckbox.Checked = false;

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
				CategoryCombobox.Enabled = false;
				NotesLabel.Enabled = false;
				NotesTextbox.Enabled = false;
				EditButton.Enabled = false;
				ImportantCheckbox.Enabled = false;

			}
		}

		private void PassVisibilityButton_Click(object sender, EventArgs e)
		{
			if (PasswordTextbox.UseSystemPasswordChar)
			{
				PasswordTextbox.UseSystemPasswordChar = false;
				PassVisibilityButton.Image = Properties.Resources.VisibilityOff24;
			}
			else
			{
				PasswordTextbox.UseSystemPasswordChar = true;
				PassVisibilityButton.Image = Properties.Resources.VisibilityOn24;
			}
		}

		private void CategoryCombobox_Validating(object sender, CancelEventArgs e)
		{
			string enteredValue = CategoryCombobox.Text;

			if (!string.IsNullOrEmpty(enteredValue))
			{
				// Case-insensitive search for a matching item
				string matchedItem = CategoryCombobox.Items
					.OfType<string>()
					.FirstOrDefault(item => item.Equals(enteredValue, StringComparison.OrdinalIgnoreCase));

				// If a matching item is found, set it as the selected item
				if (matchedItem != null)
				{
					CategoryCombobox.SelectedItem = matchedItem;
				}
			}
		}

		private void EditContent_BackColorChanged(object sender, EventArgs e)
		{
			if (this.BackColor == Color.FromArgb(230, 230, 230))
			{
				NameCombobox.BackColor = SystemColors.Window;
				URLTextbox.BackColor = SystemColors.Window;
				NameTextbox.BackColor = SystemColors.Window;
				UsernameTextbox.BackColor = SystemColors.Window;
				PasswordTextbox.BackColor = SystemColors.Window;
				CategoryCombobox.BackColor = SystemColors.Window;
				NotesTextbox.BackColor = SystemColors.Window;

			}
			else
			{
				NameCombobox.BackColor = Color.FromArgb(128, 130, 129);
				URLTextbox.BackColor = Color.FromArgb(128, 130, 129);
				NameTextbox.BackColor = Color.FromArgb(128, 130, 129);
				UsernameTextbox.BackColor = Color.FromArgb(128, 130, 129);
				PasswordTextbox.BackColor = Color.FromArgb(128, 130, 129);
				CategoryCombobox.BackColor = Color.FromArgb(128, 130, 129);
				NotesTextbox.BackColor = Color.FromArgb(128, 130, 129);

			}
		}

		[SupportedOSPlatform("windows")]
		private void EditButton_MouseEnter(object sender, EventArgs e)
		{
			EditButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline);
		}

		[SupportedOSPlatform("windows")]
		private void EditButton_MouseLeave(object sender, EventArgs e)
		{
			EditButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
		}

	
	}
}
