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
	public partial class HelpVaultForm : Form
	{
		public HelpVaultForm()
		{
			InitializeComponent();

			this.Icon = Properties.Resources.LogoIcon64123; //Loads Icon from Image folder.
		}

		private void UnderstoodButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		[SupportedOSPlatform("windows")]
		private void UnderstoodButton_MouseEnter(object sender, EventArgs e)
		{
			UnderstoodButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); 
		}

		[SupportedOSPlatform("windows")]
		private void UnderstoodButton_MouseLeave(object sender, EventArgs e)
		{
			UnderstoodButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
		}

		private void HelpVaultForm_BackColorChanged(object sender, EventArgs e)
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
