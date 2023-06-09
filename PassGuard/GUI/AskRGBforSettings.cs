using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iText.Svg.SvgConstants;

namespace PassGuard.GUI
{
	//Form to obtain new RGB values for the outline colours.
	public partial class AskRGBforSettings : Form
	{
		public Configuration config { get; private set; }
		public int[] finalCalibratedColours { get; private set; }

		public AskRGBforSettings(int[] colours, Configuration configg)
		{
			InitializeComponent();
			SetNUDs(colours);

			//Have the actualColours values in the NUDs. Could happen that none of saved configs are the one actually used...
			colours[0] = (int)RedNUD.Value;
			colours[1] = (int)GreenNUD.Value;
			colours[2] = (int)BlueNUD.Value;

			//ORDER: RMenu, GMenu, BMenu, RLogo, GLogo, BLogo, ROptic, GOptic, BOptic
			finalCalibratedColours = Utils.IntUtils.CalibrateAllColours(colours[0], colours[1], colours[2]);

			LoadContent(ConfigurationManager.AppSettings.Get("OutlineSavedColours"));
			config = configg;

		}

		private DataGridViewRow GenerateNewRow(KeyValuePair<String, List<int>> configPair)
		{
			DataGridViewRow row = new();
			DataGridViewCell chosenNameCell = new DataGridViewButtonCell
			{
				Value = configPair.Key,
				FlatStyle = FlatStyle.Flat
			};
			chosenNameCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			DataGridViewCell redCell = new DataGridViewButtonCell
			{
				Value = configPair.Value[0].ToString(),
				FlatStyle = FlatStyle.Flat
			};
			redCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			DataGridViewCell greenCell = new DataGridViewButtonCell
			{
				Value = configPair.Value[1].ToString(),
				FlatStyle = FlatStyle.Flat
			};
			greenCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			DataGridViewCell blueCell = new DataGridViewButtonCell
			{
				Value = configPair.Value[2].ToString(),
				FlatStyle = FlatStyle.Flat
			};
			blueCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			DataGridViewCell viewerCell = new DataGridViewButtonCell
			{
				Value = "    ",
				FlatStyle = FlatStyle.Flat
			};
			viewerCell.Style.BackColor = Color.FromArgb(red: configPair.Value[0], green: configPair.Value[1], blue: configPair.Value[2]);
			viewerCell.Style.SelectionBackColor = Color.FromArgb(red: configPair.Value[0], green: configPair.Value[1], blue: configPair.Value[2]);

			//ChosenConfig
			DataGridViewCell chosenConfigCell = new DataGridViewCheckBoxCell
			{
				Selected = false
			};
			if (Convert.ToBoolean(configPair.Value[3])) { chosenConfigCell.Value = 1; }
			else { chosenConfigCell.Value = 0; } //If decrypts to "1" it is chosen and used actually, else is not.

			//Favourite
			DataGridViewCell favouriteCell = new DataGridViewCheckBoxCell
			{
				Selected = false
			};
			if (Convert.ToBoolean(configPair.Value[4])) { favouriteCell.Value = 1; }
			else { favouriteCell.Value = 0; } //If decrypts to "1" it is important, else is not.

			row.Cells.Add(chosenNameCell);
			row.Cells.Add(redCell);
			row.Cells.Add(greenCell);
			row.Cells.Add(blueCell);
			row.Cells.Add(viewerCell);
			row.Cells.Add(chosenConfigCell);
			row.Cells.Add(favouriteCell);

			return row;
		}

		internal void LoadContent(String configs)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(configs);

