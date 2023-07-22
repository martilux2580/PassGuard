using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot.Wpf;
using OxyPlot.WindowsForms;
using OxyPlot.Annotations;
using PassGuard.Crypto;
using PassGuard.Utils;
using System.Runtime.Versioning;
using System.Text.Encodings.Web;
using System.Text.Json;
using iText.StyledXmlParser.Jsoup.Parser;
using System.IO;

namespace PassGuard.GUI
{
	public partial class ContentStatsUC : UserControl
	{
		private List<String[]> myData = new();
		private int[] contextColour = new int[3] { 0, 191, 144 }; //Default colour
		private Dictionary<String, List<String>> compositionNames = new()
		{
			{ "N/A", new() },
			{ "L+N", new() },
			{ "U+L", new() },
			{ "U+L+N", new() },
			{ "S+L+N", new() },
			{ "S+U+L", new() },
			{ "S+U+L+N", new() }
		};
		private List<List<String>> lengthNames = new()
		{
			new(),
			new(),
			new(),
			new(),
			new(),
			new(),
			new()
		};

		public ContentStatsUC(List<String[]> someData, int[] ContextColour)
		{
			InitializeComponent();

			myData = someData;
			contextColour = ContextColour;

			LoadStats();

		}

		private void LoadStats()
		{
			//Histogram1
			// Create a new PlotModel
			var plotModel = new PlotModel
			{
				Title = "Password Lengths"
			};

			// Create a CategoryAxis for Y-axis (Categories)
			var yAxis = new CategoryAxis
			{
				Position = AxisPosition.Left,
				Key = "YAxisKey"
			};

			// Create a LinearAxis for X-axis (Values)
			var xAxis = new LinearAxis
			{
				Position = AxisPosition.Bottom,
				Title = "Values",
			};

			// Add the axes to the plot model
			plotModel.Axes.Add(yAxis);
			plotModel.Axes.Add(xAxis);

			// Create a BarSeries for the bars
			var series = new BarSeries
			{
				Title = "Bars",
				FillColor = OxyColor.FromRgb(Convert.ToByte(contextColour[0]), Convert.ToByte(contextColour[1]), Convert.ToByte(contextColour[2])), // Set custom RGB color for the fill
				StrokeColor = OxyColors.Black,
				StrokeThickness = 1
			};

			var passLengths = calculatePassLengthsAndSum();
			// Set the data for the bars
			series.Items.Add(new BarItem { Value = passLengths[0], CategoryIndex = 0 });
			series.Items.Add(new BarItem { Value = passLengths[1], CategoryIndex = 1 });
			series.Items.Add(new BarItem { Value = passLengths[2], CategoryIndex = 2 });
			series.Items.Add(new BarItem { Value = passLengths[3], CategoryIndex = 3 });
			series.Items.Add(new BarItem { Value = passLengths[4], CategoryIndex = 4 });
			series.Items.Add(new BarItem { Value = passLengths[5], CategoryIndex = 5 });
			series.Items.Add(new BarItem { Value = passLengths[6], CategoryIndex = 6 });

			// Add the series to the plot model's series collection
			plotModel.Series.Add(series);
			// Set custom labels for the bars on the Y-axis
			yAxis.Labels.AddRange(new List<string> { "<6 chars", "6+ chars", "9+ chars", "12+ chars", "15+ chars", "18+ chars", "21+ chars" });



			//Histogram2
			// Create a new PlotModel
			var plotModel1 = new PlotModel
			{
				Title = "Password Composition"
			};

			// Create a CategoryAxis for Y-axis (Categories)
			var yAxis1 = new CategoryAxis
			{
				Position = AxisPosition.Left,
				Key = "YAxisKey1",
				FontSize = 12
			};

			// Create a LinearAxis for X-axis (Values)
			var xAxis1 = new LinearAxis
			{
				Position = AxisPosition.Bottom,
				Title = "Values",
			};

			// Add the axes to the plot model
			plotModel1.Axes.Add(yAxis1);
			plotModel1.Axes.Add(xAxis1);

			var complementaryColour = IntUtils.GetComplementaryRGB(contextColour[0], contextColour[1], contextColour[2]);
			// Create a BarSeries for the bars
			var series1 = new BarSeries
			{
				Title = "Bars",
				FillColor = OxyColor.FromRgb(Convert.ToByte(complementaryColour[0]), Convert.ToByte(complementaryColour[1]), Convert.ToByte(complementaryColour[2])), // Set custom RGB color for the fill
				StrokeColor = OxyColors.Black,
				StrokeThickness = 1
			};

			var passCompositions = calculatePassCompositions();
			// Set the data for the bars
			series1.Items.Add(new BarItem { Value = passCompositions[0], CategoryIndex = 0 });
			series1.Items.Add(new BarItem { Value = passCompositions[1], CategoryIndex = 1 });
			series1.Items.Add(new BarItem { Value = passCompositions[2], CategoryIndex = 2 });
			series1.Items.Add(new BarItem { Value = passCompositions[3], CategoryIndex = 3 });
			series1.Items.Add(new BarItem { Value = passCompositions[4], CategoryIndex = 4 });
			series1.Items.Add(new BarItem { Value = passCompositions[5], CategoryIndex = 5 });
			series1.Items.Add(new BarItem { Value = passCompositions[6], CategoryIndex = 6 });

			// Add the series to the plot model's series collection
			plotModel1.Series.Add(series1);
			// Set custom labels for the bars on the Y-axis
			yAxis1.Labels.AddRange(new List<string> { "N/A", "L+N", "U+L", "U+L+N", "S+L+N", "S+U+L", "S+U+L+N" });



			//Show both histograms at same time....
			Histogram1Plotview.Model = plotModel;
			Histogram2Plotview.Model = plotModel1;



			//StatsText
			StringBuilder sb = new();
			var d1 = myData.Count;
			var d2 = ((decimal)passLengths[7] / myData.Count);
			var d3 = (((decimal)passLengths[8] / myData.Count) * 100);

			sb.Append("Number of saved passwords: " + d1.ToString() + " passwords.");
			sb.Append("\nAverage characters per saved password: " + Math.Round(d2, 3).ToString() + " characters.");
			sb.Append("\nPercentage of important passwords from total: " + passLengths[8].ToString() + "/" + myData.Count.ToString() + " = " + Math.Round(d3, 3).ToString() + "%.");
			TextStatsLabel.Text = sb.ToString();
			H2InfoLabel.Text = "Legend: S = Symbols (@#%...), U = Upper Case letters (ABC...), L = Lower Case letters (abc...), N = Numbers (012...).";
			DownloadData1Button.Text = "Password Length Details";
			DownloadData2Button.Text = "Password Composition Details";

		}

