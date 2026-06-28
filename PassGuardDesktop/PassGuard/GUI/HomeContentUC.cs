using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace PassGuard.GUI
{
	/// <summary>
	/// UC Component with the date, it is shown at the main window.
	/// </summary>
	public partial class HomeContentUC : UserControl
	{
		public HomeContentUC()
		{
			this.Anchor = AnchorStyles.None;
			InitializeComponent();
		}

		/// <summary>
		/// Starts the time from now to simulate the seconds
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HomeContentUC_Load(object sender, EventArgs e)
		{
			Timer.Start(); //Start clock
		}

		/// <summary>
		/// Set the timer to the actual hour...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Timer_Tick(object sender, EventArgs e)
		{
			TimeLabel.Text = DateTime.Now.ToLongTimeString(); //When a tick occurs, change text and date to actual date, to simulate a clock.
			DateLabel.Text = DateTime.Now.ToString("D", new CultureInfo("en-US"));
		}

	}
}
