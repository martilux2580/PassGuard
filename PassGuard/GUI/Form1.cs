using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Runtime.Versioning;
using iText.Layout.Splitting;
using PassGuard.PDF;

namespace PassGuard
{
	//MainWindow of the Application
	public partial class mainWindow : Form
	{

		internal Task autobackup = null;

		public mainWindow()
		{
			InitializeComponent();
			this.Size = this.MinimumSize; //We init the form with the minimum size to avoid Minimum Size bug (setting Min Size in Properties to the actual size makes Minimum Size decrease by 20-30 pixels aprox).
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//Add content to ContentPanel.
			var hc = new GUI.HomeContentUC();
			ContentPanel.Controls.Add(hc);

			try
			{
				this.Icon = Properties.Resources.LogoIcon64123; //Loads Icon from Image folder. //LogoIcon64.ico
			}
			catch(FileNotFoundException)
			{
				MessageBox.Show(text: "PassGuard could not load some images.", caption: "Images not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}

			try
			{
				AppVersionLabel.Text = ConfigurationManager.AppSettings["AppVersion"];
				SetConfigTheme(); //Set theme based on saved config.
				SetConfigColours(); //Set outline colours based on saved config.

				//Code for regulating AutoBackup when it is enabled and has a time frequency (every day, week or month).
				int[] timeCodes = new int[] { 3, 4, 5 }; //Modes for everyday, week or month.
				//If Autobackup is activated and has time frequency, start a task with the function to check every time if a backup has to be made.
				if ((ConfigurationManager.AppSettings["AutoBackupState"] == "true") && timeCodes.Contains(Int32.Parse(ConfigurationManager.AppSettings["FrequencyAutoBackup"])))
				{
					autobackup = Task.Factory.StartNew(() => Backup.SystemBackup.AutoBackupTime()); //Start a task with a method
				}
			}
			catch (ConfigurationErrorsException)
			{
				MessageBox.Show(text: "PassGuard could not access config file, some features like colours setups or AutoBackup could not be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
				//Not controlling In32.Parse exceptions, if config works the data will always be a number in string format, it will parse it correctly.
			}

		}

		//Obtain data from previous executions, and set theme from saved config.
		private void SetConfigTheme()
		{
			String sAttr = ConfigurationManager.AppSettings["Theme"];
			if (sAttr == "Dark") //Change theme color depending on the backcolor of the app.
			{
				darkToolStripMenuItem.Checked = true;
				lightToolStripMenuItem.Checked = false;
				ContentPanel.BackColor = Color.FromArgb(116, 118, 117); 
			}
			else if (sAttr == "Light")
			{
				lightToolStripMenuItem.Checked = true;
				darkToolStripMenuItem.Checked = false;
				ContentPanel.BackColor = Color.FromArgb(230, 230, 230);
			}

		}

		//Obtain data from previous executions, and set theme from saved config.
		private void SetConfigColours()
		{
			int getRedMenu = Int32.Parse(ConfigurationManager.AppSettings["RedMenu"]);
			int getGreenMenu = Int32.Parse(ConfigurationManager.AppSettings["GreenMenu"]);
			int getBlueMenu = Int32.Parse(ConfigurationManager.AppSettings["BlueMenu"]);
			int getRedLogo = Int32.Parse(ConfigurationManager.AppSettings["RedLogo"]);
			int getGreenLogo = Int32.Parse(ConfigurationManager.AppSettings["GreenLogo"]);
			int getBlueLogo = Int32.Parse(ConfigurationManager.AppSettings["BlueLogo"]);
			int getRedOptions = Int32.Parse(ConfigurationManager.AppSettings["RedOptions"]);
			int getGreenOptions = Int32.Parse(ConfigurationManager.AppSettings["GreenOptions"]);
			int getBlueOptions = Int32.Parse(ConfigurationManager.AppSettings["BlueOptions"]);

			MenuPanel.BackColor = Color.FromArgb(getRedMenu, getGreenMenu, getBlueMenu);
			LogoPanel.BackColor = Color.FromArgb(getRedLogo, getGreenLogo, getBlueLogo);
			OptionsPanel.BackColor = Color.FromArgb(getRedOptions, getGreenOptions, getBlueOptions);

		}

		private void CreateVaultButton_Click(object sender, EventArgs e)
		{
			TitleLabel.Text = "CREATING A NEW PASSWORD VAULT"; //Change Title
			GUI.CreateNewVaultUC cnv = new(); //Set new UC for the action.
			ContentPanel.Controls.Clear(); //Clear everything inside the Content Panel
			ContentPanel.Controls.Add(cnv); //Add the UC to the panel
		}

		[SupportedOSPlatform("windows")]
		private void CreateVaultButton_MouseEnter(object sender, EventArgs e)
		{
			CreateVaultButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void CreateVaultButton_MouseLeave(object sender, EventArgs e)
		{
			CreateVaultButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular); //Regularise the text when mouse is not in the button
		}

		private void LoadVaultButton_Click(object sender, EventArgs e)
		{
			TitleLabel.Text = "LOADING A PASSWORD VAULT"; //Change Title
			GUI.LoadVaultUC lv = new(false); //Set new UC for the action.
			ContentPanel.Controls.Clear(); 
			ContentPanel.Controls.Add(lv);
		}

		[SupportedOSPlatform("windows")]
		private void LoadVaultButton_MouseEnter(object sender, EventArgs e)
		{
			LoadVaultButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void LoadVaultButton_MouseLeave(object sender, EventArgs e)
		{
			LoadVaultButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular); //Regularise the text when mouse is not in the button
		}

		[SupportedOSPlatform("windows")]
		private void DesignerLabel_MouseEnter(object sender, EventArgs e)
		{
			DesignerLabel.Font = new Font("Mongolian Baiti", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void DesignerLabel_MouseLeave(object sender, EventArgs e)
		{
			DesignerLabel.Font = new Font("Mongolian Baiti", 10, FontStyle.Regular); //Regularise the text when mouse is not in the button
		}

		private void DesignerLabel_MouseClick(object sender, MouseEventArgs e)
		{
			string url = "https://github.com/martilux2580?tab=repositories";
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

		private void LogoPictureBox_MouseClick(object sender, MouseEventArgs e)
		{
			GUI.HomeContentUC hc = new()
			{
				Visible = false
			}; //Put the main panel visible.
			TitleLabel.Text = "HOME";
			ContentPanel.Controls.Clear();
			ContentPanel.Controls.Add(hc);
			hc.Visible = true;

		}

		private void SettingButton_Click(object sender, EventArgs e)
		{
			SettingsCMS.Show(SettingButton, new Point(SettingButton.Width - SettingsCMS.Width, SettingButton.Height)); //Sets where to display the ContextMenuStrip...
			
		}

		private void changeComplemenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				//Open the window with the correspondent theme.
				Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

				int[] actualColours = new int[3] { (int)LogoPanel.BackColor.R, (int)LogoPanel.BackColor.G, (int)LogoPanel.BackColor.B }; //Create array with actual colours to send it to the form.
				GUI.AskRGBforSettings rgb = new(actualColours, config); //Dialog to insert rgb values
				if (darkToolStripMenuItem.Checked == true) //Change theme color depending on the backcolor of the app.
				{
					rgb.BackColor = Color.FromArgb(116, 118, 117); //Set Color for the RGB selection popup window.
				}
				else if (lightToolStripMenuItem.Checked == true)
				{
					rgb.BackColor = Color.FromArgb(230, 230, 230); //Set Color for the RGB selection popup window.
				}
				rgb.ShowDialog();  //Show dialog


				//Get values
				//If no changes were made, exit.
				int[] newRGB = new int[3] { rgb.finalRed, rgb.finalGreen, rgb.finalBlue };
				if (newRGB.SequenceEqual(actualColours))
				{
					return;
				}
				else
				{
					int[] newValues = Utils.IntUtils.calibrateColours(newRGB[0], newRGB[1], newRGB[2]); //Calibrate values and obtain them.

					//Save values for future executions, or not.
					DialogResult dialog = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", buttons: MessageBoxButtons.YesNo);
					if (dialog == DialogResult.Yes)
					{
						config.AppSettings.Settings["RedMenu"].Value = newValues[0].ToString(); //Modify data in the config file for future executions.
						config.AppSettings.Settings["GreenMenu"].Value = newValues[1].ToString();
						config.AppSettings.Settings["BlueMenu"].Value = newValues[2].ToString();
						config.AppSettings.Settings["RedLogo"].Value = newValues[3].ToString();
						config.AppSettings.Settings["GreenLogo"].Value = newValues[4].ToString();
						config.AppSettings.Settings["BlueLogo"].Value = newValues[5].ToString();
						config.AppSettings.Settings["RedOptions"].Value = newValues[6].ToString();
						config.AppSettings.Settings["GreenOptions"].Value = newValues[7].ToString();
						config.AppSettings.Settings["BlueOptions"].Value = newValues[8].ToString();
						config.Save(ConfigurationSaveMode.Modified);
						ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.
					}

					MenuPanel.BackColor = Color.FromArgb(newValues[0], newValues[1], newValues[2]);
					LogoPanel.BackColor = Color.FromArgb(newValues[3], newValues[4], newValues[5]);
					OptionsPanel.BackColor = Color.FromArgb(newValues[6], newValues[7], newValues[8]);

				}

			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not access config file, this feature can´t be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		private void darkToolStripMenuItem_Click(object sender, EventArgs e) //Check dark toolstrip, uncheck light one and change colors
		{
			try
			{
				Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

				DialogResult dialog = MessageBox.Show(text: "Would you like to save this theme configuration for next executions?", caption: "Save theme configuration", buttons: MessageBoxButtons.YesNo);
				if (dialog == DialogResult.Yes)
				{
					config.AppSettings.Settings["Theme"].Value = "Dark"; //Modify data in the config file for future executions.
					config.Save(ConfigurationSaveMode.Modified);
					ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.
				}
				darkToolStripMenuItem.Checked = true;
				lightToolStripMenuItem.Checked = false;
				ContentPanel.BackColor = Color.FromArgb(116, 118, 117);

			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not access config file, this feature can´t be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}

		}

		private void lightToolStripMenuItem_Click(object sender, EventArgs e) //Check light toolstrip, uncheck dark one and change colors
		{
			try
			{
				Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

				DialogResult dialog = MessageBox.Show(text: "Would you like to save this theme configuration for next executions?", caption: "Save theme configuration", buttons: MessageBoxButtons.YesNo);
				if (dialog == DialogResult.Yes)
				{
					config.AppSettings.Settings["Theme"].Value = "Light"; //Modify data in the config file for future executions.
					config.Save(ConfigurationSaveMode.Modified);
					ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.
				}
				lightToolStripMenuItem.Checked = true;
				darkToolStripMenuItem.Checked = false;
				ContentPanel.BackColor = Color.FromArgb(230, 230, 230);
			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not access config file, this feature can´t be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		private void saveChangesClosePassGuardToolStripMenuItem_Click(object sender, EventArgs e) //Exit app saving changes (pending)
		{
			//Implement "Save Changes" Part? Things already are saved in each change by encrypting again.
			Application.Exit(); //Close Application
		}

		[SupportedOSPlatform("windows")]
		private void CreateQuickPassButton_MouseEnter(object sender, EventArgs e)
		{
			CreateQuickPassButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void CreateQuickPassButton_MouseLeave(object sender, EventArgs e)
		{
			CreateQuickPassButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular); //Regularise the text when mouse is not in the button
		}

		private void CreateQuickPassButton_Click(object sender, EventArgs e)
		{
			TitleLabel.Text = "CREATING SAFE PASSWORDS"; //Change text
			GUI.CreateQuickPassUC cqr = new(); //Set new UC for the action.
			ContentPanel.Controls.Clear();
			ContentPanel.Controls.Add(cqr);
		}

		private void createABackupOfYourVaultToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GUI.CreateBackup cb = new()
			{
				BackColor = this.ContentPanel.BackColor
			};
			cb.ShowDialog();

			if (cb.success)
			{
				MessageBox.Show(text: "Backup of the selected Vault was created successfully :)", caption: "Success", buttons: MessageBoxButtons.OK); 
				
			}

		}

		private void configureAnAutoBackupOfAVaultToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				var previousState = ConfigurationManager.AppSettings["AutoBackupState"];
				var previousFrequency = ConfigurationManager.AppSettings["FrequencyAutoBackup"];
				int[] timeCodes = new int[] { 3, 4, 5 }; //Codes for time modes (everyday, everyweek, everymonth)

				GUI.AutoBackup ab = new()
				{
					BackColor = this.ContentPanel.BackColor
				};
				ab.ShowDialog();

				if (ab.setupSuccess) //If we exited autobackup form from the bottom, then everything was set up correctly.
				{
					var newState = ab.AutoBackupState;
					var newVaultPath = ab.pathOfVaultBackedUp;
					var newBackupsPath = ab.pathForBackups;
					var newFrequencyAutoBackup = ab.frequencyBackup;
					var newLastDateBackup = ab.lastDateBackup;

					if (newState == "false") //Your new state is AutoBackup deactivated
					{
						Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
						config.AppSettings.Settings["AutoBackupState"].Value = newState; //Modify data in the config file for future executions.
						config.AppSettings.Settings["PathVaultForAutoBackup"].Value = newVaultPath; //Modify data in the config file for future executions.
						config.AppSettings.Settings["dstBackupPathForSave"].Value = newBackupsPath; //Modify data in the config file for future executions.
						config.AppSettings.Settings["LastDateAutoBackup"].Value = newLastDateBackup; //Modify data in the config file for future executions.
						config.AppSettings.Settings["FrequencyAutoBackup"].Value = newFrequencyAutoBackup; //Modify data in the config file for future executions.
						config.Save(ConfigurationSaveMode.Modified, true);
						ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

						try
						{
							if ((previousState == "true") && (timeCodes.Contains(Int32.Parse(previousFrequency)))) //If your previous frequency was a time one, the task is running, so stop it. If not, the task isnt running.
							{
								autobackup.Wait(millisecondsTimeout: 12000); //Wait until changes make task finish its work.
								autobackup.Dispose();
								GC.Collect();
							}
							autobackup = null;
						}
						catch (Exception ex)
						{
							if (ex is ObjectDisposedException || ex is ArgumentOutOfRangeException || ex is AggregateException || ex is InvalidOperationException)
							{
								MessageBox.Show(text: "AutoBackup process could not stop.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
							}
						}

					}
					else if (newState == "true")
					{
						if (previousState == "false") //Before, AutoBackup was deactivated, so task isnt running.
						{
							Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
							config.AppSettings.Settings["AutoBackupState"].Value = newState; //Modify data in the config file for future executions.
							config.AppSettings.Settings["PathVaultForAutoBackup"].Value = newVaultPath; //Modify data in the config file for future executions.
							config.AppSettings.Settings["dstBackupPathForSave"].Value = newBackupsPath; //Modify data in the config file for future executions.
							config.AppSettings.Settings["LastDateAutoBackup"].Value = newLastDateBackup; //Modify data in the config file for future executions.
							config.AppSettings.Settings["FrequencyAutoBackup"].Value = newFrequencyAutoBackup; //Modify data in the config file for future executions.
							config.Save(ConfigurationSaveMode.Modified, true);
							ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

							if (timeCodes.Contains(Int32.Parse(newFrequencyAutoBackup))) //Autobackup before wasnt set, so start a task.
							{
								autobackup = Task.Factory.StartNew(() => Backup.SystemBackup.AutoBackupTime()); //Start task with autobackup.
							}

						}
						else if (previousState == "true")
						{
							Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
							config.AppSettings.Settings["AutoBackupState"].Value = newState; //Modify data in the config file for future executions.
							config.AppSettings.Settings["PathVaultForAutoBackup"].Value = newVaultPath; //Modify data in the config file for future executions.
							config.AppSettings.Settings["dstBackupPathForSave"].Value = newBackupsPath; //Modify data in the config file for future executions.
							config.AppSettings.Settings["LastDateAutoBackup"].Value = newLastDateBackup; //Modify data in the config file for future executions.
							config.AppSettings.Settings["FrequencyAutoBackup"].Value = newFrequencyAutoBackup; //Modify data in the config file for future executions.
							config.Save(ConfigurationSaveMode.Modified, true);
							ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

							if (timeCodes.Contains(Int32.Parse(previousFrequency))) //Previous state was activated, we wait so that changes to config make effect and task is reset with new parameters.
							{
								autobackup.Wait(millisecondsTimeout: 5000); //Wait so that changes in config reach the task and its work is updated
							}
							else //Previous state was activated but with mode 1 or 2, so task is not active -> we have to run it..
							{
								autobackup = Task.Factory.StartNew(() => Backup.SystemBackup.AutoBackupTime()); //Start task with autobackup.

							}

						}
					}


					MessageBox.Show(text: "AutoBackup was configured successfully.", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
				}
			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not access config file, this feature can´t be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
		}

		private void mainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (ConfigurationManager.AppSettings["AutoBackupState"] == "true")
				{
					if (2 == Int32.Parse(ConfigurationManager.AppSettings["FrequencyAutoBackup"])) //If app is closing and the mode is 2 (after each close of app), make backup.
					{
						if (Backup.SystemBackup.CreateBackup(srcPath: ConfigurationManager.AppSettings["PathVaultForAutoBackup"], dstPath: ConfigurationManager.AppSettings["dstBackupPathForSave"]))
						{
							MessageBox.Show(text: "AutoBackup was created successfully.", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
						}
						else
						{
							MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault, please try again later.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
						}
					}

				}
			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not access config file, Autobackup could not check state of backup.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
			
		}

		private void exportOutlineColoursAsPDFToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IPDF pdf = new PDFCreator();
			pdf.CreateOutlinePDF();

		}

		private void exportAVaultsContentAsPDFToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TitleLabel.Text = "EXPORTING A VAULT AS PDF"; //Change Title
			GUI.ExportPdfFromSettings export = new(); //Set new UC for the action.
			ContentPanel.Controls.Clear();
			ContentPanel.Controls.Add(export);

		}
	}
}
