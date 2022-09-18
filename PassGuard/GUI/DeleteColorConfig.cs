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
    public partial class DeleteColorConfig : Form
    {

        public bool deletedSuccess { get; private set; }
        public bool deletedAllSuccess { get; private set; }
        public string name { get; private set; }
        private List<String> namesStored;

        public DeleteColorConfig(List<String> names)
        {
            InitializeComponent();

            namesStored = names;
            deletedAllSuccess = false;
            deletedSuccess = false;

            NameCombobox.Items.Add("");
            foreach (String name in namesStored)
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

        private void DeleteAllButton_Click(object sender, EventArgs e)
        {
            deletedAllSuccess = true;
            this.Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            name = NameCombobox.Text;
            deletedSuccess = true;
            this.Close();

        }

        private void NameCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(NameCombobox.Text))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                Dictionary<String, List<int>> values = js.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings.Get("OutlineSavedColours"));

                NameTextbox.Text = NameCombobox.Text;
                RedNUD.Value = values[NameCombobox.Text][0];
                GreenNUD.Value = values[NameCombobox.Text][1];
                BlueNUD.Value = values[NameCombobox.Text][2];
                FavouriteCheckbox.Checked = Convert.ToBoolean(values[NameCombobox.Text][3]);
            }
            else
            {
                NameTextbox.Text = "";
                RedNUD.Value = 0;
                GreenNUD.Value = 0;
                BlueNUD.Value = 0;
                FavouriteCheckbox.Checked = false;

            }
        }

        private void Check(bool flag)
        {
            if (flag)
            {
                TitleLabel.Text = "If button is clicked, all Vault contents will be deleted.";
                NameCombobox.Enabled = false;
                NameCombobox.ResetText(); //Set text back to null/unselected

                NameTextbox.Text = null;
                RedNUD.Value = 0;
                GreenNUD.Value = 0;
                BlueNUD.Value = 0;
                NameLabel.Enabled = false;
                NameTextbox.Enabled = false;
                RedLabel.Enabled = false;
                GreenLabel.Enabled = false;
                BlueLabel.Enabled = false;
                RedNUD.Enabled = false;
                GreenNUD.Enabled = false;
                BlueNUD.Enabled = false;
                DeleteButton.Enabled = false;
                FavouriteCheckbox.Enabled = false;
                FavouriteCheckbox.Checked = false;

                DeleteAllButton.Enabled = true;
            }
            else
            {
                TitleLabel.Text = "Select the name of the password you want to delete: ";
                NameCombobox.Enabled = true;
                NameCombobox.ResetText(); //Set text back to null/unselected

                NameTextbox.Text = null;
                RedNUD.Value = 0;
                GreenNUD.Value = 0;
                BlueNUD.Value = 0;
                NameLabel.Enabled = true;
                RedLabel.Enabled = true;
                GreenLabel.Enabled = true;
                BlueLabel.Enabled = true;
                DeleteButton.Enabled = true;

                DeleteAllButton.Enabled = false;
            }
        }

        private void EnableDeleteAllCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableDeleteAllCheckbox.Checked == true)
            {
                Check(true);
            }
            else
            {
                Check(false);
            }
        }
    }
}
