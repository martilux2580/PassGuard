using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
        private String url { get; set; }
        private String name { get; set; }
        private String username { get; set; }
        private String password { get; set; }
        private String category { get; set; }
        private String notes { get; set; }

        private readonly byte[] Key;

        public DataRowUC(List<String> values, byte[] key)
        {
            InitializeComponent();
            Key = key;
            url = values[0];
            name = values[1];
            username = values[2];
            password = values[3];
            category = values[4];
            notes = values[5];

        }

        private void DataRowUC_Load(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();

            URLContent.Text = utils.DecryptText(key: Key, src: url);
            NameContent.Text = utils.DecryptText(key: Key, src: name);
            UsernameContent.Text = utils.DecryptText(key: Key, src: username);
            PassContent.Text = String.Concat(Enumerable.Repeat("*", 15)); 
            CategoryContent.Text = utils.DecryptText(key: Key, src: category);
            NotesContent.Text = utils.DecryptText(key: Key, src: notes);
        }

        private void PassContent_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();

            Clipboard.SetText(utils.DecryptText(key: Key, src: password));
            
        }

        private void URLContent_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(URLContent.Text)) Clipboard.SetText(" ");
            else Clipboard.SetText(URLContent.Text);
        }

        private void NameContent_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(NameContent.Text);
        }

        private void UsernameContent_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(UsernameContent.Text);
        }

        private void CategoryContent_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(CategoryContent.Text)) Clipboard.SetText(" ");
            else Clipboard.SetText(CategoryContent.Text);
        }

        private void NotesContent_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(NotesContent.Text)) Clipboard.SetText(" ");
            else Clipboard.SetText(NotesContent.Text);
        }
    }
}
