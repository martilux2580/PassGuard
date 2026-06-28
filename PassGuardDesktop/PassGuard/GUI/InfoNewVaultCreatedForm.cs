using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	/// <summary>
	/// Shows info about the new created form and the data user needs to remember...
	/// </summary>
	public partial class InfoNewVaultCreatedForm : Form
	{
		public InfoNewVaultCreatedForm(string data)
		{
			InitializeComponent();
			this.Icon = Properties.Resources.LogoIcon64123; //Loads Icon from Image folder.
			LoadText(data); //Load "dynamic text"
		}

		/// <summary>
		/// Loads the text with the necessary data details...
		/// </summary>
		/// <param name="data"></param>
		private void LoadText(string data)
		{
			var message = "Congrats! Your new Password Vault has been created successfully!\nThe information you must store and remember in order to load and access to your Password Vault is the following: \n\n"
				+ data + "\n\nNotes: \n\t• Without any of those three values, your Password Vault and its content will be inacessible. \n\t• By clicking OK, those three values will be copied to the clipboard, please save them carefully."
				+ "\n\t• Security Key will be remembered by PassGuard, and if the option was checked the email will be also saved. However, if another Password Vault is created, its Security Key will be remembered by PassGuard and the previous key will be deleted, and if the option was checked the email will be remembered and the previously saved email will be deleted, "
				+ "so make sure you keep save and remember the email, password and Security Key of each Password Vault you create.";

			ContentRichTextbox.AppendText(message);
		}

		/// <summary>
		/// Closes the form...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UnderstoodButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		//Mouse over button underlines button text
		[SupportedOSPlatform("windows")]
		private void UnderstoodButton_MouseEnter(object sender, EventArgs e)
		{
			UnderstoodButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline);
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void UnderstoodButton_MouseLeave(object sender, EventArgs e)
		{
			UnderstoodButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
		}

		/// <summary>
		/// Changes components theme if theme is changed...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InfoNewVaultCreatedForm_BackColorChanged(object sender, EventArgs e)
		{
			if (this.BackColor == Color.FromArgb(230, 230, 230))
			{
				ContentRichTextbox.BackColor = SystemColors.Window;

			}
			else
			{
				ContentRichTextbox.BackColor = Color.FromArgb(128, 130, 129);

			}
		}

		/// <summary>
		/// Opens clicked link in textbox in your browser, otherwise copies link to clipboard
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ContentRichTextbox_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			try
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = e.LinkText,
					UseShellExecute = true
				}); ////Open webpage with default browser...
			}
			catch (Exception)
			{

				Clipboard.SetText(!string.IsNullOrEmpty(e.LinkText) ? e.LinkText : " ");
				MessageBox.Show(text: "ERROR: The following webpage: \n\n" + e.LinkText + "\n\ncould not be opened. However, the link has been copied to your clipboard. You can paste it in your favourite web browser :)", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}
	}
}
