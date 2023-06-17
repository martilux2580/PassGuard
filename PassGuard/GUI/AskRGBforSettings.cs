using PassGuard.PDF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
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
		private int[] actualColours;
		public int[] finalCalibratedColours { get; private set; }

		[SupportedOSPlatform("windows")]
		public AskRGBforSettings(int[] colours, Configuration configg)
		{
			InitializeComponent();

			actualColours = colours; //Set actual colours used in this execution

			SetNUDs();

			SetCMS(); //Set CMSs elements

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

		internal void LoadContent(String configs)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(configs);

			ColourContentDGV.Rows.Clear();
			foreach (KeyValuePair<String, List<int>> configColor in values)
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

		private void UncheckAllMenuItems(ContextMenuStrip contextMenuStrip)
		{
			foreach (ToolStripItem item in contextMenuStrip.Items)
			{
				if (item is ToolStripMenuItem toolStripMenuItem)
				{
					toolStripMenuItem.Checked = false;
					//CMS dont have submenus, if they had we would need another method.
				}
			}
		}

		private void ResetCMS()
		{
			UncheckAllMenuItems(NameCMS);
			NameNormalToolStripMenuItem.Checked = true;

			UncheckAllMenuItems(RedCMS);
			RedNormalToolStripMenuItem.Checked = true;

			UncheckAllMenuItems(GreenCMS);
			GreenNormalToolStripMenuItem.Checked = true;

			UncheckAllMenuItems(BlueCMS);
			BlueNormalToolStripMenuItem.Checked = true;

			UncheckAllMenuItems(FavouriteCMS);
			FavouriteNormalToolStripMenuItem.Checked = true;

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
			if (Utils.BooleanUtils.IsValidColour((int)RedNowNUD.Value, (int)GreenNowNUD.Value, (int)BlueNowNUD.Value)) //Check lightness of colour to check if it is valid. Double Check.
			{
				//Calibrate colours and set the result variable, because we cannot get the NUD values if he have this.Close() the form.
				//ORDER: RMenu, GMenu, BMenu, RLogo, GLogo, BLogo, ROptic, GOptic, BOptic
				finalCalibratedColours = Utils.IntUtils.CalibrateAllColours((int)RedNowNUD.Value, (int)GreenNowNUD.Value, (int)BlueNowNUD.Value);

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

			if (add.addedSuccess)
			{
				var data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

				if (add.chosen == 1)
				{
					if (add.persists == 1) 
					{
						RedNextNUD.Value = add.red;
						GreenNextNUD.Value = add.green;
						BlueNextNUD.Value = add.blue;

						finalCalibratedColours = Utils.IntUtils.CalibrateAllColours(add.red, add.green, add.blue);

						config.AppSettings.Settings["RedMenu"].Value = finalCalibratedColours[0].ToString(); //Modify data in the config file for future executions.
						config.AppSettings.Settings["GreenMenu"].Value = finalCalibratedColours[1].ToString();
						config.AppSettings.Settings["BlueMenu"].Value = finalCalibratedColours[2].ToString();
						config.AppSettings.Settings["RedLogo"].Value = finalCalibratedColours[3].ToString();
						config.AppSettings.Settings["GreenLogo"].Value = finalCalibratedColours[4].ToString();
						config.AppSettings.Settings["BlueLogo"].Value = finalCalibratedColours[5].ToString();
						config.AppSettings.Settings["RedOptions"].Value = finalCalibratedColours[6].ToString();
						config.AppSettings.Settings["GreenOptions"].Value = finalCalibratedColours[7].ToString();
						config.AppSettings.Settings["BlueOptions"].Value = finalCalibratedColours[8].ToString();
					}

					RedNowNUD.Value = add.red;
					GreenNowNUD.Value = add.green;
					BlueNowNUD.Value = add.blue;

					actualColours = new int[] { add.red, add.green, add.blue };
					finalCalibratedColours = Utils.IntUtils.CalibrateAllColours(add.red, add.green, add.blue);
				}

				data.Add(key: add.name, value: new List<int> { add.red, add.green, add.blue, add.favourite });
				string newData = JsonSerializer.Serialize(data);

				config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				ColourContentDGV.Rows.Clear();
				LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

				//Reset ordering of rows.
				ResetCMS();
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

			if (edit.editedSuccess)
			{
				var data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);
				finalCalibratedColours = Utils.IntUtils.CalibrateAllColours(edit.red, edit.green, edit.blue);

				if (edit.chosen == 1)
				{
					if (edit.persists == 1)
					{
						RedNextNUD.Value = edit.red;
						GreenNextNUD.Value = edit.green;
						BlueNextNUD.Value = edit.blue;

						config.AppSettings.Settings["RedMenu"].Value = finalCalibratedColours[0].ToString(); //Modify data in the config file for future executions.
						config.AppSettings.Settings["GreenMenu"].Value = finalCalibratedColours[1].ToString();
						config.AppSettings.Settings["BlueMenu"].Value = finalCalibratedColours[2].ToString();
						config.AppSettings.Settings["RedLogo"].Value = finalCalibratedColours[3].ToString();
						config.AppSettings.Settings["GreenLogo"].Value = finalCalibratedColours[4].ToString();
						config.AppSettings.Settings["BlueLogo"].Value = finalCalibratedColours[5].ToString();
						config.AppSettings.Settings["RedOptions"].Value = finalCalibratedColours[6].ToString();
						config.AppSettings.Settings["GreenOptions"].Value = finalCalibratedColours[7].ToString();
						config.AppSettings.Settings["BlueOptions"].Value = finalCalibratedColours[8].ToString();
					}

					actualColours = new int[] { edit.red, edit.green, edit.blue };

					RedNowNUD.Value = edit.red;
					GreenNowNUD.Value = edit.green;
					BlueNowNUD.Value = edit.blue;

				}

				data.Remove(edit.oldname);
				data.Add(key: edit.name, value: new List<int> { edit.red, edit.green, edit.blue, edit.favourite });
				string newData = JsonSerializer.Serialize(data);

				config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				ColourContentDGV.Rows.Clear();
				LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

				//Reset ordering of rows.
				ResetCMS();

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

				//Reset ordering of rows.
				ResetCMS();
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

				//Reset ordering of rows.
				ResetCMS();
			}


		}

		private void normalOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ColourContentDGV.Rows.Clear();
			LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			NameNormalToolStripMenuItem.Checked = true;
			NameAscendingToolStripMenuItem.Checked = false;
			NameDescendingToolStripMenuItem.Checked = false;
		}

		private void ascendingOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var values = new SortedDictionary<String, List<int>>(JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]));

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(values));

			NameNormalToolStripMenuItem.Checked = false;
			NameAscendingToolStripMenuItem.Checked = true;
			NameDescendingToolStripMenuItem.Checked = false;
		}

		private void descendingOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var values = new SortedDictionary<String, List<int>>(JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]));
			var reversed = values.Reverse();
			var newValues = reversed.ToDictionary(x => x.Key, x => x.Value);

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(newValues));

			NameNormalToolStripMenuItem.Checked = false;
			NameAscendingToolStripMenuItem.Checked = false;
			NameDescendingToolStripMenuItem.Checked = true;
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
						Clipboard.SetText(ColourContentDGV.CurrentCell.Value.ToString());
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

									finalCalibratedColours = Utils.IntUtils.CalibrateAllColours(data[row.Cells[0].Value.ToString()][0], data[row.Cells[0].Value.ToString()][1], data[row.Cells[0].Value.ToString()][2]);
									
									config.AppSettings.Settings["RedMenu"].Value = finalCalibratedColours[0].ToString(); //Modify data in the config file for future executions.
									config.AppSettings.Settings["GreenMenu"].Value = finalCalibratedColours[1].ToString();
									config.AppSettings.Settings["BlueMenu"].Value = finalCalibratedColours[2].ToString();
									config.AppSettings.Settings["RedLogo"].Value = finalCalibratedColours[3].ToString();
									config.AppSettings.Settings["GreenLogo"].Value = finalCalibratedColours[4].ToString();
									config.AppSettings.Settings["BlueLogo"].Value = finalCalibratedColours[5].ToString();
									config.AppSettings.Settings["RedOptions"].Value = finalCalibratedColours[6].ToString();
									config.AppSettings.Settings["GreenOptions"].Value = finalCalibratedColours[7].ToString();
									config.AppSettings.Settings["BlueOptions"].Value = finalCalibratedColours[8].ToString();

									config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(data); //Modify data in the config file for future executions.
									config.Save(ConfigurationSaveMode.Modified);
									ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

									ColourContentDGV.Rows.Clear();
									LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);
								}
								else
								{
									RedNowNUD.Value = data[row.Cells[0].Value.ToString()][0];
									GreenNowNUD.Value = data[row.Cells[0].Value.ToString()][1];
									BlueNowNUD.Value = data[row.Cells[0].Value.ToString()][2];

									actualColours = new int[] { data[row.Cells[0].Value.ToString()][0], data[row.Cells[0].Value.ToString()][1], data[row.Cells[0].Value.ToString()][2] };

									finalCalibratedColours = Utils.IntUtils.CalibrateAllColours(data[row.Cells[0].Value.ToString()][0], data[row.Cells[0].Value.ToString()][1], data[row.Cells[0].Value.ToString()][2]);

									ColourContentDGV.Rows.Clear();
									LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

									//Reset ordering of rows.
									ResetCMS();
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

								config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(data); ; //Modify data in the config file for future executions.
								config.Save(ConfigurationSaveMode.Modified);
								ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

								ColourContentDGV.Rows.Clear();
								LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

								//Reset ordering of rows.
								ResetCMS();
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

								config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(data); ; //Modify data in the config file for future executions.
								config.Save(ConfigurationSaveMode.Modified);
								ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

								ColourContentDGV.Rows.Clear();
								LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

								//Reset ordering of rows.
								ResetCMS();
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

							config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(data); //Modify data in the config file for future executions.
							config.Save(ConfigurationSaveMode.Modified);
							ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

							ColourContentDGV.Rows.Clear();
							LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);

							//Reset ordering of rows.
							ResetCMS();
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

		private void RedNormalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<string, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(data));

			RedNormalToolStripMenuItem.Checked = true;
			RedAscendingToolStripMenuItem.Checked = false;
			RedDescendingToolStripMenuItem.Checked = false;
		}

		private void RedAscendingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<string, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			// Sort the dictionary by the fourth element of the list and then by name
			Dictionary<string, List<int>> sortedDictionary = data.OrderBy(kvp => kvp.Value.ElementAtOrDefault(0))
											.ThenBy(kvp => kvp.Key)
											.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(sortedDictionary));

			RedNormalToolStripMenuItem.Checked = false;
			RedAscendingToolStripMenuItem.Checked = true;
			RedDescendingToolStripMenuItem.Checked = false;
		}

		private void RedDescendingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<string, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			// Sort the dictionary by the fourth element of the list and then by name
			Dictionary<string, List<int>> sortedDictionary = data.OrderByDescending(kvp => kvp.Value.ElementAtOrDefault(0))
											.ThenByDescending(kvp => kvp.Key)
											.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(sortedDictionary));

			RedNormalToolStripMenuItem.Checked = false;
			RedAscendingToolStripMenuItem.Checked = false;
			RedDescendingToolStripMenuItem.Checked = true;
		}

		private void GreenNormalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<string, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(data));

			GreenNormalToolStripMenuItem.Checked = true;
			GreenAscendingToolStripMenuItem.Checked = false;
			GreenDescendingToolStripMenuItem.Checked = false;
		}

		private void GreenAscendingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<string, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			// Sort the dictionary by the fourth element of the list and then by name
			Dictionary<string, List<int>> sortedDictionary = data.OrderBy(kvp => kvp.Value.ElementAtOrDefault(1))
											.ThenBy(kvp => kvp.Key)
											.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(sortedDictionary));

			GreenNormalToolStripMenuItem.Checked = false;
			GreenAscendingToolStripMenuItem.Checked = true;
			GreenDescendingToolStripMenuItem.Checked = false;
		}

		private void GreenDescendingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<string, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			// Sort the dictionary by the fourth element of the list and then by name
			Dictionary<string, List<int>> sortedDictionary = data.OrderByDescending(kvp => kvp.Value.ElementAtOrDefault(1))
											.ThenByDescending(kvp => kvp.Key)
											.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(sortedDictionary));

			GreenNormalToolStripMenuItem.Checked = false;
			GreenAscendingToolStripMenuItem.Checked = false;
			GreenDescendingToolStripMenuItem.Checked = true;
		}

		private void BlueNormalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<string, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(data));

			BlueNormalToolStripMenuItem.Checked = true;
			BlueAscendingToolStripMenuItem.Checked = false;
			BlueDescendingToolStripMenuItem.Checked = false;
		}

		private void BlueAscendingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<string, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			// Sort the dictionary by the fourth element of the list and then by name
			Dictionary<string, List<int>> sortedDictionary = data.OrderBy(kvp => kvp.Value.ElementAtOrDefault(2))
											.ThenBy(kvp => kvp.Key)
											.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(sortedDictionary));

			BlueNormalToolStripMenuItem.Checked = false;
			BlueAscendingToolStripMenuItem.Checked = true;
			BlueDescendingToolStripMenuItem.Checked = false;
		}

		private void BlueDescendingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<string, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			// Sort the dictionary by the fourth element of the list and then by name
			Dictionary<string, List<int>> sortedDictionary = data.OrderByDescending(kvp => kvp.Value.ElementAtOrDefault(2))
											.ThenByDescending(kvp => kvp.Key)
											.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(sortedDictionary));

			BlueNormalToolStripMenuItem.Checked = false;
			BlueAscendingToolStripMenuItem.Checked = false;
			BlueDescendingToolStripMenuItem.Checked = true;
		}

		private void FavouriteNormalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<string, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(data));

			FavouriteNormalToolStripMenuItem.Checked = true;
			FavouriteAscendingToolStripMenuItem.Checked = false;
			FavouriteDescendingToolStripMenuItem.Checked = false;
		}

		private void FavouriteAscendingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<string, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			// Sort the dictionary by the fourth element of the list and then by name
			Dictionary<string, List<int>> sortedDictionary = data.OrderByDescending(kvp => kvp.Value.ElementAtOrDefault(4))
											.ThenBy(kvp => kvp.Key)
											.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(sortedDictionary));

			FavouriteNormalToolStripMenuItem.Checked = false;
			FavouriteAscendingToolStripMenuItem.Checked = true;
			FavouriteDescendingToolStripMenuItem.Checked = false;
		}

		private void FavouriteDescendingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<string, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			// Sort the dictionary by the fourth element of the list and then by name
			Dictionary<string, List<int>> sortedDictionary = data.OrderByDescending(kvp => kvp.Value.ElementAtOrDefault(4))
											.ThenByDescending(kvp => kvp.Key)
											.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

			ColourContentDGV.Rows.Clear();
			LoadContent(JsonSerializer.Serialize(sortedDictionary));

			FavouriteNormalToolStripMenuItem.Checked = false;
			FavouriteAscendingToolStripMenuItem.Checked = false;
			FavouriteDescendingToolStripMenuItem.Checked = true;
		}

		[SupportedOSPlatform("windows")]
		private void ExportAsPdfButton_MouseEnter(object sender, EventArgs e)
		{
			ExportAsPdfButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void ExportAsPdfButton_MouseLeave(object sender, EventArgs e)
		{
			ExportAsPdfButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
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

		private void ExportAsPdfButton_Click(object sender, EventArgs e)
		{
			IPDF pdf = new PDFCreator();
			pdf.CreateOutlinePDF();
		}
	}
}
