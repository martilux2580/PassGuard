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
using System.Web.Script.Serialization;

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

            Core.Utils utils = new Core.Utils();
            try
            {
                LogoPictureBox.Image = Image.FromFile(@".\Images\Logo123.png"); //Working Directory inside Release Folder. Loads Image from Image folder. //@"..\..\Images\Logo123.png"
                LogoPictureBox.SizeMode = PictureBoxSizeMode.Zoom; //PictureBoxSizeMode.Zoom
                this.Icon = new Icon(@".\Images\LogoIcon64123.ico"); //Loads Icon from Image folder. //LogoIcon64.ico
                SettingButton.Image = Image.FromFile(@".\Images\Setting.ico"); //Loads Image for the Settings Icon
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show(text: "PassGuard could not load some images.", caption: "Images not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }

            try
            {
                AppVersionLabel.Text = ConfigurationManager.AppSettings.Get("AppVersion");
                setConfigTheme(); //Set theme based on saved config.
                setConfigColours(); //Set outline colours based on saved config.

                //Code for regulating AutoBackup when it is enabled and has a time frequency (every day, week or month).
                int[] timeCodes = new int[] { 3, 4, 5 }; //Modes for everyday, week or month.
                //If Autobackup is activated and has time frequency, start a task with the function to check every time if a backup has to be made.
                if ((ConfigurationManager.AppSettings.Get("AutoBackupState") == "true") && timeCodes.Contains(Int32.Parse(ConfigurationManager.AppSettings.Get("FrequencyAutoBackup"))))
                {
                    autobackup = Task.Factory.StartNew(() => utils.AutoBackupTime()); //Start a task with a method
                }
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show(text: "PassGuard could not access config file, some features like colours setups or AutoBackup could not be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                //Not controlling In32.Parse exceptions, if config works the data will always be a number in string format, it will parse it correctly.
            }

        }

        //Obtain data from previous executions, and set theme from saved config.
        private void setConfigTheme()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            String sAttr = ConfigurationManager.AppSettings.Get("Theme");
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
        private void setConfigColours()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            int getRedMenu = Int32.Parse(ConfigurationManager.AppSettings.Get("RedMenu"));
            int getGreenMenu = Int32.Parse(ConfigurationManager.AppSettings.Get("GreenMenu"));
            int getBlueMenu = Int32.Parse(ConfigurationManager.AppSettings.Get("BlueMenu"));
            int getRedLogo = Int32.Parse(ConfigurationManager.AppSettings.Get("RedLogo"));
            int getGreenLogo = Int32.Parse(ConfigurationManager.AppSettings.Get("GreenLogo"));
            int getBlueLogo = Int32.Parse(ConfigurationManager.AppSettings.Get("BlueLogo"));
            int getRedOptions = Int32.Parse(ConfigurationManager.AppSettings.Get("RedOptions"));
            int getGreenOptions = Int32.Parse(ConfigurationManager.AppSettings.Get("GreenOptions"));
            int getBlueOptions = Int32.Parse(ConfigurationManager.AppSettings.Get("BlueOptions"));

            MenuPanel.BackColor = Color.FromArgb(getRedMenu, getGreenMenu, getBlueMenu);
            LogoPanel.BackColor = Color.FromArgb(getRedLogo, getGreenLogo, getBlueLogo);
            OptionsPanel.BackColor = Color.FromArgb(getRedOptions, getGreenOptions, getBlueOptions);

        }

        private void CreateVaultButton_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "CREATING A NEW PASSWORD VAULT"; //Change Title
            GUI.CreateNewVaultUC cnv = new GUI.CreateNewVaultUC(); //Set new UC for the action.
            ContentPanel.Controls.Clear(); //Clear everything inside the Content Panel
            ContentPanel.Controls.Add(cnv); //Add the UC to the panel
        }

        private void CreateVaultButton_MouseEnter(object sender, EventArgs e)
        {
            CreateVaultButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void CreateVaultButton_MouseLeave(object sender, EventArgs e)
        {
            CreateVaultButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular); //Regularise the text when mouse is not in the button
        }

        private void LoadVaultButton_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "LOADING A PASSWORD VAULT"; //Change Title
            GUI.LoadVaultUC lv = new GUI.LoadVaultUC(); //Set new UC for the action.
            ContentPanel.Controls.Clear(); 
            ContentPanel.Controls.Add(lv);
        }

        private void LoadVaultButton_MouseEnter(object sender, EventArgs e)
        {
            LoadVaultButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void LoadVaultButton_MouseLeave(object sender, EventArgs e)
        {
            LoadVaultButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular); //Regularise the text when mouse is not in the button
        }

        private void DesignerLabel_MouseEnter(object sender, EventArgs e)
        {
            DesignerLabel.Font = new Font("Mongolian Baiti", 10, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void DesignerLabel_MouseLeave(object sender, EventArgs e)
        {
            DesignerLabel.Font = new Font("Mongolian Baiti", 10, FontStyle.Regular); //Regularise the text when mouse is not in the button
        }

        private void DesignerLabel_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/martilux2580?tab=repositories"); //Open browser with webpage.
            }
            catch (Exception)
            {
                MessageBox.Show(text: "Webpage could not be opened.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }
        }

        private void LogoPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            GUI.HomeContentUC hc = new GUI.HomeContentUC(); //Put the main panel visible.
            hc.Visible = false;
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
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

                int[] actualColours = new int[3] { (int)LogoPanel.BackColor.R, (int)LogoPanel.BackColor.G, (int)LogoPanel.BackColor.B }; //Create array with actual colours to send it to the form.
                GUI.AskRGBforSettings rgb = new GUI.AskRGBforSettings(actualColours, config); //Dialog to insert rgb values
                if (darkToolStripMenuItem.Checked == true) //Change theme color depending on the backcolor of the app.
                {
                    rgb.BackColor = Color.FromArgb(116, 118, 117); //Set Color for the RGB selection popup window.
                }
                else if (lightToolStripMenuItem.Checked == true)
                {
                    rgb.BackColor = Color.FromArgb(230, 230, 230); //Set Color for the RGB selection popup window.
                }
                rgb.ShowDialog();  //Show dialog

                if (rgb.changedSuccess)
                {
                    int redValue = rgb.getRedNUDValue(); //Get rgb values
                    int greenValue = rgb.getGreenNUDValue();
                    int blueValue = rgb.getBlueNUDValue();
                    int newRedMenu, newGreenMenu, newBlueMenu = 0; //Organise them for config file.
                    int newRedLogo, newGreenLogo, newBlueLogo = 0;
                    int newRedOptions, newGreenOptions, newBlueOptions = 0;

                    //Correction of rgb values if higher than 235 and lower than 32, and then set the colors of the panels
                    if ((redValue > 235) && (greenValue > 235) && (blueValue > 235))
                    {
                        newRedMenu = newGreenMenu = newBlueMenu = 245;
                        newRedLogo = newGreenLogo = newBlueLogo = 255;
                        newRedOptions = newGreenOptions = newBlueOptions = 250;

                        DialogResult dialog = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", buttons: MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes) //Change config file and save values.
                        {
                            config.AppSettings.Settings["RedMenu"].Value = newRedMenu.ToString(); //Modify data in the config file for future executions.
                            config.AppSettings.Settings["GreenMenu"].Value = newGreenMenu.ToString();
                            config.AppSettings.Settings["BlueMenu"].Value = newBlueMenu.ToString();
                            config.AppSettings.Settings["RedLogo"].Value = newRedLogo.ToString();
                            config.AppSettings.Settings["GreenLogo"].Value = newGreenLogo.ToString();
                            config.AppSettings.Settings["BlueLogo"].Value = newBlueLogo.ToString();
                            config.AppSettings.Settings["RedOptions"].Value = newRedOptions.ToString();
                            config.AppSettings.Settings["GreenOptions"].Value = newGreenOptions.ToString();
                            config.AppSettings.Settings["BlueOptions"].Value = newBlueOptions.ToString();
                            config.Save(ConfigurationSaveMode.Modified);
                            ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.
                        }

                        MenuPanel.BackColor = Color.FromArgb(245, 245, 245); //Set colours.
                        LogoPanel.BackColor = Color.FromArgb(255, 255, 255);
                        OptionsPanel.BackColor = Color.FromArgb(250, 250, 250);
                    }
                    else //Correction of rgb values if they are too dark, so I can add 20 to diff between each panel and stil below 255.
                    {
                        if ((redValue > 235) || (greenValue > 235) || (blueValue > 235))
                        {
                            if (redValue > 235)
                            {
                                redValue = 235;
                            }
                            if (greenValue > 235)
                            {
                                greenValue = 235;
                            }
                            if (blueValue > 235)
                            {
                                blueValue = 235;
                            }
                        }
                        newRedMenu = redValue + 20;
                        newGreenMenu = greenValue + 20;
                        newBlueMenu = blueValue + 20;
                        newRedLogo = redValue; //Input in RGB Form will be for the logo, Menu and Options will be modified.
                        newGreenLogo = greenValue;
                        newBlueLogo = blueValue;
                        newRedOptions = redValue + 10;
                        newGreenOptions = greenValue + 10;
                        newBlueOptions = blueValue + 10;

                        DialogResult dialog = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", buttons: MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            config.AppSettings.Settings["RedMenu"].Value = newRedMenu.ToString(); //Modify data in the config file for future executions.
                            config.AppSettings.Settings["GreenMenu"].Value = newGreenMenu.ToString();
                            config.AppSettings.Settings["BlueMenu"].Value = newBlueMenu.ToString();
                            config.AppSettings.Settings["RedLogo"].Value = newRedLogo.ToString();
                            config.AppSettings.Settings["GreenLogo"].Value = newGreenLogo.ToString();
                            config.AppSettings.Settings["BlueLogo"].Value = newBlueLogo.ToString();
                            config.AppSettings.Settings["RedOptions"].Value = newRedOptions.ToString();
                            config.AppSettings.Settings["GreenOptions"].Value = newGreenOptions.ToString();
                            config.AppSettings.Settings["BlueOptions"].Value = newBlueOptions.ToString();
                            config.Save(ConfigurationSaveMode.Modified);
                            ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.
                        }

                        MenuPanel.BackColor = Color.FromArgb(newRedMenu, newGreenMenu, newBlueMenu);
                        LogoPanel.BackColor = Color.FromArgb(newRedLogo, newGreenLogo, newBlueLogo);
                        OptionsPanel.BackColor = Color.FromArgb(newRedOptions, newGreenLogo, newBlueLogo);
                    }
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
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

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
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

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

        private void CreateQuickPassButton_MouseEnter(object sender, EventArgs e)
        {
            CreateQuickPassButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void CreateQuickPassButton_MouseLeave(object sender, EventArgs e)
        {
            CreateQuickPassButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular); //Regularise the text when mouse is not in the button
        }

        private void CreateQuickPassButton_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "CREATING SAFE PASSWORDS"; //Change text
            GUI.CreateQuickPassUC cqr = new GUI.CreateQuickPassUC(); //Set new UC for the action.
            ContentPanel.Controls.Clear();
            ContentPanel.Controls.Add(cqr);
        }

        private void createABackupOfYourVaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI.CreateBackup cb = new GUI.CreateBackup();
            cb.BackColor = this.ContentPanel.BackColor;
            cb.ShowDialog();

            if (cb.getSuccess())
            {
                MessageBox.Show(text: "Backup of the selected Vault was created successfully :)", caption: "Success", buttons: MessageBoxButtons.OK); 
                
            }

        }

        private void configureAnAutoBackupOfAVaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Core.Utils utils = new Core.Utils();
                var previousState = ConfigurationManager.AppSettings.Get("AutoBackupState");
                var previousFrequency = ConfigurationManager.AppSettings.Get("FrequencyAutoBackup");
                int[] timeCodes = new int[] { 3, 4, 5 }; //Codes for time modes (everyday, everyweek, everymonth)

                GUI.AutoBackup ab = new GUI.AutoBackup(this);
                ab.BackColor = this.ContentPanel.BackColor;
                ab.ShowDialog();

                if (ab.GetSetupSuccess()) //If we exited autobackup form from the bottom, then everything was set up correctly.
                {
                    var newState = ab.GetAutoBackupState();
                    var newVaultPath = ab.GetPathOfVaultBackedUp();
                    var newBackupsPath = ab.GetPathForBackups();
                    var newFrequencyAutoBackup = ab.GetFrequencyBackup();
                    var newLastDateBackup = ab.GetLastDateBackup();

                    if (newState == "false") //Your new state is AutoBackup deactivated
                    {
                        Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
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
                            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                            config.AppSettings.Settings["AutoBackupState"].Value = newState; //Modify data in the config file for future executions.
                            config.AppSettings.Settings["PathVaultForAutoBackup"].Value = newVaultPath; //Modify data in the config file for future executions.
                            config.AppSettings.Settings["dstBackupPathForSave"].Value = newBackupsPath; //Modify data in the config file for future executions.
                            config.AppSettings.Settings["LastDateAutoBackup"].Value = newLastDateBackup; //Modify data in the config file for future executions.
                            config.AppSettings.Settings["FrequencyAutoBackup"].Value = newFrequencyAutoBackup; //Modify data in the config file for future executions.
                            config.Save(ConfigurationSaveMode.Modified, true);
                            ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

                            if (timeCodes.Contains(Int32.Parse(newFrequencyAutoBackup))) //Autobackup before wasnt set, so start a task.
                            {
                                autobackup = Task.Factory.StartNew(() => utils.AutoBackupTime()); //Start task with autobackup.
                            }

                        }
                        else if (previousState == "true")
                        {
                            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
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
                                autobackup = Task.Factory.StartNew(() => utils.AutoBackupTime()); //Start task with autobackup.

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
            Core.Utils utils = new Core.Utils();

            try
            {
                if (ConfigurationManager.AppSettings.Get("AutoBackupState") == "true")
                {
                    if (2 == Int32.Parse(ConfigurationManager.AppSettings.Get("FrequencyAutoBackup"))) //If app is closing and the mode is 2 (after each close of app), make backup.
                    {
                        if (utils.CreateBackup(srcPath: ConfigurationManager.AppSettings.Get("PathVaultForAutoBackup"), dstPath: ConfigurationManager.AppSettings.Get("dstBackupPathForSave")))
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
            Core.Utils utils = new Core.Utils();
            utils.CreateOutlinePDF();

        }

        private void exportAVaultsContentAsPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
