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

namespace PassGuard.GUI
{
	public partial class ContentStatsUC : UserControl
	{
		private List<String[]> myData = new();
		private int[] contextColour = new int[3] { 0, 191, 144 }; //Default colour

		public ContentStatsUC(List<String[]> someData, int[] ContextColour)
		{
			InitializeComponent();
			TextStatsTextbox.BackColor = Color.FromArgb(230, 230, 230); //Dunno why sometimes it shows a lighter white....
			H2LegendTextbox.BackColor = Color.FromArgb(230, 230, 230); //Dunno why sometimes it shows a lighter white....

			myData = someData;
			contextColour = ContextColour;
			LoadingLabel.Visible = true;

			LoadStats();
			LoadingLabel.Visible = false;
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
			
			

			//StatsText
			StringBuilder sb = new();
			var d1 = myData.Count;
			var d2 = ((decimal) passLengths[7] / myData.Count);
			var d3 = (((decimal) passLengths[8] / myData.Count) * 100);

			sb.Append("Number of saved passwords: " + d1.ToString() + " passwords.");
			sb.Append("\nAverage characters per saved password: " + Math.Round(d2, 3).ToString() + " characters.");
			sb.Append("\nPercentage of important passwords from total: " + passLengths[8].ToString() + "/" + myData.Count.ToString() + " = " + Math.Round(d3, 3).ToString() + "%.");
			TextStatsTextbox.Text = ""; TextStatsTextbox.Text = sb.ToString();
			TextStatsTextbox.SelectAll();
			TextStatsTextbox.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;



			//Show both histograms at same time....
			Histogram1Plotview.Model = plotModel;
			Histogram2Plotview.Model = plotModel1;

		}

		private List<int> calculatePassCompositions()
		{
			List<String> allPass = myData.Select(arr => arr[1]).ToList();
			//Second last number will hold the sum of characters, to later calculate the average..., last number will contain the count of important passwords...
			List<int> results = new List<int> { 0, 0, 0, 0, 0, 0, 0};
			string symbols = "!$%&/\\()|@#€<>[]{}+-*.:_,;ñÑ¿?=çÇ¡";

			foreach (String pass in allPass)
			{
				bool containsSymbols = pass.Any(c => symbols.Contains(c));
				bool containsUpper = pass.Any(char.IsUpper);
				bool containsLower = pass.Any(char.IsLower);
				bool containsNumbers = pass.Any(char.IsDigit);
				bool previousCases = false;

				if (containsSymbols && containsUpper && containsLower && containsNumbers) { results[6] += 1; previousCases = true; } //SULN
				if (containsSymbols && containsUpper && containsLower) { results[5] += 1; previousCases = true; } //SUN
				if (containsSymbols && containsLower && containsNumbers) { results[4] += 1; previousCases = true; } //SLN
				if (containsUpper && containsLower && containsNumbers) { results[3] += 1; previousCases = true; } //ULN
				if (containsUpper && containsLower) { results[2] += 1; previousCases = true; } //UL
				if (containsLower && containsNumbers) { results[1] += 1; previousCases = true; } //LN
				if(!previousCases) { results[0] += 1; } //NA

			}
			return results;
		}

		private List<int> calculatePassLengthsAndSum()
		{
			int[] lengths = { 6, 9, 12, 15, 18, 21 };
			List<String[]> allPass = myData.Select(arr => new String[] { arr[1], arr[2] }).ToList();
			//Second last number will hold the sum of characters, to later calculate the average..., last number will contain the count of important passwords...
			List<int> results = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; 


			foreach(String[] pass in allPass)
			{
				if(pass[0].Length < 6 ) 
				{ 
					results[0] += 1;
					results[7] += pass[0].Length; //Add to the sum
					if (pass[1] == "1") { results[8] += 1; } //If important, +1 the counter
				}
				else
				{
					for (int i = 1; i <= lengths.Length; i++)
					{
						if (pass[0].Length >= lengths[i-1])
						{
							results[i] += 1;
						}
					}
					results[7] += pass[0].Length; //Add to the sum
					if (pass[1] == "1") { results[8] += 1; } //If important, +1 the counter
				}
				
			}
			return results;
		}
	}
}
