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
using static iText.Kernel.Pdf.Colorspace.PdfDeviceCs;
using static Org.BouncyCastle.Math.EC.ECCurve;

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
		public int favourite { get; private set; }
		public int persists { get; private set; }
		private Dictionary<String, List<int>> storedConfigs;
		private string actualChosenName;

		public EditColorConfig(Dictionary<String, List<int>> configs, string ActuallyChosenName)
		{
			InitializeComponent();

			editedSuccess = false;
			storedConfigs = configs;
			actualChosenName = ActuallyChosenName;
			NameCombobox.Items.Add("");

			foreach(String name in storedConfigs.Keys)
			{
				NameCombobox.Items.Add(name);
			}

			persists = 0;

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
			bool nameEqual = NameTextbox.Text == NameCombobox.Text;

			var colorConfig = new List<int> { (int)RedNUD.Value, (int)GreenNUD.Value, (int)BlueNUD.Value };
			bool rgbEqual = colorConfig.SequenceEqual(new List<int> { storedConfigs[NameCombobox.Text][0], storedConfigs[NameCombobox.Text][1], storedConfigs[NameCombobox.Text][2] });

			bool checksEqual = (((NameCombobox.Text == actualChosenName) && ChosenCheckbox.Checked == true)
				||
				((NameCombobox.Text != actualChosenName) && ChosenCheckbox.Checked == false))
				&& (Convert.ToBoolean(storedConfigs[NameCombobox.Text][3]) == FavouriteCheckbox.Checked);
				

			string result = Convert.ToInt32(nameEqual).ToString() + Convert.ToInt32(rgbEqual).ToString() + Convert.ToInt32(checksEqual).ToString();
			string errorMessages = "The following errors have been found:\n\n";
			switch (result)
			{
				case "000":
				case "001":
					// Code for when bits is 000 or 001
					rgbEqual = false;
					foreach (List<int> savedConfig in storedConfigs.Values)
					{
						if ((savedConfig[0] == colorConfig[0]) && (savedConfig[1] == colorConfig[1]) && (savedConfig[2] == colorConfig[2])) //.Contains() does not work.
						{
							//errorMessages += "\nThere is already a saved config with that RGB configuration.";
							rgbEqual = true;
							break;
						}
					}

					if(!rgbEqual && !storedConfigs.ContainsKey(NameTextbox.Text))
					{
						oldname = NameCombobox.Text;
						name = NameTextbox.Text;
						red = (int)RedNUD.Value;
						green = (int)GreenNUD.Value;
						blue = (int)BlueNUD.Value;
						if (FavouriteCheckbox.Checked) { favourite = 1; }
						else { favourite = 0; }
						if (ChosenCheckbox.Checked)
						{
							chosen = 1;
							DialogResult dialog2 = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
							if (dialog2 == DialogResult.Yes) { persists = 1; }
							else { persists = 0; }
						}
						else { chosen = 0; }

						editedSuccess = true;
						this.Close();
					}
					else
					{
						MessageBox.Show(text: errorMessages + "\nThere is already a saved config with that name, or with that RGB configuration.", caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
					}
					break;
				case "010":
				case "011":
					// Code for when bits is 010 or 011
					if (!storedConfigs.ContainsKey(NameTextbox.Text))
					{
						oldname = NameCombobox.Text;
						name = NameTextbox.Text;
						red = (int)RedNUD.Value;
						green = (int)GreenNUD.Value;
						blue = (int)BlueNUD.Value;
						if (FavouriteCheckbox.Checked) { favourite = 1; }
						else { favourite = 0; }
						if (ChosenCheckbox.Checked)
						{
							chosen = 1;
							DialogResult dialog2 = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
							if (dialog2 == DialogResult.Yes) { persists = 1; }
							else { persists = 0; }
						}
						else { chosen = 0; }

						editedSuccess = true;
						this.Close();
					}
					else 
					{
						MessageBox.Show(text: errorMessages + "\nThere is already a saved config with that name.", caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
					}
					break;
				case "100":
				case "101":
					// Code for when bits is 100 or 101
					foreach (List<int> savedConfig in storedConfigs.Values)
					{
						if ((savedConfig[0] == colorConfig[0]) && (savedConfig[1] == colorConfig[1]) && (savedConfig[2] == colorConfig[2])) //.Contains() does not work.
						{
							//errorMessages += "\nThere is already a saved config with that RGB configuration.";
							rgbEqual = true;
							break;
						}
					}
					if (!rgbEqual)
					{
						oldname = NameCombobox.Text;
						name = NameTextbox.Text;
						red = (int)RedNUD.Value;
						green = (int)GreenNUD.Value;
						blue = (int)BlueNUD.Value;
						if (FavouriteCheckbox.Checked) { favourite = 1; }
						else { favourite = 0; }
						if (ChosenCheckbox.Checked)
						{
							chosen = 1;
							DialogResult dialog2 = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
							if (dialog2 == DialogResult.Yes) { persists = 1; }
							else { persists = 0; }
						}
						else { chosen = 0; }

						editedSuccess = true;
						this.Close();
					}
					else
					{
						MessageBox.Show(text: errorMessages + "\nThere is already a saved config with that RGB configuration.", caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
					}
					break;
				case "110":
					// Code for when bits is 110
					oldname = NameCombobox.Text;
					name = NameTextbox.Text;
					red = (int)RedNUD.Value;
					green = (int)GreenNUD.Value;
					blue = (int)BlueNUD.Value;
					if (FavouriteCheckbox.Checked) { favourite = 1; }
					else { favourite = 0; }
					if (ChosenCheckbox.Checked)
					{
						chosen = 1;
						DialogResult dialog2 = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
						if (dialog2 == DialogResult.Yes) { persists = 1; }
						else { persists = 0; }
					}
					else { chosen = 0; }

					editedSuccess = true;
					this.Close();
					break;
				case "111":
					// Code for when bits is 111
					MessageBox.Show(text: errorMessages + "There is an exact same config already saved >:(", caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
					break;
				default:
					// Code for when bits is not any of the above values
					break;
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
				ChosenCheckbox.Enabled = true;
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
				ChosenCheckbox.Enabled = false;
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
				if(NameCombobox.Text == actualChosenName) { ChosenCheckbox.Checked = true; }
				else { ChosenCheckbox.Checked = false; }
				FavouriteCheckbox.Checked = Convert.ToBoolean(storedConfigs[NameCombobox.Text][3]);

				Check(true);

			}
			else
			{
				NameTextbox.Text = "";
				RedNUD.Value = 0;
				GreenNUD.Value = 0;
				BlueNUD.Value = 0;
				FavouriteCheckbox.Checked = false;
				ChosenCheckbox.Checked = false;

				Check(false);
			}
			
		}
	}
}
