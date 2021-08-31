﻿using System;
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
            MenuPanel.BackColor = Color.FromArgb(20, 211, 164); //43, 43, 43              LeftButtons 
            LogoPanel.BackColor = Color.FromArgb(0, 184, 137); //31, 31, 31               Logo
            OptionsPanel.BackColor = Color.FromArgb(0, 191, 144); //38, 38, 38            Title
            this.BackColor = Color.FromArgb(240, 240, 240); //45, 45, 45                  Form Backcolor (collapsed by 4 panels)
            ContentPanel.BackColor = Color.FromArgb(240, 240, 240); //32, 32, 32          Content
            var directorio = Directory.GetCurrentDirectory();
            label1.Text = directorio;
            label1.Visible = false;
            LogoPictureBox.Image = Image.FromFile(@"..\..\Images\Logo.png"); //Working Directory inside Release Folder. Loads Image from Image folder.
            LogoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage; //Makes the image fit into PictureBox by resizing it.
            this.Icon = new Icon(@"..\..\Images\LogoIcon64.ico"); //Loads Icon from Image folder.
            SettingButton.Image = Image.FromFile(@"..\..\Images\Setting.ico");



            //Think of use cases of the app.
            //Determine buttons and functions
            //Cover all panels with the contents they will have before start coding functionality.
            //When changing window colors, disable black tones (0, 0, 0) so that letters and logo are visible.

            //lightToolStripMenuItem.Checked = true;
            //label2.ForeColor = Color.FromArgb(0, 0, 0); //100, 100, 100
            //label3.ForeColor = Color.FromArgb(0, 0, 0); //90, 90, 90
            //SettingPictureBox.Image = Image.FromFile(@"..\..\Images\Setting.jpg"); //Working Directory inside Release Folder. Loads Image from Image folder.
            //SettingPictureBox.SizeMode = PictureBoxSizeMode.StretchImage; //Makes the image fit into PictureBox by resizing it.
            //CreateVaultButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            //CreateVaultButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            //button1.Visible = false; //If button not visible, you cannot click it
            //LogoPictureBox.Image = null; //Remove Image from PictureBox (without removing PictureBox itself)
            //LogoPictureBox.BackColor = Color.FromArgb();



        }

        private void CreateVaultButton_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            TitleLabel.Text = "CREATING A NEW PASSWORD VAULT";
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
            TitleLabel.Text = "LOADING A PASSWORD VAULT";
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

        private void LogoPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            TitleLabel.Text = "HOME";
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            SettingsCMS.Show(SettingButton, new Point(SettingButton.Width - SettingsCMS.Width, SettingButton.Height));
            //SettingsCMS.BackColor = Color.FromArgb(45, 45, 45);
            //TitleSettingsToolStripMenuItem.BackColor = Color.FromArgb(45, 45, 45);
        }

        private void changeComplemenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI.AskRGBforSettings rgb = new GUI.AskRGBforSettings();
            if (darkToolStripMenuItem.Checked == true)
            {
                rgb.BackColor = Color.FromArgb(45, 45, 45);
            }
            else if (lightToolStripMenuItem.Checked == true)
            {
                rgb.BackColor = Color.FromArgb(240, 240, 240);
            }
            rgb.ShowDialog();
            int redValue = rgb.getRedNUDValue();
            int greenValue = rgb.getGreenNUDValue();
            int blueValue = rgb.getBlueNUDValue();


            if ((redValue > 235) && (greenValue > 235) && (blueValue > 235))
            {
                MenuPanel.BackColor = Color.FromArgb(245, 245, 245);
                LogoPanel.BackColor = Color.FromArgb(255, 255, 255); 
                OptionsPanel.BackColor = Color.FromArgb(250, 250, 250); 
            }
            else if (!((redValue < 32) && (greenValue < 32) && (greenValue < 32)))
            {
                if ((redValue > 235) || (greenValue > 235) || (blueValue > 235))
                {
                    if (redValue > 235)
                    {
                        redValue = 235;
                    }
                    if (greenValue > 235)
                    {
                        greenValue = 235;
                    }
                    if (blueValue > 235)
                    {
                        blueValue = 235;
                    }
                }
                MenuPanel.BackColor = Color.FromArgb(redValue + 20, greenValue + 20, blueValue + 20); //43, 43, 43      
                LogoPanel.BackColor = Color.FromArgb(redValue, greenValue, blueValue); //31, 31, 31    -10
                OptionsPanel.BackColor = Color.FromArgb(redValue + 10, greenValue + 10, blueValue + 10); //-
            }
            
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkToolStripMenuItem.Checked = true;
            lightToolStripMenuItem.Checked = false;
            ContentPanel.BackColor = Color.FromArgb(45, 45, 45);
            
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lightToolStripMenuItem.Checked = true;
            darkToolStripMenuItem.Checked = false;
            ContentPanel.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void saveChangesClosePassGuardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Implement "Save Changes" Part
            Application.Exit(); //Close Application
        }

        private void CreateQuickPassButton_MouseEnter(object sender, EventArgs e)
        {
            CreateQuickPassButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline);
        }

        private void CreateQuickPassButton_MouseLeave(object sender, EventArgs e)
        {
            CreateQuickPassButton.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
        }

        private void CreateQuickPassButton_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "CREATING A SAFE PASSWORD";
            if (label1.Visible == true)
            {
                label1.Visible = false;
            }
            else
            {
                label1.Visible = true;
            }
        }
    }
}
