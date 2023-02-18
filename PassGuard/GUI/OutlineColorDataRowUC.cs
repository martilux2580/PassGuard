using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
    public partial class OutlineColorDataRowUC : UserControl
    {

        private readonly AskRGBforSettings callingForm;
        public bool favourite { get; private set; }

        public OutlineColorDataRowUC(String name, List<int> rgb, AskRGBforSettings cf)
        {
            InitializeComponent();

            callingForm = cf;

            NameTextbox.Text = name;
            RedNUD.Value = rgb[0];
            GreenNUD.Value = rgb[1];
            BlueNUD.Value = rgb[2];

            var redlogo = ConfigurationManager.AppSettings["RedLogo"];
            var greenlogo = ConfigurationManager.AppSettings["GreenLogo"];
            var bluelogo = ConfigurationManager.AppSettings["BlueLogo"];

            ViewerPanel.BackColor = Color.FromArgb(rgb[0], rgb[1], rgb[2]);

            if ((rgb[0] == Int32.Parse(redlogo)) && (rgb[1] == Int32.Parse(greenlogo)) && (rgb[2] == Int32.Parse(bluelogo)))
            {
                ChosenConfigCheckbox.Checked = true;
                ChosenConfigCheckbox.Text = "Enabled";
            }

            if(Convert.ToBoolean(rgb[3]))
            {
                FavouriteButton.Image = Properties.Resources.CheckIcon;
                favourite = true;
            }

        }

        private void UncheckEverything()
        {
            foreach (CheckBox check in callingForm.checkboxes)
            {
                check.Text = "Disabled";
                check.Checked = false;
            }
        }

        private void ChosenConfigCheckbox_MouseClick(object sender, MouseEventArgs e)
        {
            UncheckEverything();

            ChosenConfigCheckbox.Text = "Enabled";
            ChosenConfigCheckbox.Checked = true;

            callingForm.SetRedNUDValue((int)RedNUD.Value);
            callingForm.SetGreenNUDValue((int)GreenNUD.Value);
            callingForm.SetBlueNUDValue((int)BlueNUD.Value);
        }

        private void FavouriteButton_Click(object sender, EventArgs e)
        {
            if (FavouriteButton.Image == null)
            {
                FavouriteButton.Image = Properties.Resources.CheckIcon;
                favourite = true;
            }
            else
            {
                FavouriteButton.Image = null;
                favourite = false;
            }

            Dictionary<String, List<int>> values = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

            var rgbconf = values[NameTextbox.Text];
            if(favourite) { rgbconf[rgbconf.Count - 1] = 1; }
            else { rgbconf[rgbconf.Count - 1] = 0; }
            
            values[NameTextbox.Text] = rgbconf;

            callingForm.config.AppSettings.Settings["OutlineSavedColours"].Value = JsonSerializer.Serialize(values); //Modify data in the config file for future executions.
            callingForm.config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

            callingForm.LoadContent(ConfigurationManager.AppSettings["OutlineSavedColours"]);
        }
    }
}
