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
using System.Collections.Specialized;

namespace PassGuard
{
    public partial class mainWindow : Form
    {
        
        public mainWindow()
        {
            InitializeComponent();
            this.Size = this.MinimumSize; //We init the form with the minimum size to avoid Minimum Size bug (setting Min Size in Properties to the actual size makes Minimum Size decrease by 20-30 pixels aprox).
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LogoPictureBox.Image = Image.FromFile(@"..\..\Images\Logo123.png"); //Working Directory inside Release Folder. Loads Image from Image folder.
            LogoPictureBox.SizeMode = PictureBoxSizeMode.Zoom; //PictureBoxSizeMode.Zoom
            this.Icon = new Icon(@"..\..\Images\LogoIcon64123.ico"); //Loads Icon from Image folder. //LogoIcon64.ico
            SettingButton.Image = Image.FromFile(@"..\..\Images\Setting.ico"); //Loads Image for the Settings Icon
            setConfigTheme(); //Set theme based on saved config.
            setConfigColours(); //Set outline colours based on saved config.

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
                ContentPanel.BackColor = Color.FromArgb(116, 118, 117); //65, 65, 65
            }
            else if(sAttr == "Light")
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
            label1.Visible = true;
            TitleLabel.Text = "CREATING A NEW PASSWORD VAULT"; //Change Title
            GUI.CreateNewVaultUC cnv = new GUI.CreateNewVaultUC(); //Set new UC for the action.
            ContentPanel.Controls.Clear();
            ContentPanel.Controls.Add(cnv);
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
            label1.Visible = false;
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
            System.Diagnostics.Process.Start("https://github.com/martilux2580?tab=repositories"); //Open browser with webpage.
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
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            GUI.AskRGBforSettings rgb = new GUI.AskRGBforSettings(); //Dialog to insert rgb values
            if (darkToolStripMenuItem.Checked == true) //Change theme color depending on the backcolor of the app.
            {
                rgb.BackColor = Color.FromArgb(116, 118, 117); //Set Color for the RGB selection popup window.
            }
            else if (lightToolStripMenuItem.Checked == true)
            {
                rgb.BackColor = Color.FromArgb(230, 230, 230); //Set Color for the RGB selection popup window.
            }
            rgb.ShowDialog();  //Show dialog

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
                    ConfigurationManager.RefreshSection("appSettings");
                }

                MenuPanel.BackColor = Color.FromArgb(245, 245, 245); //Set colours.
                LogoPanel.BackColor = Color.FromArgb(255, 255, 255); 
                OptionsPanel.BackColor = Color.FromArgb(250, 250, 250); 
            }
            else if (!((redValue < 32) && (greenValue < 32) && (greenValue < 32))) //Correction of rgb values if they are too dark.
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
                newRedLogo = redValue;
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
                    ConfigurationManager.RefreshSection("appSettings");
                }

                MenuPanel.BackColor = Color.FromArgb(newRedMenu, newGreenMenu, newBlueMenu); //43, 43, 43      
                LogoPanel.BackColor = Color.FromArgb(newRedLogo, newGreenLogo, newBlueLogo); //31, 31, 31    -10
                OptionsPanel.BackColor = Color.FromArgb(newRedOptions,newGreenLogo, newBlueLogo); //-
            }
            
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e) //Check dark toolstrip, uncheck light one and change colors
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            DialogResult dialog = MessageBox.Show(text: "Would you like to save this theme configuration for next executions?", caption: "Save theme configuration", buttons: MessageBoxButtons.YesNo);
            if(dialog == DialogResult.Yes)
            {
                config.AppSettings.Settings["Theme"].Value = "Dark"; //Modify data in the config file for future executions.
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            darkToolStripMenuItem.Checked = true;
            lightToolStripMenuItem.Checked = false;
            ContentPanel.BackColor = Color.FromArgb(116, 118, 117);
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e) //Check light toolstrip, uncheck dark one and change colors
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            DialogResult dialog = MessageBox.Show(text: "Would you like to save this theme configuration for next executions?", caption: "Save theme configuration", buttons: MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                config.AppSettings.Settings["Theme"].Value = "Light"; //Modify data in the config file for future executions.
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            lightToolStripMenuItem.Checked = true;
            darkToolStripMenuItem.Checked = false;
            ContentPanel.BackColor = Color.FromArgb(230, 230, 230);
        }

        private void saveChangesClosePassGuardToolStripMenuItem_Click(object sender, EventArgs e) //Exit app saving changes (pending)
        {
            //Implement "Save Changes" Part
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
            GUI.AutoBackup ab = new GUI.AutoBackup();
            ab.BackColor = this.ContentPanel.BackColor;
            ab.ShowDialog();




        }
    }
}
