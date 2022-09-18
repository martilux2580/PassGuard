using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace PassGuard.GUI
{
    public partial class EditColorConfig : Form
    {

        public bool editedSuccess { get; private set; }
        public string oldname { get; private set; }
        public string name { get; private set; }
        public int red { get; private set; }
        public int green { get; private set; }
        public int blue { get; private set; }
        public int important { get; private set; }
        private List<String> namesStored;

        public EditColorConfig(List<String> names)
        {
            InitializeComponent();

            namesStored = names;
            editedSuccess = false;
            NameCombobox.Items.Add("");
            foreach(String name in namesStored)
            {
                NameCombobox.Items.Add(name);
            }

            try
            {
                this.Icon = new Icon(@".\Images\LogoIcon64123.ico"); //Loads Icon from Image folder.
            }
            catch (Exception)
            {
                MessageBox.Show(text: "PassGuard could not load some images.", caption: "Images not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            String errorMessages = "";

            if (String.IsNullOrWhiteSpace(NameTextbox.Text))
            {
                errorMessages += "\nName cannot be left blank.";
            }
            if (namesStored.Contains(NameTextbox.Text) && (NameTextbox.Text != NameCombobox.Text))
            {
                errorMessages += "\nThere is already a saved config with that name.";
            }
            if ((RedNUD.Value < 32) && (GreenNUD.Value < 32) && (BlueNUD.Value < 32))
            {
                errorMessages += "\nAll three RGB values cannot be less than 32.";
            }

            if (!String.IsNullOrEmpty(errorMessages)) //If any error...
            {
                MessageBox.Show(text: "The following errors have been found:\n\n" + errorMessages, caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
            }
            else //No error in params, create vault.
            {
                oldname = NameCombobox.Text;
                name = NameTextbox.Text;
                red = (int)RedNUD.Value;
                green = (int)GreenNUD.Value;
                blue = (int)BlueNUD.Value;
                if(FavouriteCheckbox.Checked) { important = 1; }
                else { important = 0; }

                editedSuccess = true;
                this.Close();
            }
                
        }

        private void Check(bool flag)
        {
            if(flag)
            {
                NameLabel.Enabled = true;
                NameTextbox.Enabled = true;
                RedLabel.Enabled = true;
                GreenLabel.Enabled = true;
                BlueLabel.Enabled = true;
                RedNUD.Enabled = true;
                GreenNUD.Enabled = true;
                BlueNUD.Enabled = true;
                EditButton.Enabled = true;
                FavouriteCheckbox.Enabled = true;
            }
            else
            {
                NameLabel.Enabled = false;
                NameTextbox.Enabled = false;
                RedLabel.Enabled = false;
                GreenLabel.Enabled = false;
                BlueLabel.Enabled = false;
                RedNUD.Enabled = false;
                GreenNUD.Enabled = false;
                BlueNUD.Enabled = false;
                EditButton.Enabled = false;
                FavouriteCheckbox.Enabled = false;
            }
        }

        private void NameCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(NameCombobox.Text))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                Dictionary<String, List<int>> values = js.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings.Get("OutlineSavedColours"));

                NameTextbox.Text = NameCombobox.Text;
                RedNUD.Value = values[NameCombobox.Text][0];
                GreenNUD.Value = values[NameCombobox.Text][1];
                BlueNUD.Value = values[NameCombobox.Text][2];
                FavouriteCheckbox.Checked = Convert.ToBoolean(values[NameCombobox.Text][3]);

                Check(true);

            }
            else
            {
                NameTextbox.Text = "";
                RedNUD.Value = 0;
                GreenNUD.Value = 0;
                BlueNUD.Value = 0;
                FavouriteCheckbox.Checked = false;

                Check(false);
            }
        }
    }
}
