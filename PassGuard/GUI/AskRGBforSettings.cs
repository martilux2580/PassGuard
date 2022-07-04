using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
    //Form to obtain new RGB values for the outline colours.
    public partial class AskRGBforSettings : Form
    {
        public AskRGBforSettings(int[] colours)
        {
            InitializeComponent();
            SetNUDs(colours);
        }


        private void SetNUDs(int[] colours) //Set NumericUpDowns to the colours set right now in the Content Panel of Form1
        {
            RedNUD.Value = colours[0]; //Modify data in the config file for future executions.
            GreenNUD.Value = colours[1]; //Modify data in the config file for future executions.
            BlueNUD.Value = colours[2]; //Modify data in the config file for future executions.
        }

        public int getRedNUDValue()
        {
            return (int)RedNUD.Value;
        }

        public int getGreenNUDValue()
        {
            return (int)GreenNUD.Value;
        }

        public int getBlueNUDValue()
        {
            return (int)BlueNUD.Value;
        }

        private int RedRGBValue { get; set; }
        private int GreenRGBValue { get; set; }
        private int BlueRGBValue { get; set; }


        private void AskRGBforSettings_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(@".\Images\LogoIcon64123.ico"); //Loads Icon from Image folder.
                WebHelpRGB.Image = System.Drawing.Image.FromFile(@".\Images\Help32.ico");
            }
            catch (Exception)
            {
                MessageBox.Show(text: "PassGuard could not load some images.", caption: "Images not found.", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }
        }

        private void WebHelpRGB_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://htmlcolorcodes.com/es");
            }
            catch (Exception)
            {
                MessageBox.Show(text: "PassGuard could not open help webpage.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if ((RedNUD.Value < 32) && (GreenNUD.Value < 32) && (BlueNUD.Value < 32)) //Too dark colours, text cannot be visible
            {
                MessageBox.Show("All 3 RGB values are less than 32. This combination is not available. At least one of the RGB values must be greater than 32.");
            }
            else //Set variables for then set them in Form1.
            {
                RedRGBValue = (int)RedNUD.Value;
                GreenRGBValue = (int)GreenNUD.Value;
                BlueRGBValue = (int)BlueNUD.Value;
                this.Close();
            }
        }

        private void SendButton_MouseEnter(object sender, EventArgs e)
        {
            SendButton.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void SendButton_MouseLeave(object sender, EventArgs e)
        {
            SendButton.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular); //Underline the text when mouse is in the button
        }

        private void LoadSavedConfigButton_Click(object sender, EventArgs e)
        {
            try //Get data from config.
            {
                RedNUD.Value = Convert.ToDecimal(ConfigurationManager.AppSettings.Get("RedLogo")); 
                GreenNUD.Value = Convert.ToDecimal(ConfigurationManager.AppSettings.Get("GreenLogo")); 
                BlueNUD.Value = Convert.ToDecimal(ConfigurationManager.AppSettings.Get("BlueLogo")); 
            }
            catch (Exception) //If not possible, set NUDs (NumericUpDowns) to 0.
            {
                RedNUD.Value = Convert.ToDecimal(0); 
                GreenNUD.Value = Convert.ToDecimal(0);
                BlueNUD.Value = Convert.ToDecimal(0);
                MessageBox.Show(text: "PassGuard could not access config file, colour could not be loaded.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }
        }

        private void LoadSavedConfigButton_MouseEnter(object sender, EventArgs e)
        {
            LoadSavedConfigButton.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void LoadSavedConfigButton_MouseLeave(object sender, EventArgs e)
        {
            LoadSavedConfigButton.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular); //Underline the text when mouse is in the button
        }
    }
}
