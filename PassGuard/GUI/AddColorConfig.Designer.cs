﻿
namespace PassGuard.GUI
{
    partial class AddColorConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TitleLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.RedLabel = new System.Windows.Forms.Label();
            this.BlueLabel = new System.Windows.Forms.Label();
            this.GreenLabel = new System.Windows.Forms.Label();
            this.NameTextbox = new System.Windows.Forms.TextBox();
            this.RedNUD = new System.Windows.Forms.NumericUpDown();
            this.GreenNUD = new System.Windows.Forms.NumericUpDown();
            this.BlueNUD = new System.Windows.Forms.NumericUpDown();
            this.SendButton = new System.Windows.Forms.Button();
            this.FavouriteCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.RedNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(36, 24);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(278, 18);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Insert the data for the new outline colour: ";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(57, 69);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(51, 16);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "Name: ";
            // 
            // RedLabel
            // 
            this.RedLabel.AutoSize = true;
            this.RedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RedLabel.Location = new System.Drawing.Point(57, 114);
            this.RedLabel.Name = "RedLabel";
            this.RedLabel.Size = new System.Drawing.Size(78, 16);
            this.RedLabel.TabIndex = 2;
            this.RedLabel.Text = "Red Value: ";
            // 
            // BlueLabel
            // 
            this.BlueLabel.AutoSize = true;
            this.BlueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlueLabel.Location = new System.Drawing.Point(57, 203);
            this.BlueLabel.Name = "BlueLabel";
            this.BlueLabel.Size = new System.Drawing.Size(73, 16);
            this.BlueLabel.TabIndex = 4;
            this.BlueLabel.Text = "Blue Value";
            // 
            // GreenLabel
            // 
            this.GreenLabel.AutoSize = true;
            this.GreenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GreenLabel.Location = new System.Drawing.Point(57, 158);
            this.GreenLabel.Name = "GreenLabel";
            this.GreenLabel.Size = new System.Drawing.Size(92, 16);
            this.GreenLabel.TabIndex = 3;
            this.GreenLabel.Text = "Green Value:  ";
            // 
            // NameTextbox
            // 
            this.NameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameTextbox.Location = new System.Drawing.Point(197, 66);
            this.NameTextbox.Name = "NameTextbox";
            this.NameTextbox.Size = new System.Drawing.Size(143, 22);
            this.NameTextbox.TabIndex = 5;
            // 
            // RedNUD
            // 
            this.RedNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RedNUD.Location = new System.Drawing.Point(197, 112);
            this.RedNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RedNUD.Name = "RedNUD";
            this.RedNUD.Size = new System.Drawing.Size(143, 22);
            this.RedNUD.TabIndex = 6;
            // 
            // GreenNUD
            // 
            this.GreenNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GreenNUD.Location = new System.Drawing.Point(197, 156);
            this.GreenNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GreenNUD.Name = "GreenNUD";
            this.GreenNUD.Size = new System.Drawing.Size(143, 22);
            this.GreenNUD.TabIndex = 7;
            // 
            // BlueNUD
            // 
            this.BlueNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlueNUD.Location = new System.Drawing.Point(197, 201);
            this.BlueNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.BlueNUD.Name = "BlueNUD";
            this.BlueNUD.Size = new System.Drawing.Size(143, 22);
            this.BlueNUD.TabIndex = 8;
            // 
            // SendButton
            // 
            this.SendButton.FlatAppearance.BorderSize = 0;
            this.SendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendButton.Location = new System.Drawing.Point(141, 288);
            this.SendButton.Margin = new System.Windows.Forms.Padding(2);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(99, 32);
            this.SendButton.TabIndex = 12;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // FavouriteCheckbox
            // 
            this.FavouriteCheckbox.AutoSize = true;
            this.FavouriteCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FavouriteCheckbox.Location = new System.Drawing.Point(100, 246);
            this.FavouriteCheckbox.Name = "FavouriteCheckbox";
            this.FavouriteCheckbox.Size = new System.Drawing.Size(199, 20);
            this.FavouriteCheckbox.TabIndex = 13;
            this.FavouriteCheckbox.Text = "Mark this config as Favourite.";
            this.FavouriteCheckbox.UseVisualStyleBackColor = true;
            // 
            // AddColorConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 345);
            this.Controls.Add(this.FavouriteCheckbox);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.BlueNUD);
            this.Controls.Add(this.GreenNUD);
            this.Controls.Add(this.RedNUD);
            this.Controls.Add(this.NameTextbox);
            this.Controls.Add(this.BlueLabel);
            this.Controls.Add(this.GreenLabel);
            this.Controls.Add(this.RedLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.TitleLabel);
            this.Name = "AddColorConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddColorConfig";
            ((System.ComponentModel.ISupportInitialize)(this.RedNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label RedLabel;
        private System.Windows.Forms.Label BlueLabel;
        private System.Windows.Forms.Label GreenLabel;
        private System.Windows.Forms.TextBox NameTextbox;
        private System.Windows.Forms.NumericUpDown RedNUD;
        private System.Windows.Forms.NumericUpDown GreenNUD;
        private System.Windows.Forms.NumericUpDown BlueNUD;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.CheckBox FavouriteCheckbox;
    }
}