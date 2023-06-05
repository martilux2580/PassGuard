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
	public partial class AddColorConfig : Form
	{

		public string name { get; private set; }
		public int red { get; private set; }
		public int green { get; private set; }
		public int blue { get; private set; }
		public int chosen { get; private set; }
		public int favourite { get; private set; }
		private Dictionary<String, List<int>> storedConfigs;
		public bool addedSuccess { get; private set; }

		public AddColorConfig(Dictionary<String, List<int>> configs)
		{
			InitializeComponent();

			this.Icon = Properties.Resources.LogoIcon64123; //Loads Icon from Image folder.

			storedConfigs = configs;
			addedSuccess = false;
		}

		private void SendButton_Click(object sender, EventArgs e)
		{
			string errorMessages = "";

			if(String.IsNullOrWhiteSpace(NameTextbox.Text))
			{
				errorMessages += "Name cannot be left blank.";
			}
			if (storedConfigs.ContainsKey(NameTextbox.Text))
			{
				errorMessages += "\nThere is already a saved config with that name.";
			}
			if (!Utils.BooleanUtils.IsValidColour((int)RedNUD.Value, (int)GreenNUD.Value, (int)BlueNUD.Value))
			{
				errorMessages += "\nSelected RGB configuration is too dark or bright, texts or images might not be visible with this config.";
			}
			var colorConfig = new List<int> { (int)RedNUD.Value, (int)GreenNUD.Value, (int)BlueNUD.Value };
			foreach(List<int> savedConfig in storedConfigs.Values)
			{
				if ((savedConfig[0] == colorConfig[0]) && (savedConfig[1] == colorConfig[1]) && (savedConfig[2] == colorConfig[2])) //.Contains() does not work.
				{
					errorMessages += "\nThere is already a saved config with that RGB configuration.";
				}
			}
			

			if (!String.IsNullOrEmpty(errorMessages)) //If any error...
			{
				MessageBox.Show(text: "The following errors have been found:\n\n" + errorMessages, caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
			}
			else //No error in params, set params.
			{
				name = NameTextbox.Text;
				red = (int)RedNUD.Value;
				green = (int)GreenNUD.Value;
				blue = (int)BlueNUD.Value;
				if (ChosenConfigCheckbox.Checked) { chosen = 1; }
				else { chosen = 0; }
				if (FavouriteCheckbox.Checked) { favourite = 1; }
				else { favourite = 0; } 

				addedSuccess = true; //Everything went correct, send this signal to update correctly the table.

				this.Close();
			}


		}
	}
}
