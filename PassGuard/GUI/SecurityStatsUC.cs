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

namespace PassGuard.GUI
{
	public partial class SecurityStatsUC : UserControl
	{
		private List<String[]> myData = new();
		private int[] contextColour = new int[3] { 0, 191, 144 }; //Default colour

		public SecurityStatsUC(List<String[]> someData, int[] ContextColour)
		{
			InitializeComponent();

			myData = someData;
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
			decimal[] pwnsAndImportance = await CalculatePwns(myData.Distinct().ToList());
			double percentagePwns = Convert.ToDouble((((decimal)pwnsAndImportance[0] / myData.Distinct().Count()) * 100));
			double percentageImportance = Convert.ToDouble((((decimal)pwnsAndImportance[1] / pwnsAndImportance[0]) * 100));

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
			var passes = myData.Select(array => array[0]).ToList();
			var uniqueStrings = passes.GroupBy(x => x).Where(g => g.Count() == 1).Select(g => g.Key);
			int uniqueStringsCount = uniqueStrings.Count();
			int repeatedStringsCount = passes.Distinct().Count() - uniqueStringsCount;
			int totalStringsCount = passes.Count;
			var percentageUniqueStringsCount = (((double)uniqueStringsCount / passes.Distinct().Count()) * 100);
			var percentageRepeatedStringsCount = (((double)repeatedStringsCount / passes.Distinct().Count()) * 100);
			var numberRepetitions = passes.Count - uniqueStringsCount;

			var slice1 = new PieSlice("Unique", Math.Round(percentageUniqueStringsCount, 3))
			{
				Fill = OxyColor.FromRgb(Convert.ToByte(contextColour[0]), Convert.ToByte(contextColour[1]), Convert.ToByte(contextColour[2]))
			};

			var slice2 = new PieSlice("Duplicated/Repeated", Math.Round(percentageRepeatedStringsCount, 3))
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
			var d1 = myData.Count;

			sb.Append("Number of total saved passwords: " + d1.ToString() + " passwords.");
			sb.Append("        Number of total unique passwords: " + uniqueStringsCount + " passwords.");
			sb.Append("        Number of total repetitions of passwords: " + numberRepetitions + " passwords.");
			sb.Append("\nNumber of total different saved passwords: " + passes.Distinct().Count().ToString() + " passwords.");
			sb.Append("        From different pwned password, percentage of those that where marked as important: " + pwnsAndImportance[1].ToString() + "/" + pwnsAndImportance[0].ToString() + " = " + Math.Round(percentageImportance, 3).ToString() + "%.");
			sb.Append("\nPercentage of all saved passwords that have been pwned (includes repetitions): " + Math.Round(percentageImportance, 3).ToString() + "%.");
			TextStatsLabel.Text = sb.ToString();
			H1InfoLabel.Text = "Distinct passwords that appear only once in whole vault (Unique) VS Password that appear more than once.";
			H2InfoLabel.Text = "Different passwords that have been found in previous data breaches (Pwned) or not (Unpwned).";

		}

		private async Task<decimal[]> CalculatePwns(List<String[]> data)
		{
			decimal[] result = new decimal[] { 0, 0 };

			foreach (String[] pair in data)
			{
				if(await Pwned.Pwned.CheckPwnage(pair[0])) 
				{
					result[0] += 1;
					if (Convert.ToInt32(pair[1]) == 1)
					{
						result[1] += 1;
					}
				}
				
			}

			return result;
		}

	}
}
