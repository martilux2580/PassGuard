using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	public partial class EditColorConfig : Form
	{

		public bool editedSuccess { get; private set; }
		public string oldname { get; private set; }
		public string name { get; private set; }
		public int red { get; private set; }
		public int green { get; private set; }
		public int blue { get; private set; }
		public int chosen { get; private set; }
		public int important { get; private set; }
		private Dictionary<String, List<int>> storedConfigs;

		public EditColorConfig(Dictionary<String, List<int>> configs)
		{
			InitializeComponent();

			editedSuccess = false;
			storedConfigs = configs;
			NameCombobox.Items.Add("");
			foreach(String name in storedConfigs.Keys)
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

		private void EditButton_Click(object sender, EventArgs e)
		{
			String errorMessages = "";

			if (String.IsNullOrWhiteSpace(NameTextbox.Text))
			{
				errorMessages += "\nName cannot be left blank.";
			}
			if (storedConfigs.Keys.Contains(NameTextbox.Text) && (NameTextbox.Text != NameCombobox.Text))
			{
				errorMessages += "\nThere is already a saved config with that name.";
			}
			if (!Utils.BooleanUtils.IsValidColour((int)RedNUD.Value, (int)GreenNUD.Value, (int)BlueNUD.Value))
			{
				errorMessages += "\nSelected RGB configuration is too dark or bright, texts or images might not be visible with this config.";
			}
			//No comprobamos que ya haya otra config rgb igual, ya que imagina que quieres cambiar solo el nombre...no te dejaría...

			if (!String.IsNullOrEmpty(errorMessages)) //If any error...
			{
				MessageBox.Show(text: "The following errors have been found:\n\n" + errorMessages, caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
			}
			else //No error in params, create vault.
			{
				oldname = NameCombobox.Text;
				name = NameTextbox.Text;
				red = (int)RedNUD.Value;
				green = (int)GreenNUD.Value;
				blue = (int)BlueNUD.Value;
				if (ChosenConfigCheckbox.Checked) { chosen = 1; }
				else { chosen = 0; }
				if (FavouriteCheckbox.Checked) { important = 1; }
				else { important = 0; }

				editedSuccess = true;
				this.Close();
			}
				
		}

		private void Check(bool flag)
		{
			if(flag)
			{
				NameLabel.Enabled = true;
				NameTextbox.Enabled = true;
				RedLabel.Enabled = true;
				GreenLabel.Enabled = true;
				BlueLabel.Enabled = true;
				RedNUD.Enabled = true;
				GreenNUD.Enabled = true;
				BlueNUD.Enabled = true;
				EditButton.Enabled = true;
				FavouriteCheckbox.Enabled = true;
				ChosenConfigCheckbox.Enabled = true;
			}
			else
			{
				NameLabel.Enabled = false;
				NameTextbox.Enabled = false;
				RedLabel.Enabled = false;
				GreenLabel.Enabled = false;
				BlueLabel.Enabled = false;
				RedNUD.Enabled = false;
				GreenNUD.Enabled = false;
				BlueNUD.Enabled = false;
				EditButton.Enabled = false;
				FavouriteCheckbox.Enabled = false;
				ChosenConfigCheckbox.Enabled = false;
			}
		}

		private void NameCombobox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(!String.IsNullOrWhiteSpace(NameCombobox.Text))
			{
				NameTextbox.Text = NameCombobox.Text;
				RedNUD.Value = storedConfigs[NameCombobox.Text][0];
				GreenNUD.Value = storedConfigs[NameCombobox.Text][1];
				BlueNUD.Value = storedConfigs[NameCombobox.Text][2];
				ChosenConfigCheckbox.Checked = Convert.ToBoolean(storedConfigs[NameCombobox.Text][3]);
				FavouriteCheckbox.Checked = Convert.ToBoolean(storedConfigs[NameCombobox.Text][4]);

				Check(true);

			}
			else
			{
				NameTextbox.Text = "";
				RedNUD.Value = 0;
				GreenNUD.Value = 0;
				BlueNUD.Value = 0;
				FavouriteCheckbox.Checked = false;
				ChosenConfigCheckbox.Checked = false;

				Check(false);
			}
		}
	}
}
