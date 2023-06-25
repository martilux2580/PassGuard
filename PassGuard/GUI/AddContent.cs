﻿using PassGuard.Crypto;
using PassGuard.VaultQueries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	//Form that saves the data of a new entry in the Vault.
	public partial class AddContent : Form
	{
		//Attributes to save the new data.
		public String url {get; private set;}
		public String name { get; private set; }
		public String username { get; private set; }
		public String password { get; private set; }
		public String category { get; private set; }
		public String notes { get; private set; }
		public String important { get; private set; }
		private List<String> namesInDB; //List of names already in Vault.
		private List<String> categories; 
		public bool addedSuccess { get; private set; } //Bool for checking that the closing of the form was due to the button click, not from AltF4 or other methods.
		private readonly byte[] Key; //Key 
		private ICrypt crypt = new AESAlgorithm();

		public AddContent(List<String> names, byte[] key, List<String> rawCategories)
		{
			InitializeComponent();

			Key = key;
			namesInDB = names;
			for (int i = 0; i < namesInDB.Count; i++) //Decrypt names
			{
				namesInDB[i] = crypt.DecryptText(key: Key, src: namesInDB[i]);
			}
			addedSuccess = false;
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

		private void LoadCategoryCombobox(List<String> categorias)
		{
			var rawCategories = new List<String>();
			for (int i = 0; i < categorias.Count; i++) 
			{
				rawCategories.Add(crypt.DecryptText(key: Key, src: categorias[i]));
			}

			categories = new HashSet<String>(rawCategories).ToList<String>(); //Remove dups

			CategoryCombobox.Items.Add("");
			foreach (String category in categories) 
			{
				CategoryCombobox.Items.Add(category);
			}
		}

		[SupportedOSPlatform("windows")]
		private void AddButton_MouseEnter(object sender, EventArgs e)
		{
			AddButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void AddButton_MouseLeave(object sender, EventArgs e)
		{
			AddButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Underline the text when mouse is in the button
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			String errorMessages = "";

			if (String.IsNullOrWhiteSpace(NameTextbox.Text) || String.IsNullOrWhiteSpace(UsernameTextbox.Text) || String.IsNullOrWhiteSpace(PasswordTextbox.Text))
			{
				errorMessages += "Non-optional fields (Name, Username, Password) cannot be left blank.";
			}
  
			if (namesInDB.Contains(NameTextbox.Text))
			{
				errorMessages += "There is already a saved password with that name in the vault.";
			}

			if (!String.IsNullOrEmpty(errorMessages)) //If any error...
			{
				MessageBox.Show(text: "The following errors have been found:\n\n" + errorMessages, caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
			}
			else //No error in params, set params.
			{
				url = crypt.EncryptText(key: Key, src: URLTextbox.Text);
				name = crypt.EncryptText(key: Key, src: NameTextbox.Text);
				username = crypt.EncryptText(key: Key, src: UsernameTextbox.Text);
				password = crypt.EncryptText(key: Key, src: PasswordTextbox.Text);
				category = crypt.EncryptText(key: Key, src: CategoryCombobox.Text);
				notes = crypt.EncryptText(key: Key, src: NotesTextbox.Text);
				if(ImportantCheckbox.Checked) { important = crypt.EncryptText(key: Key, src: "1"); }
				else { important = crypt.EncryptText(key: Key, src: "0"); }

				addedSuccess = true; //Everything went correct, send this signal to update correctly the table.

				this.Close();

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
	}
}