			ColourContentDGV.Rows.Clear();
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
			if (Utils.BooleanUtils.IsValidColour((int)RedNUD.Value, (int)GreenNUD.Value, (int)BlueNUD.Value)) //Check lightness of colour to check if it is valid. Double Check.
			{
				//Calibrate colours and set the result variable, because we cannot get the NUD values if he have this.Close() the form.
				//ORDER: RMenu, GMenu, BMenu, RLogo, GLogo, BLogo, ROptic, GOptic, BOptic
				finalCalibratedColours = Utils.IntUtils.CalibrateAllColours((int)RedNUD.Value, (int)GreenNUD.Value, (int)BlueNUD.Value);

				this.Close();
			}
			else
			{
				MessageBox.Show(text: "ERROR: We have detected that the colour you want to use is too dark. \n\nThis might lead to inaccuracies while reading the content, please select a lighter colour to continue :)", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		private void ConfigNameButton_Click(object sender, EventArgs e)
		{
			//NameCMS.Show(ConfigNameButton, new Point(ConfigNameButton.Width - ConfigNameButton.Width, ConfigNameButton.Height)); //Sets where to display the ContextMenuStrip...
		}

		private Dictionary<String, List<int>> UncheckChosenConfigs(Dictionary<String, List<int>> values)
		{
			var configs = values;
			foreach(List<int> data in configs.Values)
			{
				data[3] = 0; //Uncheck the value of chosen in the data....
			}
			return configs;
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
				var data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

				if (add.chosen == 1)
				{
					data = UncheckChosenConfigs(data);
					RedNUD.Value = add.red;
					GreenNUD.Value = add.green;
					BlueNUD.Value = add.blue;
				}

				data.Add(key: add.name, value: new List<int> { add.red, add.green, add.blue, add.chosen, add.favourite });
				string newData = JsonSerializer.Serialize(data);

				config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				ColourContentDGV.Rows.Clear();
				LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			}

		}

		private void EditButton_Click(object sender, EventArgs e)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			GUI.EditColorConfig edit = new(values)
			{
				BackColor = this.BackColor
			};
			edit.ShowDialog();

			if (edit.editedSuccess)
			{
				String oldName = edit.oldname;
				String newName = edit.name;
				int red = edit.red;
				int green = edit.green;
				int blue = edit.blue;
				int chosen = edit.chosen;
				int important = edit.important;

				values.Remove(oldName);

				if (chosen == 1) //If chosen, then set values and unset others...
				{
					values = UncheckChosenConfigs(values);
					RedNUD.Value = edit.red;
					GreenNUD.Value = edit.green;
					BlueNUD.Value = edit.blue;
				}
				values.Add(newName, new List<int> { red, green, blue , chosen, important});
				String newData = JsonSerializer.Serialize(values);

				config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				ColourContentDGV.Rows.Clear();
				LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

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

				ColourContentDGV.Rows.Clear();
				LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			}
			else if (del.deletedAllSuccess)
			{
				values.Clear();
				values.Add("Default", new List<int> { 0, 191, 144, 1 });

				String newData = JsonSerializer.Serialize(values);

				config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				ColourContentDGV.Rows.Clear();
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

		private void AskRGBforSettings_BackColorChanged(object sender, EventArgs e)
		{
			// Set the back color of the DataGridView
			ColourContentDGV.BackgroundColor = this.BackColor;

			// Set the back color of the column headers
			ColourContentDGV.ColumnHeadersDefaultCellStyle.BackColor = this.BackColor;

			// Set the back color of the rows
			ColourContentDGV.RowsDefaultCellStyle.BackColor = this.BackColor;

			// Set the back color of the selection
			ColourContentDGV.DefaultCellStyle.SelectionBackColor = this.BackColor;
		}

		private void ColourContentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

			DataGridViewColumn clickedColumn = ColourContentDGV.Columns[e.ColumnIndex];
			string columnName = clickedColumn.HeaderText;

			// Show the context menu strip at the mouse location 
			switch (columnName)
			{
				case "ConfigName":
				case "Red":
				case "Green":
				case "Blue":
					//Todos los de arriba solo copias los valores al clipboard.
					Clipboard.SetText(ColourContentDGV.CurrentCell.Value.ToString());
					break;
				case "ChosenConfig":
					//Deseleccionar todos los chosen y poner esto con mgbox, ademas de guardar en array...
					if((ColourContentDGV.CurrentCell != null) && (!Convert.ToBoolean(ColourContentDGV.CurrentCell.Value))) //If checkbox is not checked then we do something...
					{
						var row = ColourContentDGV.Rows[e.RowIndex];
						var dialog = MessageBox.Show(text: "Do you want to select and use the config with name '" + row.Cells[0].Value.ToString() + "'?", caption: "Select and Use Config", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
						if (dialog == DialogResult.Yes)
						{
							Dictionary<String, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

							data = UncheckChosenConfigs(data);

							//Set NUDs and chosen to 1...
							RedNUD.Value = data[row.Cells[0].Value.ToString()][0];
							GreenNUD.Value = data[row.Cells[0].Value.ToString()][1];
							BlueNUD.Value = data[row.Cells[0].Value.ToString()][2];
							data[row.Cells[0].Value.ToString()][3] = 1; //Set chosen to 1 in the row...

							config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(data); ; //Modify data in the config file for future executions.
							config.Save(ConfigurationSaveMode.Modified);
							ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

							ColourContentDGV.Rows.Clear();
							LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);
						}
						else
						{
							ColourContentDGV.CurrentCell.Value = false; //Uncheck checkbox, just in case...
						}
					}

					break;
				case "Favourite":
					//Seleccionar esto como favourite y poner esto con mgbox, ademas de guardar en array....
					break;
				case "Delete Row":
					//Borrar este dato y la fila y poner esto con mgbox, ademas de guardar en array....
					break;
				default:
					break;
			}


		}

	}
}
