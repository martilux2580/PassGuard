using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PassGuard
{
    public partial class mainWindow : Form
    {
        public mainWindow()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MenuPanel.BackColor = Color.FromArgb(5, 196, 149); //43, 43, 43
            LogoPanel.BackColor = Color.FromArgb(0, 184, 137); //31, 31, 31
            OptionsPanel.BackColor = Color.FromArgb(0, 191, 144); //38, 38, 38
            this.BackColor = Color.FromArgb(225, 225, 225); //45, 45, 45
            var directorio = Directory.GetCurrentDirectory();
            label1.Text = directorio;
            label1.Visible = false;
            LogoPictureBox.Image = Image.FromFile(@"..\..\Images\Logo.png"); //Working Directory inside Release Folder. Loads Image from Image folder.
            LogoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage; //Makes the image fit into PictureBox by resizing it.
            this.Icon = new Icon(@"..\..\Images\LogoIcon64.ico"); //Loads Icon from Image folder.
            //Determine buttons and functions
            //Think of use cases of the app.
            //Cover all panels with the contents they will have before start coding functionality.
            //When changing window colors, disable black tones (0, 0, 0) so that letters and logo are visible.

            //label2.ForeColor = Color.FromArgb(0, 0, 0); //100, 100, 100
            //label3.ForeColor = Color.FromArgb(0, 0, 0); //90, 90, 90
            //CreateVaultButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //CreateVaultButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            //button1.Visible = false; //If button not visible, you cannot click it
            //LogoPictureBox.Image = null; //Remove Image from PictureBox (without removing PictureBox itself)
            //LogoPictureBox.BackColor = Color.FromArgb();



        }

        private void CreateVaultButton_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
        }

        private void CreateVaultButton_MouseEnter(object sender, EventArgs e)
        {
            CreateVaultButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline);
        }

        private void CreateVaultButton_MouseLeave(object sender, EventArgs e)
        {
            CreateVaultButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
        }

        private void LoadVaultButton_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void LoadVaultButton_MouseEnter(object sender, EventArgs e)
        {
            LoadVaultButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline);
        }

        private void LoadVaultButton_MouseLeave(object sender, EventArgs e)
        {
            LoadVaultButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
        }

        private void DesignerLabel_MouseEnter(object sender, EventArgs e)
        {
            DesignerLabel.Font = new Font("Mongolian Baiti", 10, FontStyle.Underline);
        }

        private void DesignerLabel_MouseLeave(object sender, EventArgs e)
        {
            DesignerLabel.Font = new Font("Mongolian Baiti", 10, FontStyle.Regular);
        }

        private void DesignerLabel_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/martilux2580?tab=repositories");
        }
    }
}