		private List<int> calculatePassCompositions()
		{
			//Second last number will hold the sum of characters, to later calculate the average..., last number will contain the count of important passwords...
			List<int> results = new() { 0, 0, 0, 0, 0, 0, 0};
			string symbols = "!$%&/\\()|@#€<>[]{}+-*.:_,;ñÑ¿?=çÇ¡";

			foreach (String[] data in myData)
			{
				bool containsSymbols = data[1].Any(c => symbols.Contains(c));
				bool containsUpper = data[1].Any(char.IsUpper);
				bool containsLower = data[1].Any(char.IsLower);
				bool containsNumbers = data[1].Any(char.IsDigit);
				bool previousCases = false;
				
				if (containsSymbols && containsUpper && containsLower && containsNumbers) { results[6] += 1; previousCases = true; compositionNames["S+U+L+N"].Add(data[0]); } //SULN
				if (containsSymbols && containsUpper && containsLower) { results[5] += 1; previousCases = true; compositionNames["S+U+L"].Add(data[0]); } //SUN
				if (containsSymbols && containsLower && containsNumbers) { results[4] += 1; previousCases = true; compositionNames["S+L+N"].Add(data[0]); } //SLN
				if (containsUpper && containsLower && containsNumbers) { results[3] += 1; previousCases = true; compositionNames["U+L+N"].Add(data[0]); } //ULN
				if (containsUpper && containsLower) { results[2] += 1; previousCases = true; compositionNames["U+L"].Add(data[0]); } //UL
				if (containsLower && containsNumbers) { results[1] += 1; previousCases = true; compositionNames["L+N"].Add(data[0]); } //LN
				if (!previousCases) { results[0] += 1; compositionNames["N/A"].Add(data[0]); } //NA

			}
			return results;
		}

