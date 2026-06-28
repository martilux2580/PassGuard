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
	/// Form that allows editing a colour config
	/// </summary>
	public partial class EditColorConfig : Form
	{
		public bool EditedSuccess { get; private set; } //Sets whether user inputted and exited correctly form, or aborted operation
		public string Oldname { get; private set; } //Name of password to be edited
		public string name { get; private set; } //New value for the OldName
		public int Red { get; private set; }
		public int Green { get; private set; }
		public int Blue { get; private set; }
		public int Chosen { get; private set; }
		public int Favourite { get; private set; }
		public int Persists { get; private set; } //Know if config will stay for future executions...
		private readonly Dictionary<String, List<int>> storedConfigs; //Previously stored configurations
		private readonly string actualChosenName; //Name of config that has chosen checkbox checked, config that is being used in the current execution...

		public EditColorConfig(Dictionary<String, List<int>> configs, string ActuallyChosenName)
		{
			InitializeComponent();

			EditedSuccess = false;
			storedConfigs = configs;
			actualChosenName = ActuallyChosenName;

			NameCombobox.Items.Add(""); //Lets user reset combobox text
			foreach (String name in storedConfigs.Keys)
			{
				NameCombobox.Items.Add(name);
			}

			Persists = 0; //Kind of a bool value

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
		/// Removes leading and trailing spaces from text and comboboxes...
		/// </summary>
		public void TrimComponents()
		{
			NameCombobox.Text = NameCombobox.Text.Trim();
			NameTextbox.Text = NameTextbox.Text.Trim();
		}

		private void EditButton_Click(object sender, EventArgs e)
		{
			/** 3 things to check:
			 *	Name changes or not
			 *	RGB values changes or not
			 *	Checkboxes changes or not (either fav or chosen)
			 *	
			 *	The order for the bits is Name, RGB (any of the 3), Checkboxes (any of the 2)...
			 *	
			 *	We will calculate the resulting binary number with the changes ocurred, and handle the case appropiately
			 **/

			TrimComponents();

			//3 bool variables checking if those 3 things changed, if they changed value is 0, not changed is 1
			bool nameEqual = NameTextbox.Text == NameCombobox.Text;

			var colorConfig = new List<int> { (int)RedNUD.Value, (int)GreenNUD.Value, (int)BlueNUD.Value };
			bool rgbEqual = colorConfig.SequenceEqual(new List<int> { storedConfigs[NameCombobox.Text][0], storedConfigs[NameCombobox.Text][1], storedConfigs[NameCombobox.Text][2] });

			bool checksEqual = (((NameCombobox.Text == actualChosenName) && ChosenCheckbox.Checked == true)
				||
				((NameCombobox.Text != actualChosenName) && ChosenCheckbox.Checked == false))
				&& (Convert.ToBoolean(storedConfigs[NameCombobox.Text][3]) == FavouriteCheckbox.Checked);
				

			//Compound the bits and check what changed exactly, so to act for each case....
			string result = Convert.ToInt32(nameEqual).ToString() + Convert.ToInt32(rgbEqual).ToString() + Convert.ToInt32(checksEqual).ToString();
			string errorMessages = "The following errors have been found:\n\n";
			switch (result)
			{
				case "000": //3 values changed
				case "001": //Name and rgb changed
					// Check if new name and rgb arent already saved....
					rgbEqual = false;
					foreach (List<int> savedConfig in storedConfigs.Values)
					{
						if ((savedConfig[0] == colorConfig[0]) && (savedConfig[1] == colorConfig[1]) && (savedConfig[2] == colorConfig[2])) //.Contains() does not work.
						{
							rgbEqual = true; //There is already a saved rgb with those values, reuse variable....
							break;
						}
					}

					if(!rgbEqual && !storedConfigs.ContainsKey(NameTextbox.Text)) //If rgbs arent equal and name isnt already saved....then set values.
					{
						Oldname = NameCombobox.Text;
						name = NameTextbox.Text;
						Red = (int)RedNUD.Value;
						Green = (int)GreenNUD.Value;
						Blue = (int)BlueNUD.Value;
						if (FavouriteCheckbox.Checked) { Favourite = 1; }
						else { Favourite = 0; }
						if (ChosenCheckbox.Checked)
						{
							Chosen = 1;
							DialogResult dialog2 = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
							if (dialog2 == DialogResult.Yes) { Persists = 1; }
							else { Persists = 0; }
						}
						else { Chosen = 0; }

						EditedSuccess = true;
						this.Close();
					}
					else
					{
						MessageBox.Show(text: errorMessages + "\nThere is already a saved config with that name, or with that RGB configuration.", caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
					}
					break;
				case "010": //Name and checkboxes changed
				case "011": //Name changed
					// Just check if new name isnt already saved...
					if (!storedConfigs.ContainsKey(NameTextbox.Text))
					{
						Oldname = NameCombobox.Text;
						name = NameTextbox.Text;
						Red = (int)RedNUD.Value;
						Green = (int)GreenNUD.Value;
						Blue = (int)BlueNUD.Value;
						if (FavouriteCheckbox.Checked) { Favourite = 1; }
						else { Favourite = 0; }
						if (ChosenCheckbox.Checked)
						{
							Chosen = 1;
							DialogResult dialog2 = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
							if (dialog2 == DialogResult.Yes) { Persists = 1; }
							else { Persists = 0; }
						}
						else { Chosen = 0; }

						EditedSuccess = true;
						this.Close();
					}
					else 
					{
						MessageBox.Show(text: errorMessages + "\nThere is already a saved config with that name.", caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
					}
					break;
				case "100": //Rgb and checks changed
				case "101": //Rgb changed
					// Check new rgb isnt already saved...
					foreach (List<int> savedConfig in storedConfigs.Values)
					{
						if ((savedConfig[0] == colorConfig[0]) && (savedConfig[1] == colorConfig[1]) && (savedConfig[2] == colorConfig[2])) //.Contains() does not work.
						{
							rgbEqual = true; //There is already a saved rgb with those values, reuse variable....
							break;
						}
					}
					if (!rgbEqual) //If rgbs arent equal....
					{
						Oldname = NameCombobox.Text;
						name = NameTextbox.Text;
						Red = (int)RedNUD.Value;
						Green = (int)GreenNUD.Value;
						Blue = (int)BlueNUD.Value;
						if (FavouriteCheckbox.Checked) { Favourite = 1; }
						else { Favourite = 0; }
						if (ChosenCheckbox.Checked)
						{
							Chosen = 1;
							DialogResult dialog2 = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
							if (dialog2 == DialogResult.Yes) { Persists = 1; }
							else { Persists = 0; }
						}
						else { Chosen = 0; }

						EditedSuccess = true;
						this.Close();
					}
					else
					{
						MessageBox.Show(text: errorMessages + "\nThere is already a saved config with that RGB configuration.", caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
					}
					break;
				case "110": //Checks changed
					// Just save it, no need to check anything...
					Oldname = NameCombobox.Text;
					name = NameTextbox.Text;
					Red = (int)RedNUD.Value;
					Green = (int)GreenNUD.Value;
					Blue = (int)BlueNUD.Value;
					if (FavouriteCheckbox.Checked) { Favourite = 1; }
					else { Favourite = 0; }
					if (ChosenCheckbox.Checked)
					{
						Chosen = 1;
						DialogResult dialog2 = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
						if (dialog2 == DialogResult.Yes) { Persists = 1; }
						else { Persists = 0; }
					}
					else { Chosen = 0; }

					EditedSuccess = true;
					this.Close();
					break;
				case "111": //Nothing changed
					// Tell user that nothing changed...
					MessageBox.Show(text: errorMessages + "There is an exact same config already saved >:(", caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
					break;
				default:
					// Code for when bits is not any of the above values
					break;
			}
				
		}

		/// <summary>
		/// Enable or disable all components depending on flag...
		/// </summary>
		/// <param name="flag"></param>
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

		/// <summary>
		/// Display data associated to that name of password and enable components....if no name is selected reset components data and disable them...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Set theme of components when theme changes....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditColorConfig_BackColorChanged(object sender, EventArgs e)
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
				NameTextbox.BackColor = Color.FromArgb(128, 130, 129);
				RedNUD.BackColor = Color.FromArgb(128, 130, 129);
				GreenNUD.BackColor = Color.FromArgb(128, 130, 129);
				BlueNUD.BackColor = Color.FromArgb(128, 130, 129);

			}
		}
		
		//Mouse over button underlines button text
		[SupportedOSPlatform("windows")]
		private void EditButton_MouseEnter(object sender, EventArgs e)
		{
			EditButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline);
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void EditButton_MouseLeave(object sender, EventArgs e)
		{
			EditButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
		}
	}
}
