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
	//UC Component with the date, it is shown at the main window.
	public partial class HomeContentUC : UserControl
	{
		public HomeContentUC()
		{
			this.Anchor = AnchorStyles.None;
			InitializeComponent();
		}

		private void HomeContentUC_Load(object sender, EventArgs e)
		{
			Timer.Start(); //Start clock
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			TimeLabel.Text = DateTime.Now.ToLongTimeString(); //When a tick occurs, change text and date to actual date, to simulate a clock.
			DateLabel.Text = DateTime.Now.ToString("D", new CultureInfo("en-US"));
		}
	}
}
