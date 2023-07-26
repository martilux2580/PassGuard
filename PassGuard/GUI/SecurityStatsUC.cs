using OxyPlot.Axes;
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
using PassGuard.Utils;
using OxyPlot.WindowsForms;
using OxyPlot.Wpf;
using System.IO.Packaging;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Windows.Input;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json.Nodes;

namespace PassGuard.GUI
{
	public partial class SecurityStatsUC : UserControl
	{
		private List<String[]> myData = new();
		private List<String> passes = new();
		private int[] contextColour = new int[3] { 0, 191, 144 }; //Default colour

		private Dictionary<String, int[]> pwns = new();

		public SecurityStatsUC(List<String[]> someData, int[] ContextColour)
		{
			InitializeComponent();

			myData = someData;
			// Separate the first and second values using LINQ
			passes = myData.Select(item => item[1]).ToList();
			contextColour = ContextColour;

			LoadStats();
		}

		private async void LoadStats()
		{
			//Piechart1
			// Create the plot model
			var plotModel1 = new PlotModel
			{
				Title = "Distinct Password Pwnage"
			};

			// Create the pie series
			var pieSeries1 = new PieSeries
			{
				StrokeThickness = 1.0,
				InsideLabelPosition = 0.8,
				AngleSpan = 360,
				StartAngle = 0,
				OutsideLabelFormat = "{0:0.000}%" // Format specifier for 3 decimal places
			};

			// Add data to the pie series
			pwns = await CalculatePwns();
			double percentagePwns = Convert.ToDouble((((decimal)pwns.Values.Count(arr => arr[0] != 0) / pwns.Count()) * 100));
			//double percentageImportance = Convert.ToDouble((((decimal)pwnsAndImportance[1] / pwnsAndImportance[0]) * 100));

			var slice11 = new PieSlice("Pwned", Math.Round(percentagePwns, 3))
			{
				Fill = OxyColor.FromRgb(Convert.ToByte(contextColour[0]), Convert.ToByte(contextColour[1]), Convert.ToByte(contextColour[2]))
			};

			var complementaryColour = IntUtils.GetComplementaryRGB(contextColour[0], contextColour[1], contextColour[2]);
			var slice22 = new PieSlice("Unpwned", Math.Round(100 - percentagePwns, 3))
			{
				Fill = OxyColor.FromRgb(Convert.ToByte(complementaryColour[0]), Convert.ToByte(complementaryColour[1]), Convert.ToByte(complementaryColour[2]))
			};


			pieSeries1.Slices.Add(slice11);
			pieSeries1.Slices.Add(slice22);

			// Add the pie series to the plot model
			plotModel1.Series.Add(pieSeries1);



			//PieChart2
			// Create the plot model
			var plotModel = new PlotModel
			{
				Title = "Distinct Password Repetition"
			};

			// Create the pie series
			var pieSeries = new PieSeries
			{
				StrokeThickness = 1.0,
				InsideLabelPosition = 0.8,
				AngleSpan = 360,
				StartAngle = 0,
				OutsideLabelFormat = "{0:0.000}%" // Format specifier for 3 decimal places
			};

			// Add data to the pie series

			decimal percentageUnique = (((decimal)passes.GroupBy(x => x).Where(g => g.Count() == 1).Select(g => g.Key).Count() / passes.Distinct().Count()) * 100);
			var slice1 = new PieSlice("Unique", Math.Round(Convert.ToDouble(percentageUnique), 3))
			{
				Fill = OxyColor.FromRgb(Convert.ToByte(contextColour[0]), Convert.ToByte(contextColour[1]), Convert.ToByte(contextColour[2]))
			};

			var slice2 = new PieSlice("Duplicated/Repeated", Math.Round(Convert.ToDouble(100 - percentageUnique), 3))
			{
				Fill = OxyColor.FromRgb(Convert.ToByte(complementaryColour[0]), Convert.ToByte(complementaryColour[1]), Convert.ToByte(complementaryColour[2]))
			};


			pieSeries.Slices.Add(slice1);
			pieSeries.Slices.Add(slice2);

			// Add the pie series to the plot model
			plotModel.Series.Add(pieSeries);



			//Show both histograms at same time....
			Histogram1Plotview.Model = plotModel1;
			Histogram2Plotview.Model = plotModel;



			//StatsText
			StringBuilder sb = new();

			sb.Append("Total saved passwords: " + myData.Count.ToString() + " passwords.");
			sb.Append("        Total number of passwords used once (Unique): " + passes.GroupBy(x => x).Where(g => g.Count() == 1).Select(g => g.Key).Count().ToString() + " passwords.");
			sb.Append("\nTotal number repetitions of passwords: " + (passes.Count - passes.GroupBy(x => x).Where(g => g.Count() == 1).Select(g => g.Key).Count()).ToString() + " passwords.");
			sb.Append("        Number of distinct saved passwords: " + passes.Distinct().Count().ToString() + " passwords.");
			sb.Append("\nDistinct pwned passwords: " + pwns.Values.Count(arr => arr[0] != 0).ToString() + " passwords.");
			sb.Append("        Total pwned passwords: " + pwns.Values.Sum(arr => arr[0]).ToString() + " passwords.");
			sb.Append("        Total pwned passwords marked as Important: " + pwns.Values.Where(arr => arr[1] == 1).Sum(arr => arr[0]).ToString() + " passwords.");
			TextStatsLabel.Text = sb.ToString();
			H2InfoLabel.Text = "Distinct passwords that appear only once in whole vault (Unique) VS Password that appear more than once.";
			H1InfoLabel.Text = "Distinct passwords that have been found in previous data breaches (Pwned) or not (Unpwned).";
			DownloadData1Button.Text = "Download Pwned Data Details";
			DownloadData2Button.Text = "Download Password Repetition Details";

		}

