using PassGuard.PDF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PassGuard.GUI
{
	public partial class ExportVaultConfigs : Form
	{
		public ExportVaultConfigs()
		{
			InitializeComponent();
			this.Icon = Properties.Resources.LogoIcon64123; //Loads Icon from Image folder.

		}

		private void ExportButton_Click(object sender, EventArgs e)
		{
			if (PdfRadioButton.Checked)
			{
				IPDF pdf = new PDFCreator();
				pdf.CreateOutlinePDF();
				this.Close();
			}
			else if(JsonRadioButton.Checked)
			{
				var fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ColoursTable-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".json"; //Name of file

				// Deserialize JSON string to object
				var data = JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]);

				// Configure the JsonSerializerOptions for formatting
				var options = new JsonSerializerOptions
				{
					WriteIndented = true // Set to true to enable indentation
				};
				// Serialize object to JSON with indentations
				var serializedJson = JsonSerializer.Serialize(data, options);

				// Write serialized JSON to a file
				File.WriteAllText(fileLocation, serializedJson);

				MessageBox.Show(text: "JSON file with your Outline Color Configurations was generated successfully in your Documents Folder :)", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);

				this.Close();
			}
		}

		private void PdfRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (PdfRadioButton.Checked)
			{
				JsonRadioButton.Checked = false;
			}
		}

		private void JsonRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (JsonRadioButton.Checked)
			{
				PdfRadioButton.Checked = false;
			}
		}

		[SupportedOSPlatform("windows")]
		private void ExportButton_MouseEnter(object sender, EventArgs e)
		{
			ExportButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the butto
		}

		[SupportedOSPlatform("windows")]
		private void ExportButton_MouseLeave(object sender, EventArgs e)
		{
			ExportButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Dont underline the text when mouse leaves
		}
	}
}
