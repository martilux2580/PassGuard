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

namespace PassGuard.GUI
{
	public partial class SecurityStatsUC : UserControl
	{
		private List<String[]> myData = new();
		private List<String> passes = new();
		private List<String> importances = new();
		private int[] contextColour = new int[3] { 0, 191, 144 }; //Default colour

		public SecurityStatsUC(List<String[]> someData, int[] ContextColour)
		{
			InitializeComponent();

			myData = someData;
			// Separate the first and second values using LINQ
			passes = myData.Select(item => item[0]).ToList();
			importances = myData.Select(item => item[1]).ToList();
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
			Dictionary<String, int[]> pwns = await CalculatePwns();
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
			sb.Append("        Total number repetitions of passwords: " + (passes.Count - passes.GroupBy(x => x).Where(g => g.Count() == 1).Select(g => g.Key).Count()).ToString() + " passwords.");
			sb.Append("\nNumber of distinct saved passwords: " + passes.Distinct().Count().ToString() + " passwords.");
			sb.Append("        Distinct pwned passwords: " + pwns.Values.Count(arr => arr[0] != 0).ToString() + " passwords.");
			sb.Append("        Total pwned passwords: " + pwns.Values.Sum(arr => arr[0]).ToString() + " passwords.");
			sb.Append("\nTotal pwned passwords marked as Important: " + pwns.Values.Where(arr => arr[1] == 1).Sum(arr => arr[0]).ToString() + " passwords.");
			TextStatsLabel.Text = sb.ToString();
			H2InfoLabel.Text = "Distinct passwords that appear only once in whole vault (Unique) VS Password that appear more than once.";
			H1InfoLabel.Text = "Distinct passwords that have been found in previous data breaches (Pwned) or not (Unpwned).";

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
						if (myData.Any(arr => arr[0] == data[i] && arr[1] == "1")) { passAndPwns[data[i]] = new int[] { 1, 1 }; }
						else { passAndPwns[data[i]] = new int[] { 1, 0 }; }

					}
					else 
					{
						if (myData.Any(arr => arr[0] == data[i] && arr[1] == "1")) { passAndPwns[data[i]] = new int[] { 0, 1 }; }
						else { passAndPwns[data[i]] = new int[] { 0, 0 }; }
					}
				}
				else
				{
					if (passAndPwns[data[i]][0] > 0)
					{
						if (myData.Any(arr => arr[0] == data[i] && arr[1] == "1")) 
						{
							passAndPwns[data[i]] = new int[] { passAndPwns[data[i]][0] + 1, passAndPwns[data[i]][1] };

						}
						else { passAndPwns[data[i]] = new int[] { passAndPwns[data[i]][0] += 1, 0 }; }
					}
					else
					{
						if (myData.Any(arr => arr[0] == data[i] && arr[1] == "1")) { passAndPwns[data[i]] = new int[] { 0, 1 }; }
						else { passAndPwns[data[i]] = new int[] { 0, 0 }; }
					}
				}
			}

			return passAndPwns;
		}

	}
}
