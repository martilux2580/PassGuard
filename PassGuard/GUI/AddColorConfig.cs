﻿using System;
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
    public partial class AddColorConfig : Form
    {

        public string name { get; private set; }
        public int red { get; private set; }
        public int green { get; private set; }
        public int blue { get; private set; }
        public int favourite { get; private set; }
        private Dictionary<String, List<int>> storedConfigs;
        public bool addedSuccess { get; private set; }

        public AddColorConfig(Dictionary<String, List<int>> configs)
        {
            InitializeComponent();

            this.Icon = new Icon(@".\Images\LogoIcon64123.ico"); //Loads Icon from Image folder.

            storedConfigs = configs;
            addedSuccess = false;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            string errorMessages = "";

            if(String.IsNullOrWhiteSpace(NameTextbox.Text))
            {
                errorMessages += "Name cannot be left blank.";
            }
            if ((RedNUD.Value < 32) && (GreenNUD.Value < 32) && (BlueNUD.Value < 32))
            {
                errorMessages += "\nAll three RGB values cannot be less than 32.";
            }
            if (storedConfigs.Keys.Contains(NameTextbox.Text))
            {
                errorMessages += "\nThere is already a saved config with that name.";
            }

            var colorConfig = new List<int> { (int)RedNUD.Value, (int)GreenNUD.Value, (int)BlueNUD.Value };
            foreach(List<int> savedConfig in storedConfigs.Values)
            {
                if ((savedConfig[0] == colorConfig[0]) && (savedConfig[1] == colorConfig[1]) && (savedConfig[2] == colorConfig[2])) //.Contains() does not work.
                {
                    errorMessages += "\nThere is already a saved config with that RGB configuration.";
                }
            }
            

            if (!String.IsNullOrEmpty(errorMessages)) //If any error...
            {
                MessageBox.Show(text: "The following errors have been found:\n\n" + errorMessages, caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
            }
            else //No error in params, set params.
            {
                name = NameTextbox.Text;
                red = (int)RedNUD.Value;
                green = (int)GreenNUD.Value;
                blue = (int)BlueNUD.Value;
                if(FavouriteCheckbox.Checked) { favourite = 1; }
                else { favourite = 0; } 

                addedSuccess = true; //Everything went correct, send this signal to update correctly the table.

                this.Close();
            }


        }
    }
}