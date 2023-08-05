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
	/// <summary>
	/// Form to obtain new RGB values for the outline colours.
	/// </summary>
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
			NULLVALUESS, //No order
			Name,
			Red,
			Green,
			Blue,
			Favourite
		}
		public Configuration Config { get; private set; } //Send config, so that there is only one instance of config file opening...
		private int[] actualColours; //Actual colours used in current execution
		public int[] FinalCalibratedColours { get; private set; } //Set the final calibrated colours of the execution...
		private CFColumns actualColumn; //Actual column to order
		private Order actualOrder; //Actual ordering way to order
		private bool isSearched; //Flag that tells if a search is ongoing

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
				SetNUDs(); //Set values of the 6 nuds...

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
		
		/// <summary>
		/// Removes leading and trailing whitespaces from textboxes...
		/// </summary>
		public void TrimComponents()
		{
			SearchTextbox.Text = SearchTextbox.Text.Trim();
		}

		/// <summary>
		/// Given a pair of values, generates a row with the corresponding format and content for the ColourContentDGV component
		/// </summary>
		/// <param name="configPair"></param>
		/// <returns></returns>
		private DataGridViewRow GenerateNewRow(KeyValuePair<String, List<int>> configPair)
		{
			DataGridViewRow row = new(); //Cell for the name
			DataGridViewCell chosenNameCell = new DataGridViewButtonCell
			{
				Value = configPair.Key,
				FlatStyle = FlatStyle.Flat
			};
			chosenNameCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			DataGridViewCell redCell = new DataGridViewButtonCell //Cell for the Red component of RGB
			{
				Value = configPair.Value[0].ToString(),
				FlatStyle = FlatStyle.Flat
			};
			redCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			DataGridViewCell greenCell = new DataGridViewButtonCell //Cell for the Green component of RGB
			{
				Value = configPair.Value[1].ToString(),
				FlatStyle = FlatStyle.Flat
			};
			greenCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			DataGridViewCell blueCell = new DataGridViewButtonCell //Cell for the Blue component of RGB
			{
				Value = configPair.Value[2].ToString(),
				FlatStyle = FlatStyle.Flat
			};
			blueCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			DataGridViewCell viewerCell = new DataGridViewButtonCell //Cell for the viewer panel of the rgb colour...
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
			if (configPair.Value[0] == actualColours[0] //If the pair we evaluating is the same as the actualcolor used, then mark the config as chosen
				&& configPair.Value[1] == actualColours[1]
				&& configPair.Value[2] == actualColours[2])
			{
				chosenConfigCell.Value = 1;
			}

			//Favourite
			DataGridViewCell favouriteCell = new DataGridViewCheckBoxCell //Cell for the Important value of the color configuration
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

		/// <summary>
		/// Loads the saved colour configurations in the specified order by the specified column
		/// </summary>
		/// <param name="col"></param>
		/// <param name="order"></param>
		private void LoadContent(CFColumns col, Order order)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]); //Saved configs as they are
			Dictionary<String, List<int>> valuesToShow = new(); //Will hold the elements to show (all), in the order by the column specified

			if (order == Order.Normal) { valuesToShow = values; } //Ordering is normal, show data as it is....
			else if((order == Order.Asc) || (order == Order.Desc))
			{
				switch (col)
				{
					case CFColumns.Name: //Ascending or descending by name, order by name ascending and if order is descending then reverse list
						var sortedNames = new SortedDictionary<String, List<int>>(JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"])).ToDictionary(x => x.Key, x => x.Value);
						if(order == Order.Desc)
						{
							var reversedTemp = sortedNames.Reverse();
							sortedNames = reversedTemp.ToDictionary(x => x.Key, x => x.Value);
						}
						valuesToShow = sortedNames;
						break;

					case CFColumns.Red: //Ascending or descending by Red, order by red (if equals by name and same ordering) ascending and if order is descending then reverse list
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
					case CFColumns.Green: //Ascending or descending by Green, order by green (if equals by name and same ordering) ascending and if order is descending then reverse list
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
					case CFColumns.Blue: //Ascending or descending by Blue, order by blue (if equals by name and same ordering) ascending and if order is descending then reverse list
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
					case CFColumns.Favourite:  //Ascending or descending by Favourite, order by favourite (if equals by name and same ordering) ascending and if order is descending then reverse list. Favourite passwords will be ordered always on top

											   //Substitutes the 1s of Favourite column to 0s (and viceversa) in the OrderBy, first thenBy orders ascending by Favourite (all favourites will be up and normal will be down), last thenBy unevens the ordering
											   //	by ordering by Name and same ordering as Favourite.
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

			//Add new rows with the data to show and put them inside the ColourContentDGV....
			ColourContentDGV.Rows.Clear();
			foreach (KeyValuePair<String, List<int>> configColor in valuesToShow)
			{
				ColourContentDGV.Rows.Add(GenerateNewRow(configColor));

			}

		}

		/// <summary>
		/// Sets the initial values of the NUDs of this execution and next execution.
		/// </summary>
		private void SetNUDs() //Set NumericUpDowns to the colours set right now in the Content Panel of Form1
		{
			RedNextNUD.Value = int.Parse(ConfigurationManager.AppSettings["RedLogo"]); //Modify data in the config file for future executions.
			GreenNextNUD.Value = int.Parse(ConfigurationManager.AppSettings["GreenLogo"]); //Modify data in the config file for future executions.
			BlueNextNUD.Value = int.Parse(ConfigurationManager.AppSettings["BlueLogo"]); //Modify data in the config file for future executions.

			RedNowNUD.Value = actualColours[0]; //Modify data in the config file for future executions.
			GreenNowNUD.Value = actualColours[1]; //Modify data in the config file for future executions.
			BlueNowNUD.Value = actualColours[2]; //Modify data in the config file for future executions.
		}

		/// <summary>
		/// Resets all the CMS to normal ordering, and if actualSelects also the variables that hold the actualOrder aand actualColumn....
		/// </summary>
		/// <param name="actualSelects"></param>
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

		/// <summary>
		/// Unchecks all the CMS...
		/// </summary>
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

		/// <summary>
		/// Opens your webbrowser with the link to get help with rgb, otherwise copies that link to the clipboard and informs the user.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Takes the selected RGB and gets the RGB values for the 3 panels of the main view, and closes the window....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Handles the addition of a new config....opens the form and when it is closed retrieves the values and saves the values depending of the user needs....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddButton_Click(object sender, EventArgs e)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			//Open window with the data available....
			GUI.AddColorConfig add = new(values)
			{
				BackColor = this.BackColor
			};
			add.ShowDialog();

			if (add.AddedSuccess) //If everything went ok (no altf4 or something strange)
			{
				var data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

				if (add.Chosen == 1) //User wants to use the config, but we dont know if just for this execution or future ones
				{
					if (add.Persists == 1)  //User wants the config to stay for future executions
					{
						RedNextNUD.Value = add.Red;
						GreenNextNUD.Value = add.Green;
						BlueNextNUD.Value = add.Blue;

						FinalCalibratedColours = Utils.IntUtils.CalibrateAllColours(add.Red, add.Green, add.Blue); //Calibrate and save values for future executions

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

					//User may have wanted to use this config for future executions, but if it is chosen then at least he wants to use it for this execution
					RedNowNUD.Value = add.Red;
					GreenNowNUD.Value = add.Green;
					BlueNowNUD.Value = add.Blue;

					actualColours = new int[] { add.Red, add.Green, add.Blue }; //New actualcolours for this execution
					FinalCalibratedColours = Utils.IntUtils.CalibrateAllColours(add.Red, add.Green, add.Blue);
				}

				data.Add(key: add.name, value: new List<int> { add.Red, add.Green, add.Blue, add.Favourite }); //Add the new config and save it
				string newData = JsonSerializer.Serialize(data);

				Config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				Config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched) //If there was a search ongoing, redo it....
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

		/// <summary>
		/// Handles the editing of a new config....opens the form and when it is closed retrieves the values and saves the new valuess....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditButton_Click(object sender, EventArgs e)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			//Get the name of the actual configuration used in this execution
			var actualNameChosen = ColourContentDGV.Rows.Cast<DataGridViewRow>().FirstOrDefault(
				row => row.Cells["RedColumn"].Value.ToString() == RedNowNUD.Value.ToString() &&
						row.Cells["GreenColumn"].Value.ToString() == GreenNowNUD.Value.ToString() &&
						row.Cells["BlueColumn"].Value.ToString() == BlueNowNUD.Value.ToString())?
						.Cells["ConfigNameColumn"].Value.ToString();

			//Opens the form and gives it the needed parameters
			GUI.EditColorConfig edit = new(values, actualNameChosen)
			{
				BackColor = this.BackColor
			};
			edit.ShowDialog();

			if (edit.EditedSuccess) //If everything went ok (no altf4 or something strange)
			{
				var data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);
				FinalCalibratedColours = Utils.IntUtils.CalibrateAllColours(edit.Red, edit.Green, edit.Blue);

				if (edit.Chosen == 1) //User wants to use the config in this execution, but dunno about next executions
				{
					if (edit.Persists == 1) //Users want to use config also for future executions...so set the NUDs and save the data.
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

					//New actual colours for the new chosen configuration
					actualColours = new int[] { edit.Red, edit.Green, edit.Blue };

					RedNowNUD.Value = edit.Red;
					GreenNowNUD.Value = edit.Green;
					BlueNowNUD.Value = edit.Blue;

				}

				data.Remove(edit.Oldname); //Remove previous configuration with that name
				data.Add(key: edit.name, value: new List<int> { edit.Red, edit.Green, edit.Blue, edit.Favourite }); //Insert a new configuration with the new data....
				string newData = JsonSerializer.Serialize(data);

				Config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				Config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched) //If search was ongoing, redo search
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

		/// <summary>
		/// Handles the deletion of one or all configs saved...opens the form and when it is closed retrieves the values and depending on the mode of deletion proceeds....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteButton_Click(object sender, EventArgs e)
		{
			Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

			var namesList = new List<String>(values.Keys); //All the names of saved configs
			//Get the name of the actual config used in this execution
			var actualNameChosen = ColourContentDGV.Rows.Cast<DataGridViewRow>().FirstOrDefault( 
				row => row.Cells["RedColumn"].Value.ToString() == RedNowNUD.Value.ToString() &&
						row.Cells["GreenColumn"].Value.ToString() == GreenNowNUD.Value.ToString() &&
						row.Cells["BlueColumn"].Value.ToString() == BlueNowNUD.Value.ToString())?
						.Cells["ConfigNameColumn"].Value.ToString();

			//Opens form and send corresponding parameters to it...
			GUI.DeleteColorConfig del = new(namesList, actualNameChosen)
			{
				BackColor = this.BackColor,
				Enabled = true
			};
			del.ShowDialog();

			if (del.DeletedSuccess) //User wants to delete one colour config
			{
				values.Remove(del.name); //Removes the config
				if(values.Count < 1) { values.Add("Default", new List<int> { 0, 191, 144, 1 }); } //If it was the last saved config, readd the default config....

				String newData = JsonSerializer.Serialize(values);

				Config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				Config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched) //If search was ongoing, redo it
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

			}
			else if (del.DeletedAllSuccess) //User wants to delete all configurations
			{
				values.Clear(); //Delete all configs
				values.Add("Default", new List<int> { 0, 191, 144, 1 }); //Add the default configuration

				String newData = JsonSerializer.Serialize(values);

				Config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
				Config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched) //If search was ongoing, redo it
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

		/// <summary>
		/// Changes the components theme when general theme is changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Handles the actions when the user clicks a cell in the ColourContentDGV component
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
						//All columns above just copy the text of the button to clipboard....
						Clipboard.SetText(!string.IsNullOrEmpty(ColourContentDGV.CurrentCell.Value.ToString()) ? ColourContentDGV.CurrentCell.Value.ToString() : " ");
						break;
					case "ChosenConfig": //User wants to use this config at least in this execution
						if ((ColourContentDGV.CurrentCell != null) && (!Convert.ToBoolean(ColourContentDGV.CurrentCell.Value))) //If checkbox is not checked then we do something...
						{
							var row = ColourContentDGV.Rows[e.RowIndex];
							var dialog = MessageBox.Show(text: "Do you want to select and use the config with name '" + row.Cells[0].Value.ToString() + "'?", caption: "Select and Use Config", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
							if (dialog == DialogResult.Yes) //User indeed wants to use this config at least in this execution
							{
								Dictionary<String, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

								DialogResult dialog2 = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
								if (dialog2 == DialogResult.Yes) //User wants to use this config in next executions also
								{
									RedNextNUD.Value = data[row.Cells[0].Value.ToString()][0];
									GreenNextNUD.Value = data[row.Cells[0].Value.ToString()][1];
									BlueNextNUD.Value = data[row.Cells[0].Value.ToString()][2];
									RedNowNUD.Value = data[row.Cells[0].Value.ToString()][0];
									GreenNowNUD.Value = data[row.Cells[0].Value.ToString()][1];
									BlueNowNUD.Value = data[row.Cells[0].Value.ToString()][2];

									actualColours = new int[] { data[row.Cells[0].Value.ToString()][0], data[row.Cells[0].Value.ToString()][1], data[row.Cells[0].Value.ToString()][2] }; //set new actual colours

									FinalCalibratedColours = Utils.IntUtils.CalibrateAllColours(data[row.Cells[0].Value.ToString()][0], data[row.Cells[0].Value.ToString()][1], data[row.Cells[0].Value.ToString()][2]); //calibrate for the three panels, then save
									
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
									if (isSearched) //If search was ongoing, redo it
									{
										SearchButton.PerformClick();
									}
									else
									{
										//Load the ordered content depending on column and order, and set toolstrip check property.
										LoadContent(actualColumn, actualOrder);
									}
								}
								else //User just wants to use it for this execution
								{
									RedNowNUD.Value = data[row.Cells[0].Value.ToString()][0];
									GreenNowNUD.Value = data[row.Cells[0].Value.ToString()][1];
									BlueNowNUD.Value = data[row.Cells[0].Value.ToString()][2];

									actualColours = new int[] { data[row.Cells[0].Value.ToString()][0], data[row.Cells[0].Value.ToString()][1], data[row.Cells[0].Value.ToString()][2] }; //set new actual colours

									FinalCalibratedColours = Utils.IntUtils.CalibrateAllColours(data[row.Cells[0].Value.ToString()][0], data[row.Cells[0].Value.ToString()][1], data[row.Cells[0].Value.ToString()][2]); //calibrate for the three panels

									ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
									if (isSearched) //If search was ongoing, then redo it
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
					case "Favourite": //User wants to mark or unmark this as favourite
						if (Convert.ToBoolean(ColourContentDGV.CurrentCell.Value)) //If checkbox is checked....
						{
							var row = ColourContentDGV.Rows[e.RowIndex];
							var dialog = MessageBox.Show(text: "Do you want to unmark the config with name '" + row.Cells[0].Value.ToString() + "' as favourite?", caption: "Unmark config as favourite.", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);

							if (dialog == DialogResult.Yes) //User wants to unmark config as favourite
							{
								Dictionary<String, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

								data[row.Cells[0].Value.ToString()][3] = 0; //Unset favourite

								Config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(data); ; //Modify data in the config file for future executions.
								Config.Save(ConfigurationSaveMode.Modified);
								ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

								ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
								if (isSearched) //If search was ongoing, then redo it
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

							if (dialog == DialogResult.Yes) //User wants to mark this config as favourite
							{
								Dictionary<String, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

								data[row.Cells[0].Value.ToString()][3] = 1; //Set favourite in the row...

								Config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(data); ; //Modify data in the config file for future executions.
								Config.Save(ConfigurationSaveMode.Modified);
								ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

								ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
								if (isSearched) //If search was ongoing, then redo it
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
					case "Delete Row": //User wants to delete that colour configuration
						var rowToBeDeleted = ColourContentDGV.Rows[e.RowIndex];
						var deleteDialog = MessageBox.Show(text: "Do you want to delete the config with name '" + rowToBeDeleted.Cells[0].Value.ToString() + "'? \n\nNote: This action cannot be undone.", caption: "Delete config.", icon: MessageBoxIcon.Exclamation, buttons: MessageBoxButtons.YesNo);

						if (deleteDialog == DialogResult.Yes) //User wants to delete that colour config
						{
							Dictionary<String, List<int>> data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

							data.Remove(rowToBeDeleted.Cells[0].Value.ToString()); //Remove row from data

							Config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(data); //Modify data in the config file for future executions.
							Config.Save(ConfigurationSaveMode.Modified);
							ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

							ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
							if (isSearched) //If search was ongoing, then redo it
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

		/// <summary>
		/// Sets the contents for the CMS of each header button (except Viewer, ChosenConfig and DeleteRow)
		/// </summary>
		[SupportedOSPlatform("windows")]
		private void SetCMS()
		{
			var titleName = new ToolStripLabel("ORDER BY NAME") //Create CMS for Name
			{
				Font = new Font("Segoe UI", 10, FontStyle.Bold),
				TextAlign = ContentAlignment.MiddleCenter,
				ForeColor = Color.FromArgb(109, 109, 109)
			};
			NameCMS.Items.Insert(0, titleName);

			var titleRed = new ToolStripLabel("ORDER BY RED") //Create CMS for Red
			{
				Font = new Font("Segoe UI", 10, FontStyle.Bold),
				TextAlign = ContentAlignment.MiddleCenter,
				ForeColor = Color.FromArgb(109, 109, 109)
			};
			RedCMS.Items.Insert(0, titleRed);

			var titleGreen = new ToolStripLabel("ORDER BY GREEN") //Create CMS for Green
			{
				Font = new Font("Segoe UI", 10, FontStyle.Bold),
				TextAlign = ContentAlignment.MiddleCenter,
				ForeColor = Color.FromArgb(109, 109, 109)
			};
			GreenCMS.Items.Insert(0, titleGreen);

			var titleBlue = new ToolStripLabel("ORDER BY BLUE") //Create CMS for Blue
			{
				Font = new Font("Segoe UI", 10, FontStyle.Bold),
				TextAlign = ContentAlignment.MiddleCenter,
				ForeColor = Color.FromArgb(109, 109, 109)
			};
			BlueCMS.Items.Insert(0, titleBlue);

			var titleFavourite = new ToolStripLabel("ORDER BY FAVOURITE") //Create CMS for Favourite
			{
				Font = new Font("Segoe UI", 10, FontStyle.Bold),
				TextAlign = ContentAlignment.MiddleCenter,
				ForeColor = Color.FromArgb(109, 109, 109)
			};
			FavouriteCMS.Items.Insert(0, titleFavourite);
		}

		/// <summary>
		/// If a column header is clicked and it has associated CMS then show that CMS...(for Name, Red, Green, Blue, Favourite)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the normal order of column Name
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NameNormalCMS_Click(object sender, EventArgs e)
		{
			try
			{
				//Sets order for then calling the loading metthod
				actualOrder = Order.Normal;
				actualColumn = CFColumns.Name;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched) //If search was ongoing, then redo search
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				ResetToNormalOrdering(false); //Set all the CMS to normal, as if you are ordering by normal it is in reality for all columns and not just for name
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
				ResetToNormalOrdering(true); //Any error should reset actual orderings and all CMS to normal mode....
			}
		}

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the ascending order of column Name
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NameAscendingCMS_Click(object sender, EventArgs e)
		{
			try
			{
				//Sets order for then calling the loading method
				actualOrder = Order.Asc;
				actualColumn = CFColumns.Name;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched) //If search was ongoing, then redo search
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				UncheckOrdering(); //Unchecks all CMS.
				NameAscendingCMS.Checked = true; //Checks the CMS for the ordering we are doing.
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
				ResetToNormalOrdering(true); //Any error should reset actual orderings and all CMS to normal mode....
			}
		}

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the descending order of column Name
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NameDescendingCMS_Click(object sender, EventArgs e)
		{
			try
			{
				//Sets order for then calling the loading method
				actualOrder = Order.Desc;
				actualColumn = CFColumns.Name;

				ColourContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched) //If search was ongoing then redo it
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(actualColumn, actualOrder);
				}

				UncheckOrdering(); //Unchecks all CMS.
				NameDescendingCMS.Checked = true; //Checks the CMS for the ordering we are doing.
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
				ResetToNormalOrdering(true); //Any error should reset actual orderings and all CMS to normal mode....
			}
		}

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the normal order of column Red
		/// Review documentation for method NameNormalCMS_Click(), as it is really similar to this one...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the ascending order of column Red
		/// Review documentation for method NameAscendingCMS_Click(), as it is really similar to this one...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the descending order of column Red
		/// Review documentation for method NameDescendingCMS_Click(), as it is really similar to this one...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the normal order of column Green
		/// Review documentation for method NameNormalCMS_Click(), as it is really similar to this one...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the ascending order of column Green
		/// Review documentation for method NameAscendingCMS_Click(), as it is really similar to this one...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the descending order of column Green
		/// Review documentation for method NameDescendingCMS_Click(), as it is really similar to this one...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the normal order of column Blue
		/// Review documentation for method NameNormalCMS_Click(), as it is really similar to this one...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the ascending order of column Blue
		/// Review documentation for method NameAscendingCMS_Click(), as it is really similar to this one...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the descending order of column Blue
		/// Review documentation for method NameDescendingCMS_Click(), as it is really similar to this one...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the normal order of column Favourite
		/// Review documentation for method NameNormalCMS_Click(), as it is really similar to this one...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the ascending order of column Favourite
		/// Review documentation for method NameAscendingCMS_Click(), as it is really similar to this one...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Orders the content of the ColourConfigDGV by the descending order of column Favourite
		/// Review documentation for method NameDescendingCMS_Click(), as it is really similar to this one...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		//Mouse enters button underlines button text
		[SupportedOSPlatform("windows")]
		private void ExportButton_MouseEnter(object sender, EventArgs e)
		{
			ExportButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void ExportButton_MouseLeave(object sender, EventArgs e)
		{
			ExportButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		//Mouse enters button underlines button text
		[SupportedOSPlatform("windows")]
		private void SendButton_MouseEnter(object sender, EventArgs e)
		{
			SendButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void SendButton_MouseLeave(object sender, EventArgs e)
		{
			SendButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		//Mouse enters button underlines button text
		[SupportedOSPlatform("windows")]
		private void DeleteButton_MouseEnter(object sender, EventArgs e)
		{
			DeleteButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void DeleteButton_MouseLeave(object sender, EventArgs e)
		{
			DeleteButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		//Mouse enters button underlines button text
		[SupportedOSPlatform("windows")]
		private void EditButton_MouseEnter(object sender, EventArgs e)
		{
			EditButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void EditButton_MouseLeave(object sender, EventArgs e)
		{
			EditButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		//Mouse enters button underlines button text
		[SupportedOSPlatform("windows")]
		private void AddButton_MouseEnter(object sender, EventArgs e)
		{
			AddButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void AddButton_MouseLeave(object sender, EventArgs e)
		{
			AddButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		/// <summary>
		/// Shows the form where user can select whether to export the configs as JSON file or PDF file....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExportButton_Click(object sender, EventArgs e)
		{
			ExportVaultConfigs export = new()
			{
				BackColor = this.BackColor
			};
			export.ShowDialog();
		}

		//Mouse enters button underlines button text
		[SupportedOSPlatform("windows")]
		private void HelpButton_MouseEnter(object sender, EventArgs e)
		{
			HelpFormButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the butto
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void HelpButton_MouseLeave(object sender, EventArgs e)
		{
			HelpFormButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		/// <summary>
		/// Shows the help form with info about how to handle this view of colour configurations
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HelpButton_Click(object sender, EventArgs e)
		{
			GUI.HelpColourConfigsForm helpForm = new()
			{
				BackColor = this.BackColor
			};
			helpForm.ShowDialog();
		}

		//Mouse enters button underlines button text
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

		/// <summary>
		/// Handles the import of colour configurations from a JSON file, selects correct json file, validates its content and saves it for future executions as well as shows it in the ColourContentDGV....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImportButton_Click(object sender, EventArgs e)
		{
			DialogResult confirmation = MessageBox.Show(text: "If you import configurations from a file, your saved configurations will be replaced by the imported configurations, and cannot be recovered.\n\nAre you sure you want to continue with the import process?", caption: "Import confirmation", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);
			if (confirmation == DialogResult.Yes) //User wants to import data from JSON file
			{
				//Part1: Select JSON file

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

				//Part2: If there is a path for selected file and user didnt exit the folder selector dialog with altF4 or something...., validate the data form (structure of json) and content
				if (filepath != "") 
				{
					if (ValidateJsonFormat(File.ReadAllText(filepath))) //Validate the format of the content, not the content....
					{
						// Deserialize JSON content into an object
						var goodFormData = JsonSerializer.Deserialize<Dictionary<string, List<int>>>(File.ReadAllText(filepath));
						var newData = ValidateAndGetJsonContent(goodFormData); //Validate file content
						if (newData != null) //We can either have empty data or values
						{
							if(newData.Count == 0) //If empty data then add default value...
							{
								newData.Add("Default", new List<int> { 0, 191, 144, 1 });
							}
							Config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(newData); //Modify data in the config file for future executions.
							Config.Save(ConfigurationSaveMode.Modified);
							ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

							ColourContentDGV.Rows.Clear();
							LoadContent(actualColumn, actualOrder); //Load new content saved
							//When we import, I assume we want to see all the content and not the result of a previous search from a previous set of configs....
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

		/// <summary>
		/// Just validates json format, not content, by parsing the string rawdata to a dictionary object, which has similar structure to a json....
		/// </summary>
		/// <param name="rawData"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Validates the content of the JSON file, needs to fulfill these requirements:
		///		Key must be a string and value must be a list of just 4 integers
		///		3 first integers in list must be between 0 and 255
		///		4th integer must be either 0 or 1.
		///		With those above conditions we would add the config, but we need to check also if the rgbs or the name are already in the list we are building to later display in the ColourContentDGV
		///			Summarizing, we need to check the conditions we would check in the AddColorConfig view....
		/// </summary>
		/// <param name="rawData"></param>
		/// <returns></returns>
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
					bool areValuesValid = CheckAddConfigConditions(result, key, values); //Check add conditions in regard to the rest of evaluated configs....
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

		/// <summary>
		/// Checks adding conditions of a new config with regard to a list of existing configs....
		///		Checks if name is already in and not null or whitespaces....
		///		Checks if rgb config is already there....
		///		Checks if config is valid colour (not too bright or dark)
		/// </summary>
		/// <param name="content"></param>
		/// <param name="key"></param>
		/// <param name="values"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Applies a filter on the contents of the ColourContentDGV with the specified searching parameters for the config name
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SearchButton_Click(object sender, EventArgs e)
		{
			TrimComponents();

			//Reset content, so that you dont search content on the previously searched ocntent....
			ColourContentDGV.Rows.Clear();
			LoadContent(actualColumn, actualOrder);

			List<DataGridViewRow> matchingRows = new();

			//Get the matching names based on the filter
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

			ResetButton.Enabled = true; //Enable reset as we have done a search
			isSearched = true; //We have done a search, it is ongoing....
		}

		//Mouse enters button underlines button text
		[SupportedOSPlatform("windows")]
		private void SearchButton_MouseEnter(object sender, EventArgs e)
		{
			SearchButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Underline the text when mouse leaves
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void SearchButton_MouseLeave(object sender, EventArgs e)
		{
			SearchButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		//Mouse enters button underlines button text
		[SupportedOSPlatform("windows")]
		private void ResetButton_MouseEnter(object sender, EventArgs e)
		{
			ResetButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Underline the text when mouse leaves
		}

		//Mouse leaves button regularises button text
		[SupportedOSPlatform("windows")]
		private void ResetButton_MouseLeave(object sender, EventArgs e)
		{
			ResetButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		/// <summary>
		/// Loads all the saved content, and resets the search parameters
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResetButton_Click(object sender, EventArgs e)
		{
			ColourContentDGV.Rows.Clear();
			SearchTextbox.Text = string.Empty;
			LoadContent(actualColumn, actualOrder);

			ResetButton.Enabled = false;
			isSearched = false; //We arent in a search anymore
		}

		/// <summary>
		/// If there are contents in the textbox, then we can enable the search button to search that content....
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
