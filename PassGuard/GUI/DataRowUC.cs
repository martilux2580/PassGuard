﻿using PassGuard.Crypto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	//UserControl Component that represents a Password in the Vault, it is composed of 6 buttons whose text is the data of the password (Url, Name, Username, SitePassword, Category, Notes)
	public partial class DataRowUC : UserControl
	{
		private String url;
		private String name;
		private String username;
		private String password;
		private String category;
		private String notes;
		private String important;
		private ICrypt crypt = new AESAlgorithm();

		private readonly byte[] Key;

		public DataRowUC(List<String> values, byte[] key)
		{
			InitializeComponent();
			Key = key;
			//Set the encrypted values to the attributes
			url = values[0];
			name = values[1];
			username = values[2];
			password = values[3];
			category = values[4];
			notes = values[5];
			important = values[6];

		}

		private void DataRowUC_Load(object sender, EventArgs e)
		{
			//Decrypt the values and set them as text of the buttons
			URLContent.Text = crypt.DecryptText(key: Key, src: url);
			NameContent.Text = crypt.DecryptText(key: Key, src: name);
			UsernameContent.Text = crypt.DecryptText(key: Key, src: username);
			PassContent.Text = String.Concat(Enumerable.Repeat("*", 15)); //Hide the password
			CategoryContent.Text = crypt.DecryptText(key: Key, src: category);
			NotesContent.Text = crypt.DecryptText(key: Key, src: notes);

			if(Convert.ToBoolean(Int32.Parse(crypt.DecryptText(key: Key, src: important))))
			{
				ImportantContent.Image = Properties.Resources.CheckIconBig;
			}
			else
			{
				ImportantContent.Image = null;
			}
		}

		private void PassContent_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(crypt.DecryptText(key: Key, src: password)); //If click on password button, copy in clipboard the decryption of the attribute, not the 15x"*"
			
		}

		private void URLContent_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrWhiteSpace(URLContent.Text)) Clipboard.SetText(" "); //If empty, set clipboard as " "
			else Clipboard.SetText(URLContent.Text);
		}

		private void NameContent_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(NameContent.Text); //Cannot be empty, so directly copy text to clipboard.
		}

		private void UsernameContent_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(UsernameContent.Text);
		}

		private void CategoryContent_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrWhiteSpace(CategoryContent.Text)) Clipboard.SetText(" ");
			else Clipboard.SetText(CategoryContent.Text);
		}

		private void NotesContent_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrWhiteSpace(NotesContent.Text)) Clipboard.SetText(" ");
			else Clipboard.SetText(NotesContent.Text);
		}

		private void ImportantContent_Click(object sender, EventArgs e)
		{
			if (ImportantContent.Image == null)
			{
				Clipboard.SetText("Not Important");
			}
			else
			{
				Clipboard.SetText("Important");
			}
		}
	}
}
