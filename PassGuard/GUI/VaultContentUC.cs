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
    public partial class VaultContentUC : UserControl
    {
        private List<DataRowUC> fullContents = new List<DataRowUC>();

        public VaultContentUC()
        {
            InitializeComponent();
        }

        private void URLButton_Click(object sender, EventArgs e)
        {
            ContentFlowLayoutPanel.Controls.Add(new GUI.DataRowUC());
            URLButton.Text = URLButton.Location.ToString();
            NameButton.Text = NameButton.Location.ToString();
            UsernameButton.Text = UsernameButton.Location.ToString();
            PassButton.Text = PassButton.Location.ToString();
            CategoryButton.Text = CategoryButton.Location.ToString();
            NotesButton.Text = NotesButton.Location.ToString();
        }

        private void NameButton_Click(object sender, EventArgs e)
        {

        }

        private void UsernameButton_Click(object sender, EventArgs e)
        {

        }

        private void PassButton_Click(object sender, EventArgs e)
        {

        }

        private void CategoryButton_Click(object sender, EventArgs e)
        {

        }

        private void NotesButton_Click(object sender, EventArgs e)
        {

        }
    }
}
