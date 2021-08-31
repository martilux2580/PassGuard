using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
    public partial class AskRGBforSettings : Form
    {
        public AskRGBforSettings()
        {
            InitializeComponent();
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
            this.Icon = new Icon(@"..\..\Images\LogoIcon64.ico"); //Loads Icon from Image folder.
            WebHelpRGB.Image = System.Drawing.Image.FromFile(@"..\..\Images\Help32.ico");
        }

        private void SendRGBButton_Click(object sender, EventArgs e)
        {
            if ((RedNUD.Value < 32) && (GreenNUD.Value < 32) && (BlueNUD.Value < 32))
            {
                MessageBox.Show("All 3 RGB values are less than 32. This combination is not available. At least one of the RGB values must be greater than 32.");
            }
            else
            {
                RedRGBValue = (int)RedNUD.Value;
                GreenRGBValue = (int)GreenNUD.Value;
                BlueRGBValue = (int)BlueNUD.Value;
                this.Close();
            }
        }

        private void WebHelpRGB_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://htmlcolorcodes.com/es");
        }

    }
}
