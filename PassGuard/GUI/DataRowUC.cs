using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
    public partial class DataRowUC : UserControl
    {
        public DataRowUC()
        {
            InitializeComponent();
        }

        private void DataRowUC_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            URLContent.Text = URLContent.Location.ToString();
            NameContent.Text = NameContent.Location.ToString();
            PassContent.Text = PassContent.Location.ToString();
            UsernameContent.Text = UsernameContent.Location.ToString();
            NotesContent.Text = NotesContent.Location.ToString();
            CategoryContent.Text = CategoryContent.Location.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
