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
	/// <summary>
	/// Form to delete a password from the Vault, or all passwords from the Vault.
	/// </summary>
	public partial class DeleteContent : Form
	{
		private readonly List<String> namesInDB; //Names already in vault.
		private readonly byte[] Key; //ckey
		private readonly String decPath;
		public bool DeletedSuccess { get; private set; } //Signal for user deleted one password.
		public bool DeletedAllSuccess { get; private set; } //Signal for user deleted all passwords
		public String NameToBeDeleted { get; private set; } //Name the user wants to delete
		private readonly Dictionary<String, String> map; //No duplicate keys, (encryptedName, decryptedName)
		private readonly ICrypt crypt = new AESAlgorithm();

		public DeleteContent(List<String> names, byte[] key, String decpath)
		{
			InitializeComponent();
			decPath = decpath;
			Key = key;

			namesInDB = names;
			map = new Dictionary<string, string>();
			foreach (String enc in namesInDB) //Decrypt names already in db and add them to Dictionary with its key correspondence.
			{
				map.Add(enc, crypt.DecryptText(key: Key, src: enc));
			}

			NameCombobox.Items.Add(""); //Allow user to reset combobox text
			foreach (String name in namesInDB) //Decrypt names in db.
			{
				NameCombobox.Items.Add(crypt.DecryptText(key: Key, src: name));
			}
			DeletedSuccess = false;
			DeletedAllSuccess = false;

			try
			{
				this.Icon = Properties.Resources.LogoIcon64123; //Loads Icon from Image folder.
			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not load some images.", caption: "Images not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}

		}

		/// <summary>
		/// Removes leading and trailing whitespaces of textboxes
		/// </summary>
		public void TrimComponents()
		{
			NameCombobox.Text = NameCombobox.Text.Trim();
			URLTextbox.Text = URLTextbox.Text.Trim();
			NameTextbox.Text = NameTextbox.Text.Trim();
			UsernameTextbox.Text = UsernameTextbox.Text.Trim();
			PasswordTextbox.Text = PasswordTextbox.Text.Trim();
			CategoryTextbox.Text = CategoryTextbox.Text.Trim();
			NotesTextbox.Text = NotesTextbox.Text.Trim();
		}

		/// <summary>
		/// Handle enabling/disabling components when the user wants to either delete all passwords or just one
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EnableDeleteAllCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			if(EnableDeleteAllCheckbox.Checked == true) //User wants to delete all passwords.
			{
				//Null textboxes
				URLTextbox.Text = null;
				NameTextbox.Text = null;
				UsernameTextbox.Text = null;
				PasswordTextbox.Text = null;
				CategoryTextbox.Text = null;
				NotesTextbox.Text = null;
				ImportantCheckbox.Checked= false;

				TitleLabel.Text = "If button is clicked, all Vault contents will be deleted.";
				NameCombobox.Enabled = false;
				NameCombobox.ResetText(); //Set text back to null/unselected

				//Enable elements
				DeleteAllButton.Enabled = true;
				DeleteButton.Enabled = false;

			}
			else if (EnableDeleteAllCheckbox.Checked == false) //User wants to delete one password...
			{
				TitleLabel.Text = "Select the name of the password you want to delete: ";

				//Enable elements
				DeleteAllButton.Enabled = false;
				DeleteButton.Enabled = true;
				NameCombobox.Enabled = true;

			}
		}

		/// <summary>
		/// Gets the encrypted name to be deleted and closes the form if everything went okey...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteButton_Click(object sender, EventArgs e)
		{
			TrimComponents();

			if (String.IsNullOrEmpty(NameCombobox.Text) || String.IsNullOrWhiteSpace(NameCombobox.Text))
			{
				MessageBox.Show(text: "An element must be selected in order to be deleted.", caption: "No element selected", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
			}
			else
			{
				DialogResult dialog = MessageBox.Show(text: "Are you sure you want to delete the element with name '" + NameTextbox.Text + "' from your Vault?\n\nNote: This action cannot be undone.", caption: "Confirm the deletion of the element", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
				if (dialog == DialogResult.Yes)
				{
					NameToBeDeleted = map.FirstOrDefault(x => x.Value == NameCombobox.Text).Key; //Get encrypted name to be deleted.
					DeletedSuccess = true;
					this.Close();

				}
			
			}
		}


		/// <summary>
		/// Activates DeletedAllSuccess flag to send to the calling function that user wants to delete all passwords....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteAllButton_Click(object sender, EventArgs e)
		{
			DialogResult dialog = MessageBox.Show(text: "Are you sure you want to delete all the elements in your Vault? After this action, your Vault will be completely empty. \n\nNote: This action cannot be undone.", caption: "Confirm the deletion of all the elements", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
			if (dialog == DialogResult.Yes)
			{
				DeletedAllSuccess = true;
				this.Close();

			}
		}

		/// <summary>
		/// Gets the data of the selected name and fills textboxes, or resets them if selected name is null
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NameCombobox_SelectedIndexChanged(object sender, EventArgs e)
		{
			IQuery query;
			if ((!String.IsNullOrWhiteSpace(NameCombobox.Text)) && (!String.IsNullOrEmpty(NameCombobox.Text))) //Fetch data of password given the name of the password.
			{
				query = new Query(decPath);
				var keyToSearch = map.FirstOrDefault(x => x.Value == NameCombobox.Text).Key;

				String[] fullResults = query.GetPassword(keyToSearch);

				//Set data in textboxes
				URLTextbox.Text = crypt.DecryptText(key: Key, src: fullResults[0]);
				NameTextbox.Text = crypt.DecryptText(key: Key, src: fullResults[1]);
				UsernameTextbox.Text = crypt.DecryptText(key: Key, src: fullResults[2]);
				PasswordTextbox.Text = crypt.DecryptText(key: Key, src: fullResults[3]);
				CategoryTextbox.Text = crypt.DecryptText(key: Key, src: fullResults[4]);
				NotesTextbox.Text = crypt.DecryptText(key: Key, src: fullResults[5]);
				if (Convert.ToBoolean(Int32.Parse(crypt.DecryptText(key: Key, src: fullResults[6])))) { ImportantCheckbox.Checked = true; }
				else { ImportantCheckbox.Checked = false; }

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
				ImportantCheckbox.Checked = false;

			}
		}

		/// <summary>
		/// Shows or hides plaintext password....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PassVisibilityButton_Click(object sender, EventArgs e)
		{
			if (PasswordTextbox.UseSystemPasswordChar) //Password is hidden, show it
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

		/// <summary>
		/// Changes components theme accordingly to the new set theme
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteContent_BackColorChanged(object sender, EventArgs e)
		{
			if (this.BackColor == Color.FromArgb(230, 230, 230))
			{
				NameCombobox.BackColor = SystemColors.Window;
				URLTextbox.BackColor = SystemColors.Window;
				NameTextbox.BackColor = SystemColors.Window;
				UsernameTextbox.BackColor = SystemColors.Window;
				PasswordTextbox.BackColor = SystemColors.Window;
				CategoryTextbox.BackColor = SystemColors.Window;
				NotesTextbox.BackColor = SystemColors.Window;

			}
			else
			{
				NameCombobox.BackColor = Color.FromArgb(128, 130, 129);
				URLTextbox.BackColor = Color.FromArgb(152, 154, 153);
				NameTextbox.BackColor = Color.FromArgb(152, 154, 153);
				UsernameTextbox.BackColor = Color.FromArgb(152, 154, 153);
				PasswordTextbox.BackColor = Color.FromArgb(152, 154, 153);
				CategoryTextbox.BackColor = Color.FromArgb(152, 154, 153);
				NotesTextbox.BackColor = Color.FromArgb(152, 154, 153);

			}
		}

		//Mouse enters button underlines button text.
		[SupportedOSPlatform("windows")]
		private void DeleteButton_MouseEnter(object sender, EventArgs e)
		{
			DeleteButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline);
		}

		//Mouse leaves button regularises button text.
		[SupportedOSPlatform("windows")]
		private void DeleteButton_MouseLeave(object sender, EventArgs e)
		{
			DeleteButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
		}

		//Mouse enters button underlines button text.
		[SupportedOSPlatform("windows")]
		private void DeleteAllButton_MouseEnter(object sender, EventArgs e)
		{
			DeleteAllButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline);
		}

		//Mouse leaves button regularises button text.
		[SupportedOSPlatform("windows")]
		private void DeleteAllButton_MouseLeave(object sender, EventArgs e)
		{
			DeleteAllButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
		}
	}
}
