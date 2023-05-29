using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	//Form to obtain new RGB values for the outline colours.
	public partial class AskRGBforSettings : Form
	{
		public Configuration config { get; private set; }
		public int finalRed { get; private set; }
		public int finalGreen { get; private set; }
		public int finalBlue { get; private set; }

		public AskRGBforSettings(int[] colours, Configuration configg)
		{
			InitializeComponent();
			SetNUDs(colours);

			finalRed = (int)RedNUD.Value;
			finalGreen = (int)GreenNUD.Value;
			finalBlue = (int)BlueNUD.Value;

			LoadContent(ConfigurationManager.AppSettings.Get("OutlineSavedColours"));
			config = configg;

		}

		private DataGridViewRow GenerateNewRow(KeyValuePair<String, List<int>> configPair)
		{
			DataGridViewRow row = new();
			DataGridViewCell nameCell = new DataGridViewTextBoxCell
			{
				Value = configPair.Key
			};
			nameCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			DataGridViewCell redCell = new DataGridViewTextBoxCell
			{
				Value = configPair.Value[0].ToString()
			};
			redCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			DataGridViewCell greenCell = new DataGridViewTextBoxCell
			{
				Value = configPair.Value[1].ToString()
			};
			greenCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			DataGridViewCell blueCell = new DataGridViewTextBoxCell
			{
				Value = configPair.Value[2].ToString()
			};
			blueCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			DataGridViewCell viewerCell = new DataGridViewTextBoxCell
			{
				Value = ""
			};
			viewerCell.Style.BackColor = Color.FromArgb(red: configPair.Value[0], green: configPair.Value[1], blue: configPair.Value[2]);

			//Chosen

			//Favourite

			//Pensar en añadir datos al appconfig...

			return row;
		}

		internal void LoadContent(String configs)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(configs);

			ColourContentDGV.Controls.Clear();
			foreach (KeyValuePair<String, List<int>> configColor in values)
			{
				ColourContentDGV.Rows.Add(GenerateNewRow(configColor));
			}
		}

		private void SetNUDs(int[] colours) //Set NumericUpDowns to the colours set right now in the Content Panel of Form1
		{
			RedNUD.Value = colours[0]; //Modify data in the config file for future executions.
			GreenNUD.Value = colours[1]; //Modify data in the config file for future executions.
			BlueNUD.Value = colours[2]; //Modify data in the config file for future executions.
		}

		private void AskRGBforSettings_Load(object sender, EventArgs e)
		{
			try
			{
				this.Icon = Properties.Resources.LogoIcon64123; //Loads Icon from Image folder.
			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not load some images.", caption: "Images not found.", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		private void WebHelpRGB_Click(object sender, EventArgs e)
		{
			string url = "https://htmlcolorcodes.com/es";
			try
			{
				System.Diagnostics.Process.Start(url); //Open browser with webpage.
			}
			catch (Exception)
			{
				Clipboard.SetText(url);
				MessageBox.Show(text: "ERROR: The following webpage: \n\n" + url + "\n\ncould not be opened. However, the link has been copied to your clipboard. You can paste it in your favourite web browser :)", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		private void SendButton_Click(object sender, EventArgs e)
		{
			//Set these variables, because we cannot get the NUD values if he have this.Close() the form.
			finalRed = (int)RedNUD.Value;
			finalGreen = (int)GreenNUD.Value;
			finalBlue = (int)BlueNUD.Value;

			this.Close();
		}

		private void ConfigNameButton_Click(object sender, EventArgs e)
		{
			//NameCMS.Show(ConfigNameButton, new Point(ConfigNameButton.Width - ConfigNameButton.Width, ConfigNameButton.Height)); //Sets where to display the ContextMenuStrip...
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			GUI.AddColorConfig add = new(values)
			{
				BackColor = this.BackColor
			};
			add.ShowDialog();

			if (add.addedSuccess)
			{
				String newName = add.name;
				int newRed = add.red;
				int newGreen = add.green;
				int newBlue = add.blue;
				int newFav = add.favourite;

				var data = ConfigurationManager.AppSettings["OutlineSavedColours"];
				var newData = data.Insert(data.Length - 1, ",\"" + newName  + "\":[" + newRed.ToString() + ", " + newGreen.ToString() + ", " + newBlue.ToString() + "," + newFav.ToString() + "]");

				config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				//ContentFlowLayoutPanel.Controls.Clear();
				LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

				//ContentFlowLayoutPanel.Refresh();

			}

		}

		private void EditButton_Click(object sender, EventArgs e)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			var namesList = new List<String>(values.Keys);

			GUI.EditColorConfig edit = new(namesList)
			{
				BackColor = this.BackColor
			};
			edit.ShowDialog();

			if(edit.editedSuccess)
			{
				String oldName = edit.oldname;
				String newName = edit.name;
				int red = edit.red;
				int green = edit.green;
				int blue = edit.blue;
				int important = edit.important;

				values.Remove(oldName);
				values.Add(newName, new List<int> { red, green, blue , important});

				String newData = JsonSerializer.Serialize(values);

				config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				//ContentFlowLayoutPanel.Controls.Clear();
				LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

				//ContentFlowLayoutPanel.Refresh();
			}

		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			var namesList = new List<String>(values.Keys);

			GUI.DeleteColorConfig del = new(namesList)
			{
				BackColor = this.BackColor,
				Enabled = true
			};
			del.ShowDialog();

			if (del.deletedSuccess)
			{
				values.Remove(del.name);
				if(values.Count < 1) { values.Add("Default", new List<int> { 0, 191, 144, 1 }); }

				String newData = JsonSerializer.Serialize(values);

				config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				//ContentFlowLayoutPanel.Controls.Clear();
				LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

				//ContentFlowLayoutPanel.Refresh();
			}
			else if (del.deletedAllSuccess)
			{
				values.Clear();
				values.Add("Default", new List<int> { 0, 191, 144, 1 });

				String newData = JsonSerializer.Serialize(values);

				config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				//ContentFlowLayoutPanel.Controls.Clear();
				LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			}


		}

		private void normalOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			normalOrderToolStripMenuItem.Checked = true;
			ascendingOrderToolStripMenuItem.Checked = false;
			descendingOrderToolStripMenuItem.Checked = false;

			LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

		}

		private void ascendingOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			normalOrderToolStripMenuItem.Checked = false;
			ascendingOrderToolStripMenuItem.Checked = true;
			descendingOrderToolStripMenuItem.Checked = false;

			var values = new SortedDictionary<String, List<int>>(JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]));

			LoadContent(JsonSerializer.Serialize(values));
			
		}

		private void descendingOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			normalOrderToolStripMenuItem.Checked = false;
			ascendingOrderToolStripMenuItem.Checked = false;
			descendingOrderToolStripMenuItem.Checked = true;

			var values = new SortedDictionary<String, List<int>>(JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]));
			var reversed = values.Reverse();
			var newValues = reversed.ToDictionary(x => x.Key, x => x.Value);

			LoadContent(JsonSerializer.Serialize(newValues));

		}
	}
}
