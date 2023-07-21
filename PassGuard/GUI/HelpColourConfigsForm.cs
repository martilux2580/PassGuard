using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	public partial class HelpColourConfigsForm : Form
	{
		public HelpColourConfigsForm()
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

		private void HelpColourConfigsForm_BackColorChanged(object sender, EventArgs e)
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
	}
}