		private async Task<Dictionary<String, int[]>> CalculatePwns()
		{
			Dictionary<String, int[]> passAndPwns = new();
			var data = passes.ToList();

			// Using a for loop to iterate through the list
			for (int i = 0; i < data.Count; i++)
			{
				if (!passAndPwns.ContainsKey(data[i]))
				{
					if (await Pwned.Pwned.CheckPwnage(data[i]))
					{
						if (myData.Any(arr => arr[1] == data[i] && arr[2] == "1")) { passAndPwns[data[i]] = new int[] { 1, 1 }; }
						else { passAndPwns[data[i]] = new int[] { 1, 0 }; }

					}
					else 
					{
						if (myData.Any(arr => arr[1] == data[i] && arr[2] == "1")) { passAndPwns[data[i]] = new int[] { 0, 1 }; }
						else { passAndPwns[data[i]] = new int[] { 0, 0 }; }
					}
				}
				else
				{
					if (passAndPwns[data[i]][0] > 0)
					{
						if (myData.Any(arr => arr[1] == data[i] && arr[2] == "1")) 
						{
							passAndPwns[data[i]] = new int[] { passAndPwns[data[i]][0] + 1, passAndPwns[data[i]][1] };

						}
						else { passAndPwns[data[i]] = new int[] { passAndPwns[data[i]][0] += 1, 0 }; }
					}
					else
					{
						if (myData.Any(arr => arr[1] == data[i] && arr[2] == "1")) { passAndPwns[data[i]] = new int[] { 0, 1 }; }
						else { passAndPwns[data[i]] = new int[] { 0, 0 }; }
					}
				}
			}

			return passAndPwns;
		}

		private void DownloadData1Button_Click(object sender, EventArgs e)
		{
			string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PwnageStats-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".json"; // Replace with the desired file path

			if (File.Exists(filePath)) { MessageBox.Show(text: "There is already a file with the name of the JSON file. Please try again later.", caption: "File with same name at path", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error); }
			else
			{
				var notPwnedPasses = pwns.Where(kvp => kvp.Value[0] == 0).Select(kvp => kvp.Key).ToList();
				var notPwnedNames = myData.Where(arr => notPwnedPasses.Contains(arr[1])).Select(arr => arr[0]).ToList();
				var pwnedPasses = pwns.Where(kvp => kvp.Value[0] > 0).Select(kvp => kvp.Key).ToList();
				var pwnedNames = myData.Where(arr => pwnedPasses.Contains(arr[1])).Select(arr => arr[0]).ToList();


				// Create a JSON object with some data
				Dictionary<string, List<string>> jsonObject = new Dictionary<string, List<string>>
				{
					{ "PwnedPasswordNames", pwnedNames },
					{ "NotPwnedPasswordNames", notPwnedNames },
					{ "TotalNameCount", new List<string> { (pwnedNames.Count + notPwnedNames.Count).ToString() } }
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
			string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\RepetitionStats-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".json"; // Replace with the desired file path

			if (File.Exists(filePath)) { MessageBox.Show(text: "There is already a file with the name of the JSON file. Please try again later.", caption: "File with same name at path", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error); }
			else
			{
				var notRepeatedPasses = passes.GroupBy(x => x).Where(g => g.Count() == 1).Select(g => g.Key);
				var notRepeatedNames = myData.Where(arr => notRepeatedPasses.Contains(arr[1])).Select(arr => arr[0]).ToList();
				var repeatedPasses = passes.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key);
				var repeatedNames = myData.Where(arr => repeatedPasses.Contains(arr[1])).Select(arr => arr[0]).ToList();


				// Create a JSON object with some data
				Dictionary<string, List<string>> jsonObject = new Dictionary<string, List<string>>
				{
					{ "PasswordNames with repeated passwords", repeatedNames },
					{ "PasswordNames with unique passwords", notRepeatedNames },
					{ "TotalNameCount", new List<string> { (repeatedNames.Count + notRepeatedNames.Count).ToString() } }
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

		[SupportedOSPlatform("windows")]
		private void DownloadData1Button_MouseEnter(object sender, EventArgs e)
		{
			DownloadData1Button.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void DownloadData1Button_MouseLeave(object sender, EventArgs e)
		{
			DownloadData1Button.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
		}

		[SupportedOSPlatform("windows")]
		private void DownloadData2Button_MouseEnter(object sender, EventArgs e)
		{
			DownloadData2Button.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void DownloadData2Button_MouseLeave(object sender, EventArgs e)
		{
			DownloadData2Button.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
		}
	}
}
