using PassGuard.Crypto;
using PassGuard.VaultQueries;
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
	public partial class VaultStats : Form
	{
		private readonly byte[] Key; //Content
		private ICrypt crypt = new AESAlgorithm();
		private IQuery query;
		private readonly List<string[]> allData = new();
		private int[] contextColour = new int[3] { 0, 191, 144 }; //Default colour

		public VaultStats(byte[] cKey, List<string[]> Data, int[] ContextColour)
		{
			InitializeComponent();
			try
			{
				this.Icon = Properties.Resources.LogoIcon64123; //Loads Icon from Image folder.
				Key = cKey;
				allData = Data;
				contextColour = ContextColour;

			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}


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

		private void SearchButton_Click(object sender, EventArgs e)
		{
			switch (StatTypeCombobox.Text)
			{
				case "Content Properties":
					var someData = allData.Select(arr => new string[] { arr[1], arr[3], arr[6] }).ToList(); //Get just Name, Pass and Importance from all data.
					var someDataDecrypted = someData.Select(arr => arr.Select(x => crypt.DecryptText(Key, x)).ToArray()).ToList();

					StatsPanel.Controls.Clear();
					GUI.ContentStatsUC stat = new(someDataDecrypted, contextColour);
					StatsPanel.Controls.Add(stat);

					break;
				case "Security Properties":
					var someSecData = allData.Select(arr => arr[3]).ToList(); //Get just Name, Pass and Importance from all data.
					var someSecDataDecrypted = someSecData.Select(str => crypt.DecryptText(Key, str)).ToList();

					StatsPanel.Controls.Clear();
					GUI.SecurityStatsUC stat1 = new(someSecDataDecrypted, contextColour);
					StatsPanel.Controls.Add(stat1);
					break;
				default:
					break;
			}

			ResetButton.Enabled = true;
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			StatsPanel.Controls.Clear();
			StatTypeCombobox.Text = "";

			ResetButton.Enabled = false;
		}

		[SupportedOSPlatform("windows")]
		private void ResetButton_MouseEnter(object sender, EventArgs e)
		{
			ResetButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline);
		}

		[SupportedOSPlatform("windows")]
		private void ResetButton_MouseLeave(object sender, EventArgs e)
		{
			ResetButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
		}

		[SupportedOSPlatform("windows")]
		private void SearchButton_MouseEnter(object sender, EventArgs e)
		{
			SearchButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline);
		}

		[SupportedOSPlatform("windows")]
		private void SearchButton_MouseLeave(object sender, EventArgs e)
		{
			SearchButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
		}

		private void StatTypeCombobox_TextChanged(object sender, EventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(StatTypeCombobox.Text))
			{
				SearchButton.Enabled = true;
			}
			else { SearchButton.Enabled = false; }
		}
	}
}
