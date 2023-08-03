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
	/// Shows text with info about how do we know if a password has been pwned....
	/// </summary>
	public partial class InfoPwnageForm : Form
	{
		public InfoPwnageForm()
		{
			InitializeComponent();
			this.Icon = Properties.Resources.LogoIcon64123; //Loads Icon from Image folder.

		}

		/// <summary>
		/// Closes the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UnderstoodButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Changes backcolor of components if theme changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InfoPwnageForm_BackColorChanged(object sender, EventArgs e)
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
		/// Opens webbrowser with the link clicked in the richtextbox....otherwise copies the link to clipboard...
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
	}
}
