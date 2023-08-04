using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	/// <summary>
	/// Form to delete a color configuration, or all of them
	/// </summary>
	public partial class DeleteColorConfig : Form
	{

		public bool DeletedSuccess { get; private set; } //Flag if user deletes one config
		public bool DeletedAllSuccess { get; private set; } //Flag if user deletes all
		public string name { get; private set; } //Name of the config to delete
		private readonly List<String> namesStored; //All config names stored
		private readonly string actualChosenName; //Name of the config already in use in this execution....

		public DeleteColorConfig(List<String> names, string ActualChosenName)
		{
			InitializeComponent();

			namesStored = names;
			DeletedAllSuccess = false;
			DeletedSuccess = false;
			actualChosenName = ActualChosenName;

			NameCombobox.Items.Add(""); //Allows user to reset combobox text
			foreach (String name in namesStored)
			{
				NameCombobox.Items.Add(name);
			}

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
		/// Removes leading and trailing whitespaces from textboxes...
		/// </summary>
		public void TrimComponents()
		{
			NameTextbox.Text = NameTextbox.Text.Trim();
			NameCombobox.Text = NameCombobox.Text.Trim();
		}

		/// <summary>
		/// Sets the flag of deleteAll and closes form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteAllButton_Click(object sender, EventArgs e)
		{
			DeletedAllSuccess = true;
			this.Close();
		}

		/// <summary>
		/// Gets the name of the config to remove, sets the corresponding flag and closes form...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteButton_Click(object sender, EventArgs e)
		{
			TrimComponents();

			name = NameCombobox.Text;
			DeletedSuccess = true;
			this.Close();

		}

		/// <summary>
		/// Sets the parameters in the corresponding components on the selected config name..., otherwise reset those components content
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NameCombobox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(NameCombobox.Text))
			{
				Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

				NameTextbox.Text = NameCombobox.Text;
				RedNUD.Value = values[NameCombobox.Text][0];
				GreenNUD.Value = values[NameCombobox.Text][1];
				BlueNUD.Value = values[NameCombobox.Text][2];
				FavouriteCheckbox.Checked = Convert.ToBoolean(values[NameCombobox.Text][3]);
				if (NameCombobox.Text == actualChosenName) { ChosenCheckbox.Checked = true; }
				else { ChosenCheckbox.Checked = false; }
			}
			else
			{
				NameTextbox.Text = "";
				RedNUD.Value = 0;
				GreenNUD.Value = 0;
				BlueNUD.Value = 0;
				FavouriteCheckbox.Checked = false;
				ChosenCheckbox.Checked = false;
			}
		}

		/// <summary>
		/// Enables or disables components whether flag is set to true or false
		/// </summary>
		/// <param name="flag"></param>
		private void Check(bool flag)
		{
			if (flag)
			{
				TitleLabel.Text = "If button is clicked, all Vault contents will be deleted.";
				NameCombobox.Enabled = false;
				NameCombobox.ResetText(); //Set text back to null/unselected

				NameTextbox.Text = null;
				RedNUD.Value = 0;
				GreenNUD.Value = 0;
				BlueNUD.Value = 0;
				NameLabel.Enabled = false;
				NameTextbox.Enabled = false;
				RedLabel.Enabled = false;
				GreenLabel.Enabled = false;
				BlueLabel.Enabled = false;
				RedNUD.Enabled = false;
				GreenNUD.Enabled = false;
				BlueNUD.Enabled = false;
				DeleteButton.Enabled = false;
				FavouriteCheckbox.Enabled = false;
				FavouriteCheckbox.Checked = false;
				ChosenCheckbox.Enabled = false;
				ChosenCheckbox.Checked = false;

				DeleteAllButton.Enabled = true;
			}
			else
			{
				TitleLabel.Text = "Select the name of the password you want to delete: ";
				NameCombobox.Enabled = true;
				NameCombobox.ResetText(); //Set text back to null/unselected

				NameTextbox.Text = null;
				RedNUD.Value = 0;
				GreenNUD.Value = 0;
				BlueNUD.Value = 0;
				NameLabel.Enabled = true;
				RedLabel.Enabled = true;
				GreenLabel.Enabled = true;
				BlueLabel.Enabled = true;
				DeleteButton.Enabled = true;

				DeleteAllButton.Enabled = false;
			}
		}

		/// <summary>
		/// Enables or disables components whether user wants to delete all passwords or just one....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EnableDeleteAllCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			if (EnableDeleteAllCheckbox.Checked == true)
			{
				Check(true);
			}
			else
			{
				Check(false);
			}
		}

		/// <summary>
		/// Changes components theme when general theme is changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteColorConfig_BackColorChanged(object sender, EventArgs e)
		{
			if (this.BackColor == Color.FromArgb(230, 230, 230))
			{
				NameCombobox.BackColor = SystemColors.Window;
				NameTextbox.BackColor = SystemColors.Window;
				RedNUD.BackColor = SystemColors.Window;
				GreenNUD.BackColor = SystemColors.Window;
				BlueNUD.BackColor = SystemColors.Window;

			}
			else
			{
				NameCombobox.BackColor = Color.FromArgb(128, 130, 129);
				NameTextbox.BackColor = Color.FromArgb(152, 154, 153);
				RedNUD.BackColor = Color.FromArgb(152, 154, 153);
				GreenNUD.BackColor = Color.FromArgb(152, 154, 153);
				BlueNUD.BackColor = Color.FromArgb(152, 154, 153);

			}
		}

		//Mouse over button underlines button text
		[SupportedOSPlatform("windows")]
		private void DeleteButton_MouseEnter(object sender, EventArgs e)
		{
			DeleteButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline);
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void DeleteButton_MouseLeave(object sender, EventArgs e)
		{
			DeleteButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
		}

		//Mouse over button underlines button text
		[SupportedOSPlatform("windows")]
		private void DeleteAllButton_MouseEnter(object sender, EventArgs e)
		{
			DeleteAllButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline);
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void DeleteAllButton_MouseLeave(object sender, EventArgs e)
		{
			DeleteAllButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
		}
	}
}
