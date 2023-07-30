using PassGuard.PDF;
using PassGuard.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.Versioning;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	//Form to obtain new RGB values for the outline colours.
	public partial class AskRGBforSettings : Form
	{
		private enum Order //Enum for the order of each column
		{
			Normal,
			Asc,
			Desc
		}
		private enum CFColumns //Enum for the valid names 
		{
			NULLVALUESS,
			Name,
			Red,
			Green,
			Blue,
			Favourite
		}
		public Configuration Config { get; private set; }
		private int[] actualColours;
		public int[] FinalCalibratedColours { get; private set; }
		private CFColumns actualColumn;
		private Order actualOrder;
		private bool isSearched;

		[SupportedOSPlatform("windows")]
		public AskRGBforSettings(int[] colours, Configuration configg)
		{
			InitializeComponent();

			actualColours = colours; //Set actual colours used in this execution
			actualColumn = CFColumns.NULLVALUESS;
			actualOrder = Order.Normal;
			isSearched = false;
			
			try
			{
				SetNUDs();

				SetCMS(); //Set CMSs elements

				//ORDER: RMenu, GMenu, BMenu, RLogo, GLogo, BLogo, ROptic, GOptic, BOptic
				FinalCalibratedColours = Utils.IntUtils.CalibrateAllColours(colours[0], colours[1], colours[2]);

				LoadContent(CFColumns.NULLVALUESS, Order.Normal);
			}
			catch(Exception)
			{
				MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
			
			Config = configg;

		}
		
		public void TrimComponents()
		{
			SearchTextbox.Text = SearchTextbox.Text.Trim();
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
			if (configPair.Value[0] == actualColours[0] //
				&& configPair.Value[1] == actualColours[1]
				&& configPair.Value[2] == actualColours[2])
			{
				chosenConfigCell.Value = 1;
			}

			//Favourite
			DataGridViewCell favouriteCell = new DataGridViewCheckBoxCell
			{
				Selected = false
			};
			if (Convert.ToBoolean(configPair.Value[3])) { favouriteCell.Value = 1; }
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

		private void LoadContent(CFColumns col, Order order)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);
			Dictionary<String, List<int>> valuesToShow = new();

			if (order == Order.Normal) { valuesToShow = values; }
			else if((order == Order.Asc) || (order == Order.Desc))
			{
				switch (col)
				{
					case CFColumns.Name:
						var sortedNames = new SortedDictionary<String, List<int>>(JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"])).ToDictionary(x => x.Key, x => x.Value);
						if(order == Order.Desc)
						{
							var reversedTemp = sortedNames.Reverse();
							sortedNames = reversedTemp.ToDictionary(x => x.Key, x => x.Value);
						}
						valuesToShow = sortedNames;
						break;

					case CFColumns.Red:
						// Sort the dictionary by the fourth element of the list and then by name
						Dictionary<string, List<int>> sortedReds = values.OrderBy(kvp => kvp.Value.ElementAtOrDefault(0))
														.ThenBy(kvp => kvp.Key)
														.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
						if (order == Order.Desc)
						{
							var reversedTemp = sortedReds.Reverse();
							sortedReds = reversedTemp.ToDictionary(x => x.Key, x => x.Value);
						}
						valuesToShow = sortedReds;
						break;
					case CFColumns.Green:
						// Sort the dictionary by the fourth element of the list and then by name
						Dictionary<string, List<int>> sortedGreens = values.OrderBy(kvp => kvp.Value.ElementAtOrDefault(1))
														.ThenBy(kvp => kvp.Key)
														.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

						if (order == Order.Desc)
						{
							var reversedTemp = sortedGreens.Reverse();
							sortedGreens = reversedTemp.ToDictionary(x => x.Key, x => x.Value);
						}
						valuesToShow = sortedGreens;
						break;
					case CFColumns.Blue:
						// Sort the dictionary by the fourth element of the list and then by name
						Dictionary<string, List<int>> sortedBlues = values.OrderBy(kvp => kvp.Value.ElementAtOrDefault(2))
														.ThenBy(kvp => kvp.Key)
														.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

						if (order == Order.Desc)
						{
							var reversedTemp = sortedBlues.Reverse();
							sortedBlues = reversedTemp.ToDictionary(x => x.Key, x => x.Value);
						}
						valuesToShow = sortedBlues;
						break;
					case CFColumns.Favourite:
						// Sort the dictionary by the fourth element of the list and then by name
						//Sustituye los 1 del Favourite por cero para así ordenar ascendente y que los Favourite queden arriba, ya que pasan a ser 0 en vez de 1.
						Dictionary<string, List<int>> sortedFavs = values.OrderBy(kv => kv.Value[3] == 1 ? 0 : 1).ThenBy(kv => kv.Value[3]).ThenBy(kv => kv.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
						if (order == Order.Desc)
						{
							sortedFavs = values.OrderBy(kv => kv.Value[3] == 1 ? 0 : 1).ThenByDescending(kv => kv.Value[3]).ThenByDescending(kv => kv.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
						}
						valuesToShow = sortedFavs;
						break;
					default:
						break;
				}
			}

			ColourContentDGV.Rows.Clear();
			foreach (KeyValuePair<String, List<int>> configColor in valuesToShow)
			{
				ColourContentDGV.Rows.Add(GenerateNewRow(configColor));

			}

		}

		private void SetNUDs() //Set NumericUpDowns to the colours set right now in the Content Panel of Form1
		{
			RedNextNUD.Value = int.Parse(ConfigurationManager.AppSettings["RedLogo"]); //Modify data in the config file for future executions.
			GreenNextNUD.Value = int.Parse(ConfigurationManager.AppSettings["GreenLogo"]); //Modify data in the config file for future executions.
			BlueNextNUD.Value = int.Parse(ConfigurationManager.AppSettings["BlueLogo"]); //Modify data in the config file for future executions.

			RedNowNUD.Value = actualColours[0]; //Modify data in the config file for future executions.
			GreenNowNUD.Value = actualColours[1]; //Modify data in the config file for future executions.
			BlueNowNUD.Value = actualColours[2]; //Modify data in the config file for future executions.
		}

		private void ResetToNormalOrdering(bool actualSelects)
		{
			if (actualSelects)
			{
				actualOrder = Order.Normal;
				actualColumn = CFColumns.NULLVALUESS;
			}

			NameNormalCMS.Checked = true;
			NameAscendingCMS.Checked = false;
			NameDescendingCMS.Checked = false;

			RedNormalCMS.Checked = true;
			RedAscendingCMS.Checked = false;
			RedDescendingCMS.Checked = false;

			GreenNormalCMS.Checked = true;
			GreenAscendingCMS.Checked = false;
			GreenDescendingCMS.Checked = false;

			BlueNormalCMS.Checked = true;
			BlueAscendingCMS.Checked = false;
			BlueDescendingCMS.Checked = false;

			FavouriteNormalCMS.Checked = true;
			FavouriteAscendingCMS.Checked = false;
			FavouriteDescendingCMS.Checked = false;
		}

		private void UncheckOrdering()
		{
			NameNormalCMS.Checked = false;
			NameAscendingCMS.Checked = false;
			NameDescendingCMS.Checked = false;

			RedNormalCMS.Checked = false;
			RedAscendingCMS.Checked = false;
			RedDescendingCMS.Checked = false;

			GreenNormalCMS.Checked = false;
			GreenAscendingCMS.Checked = false;
			GreenDescendingCMS.Checked = false;

			BlueNormalCMS.Checked = false;
			BlueAscendingCMS.Checked = false;
			BlueDescendingCMS.Checked = false;

			FavouriteNormalCMS.Checked = false;
			FavouriteAscendingCMS.Checked = false;
			FavouriteDescendingCMS.Checked = false;
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
				Process.Start(new ProcessStartInfo
				{
					FileName = url,
					UseShellExecute = true
				}); ////Open webpage with default browser...
			}
			catch (Exception)
			{
				Clipboard.SetText(!string.IsNullOrEmpty(url) ? url : " ");
				MessageBox.Show(text: "ERROR: The following webpage: \n\n" + url + "\n\ncould not be opened. However, the link has been copied to your clipboard. You can paste it in your favourite web browser :)", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		private void SendButton_Click(object sender, EventArgs e)
		{
			if (Utils.BooleanUtils.IsValidColour((int)RedNowNUD.Value, (int)GreenNowNUD.Value, (int)BlueNowNUD.Value)) //Check lightness of colour to check if it is valid. Double Check.
			{
				//Calibrate colours and set the result variable, because we cannot get the NUD values if he have this.Close() the form.
				//ORDER: RMenu, GMenu, BMenu, RLogo, GLogo, BLogo, ROptic, GOptic, BOptic
				FinalCalibratedColours = Utils.IntUtils.CalibrateAllColours((int)RedNowNUD.Value, (int)GreenNowNUD.Value, (int)BlueNowNUD.Value);

				this.Close();
			}
			else
			{
				MessageBox.Show(text: "ERROR: We have detected that the colour you want to use is too dark. \n\nThis might lead to inaccuracies while reading the content, please select a lighter colour to continue :)", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			GUI.AddColorConfig add = new(values)
			{
				BackColor = this.BackColor
			};
			add.ShowDialog();

			if (add.AddedSuccess)
			{
				var data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

				if (add.Chosen == 1)
				{
					if (add.Persists == 1) 
					{
						RedNextNUD.Value = add.Red;
						GreenNextNUD.Value = add.Green;
						BlueNextNUD.Value = add.Blue;

						FinalCalibratedColours = Utils.IntUtils.CalibrateAllColours(add.Red, add.Green, add.Blue);

						Config.AppSettings.Settings["RedMenu"].Value = FinalCalibratedColours[0].ToString(); //Modify data in the config file for future executions.
						Config.AppSettings.Settings["GreenMenu"].Value = FinalCalibratedColours[1].ToString();
						Config.AppSettings.Settings["BlueMenu"].Value = FinalCalibratedColours[2].ToString();
						Config.AppSettings.Settings["RedLogo"].Value = FinalCalibratedColours[3].ToString();
						Config.AppSettings.Settings["GreenLogo"].Value = FinalCalibratedColours[4].ToString();
						Config.AppSettings.Settings["BlueLogo"].Value = FinalCalibratedColours[5].ToString();
						Config.AppSettings.Settings["RedOptions"].Value = FinalCalibratedColours[6].ToString();
						Config.AppSettings.Settings["GreenOptions"].Value = FinalCalibratedColours[7].ToString();
						Config.AppSettings.Settings["BlueOptions"].Value = FinalCalibratedColours[8].ToString();
					}

					RedNowNUD.Value = add.Red;
					GreenNowNUD.Value = add.Green;
					BlueNowNUD.Value = add.Blue;

					actualColours = new int[] { add.Red, add.Green, add.Blue };
					FinalCalibratedColours = Utils.IntUtils.CalibrateAllColours(add.Red, add.Green, add.Blue);
				}

				data.Add(key: add.name, value: new List<int> { add.Red, add.Green, add.Blue, add.Favourite });
				string newData = JsonSerializer.Serialize(data);

				Config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				Config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

			}

		}

		private void EditButton_Click(object sender, EventArgs e)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			var actualNameChosen = ColourContentDGV.Rows.Cast<DataGridViewRow>().FirstOrDefault(
				row => row.Cells["RedColumn"].Value.ToString() == RedNowNUD.Value.ToString() &&
						row.Cells["GreenColumn"].Value.ToString() == GreenNowNUD.Value.ToString() &&
						row.Cells["BlueColumn"].Value.ToString() == BlueNowNUD.Value.ToString())?
						.Cells["ConfigNameColumn"].Value.ToString();

			GUI.EditColorConfig edit = new(values, actualNameChosen)
			{
				BackColor = this.BackColor
			};
			edit.ShowDialog();

			if (edit.EditedSuccess)
			{
				var data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);
				FinalCalibratedColours = Utils.IntUtils.CalibrateAllColours(edit.Red, edit.Green, edit.Blue);

				if (edit.Chosen == 1)
				{
					if (edit.Persists == 1)
					{
						RedNextNUD.Value = edit.Red;
						GreenNextNUD.Value = edit.Green;
						BlueNextNUD.Value = edit.Blue;

						Config.AppSettings.Settings["RedMenu"].Value = FinalCalibratedColours[0].ToString(); //Modify data in the config file for future executions.
						Config.AppSettings.Settings["GreenMenu"].Value = FinalCalibratedColours[1].ToString();
						Config.AppSettings.Settings["BlueMenu"].Value = FinalCalibratedColours[2].ToString();
						Config.AppSettings.Settings["RedLogo"].Value = FinalCalibratedColours[3].ToString();
						Config.AppSettings.Settings["GreenLogo"].Value = FinalCalibratedColours[4].ToString();
						Config.AppSettings.Settings["BlueLogo"].Value = FinalCalibratedColours[5].ToString();
						Config.AppSettings.Settings["RedOptions"].Value = FinalCalibratedColours[6].ToString();
						Config.AppSettings.Settings["GreenOptions"].Value = FinalCalibratedColours[7].ToString();
						Config.AppSettings.Settings["BlueOptions"].Value = FinalCalibratedColours[8].ToString();
					}

					actualColours = new int[] { edit.Red, edit.Green, edit.Blue };

					RedNowNUD.Value = edit.Red;
					GreenNowNUD.Value = edit.Green;
					BlueNowNUD.Value = edit.Blue;

				}

				data.Remove(edit.Oldname);
				data.Add(key: edit.name, value: new List<int> { edit.Red, edit.Green, edit.Blue, edit.Favourite });
				string newData = JsonSerializer.Serialize(data);

				Config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				Config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

			}

		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			var namesList = new List<String>(values.Keys);
			var actualNameChosen = ColourContentDGV.Rows.Cast<DataGridViewRow>().FirstOrDefault(
				row => row.Cells["RedColumn"].Value.ToString() == RedNowNUD.Value.ToString() &&
						row.Cells["GreenColumn"].Value.ToString() == GreenNowNUD.Value.ToString() &&
						row.Cells["BlueColumn"].Value.ToString() == BlueNowNUD.Value.ToString())?
						.Cells["ConfigNameColumn"].Value.ToString();

			GUI.DeleteColorConfig del = new(namesList, actualNameChosen)
			{
				BackColor = this.BackColor,
				Enabled = true
			};
			del.ShowDialog();

			if (del.DeletedSuccess)
			{
				values.Remove(del.name);
				if(values.Count < 1) { values.Add("Default", new List<int> { 0, 191, 144, 1 }); }

				String newData = JsonSerializer.Serialize(values);

				Config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				Config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

			}
			else if (del.DeletedAllSuccess)
			{
				values.Clear();
				values.Add("Default", new List<int> { 0, 191, 144, 1 });

				String newData = JsonSerializer.Serialize(values);

				Config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				Config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

			}


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

			if (this.BackColor == Color.FromArgb(230, 230, 230))
			{
				SearchTextbox.BackColor = SystemColors.Window;
				RedNextNUD.BackColor = SystemColors.Window;
				GreenNextNUD.BackColor = SystemColors.Window;
				BlueNextNUD.BackColor = SystemColors.Window;
				RedNowNUD.BackColor = SystemColors.Window;
				GreenNowNUD.BackColor = SystemColors.Window;
				BlueNowNUD.BackColor = SystemColors.Window;

			}
			else
			{
				SearchTextbox.BackColor = Color.FromArgb(152, 154, 153);
				RedNextNUD.BackColor = Color.FromArgb(152, 154, 153);
				GreenNextNUD.BackColor = Color.FromArgb(152, 154, 153);
				BlueNextNUD.BackColor = Color.FromArgb(152, 154, 153);
				RedNowNUD.BackColor = Color.FromArgb(152, 154, 153);
				GreenNowNUD.BackColor = Color.FromArgb(152, 154, 153);
				BlueNowNUD.BackColor = Color.FromArgb(152, 154, 153);

			}
		}

		private void ColourContentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if(e.RowIndex >= 0) //It is a normal cell, not a column header....
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
						Clipboard.SetText(!string.IsNullOrEmpty(ColourContentDGV.CurrentCell.Value.ToString()) ? ColourContentDGV.CurrentCell.Value.ToString() : " ");
						break;
					case "ChosenConfig":
						if ((ColourContentDGV.CurrentCell != null) && (!Convert.ToBoolean(ColourContentDGV.CurrentCell.Value))) //If checkbox is not checked then we do something...
						{
							var row = ColourContentDGV.Rows[e.RowIndex];
							var dialog = MessageBox.Show(text: "Do you want to select and use the config with name '" + row.Cells[0].Value.ToString() + "'?", caption: "Select and Use Config", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
							if (dialog == DialogResult.Yes)
							{
								Dictionary<String, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

								DialogResult dialog2 = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
								if (dialog2 == DialogResult.Yes)
								{
									RedNextNUD.Value = data[row.Cells[0].Value.ToString()][0];
									GreenNextNUD.Value = data[row.Cells[0].Value.ToString()][1];
									BlueNextNUD.Value = data[row.Cells[0].Value.ToString()][2];
									RedNowNUD.Value = data[row.Cells[0].Value.ToString()][0];
									GreenNowNUD.Value = data[row.Cells[0].Value.ToString()][1];
									BlueNowNUD.Value = data[row.Cells[0].Value.ToString()][2];

									actualColours = new int[] { data[row.Cells[0].Value.ToString()][0], data[row.Cells[0].Value.ToString()][1], data[row.Cells[0].Value.ToString()][2] };

									FinalCalibratedColours = Utils.IntUtils.CalibrateAllColours(data[row.Cells[0].Value.ToString()][0], data[row.Cells[0].Value.ToString()][1], data[row.Cells[0].Value.ToString()][2]);
									
									Config.AppSettings.Settings["RedMenu"].Value = FinalCalibratedColours[0].ToString(); //Modify data in the config file for future executions.
									Config.AppSettings.Settings["GreenMenu"].Value = FinalCalibratedColours[1].ToString();
									Config.AppSettings.Settings["BlueMenu"].Value = FinalCalibratedColours[2].ToString();
									Config.AppSettings.Settings["RedLogo"].Value = FinalCalibratedColours[3].ToString();
									Config.AppSettings.Settings["GreenLogo"].Value = FinalCalibratedColours[4].ToString();
									Config.AppSettings.Settings["BlueLogo"].Value = FinalCalibratedColours[5].ToString();
									Config.AppSettings.Settings["RedOptions"].Value = FinalCalibratedColours[6].ToString();
									Config.AppSettings.Settings["GreenOptions"].Value = FinalCalibratedColours[7].ToString();
									Config.AppSettings.Settings["BlueOptions"].Value = FinalCalibratedColours[8].ToString();

									Config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(data); //Modify data in the config file for future executions.
									Config.Save(ConfigurationSaveMode.Modified);
									ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

									ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
									if (isSearched)
									{
										SearchButton.PerformClick();
									}
									else
									{
										//Load the ordered content depending on column and order, and set toolstrip check property.
										LoadContent(actualColumn, actualOrder);
									}
								}
								else
								{
									RedNowNUD.Value = data[row.Cells[0].Value.ToString()][0];
									GreenNowNUD.Value = data[row.Cells[0].Value.ToString()][1];
									BlueNowNUD.Value = data[row.Cells[0].Value.ToString()][2];

									actualColours = new int[] { data[row.Cells[0].Value.ToString()][0], data[row.Cells[0].Value.ToString()][1], data[row.Cells[0].Value.ToString()][2] };

									FinalCalibratedColours = Utils.IntUtils.CalibrateAllColours(data[row.Cells[0].Value.ToString()][0], data[row.Cells[0].Value.ToString()][1], data[row.Cells[0].Value.ToString()][2]);

									ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
									if (isSearched)
									{
										SearchButton.PerformClick();
									}
									else
									{
										//Load the ordered content depending on column and order, and set toolstrip check property.
										LoadContent(actualColumn, actualOrder);
									}

								}
							}
							else
							{
								ColourContentDGV.CurrentCell.Value = false; //Uncheck checkbox, just in case...
							}
						}

						break;
					case "Favourite":
						//Seleccionar esto como favourite y poner esto con mgbox, ademas de guardar en array....
						if (Convert.ToBoolean(ColourContentDGV.CurrentCell.Value)) //If checkbox is checked....
						{
							var row = ColourContentDGV.Rows[e.RowIndex];
							var dialog = MessageBox.Show(text: "Do you want to unmark the config with name '" + row.Cells[0].Value.ToString() + "' as favourite?", caption: "Unmark config as favourite.", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);

							if (dialog == DialogResult.Yes)
							{
								Dictionary<String, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

								//Set NUDs and chosen to 1...
								data[row.Cells[0].Value.ToString()][3] = 0; //Set favourite in the row...

								Config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(data); ; //Modify data in the config file for future executions.
								Config.Save(ConfigurationSaveMode.Modified);
								ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

								ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
								if (isSearched)
								{
									SearchButton.PerformClick();
								}
								else
								{
									//Load the ordered content depending on column and order, and set toolstrip check property.
									LoadContent(actualColumn, actualOrder);
								}

							}
							else
							{
								ColourContentDGV.CurrentCell.Value = true; //Maintain checkbox as it was, just in case...
							}
						}
						else //If checkbox isnt checked...
						{
							var row = ColourContentDGV.Rows[e.RowIndex];
							var dialog = MessageBox.Show(text: "Do you want to mark the config with name '" + row.Cells[0].Value.ToString() + "' as favourite?", caption: "Mark config as favourite.", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);

							if (dialog == DialogResult.Yes)
							{
								Dictionary<String, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

								//Set NUDs and chosen to 1...
								data[row.Cells[0].Value.ToString()][3] = 1; //Set favourite in the row...

								Config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(data); ; //Modify data in the config file for future executions.
								Config.Save(ConfigurationSaveMode.Modified);
								ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

								ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
								if (isSearched)
								{
									SearchButton.PerformClick();
								}
								else
								{
									//Load the ordered content depending on column and order, and set toolstrip check property.
									LoadContent(actualColumn, actualOrder);
								}

							}
							else
							{
								ColourContentDGV.CurrentCell.Value = false; //Uncheck checkbox, just in case...
							}
						}
						break;
					case "Delete Row":
						var rowToBeDeleted = ColourContentDGV.Rows[e.RowIndex];
						var deleteDialog = MessageBox.Show(text: "Do you want to delete the config with name '" + rowToBeDeleted.Cells[0].Value.ToString() + "'? \n\nNote: This action cannot be undone.", caption: "Delete config.", icon: MessageBoxIcon.Exclamation, buttons: MessageBoxButtons.YesNo);

						if (deleteDialog == DialogResult.Yes)
						{
							Dictionary<String, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

							//Set NUDs and chosen to 1...
							data.Remove(rowToBeDeleted.Cells[0].Value.ToString()); //Remove row from data

							Config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(data); //Modify data in the config file for future executions.
							Config.Save(ConfigurationSaveMode.Modified);
							ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

							ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
							if (isSearched)
							{
								SearchButton.PerformClick();
							}
							else
							{
								//Load the ordered content depending on column and order, and set toolstrip check property.
								LoadContent(actualColumn, actualOrder);
							}

						}
						break;
					default:
						break;
				}
			}

		}

		//Sets the contents for the CMS of each header button (except SitePassword)
		[SupportedOSPlatform("windows")]
		private void SetCMS()
		{
			var titleName = new ToolStripLabel("ORDER BY NAME")
			{
				Font = new Font("Segoe UI", 10, FontStyle.Bold),
				TextAlign = ContentAlignment.MiddleCenter,
				ForeColor = Color.FromArgb(109, 109, 109)
			};
			NameCMS.Items.Insert(0, titleName);

			var titleRed = new ToolStripLabel("ORDER BY RED")
			{
				Font = new Font("Segoe UI", 10, FontStyle.Bold),
				TextAlign = ContentAlignment.MiddleCenter,
				ForeColor = Color.FromArgb(109, 109, 109)
			};
			RedCMS.Items.Insert(0, titleRed);

			var titleGreen = new ToolStripLabel("ORDER BY GREEN")
			{
				Font = new Font("Segoe UI", 10, FontStyle.Bold),
				TextAlign = ContentAlignment.MiddleCenter,
				ForeColor = Color.FromArgb(109, 109, 109)
			};
			GreenCMS.Items.Insert(0, titleGreen);

			var titleBlue = new ToolStripLabel("ORDER BY BLUE")
			{
				Font = new Font("Segoe UI", 10, FontStyle.Bold),
				TextAlign = ContentAlignment.MiddleCenter,
				ForeColor = Color.FromArgb(109, 109, 109)
			};
			BlueCMS.Items.Insert(0, titleBlue);

			var titleFavourite = new ToolStripLabel("ORDER BY FAVOURITE")
			{
				Font = new Font("Segoe UI", 10, FontStyle.Bold),
				TextAlign = ContentAlignment.MiddleCenter,
				ForeColor = Color.FromArgb(109, 109, 109)
			};
			FavouriteCMS.Items.Insert(0, titleFavourite);
		}

		private void ColourContentDGV_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if ((e.Button == MouseButtons.Left) && (e.RowIndex < 0)) //It is a column header, not a normal row
			{
				DataGridViewColumn clickedColumn = ColourContentDGV.Columns[e.ColumnIndex];
				string columnName = clickedColumn.HeaderText;

				// Show the context menu strip at the mouse location 
				switch (columnName)
				{
					case "ConfigName":
						NameCMS.Show(Cursor.Position);
						break;
					case "Red":
						RedCMS.Show(Cursor.Position);
						break;
					case "Green":
						GreenCMS.Show(Cursor.Position);
						break;
					case "Blue":
						BlueCMS.Show(Cursor.Position);
						break;
					case "Favourite":
						FavouriteCMS.Show(Cursor.Position);
						break;
					default:
						break;
				}

			}
		}

		private void NameNormalCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Normal;
				actualColumn = CFColumns.Name;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				ResetToNormalOrdering(false);
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void NameAscendingCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Asc;
				actualColumn = CFColumns.Name;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				UncheckOrdering();
				NameAscendingCMS.Checked = true;
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void NameDescendingCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Desc;
				actualColumn = CFColumns.Name;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				UncheckOrdering();
				NameDescendingCMS.Checked = true;
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void RedNormalCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Normal;
				actualColumn = CFColumns.Red;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				ResetToNormalOrdering(false);
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void RedAscendingCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Asc;
				actualColumn = CFColumns.Red;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				UncheckOrdering();
				RedAscendingCMS.Checked = true;
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void RedDescendingCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Desc;
				actualColumn = CFColumns.Red;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				UncheckOrdering();
				RedDescendingCMS.Checked = true;
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void GreenNormalCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Normal;
				actualColumn = CFColumns.Green;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				ResetToNormalOrdering(false);
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void GreenAscendingCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Asc;
				actualColumn = CFColumns.Green;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				UncheckOrdering();
				GreenAscendingCMS.Checked = true;
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void GreenDescendingCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Desc;
				actualColumn = CFColumns.Green;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				UncheckOrdering();
				GreenDescendingCMS.Checked = true;
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void BlueNormalCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Normal;
				actualColumn = CFColumns.Blue;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				ResetToNormalOrdering(false);
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void BlueAscendingCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Asc;
				actualColumn = CFColumns.Blue;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				UncheckOrdering();
				BlueAscendingCMS.Checked = true;
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void BlueDescendingCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Desc;
				actualColumn = CFColumns.Blue;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				UncheckOrdering();
				BlueDescendingCMS.Checked = true;
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void FavouriteNormalCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Normal;
				actualColumn = CFColumns.Favourite;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				ResetToNormalOrdering(false);
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void FavouriteAscendingCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Asc;
				actualColumn = CFColumns.Favourite;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				UncheckOrdering();
				FavouriteAscendingCMS.Checked = true;
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		private void FavouriteDescendingCMS_Click(object sender, EventArgs e)
		{
			try
			{
				actualOrder = Order.Desc;
				actualColumn = CFColumns.Favourite;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				UncheckOrdering();
				FavouriteDescendingCMS.Checked = true;
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException)
				{
					MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				}
				ResetToNormalOrdering(true);
			}
		}

		[SupportedOSPlatform("windows")]
		private void ExportButton_MouseEnter(object sender, EventArgs e)
		{
			ExportButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void ExportButton_MouseLeave(object sender, EventArgs e)
		{
			ExportButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void SendButton_MouseEnter(object sender, EventArgs e)
		{
			SendButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void SendButton_MouseLeave(object sender, EventArgs e)
		{
			SendButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void DeleteButton_MouseEnter(object sender, EventArgs e)
		{
			DeleteButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void DeleteButton_MouseLeave(object sender, EventArgs e)
		{
			DeleteButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void EditButton_MouseEnter(object sender, EventArgs e)
		{
			EditButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void EditButton_MouseLeave(object sender, EventArgs e)
		{
			EditButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void AddButton_MouseEnter(object sender, EventArgs e)
		{
			AddButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void AddButton_MouseLeave(object sender, EventArgs e)
		{
			AddButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		private void ExportButton_Click(object sender, EventArgs e)
		{
			ExportVaultConfigs export = new()
			{
				BackColor = this.BackColor
			};
			export.ShowDialog();
		}

		[SupportedOSPlatform("windows")]
		private void HelpButton_MouseEnter(object sender, EventArgs e)
		{
			HelpFormButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the butto
		}

		[SupportedOSPlatform("windows")]
		private void HelpButton_MouseLeave(object sender, EventArgs e)
		{
			HelpFormButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		private void HelpButton_Click(object sender, EventArgs e)
		{
			GUI.HelpColourConfigsForm helpForm = new()
			{
				BackColor = this.BackColor
			};
			helpForm.ShowDialog();
		}

		[SupportedOSPlatform("windows")]
		private void ImportButton_MouseEnter(object sender, EventArgs e)
		{
			ImportButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the butto
		}

		[SupportedOSPlatform("windows")]
		private void ImportButton_MouseLeave(object sender, EventArgs e)
		{
			ImportButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		private void ImportButton_Click(object sender, EventArgs e)
		{
			DialogResult confirmation = MessageBox.Show(text: "If you import configurations from a file, your saved configurations will be replaced by the imported configurations, and cannot be recovered.\n\nAre you sure you want to continue with the import process?", caption: "Import confirmation", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);
			if (confirmation == DialogResult.Yes)
			{
				//Select and Save filepath and extension.
				string filepath = "";
				string ext = ""; //File extension
				bool cancelPathSearch = false;
				while (ext != ".json" && !cancelPathSearch)
				{
					OpenFileDialog ofd = new()
					{
						Filter = "JSON file|*.json" //Type of file we are looking for...
					}; //File selector

					var result = ofd.ShowDialog();
					filepath = ofd.FileName;
					ext = Path.GetExtension(filepath).ToLower();

					if (result == DialogResult.Cancel)
					{
						cancelPathSearch = true; //Stop loop, as user hit cancel button.
					}
					else if (ext != ".json") //If user specified file with diff extension...
					{
						MessageBox.Show(text: "Selected file must have .json extension.", caption: "Wrong File", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
					}
				}
				if (filepath != "")
				{
					if (ValidateJsonFormat(File.ReadAllText(filepath)))
					{
						// Deserialize JSON content into an object
						var goodFormData = JsonSerializer.Deserialize<Dictionary<string, List<int>>>(File.ReadAllText(filepath));
						var newData = ValidateAndGetJsonContent(goodFormData);
						if (newData != null)
						{
							if(newData.Count == 0)
							{
								newData.Add("Default", new List<int> { 0, 191, 144, 1 });
							}
							Config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(newData); //Modify data in the config file for future executions.
							Config.Save(ConfigurationSaveMode.Modified);
							ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

							ColourContentDGV.Rows.Clear();
							//When we import, I assume we want to see all the content and not the result of a previous search from a previous set of configs....
							LoadContent(actualColumn, actualOrder);
							ResetButton.PerformClick();

						}
						else
						{
							MessageBox.Show(text: "Content of your JSON file is not valid.", caption: "Content Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
						}
					}
				}
			}

		}

		private static bool ValidateJsonFormat(string rawData)
		{
			try
			{
				// Deserialize JSON content into an object
				var data = JsonSerializer.Deserialize<Dictionary<string, List<int>>>(rawData);
				return true;
			}
			catch(Exception)
			{
				MessageBox.Show(text: "Format of your JSON file is not valid.", caption: "Format Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
				return false;
			}
		}

		private static Dictionary<string, List<int>> ValidateAndGetJsonContent(Dictionary<string, List<int>> rawData)
		{
			//If all values are OK, we will return them. If not, we will return an empty dict.
			//With all this conditions for the JSON, we are pretty much controlling JSON Injections....
			Dictionary<string, List<int>> result = new();
			// Validate the JSON data
			foreach (var kvp in rawData)
			{
				string key = kvp.Key;
				List<int> values = kvp.Value;

				// Check if the value is a list of 4 integers
				if (!(values.Count == 4)) //All integers
				{
					result = null;
					return result;
				}

				Predicate<int> isInRange = number => number >= 0 && number <= 255;
				// Check if the first 3 values are between 0 and 255
				if (!values.GetRange(0, 3).TrueForAll(isInRange))
				{
					result = null;
					return result;
				}

				// Check the last value is either 0 or 1
				if (values[3] != 0 && values[3] != 1)
				{
					result = null;
					return result;
				}

				// Check all the things you would check when you add a new config
				if (!(result == null || result.Count <= 0))
				{
					bool areValuesValid = CheckAddConfigConditions(result, key, values);
					if (!areValuesValid)
					{
						result = null;
						return result;
					}
				}

				result.Add(key, values);
			}

			return result;
		}

		private static bool CheckAddConfigConditions(Dictionary<string, List<int>> content, string key, List<int> values)
		{
			if ((String.IsNullOrWhiteSpace(key)) || (content.ContainsKey(key))
			|| (!Utils.BooleanUtils.IsValidColour(values[0], values[1], values[2]))
			)
			{
				return false;
			}

			var colorConfig = new List<int> { values[0], values[1], values[2] };
			foreach (List<int> savedConfig in content.Values)
			{
				if ((savedConfig[0] == colorConfig[0]) && (savedConfig[1] == colorConfig[1]) && (savedConfig[2] == colorConfig[2])) //.Contains() does not work.
				{
					return false;
				}
			}

			return true;
		}

		private void SearchButton_Click(object sender, EventArgs e)
		{
			TrimComponents();

			//Reset content, so that you dont search content on the previously searched ocntent....
			ColourContentDGV.Rows.Clear();
			LoadContent(actualColumn, actualOrder);

			List<DataGridViewRow> matchingRows = new();

			foreach (DataGridViewRow row in ColourContentDGV.Rows)
			{
				if (row.Cells["ConfigNameColumn"].Value != null)
				{
					string name = row.Cells["ConfigNameColumn"].Value.ToString();

					if (name.Contains(SearchTextbox.Text, StringComparison.OrdinalIgnoreCase))
					{
						matchingRows.Add(row);
					}
				}
			}

			// Display the matching rows in the DataGridView
			ColourContentDGV.Rows.Clear();
			ColourContentDGV.Rows.AddRange(matchingRows.ToArray());

			ResetButton.Enabled = true;
			isSearched = true;
		}

		[SupportedOSPlatform("windows")]
		private void SearchButton_MouseEnter(object sender, EventArgs e)
		{
			SearchButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void SearchButton_MouseLeave(object sender, EventArgs e)
		{
			SearchButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void ResetButton_MouseEnter(object sender, EventArgs e)
		{
			ResetButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void ResetButton_MouseLeave(object sender, EventArgs e)
		{
			ResetButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			ColourContentDGV.Rows.Clear();
			SearchTextbox.Text = string.Empty;
			LoadContent(actualColumn, actualOrder);

			ResetButton.Enabled = false;
			isSearched = false;
		}

		private void SearchTextbox_TextChanged(object sender, EventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(SearchTextbox.Text))
			{
				SearchButton.Enabled = true;
			}
			else { SearchButton.Enabled = false; }
		}
	}

}
