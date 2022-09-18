﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace PassGuard.GUI
{
    //Form to obtain new RGB values for the outline colours.
    public partial class AskRGBforSettings : Form
    {

        private List<OutlineColorDataRowUC> ConfigUCList = new List<OutlineColorDataRowUC>(); //List of DataRows with the data of the passwords.
        public List<CheckBox> checkboxes { get; internal set; } = new List<CheckBox>();
        public bool changedSuccess { get; private set; }
        public Configuration config { get; private set; }
        private int RedRGBValue { get; set; }
        private int GreenRGBValue { get; set; }
        private int BlueRGBValue { get; set; }
        public int OrderMode { get; set; }
     

    public AskRGBforSettings(int[] colours, Configuration configg)
        {
            InitializeComponent();
            SetNUDs(colours);
            LoadContent(ConfigurationManager.AppSettings.Get("OutlineSavedColours"));
            changedSuccess = false;
            config = configg;

        }

        internal bool CheckFav(OutlineColorDataRowUC row)
        {
            return row.favourite;
        }

        internal void LoadContent(String configs)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<String, List<int>> values = js.Deserialize<Dictionary<String, List<int>>>(configs);

            ContentFlowLayoutPanel.Controls.Clear();
            ConfigUCList.Clear();
            checkboxes.Clear();
            
            foreach(KeyValuePair<String, List<int>> configColor in values)
            {
                var row = new OutlineColorDataRowUC(configColor.Key, configColor.Value, this);

                checkboxes.Add((CheckBox)row.Controls[1]); //Add the checkbox.

                ConfigUCList.Add(row);
                //ContentFlowLayoutPanel.Controls.Add(row);
            }

            var xd = ConfigUCList.FindAll(CheckFav);
            foreach (OutlineColorDataRowUC row in xd)
            {
                ContentFlowLayoutPanel.Controls.Add(row);
            }
            foreach(OutlineColorDataRowUC row in ConfigUCList)
            {
                if (!xd.Contains(row)) { ContentFlowLayoutPanel.Controls.Add(row); }
            }
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

        public void setRedNUDValue(int value)
        {
            RedNUD.Value = value;
        }

        public void setGreenNUDValue(int value)
        {
            GreenNUD.Value = value;
        }

        public void setBlueNUDValue(int value)
        {
            BlueNUD.Value = value;
        }

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
                changedSuccess = true;
                this.Close();
            }
        }

        private void ConfigNameButton_Click(object sender, EventArgs e)
        {
            NameCMS.Show(ConfigNameButton, new Point(ConfigNameButton.Width - ConfigNameButton.Width, ConfigNameButton.Height)); //Sets where to display the ContextMenuStrip...
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<String, List<int>> values = js.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings.Get("OutlineSavedColours"));

            GUI.AddColorConfig add = new GUI.AddColorConfig(values);
            add.BackColor = this.BackColor;
            add.ShowDialog();

            if (add.addedSuccess)
            {
                String newName = add.name;
                int newRed = add.red;
                int newGreen = add.green;
                int newBlue = add.blue;
                int newFav = add.favourite;

                var data = ConfigurationManager.AppSettings.Get("OutlineSavedColours");
                var newData = data.Insert(data.Length - 1, ",\"" + newName  + "\":[" + newRed.ToString() + ", " + newGreen.ToString() + ", " + newBlue.ToString() + "," + newFav.ToString() + "]");

                config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

                ContentFlowLayoutPanel.Controls.Clear();
                LoadContent(ConfigurationManager.AppSettings.Get("OutlineSavedColours"));

                ContentFlowLayoutPanel.Refresh();

            }

        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<String, List<int>> values = js.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings.Get("OutlineSavedColours"));

            var namesList = new List<String>(values.Keys);

            GUI.EditColorConfig edit = new GUI.EditColorConfig(namesList);
            edit.BackColor = this.BackColor;
            edit.ShowDialog();

            if(edit.editedSuccess)
            {
                String oldName = edit.oldname;
                String newName = edit.name;
                int red = edit.red;
                int green = edit.green;
                int blue = edit.blue;
                int important = edit.important;

                values.Remove(oldName);
                values.Add(newName, new List<int> { red, green, blue , important});

                String newData = js.Serialize(values);

                config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

                ContentFlowLayoutPanel.Controls.Clear();
                LoadContent(ConfigurationManager.AppSettings.Get("OutlineSavedColours"));

                ContentFlowLayoutPanel.Refresh();
            }

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<String, List<int>> values = js.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings.Get("OutlineSavedColours"));

            var namesList = new List<String>(values.Keys);

            GUI.DeleteColorConfig del = new GUI.DeleteColorConfig(namesList);
            del.BackColor = this.BackColor;
            del.Enabled = true;
            del.ShowDialog();

            if (del.deletedSuccess)
            {
                values.Remove(del.name);
                if(values.Count < 1) { values.Add("Default", new List<int> { 0, 191, 144 }); }

                String newData = js.Serialize(values);

                config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

                ContentFlowLayoutPanel.Controls.Clear();
                LoadContent(ConfigurationManager.AppSettings.Get("OutlineSavedColours"));

                ContentFlowLayoutPanel.Refresh();
            }
            else if (del.deletedAllSuccess)
            {
                values.Clear();
                values.Add("Default", new List<int> { 0, 191, 144 });

                String newData = js.Serialize(values);

                config.AppSettings.Settings["OutlineSavedColours"].Value = newData; //Modify data in the config file for future executions.
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.

                ContentFlowLayoutPanel.Controls.Clear();
                LoadContent(ConfigurationManager.AppSettings.Get("OutlineSavedColours"));

            }


        }

        private void normalOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normalOrderToolStripMenuItem.Checked = true;
            ascendingOrderToolStripMenuItem.Checked = false;
            descendingOrderToolStripMenuItem.Checked = false;

            LoadContent(ConfigurationManager.AppSettings.Get("OutlineSavedColours"));

        }

        private void ascendingOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normalOrderToolStripMenuItem.Checked = false;
            ascendingOrderToolStripMenuItem.Checked = true;
            descendingOrderToolStripMenuItem.Checked = false;

            JavaScriptSerializer js = new JavaScriptSerializer();
            var values = new SortedDictionary<String, List<int>>(js.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings.Get("OutlineSavedColours")));

            LoadContent(js.Serialize(values));
            
        }

        private void descendingOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normalOrderToolStripMenuItem.Checked = false;
            ascendingOrderToolStripMenuItem.Checked = false;
            descendingOrderToolStripMenuItem.Checked = true;

            JavaScriptSerializer js = new JavaScriptSerializer();
            var values = new SortedDictionary<String, List<int>>(js.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings.Get("OutlineSavedColours")));
            var reversed = values.Reverse();
            var newValues = reversed.ToDictionary(x => x.Key, x => x.Value);

            LoadContent(js.Serialize(newValues));

        }

        private void RedButton_Click(object sender, EventArgs e)
        {
            ContentFlowLayoutPanel.Controls.Add(new GUI.OutlineColorDataRowUC("xd", new List<int> { 1, 2, 3, 4 }, this));
        }
    }
}
