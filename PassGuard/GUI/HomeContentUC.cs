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
    public partial class HomeContentUC : UserControl
    {
        public HomeContentUC()
        {
            InitializeComponent();
        }

        private void HomeContentUC_Load(object sender, EventArgs e)
        {
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeLabel.Text = DateTime.Now.ToLongTimeString();
            DateLabel.Text = DateTime.Now.ToLongDateString(); 
            //CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Now.ToLongDateString());  //"Miércoles, 31 De Septiembre De 2021";


        }
    }
}
