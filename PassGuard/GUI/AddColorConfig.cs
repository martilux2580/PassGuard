﻿using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	/// <summary>
	/// Handles the addition of a new colour config....
	/// </summary>
	public partial class AddColorConfig : Form
	{
		//Attributes that will hold the final values of the new addition
		public string name { get; private set; }
		public int Red { get; private set; }
		public int Green { get; private set; }
		public int Blue { get; private set; }
		public int Chosen { get; private set; } //Sets whether the user wants to use this config in this execution
		public int Favourite { get; private set; }
		public int Persists { get; private set; } //Sets whether the user wants to use this config for following execution
		private readonly Dictionary<String, List<int>> storedConfigs; //All saved configs
		public bool AddedSuccess { get; private set; } //Bool for checking that the closing of the form was due to the button click, not from AltF4 or other methods.

		public AddColorConfig(Dictionary<String, List<int>> configs)
		{
			InitializeComponent();

			this.Icon = Properties.Resources.LogoIcon64123; //Loads Icon from Image folder.

			storedConfigs = configs;
			AddedSuccess = false;
			Persists = 0;
		}

		/// <summary>
		/// Removes leading and trailing spaces from the textboxes
		/// </summary>
		public void TrimComponents()
		{
			NameTextbox.Text = NameTextbox.Text.Trim();
		}

		/// <summary>
		/// Handles addition of new config: need to check that config name and rgb arent already saved or blank before seting the parameters for addition
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SendButton_Click(object sender, EventArgs e)
		{
			TrimComponents();
			string errorMessages = "";

			if(String.IsNullOrWhiteSpace(NameTextbox.Text))
			{
				errorMessages += "Name cannot be left blank.";
			}
			if (storedConfigs.ContainsKey(NameTextbox.Text))
			{
				errorMessages += "\nThere is already a saved config with that name.";
			}
			if (!Utils.BooleanUtils.IsValidColour((int)RedNUD.Value, (int)GreenNUD.Value, (int)BlueNUD.Value))
			{
				errorMessages += "\nSelected RGB configuration is too dark or bright, texts or images might not be visible with this config.";
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
				Red = (int)RedNUD.Value;
				Green = (int)GreenNUD.Value;
				Blue = (int)BlueNUD.Value;
				if (FavouriteCheckbox.Checked) { Favourite = 1; }
				else { Favourite = 0; }
				if (ChosenCheckbox.Checked) //User wants to use the config in this execution
				{
					Chosen = 1;
					DialogResult dialog2 = MessageBox.Show(text: "Would you like to save this outline colour configuration for next executions?", caption: "Save outline colour configuration", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
					if (dialog2 == DialogResult.Yes) { Persists = 1; } //User wants to save config for future executions...
					else { Persists = 0; }
				}
				else { Chosen = 0; }
				

				AddedSuccess = true; //Everything went correct, send this signal to update correctly the table.

				this.Close();
			}


		}

		/// <summary>
		/// Changes components theme when general theme is changed...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddColorConfig_BackColorChanged(object sender, EventArgs e)
		{
			if (this.BackColor == Color.FromArgb(230, 230, 230))
			{
				NameTextbox.BackColor = SystemColors.Window;
				RedNUD.BackColor = SystemColors.Window;
				GreenNUD.BackColor = SystemColors.Window;
				BlueNUD.BackColor = SystemColors.Window;

			}
			else
			{
				NameTextbox.BackColor = Color.FromArgb(128, 130, 129);
				RedNUD.BackColor = Color.FromArgb(128, 130, 129);
				GreenNUD.BackColor = Color.FromArgb(128, 130, 129);
				BlueNUD.BackColor = Color.FromArgb(128, 130, 129);

			}
		}

		//Mouse enters button underlines button text...
		[SupportedOSPlatform("windows")]
		private void SendButton_MouseEnter(object sender, EventArgs e)
		{
			SendButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		//Mouse leaves button regularises button text...
		[SupportedOSPlatform("windows")]
		private void SendButton_MouseLeave(object sender, EventArgs e)
		{
			SendButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
		}
	}
}
