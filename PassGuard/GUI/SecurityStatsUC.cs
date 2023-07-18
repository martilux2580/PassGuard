﻿using OxyPlot.Axes;
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

namespace PassGuard.GUI
{
	public partial class SecurityStatsUC : UserControl
	{
		private List<String> myData = new();
		private int[] contextColour = new int[3] { 0, 191, 144 }; //Default colour

		public SecurityStatsUC(List<String> someData, int[] ContextColour)
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
				Title = "Distinct Password Repetition"
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
			double percentagePwned = Convert.ToDouble(await CalculatePwns(myData.Distinct().ToList()));

			var slice11 = new PieSlice("Pwned", Math.Round(percentagePwned, 3))
			{
				Fill = OxyColor.FromRgb(Convert.ToByte(contextColour[0]), Convert.ToByte(contextColour[1]), Convert.ToByte(contextColour[2]))
			};

			var complementaryColour = IntUtils.GetComplementaryRGB(contextColour[0], contextColour[1], contextColour[2]);
			var slice22 = new PieSlice("Unpwned", Math.Round(100 - percentagePwned, 3))
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
			var uniqueStrings = myData.GroupBy(x => x).Where(g => g.Count() == 1).Select(g => g.Key);
			int uniqueStringsCount = uniqueStrings.Count();
			int repeatedStringsCount = myData.Distinct().Count() - uniqueStringsCount;
			int totalStringsCount = myData.Count;
			var percentageUniqueStringsCount = (((double)uniqueStringsCount / myData.Distinct().Count()) * 100);
			var percentageRepeatedStringsCount = (((double)repeatedStringsCount / myData.Distinct().Count()) * 100);
			var numberRepetitions = myData.Count - uniqueStringsCount;

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
			sb.Append("\nNumber of total different saved passwords: " + myData.Distinct().Count().ToString() + " passwords.");
			//TODO
			TextStatsLabel.Text = sb.ToString();
			H1InfoLabel.Text = "Distinct passwords that appear only once in whole vault (Unique) VS Password that appear more than once.";
			H2InfoLabel.Text = "Different passwords that have been found in previous data breaches (Pwned) or not (Unpwned).";

		}

		private async Task<decimal> CalculatePwns(List<String> data)
		{
			int result = 0;

			foreach(String pass in data)
			{
				if(await Pwned.Pwned.CheckPwnage(pass)) 
				{ 
					result += 1; 
				}
				
			}
			
			return (((decimal)result / data.Count) * 100);
		}
	}
}