		private List<int> calculatePassLengthsAndSum()
		{
			int[] lengths = { 6, 9, 12, 15, 18, 21 };
			//Second last number will hold the sum of characters, to later calculate the average..., last number will contain the count of important passwords...
			List<int> results = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; 


			foreach(String[] pass in myData)
			{
				if(pass[1].Length < 6 ) 
				{ 
					results[0] += 1;
					results[7] += pass[1].Length; //Add to the sum
					if (pass[2] == "1") { results[8] += 1; } //If important, +1 the counter
					lengthNames[0].Add(pass[0]);
				}
				else
				{
					for (int i = 1; i <= lengths.Length; i++)
					{
						if (pass[1].Length >= lengths[i-1])
						{
							results[i] += 1;
							lengthNames[i].Add(pass[0]);
						}
					}
					results[7] += pass[1].Length; //Add to the sum
					if (pass[2] == "1") { results[8] += 1; } //If important, +1 the counter
				}
				
			}
			return results;
		}

		private void DownloadData1Button_Click(object sender, EventArgs e)
		{
			string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\LengthStats-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".json"; // Replace with the desired file path

			if (File.Exists(filePath)) { MessageBox.Show(text: "There is already a file with the name of the JSON file. Please try again later.", caption: "File with same name at path", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error); }
			else
			{
				// Create a JSON object with some data
				Dictionary<String, List<String>> jsonObject = new()
				{
					{ "<6 chars", lengthNames[0] },
					{ "6+ chars", lengthNames[1] },
					{ "9+ chars", lengthNames[2] },
					{ "12+ chars", lengthNames[3] },
					{ "15+ chars", lengthNames[4] },
					{ "18+ chars", lengthNames[5] },
					{ "21+ chars", lengthNames[6] },
					{ "TotalNameCount", new() { myData.Count.ToString() } }
				};

				// Serialize the dictionary to a JSON string
				string jsonString = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions
				{
					WriteIndented = true,
					Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
				});

				// Export the JSON string to a JSON file
				File.WriteAllText(filePath, jsonString);

				MessageBox.Show(text: "JSON file with the content of the Vault was generated successfully in your Documents Folder :)", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
			}

		}

		private void DownloadData2Button_Click(object sender, EventArgs e)
		{
			string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CompositionStats-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".json"; // Replace with the desired file path

			if (File.Exists(filePath)) { MessageBox.Show(text: "There is already a file with the name of the JSON file. Please try again later.", caption: "File with same name at path", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error); }
			else
			{
				// Create a JSON object with some data
				Dictionary<String, List<String>> jsonObject = compositionNames;
				jsonObject.Add("TotalNameCount", new() { myData.Count.ToString() } );

				// Serialize the dictionary to a JSON string
				string jsonString = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions
				{
					WriteIndented = true,
					Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
				});

				// Export the JSON string to a JSON file
				File.WriteAllText(filePath, jsonString);

				MessageBox.Show(text: "JSON file with the content of the Vault was generated successfully in your Documents Folder :)", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
			}
		}

		[SupportedOSPlatform("windows")]
		private void DownloadData1Button_MouseEnter(object sender, EventArgs e)
		{
			DownloadData1Button.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void DownloadData1Button_MouseLeave(object sender, EventArgs e)
		{
			DownloadData1Button.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
		}

		[SupportedOSPlatform("windows")]
		private void DownloadData2Button_MouseEnter(object sender, EventArgs e)
		{
			DownloadData2Button.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void DownloadData2Button_MouseLeave(object sender, EventArgs e)
		{
			DownloadData2Button.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
		}
	}
}
