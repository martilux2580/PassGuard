﻿
namespace PassGuard.GUI
{
    partial class EditColorConfig
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
			this.NameCombobox = new System.Windows.Forms.ComboBox();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.BlueLabel = new System.Windows.Forms.Label();
			this.GreenLabel = new System.Windows.Forms.Label();
			this.NameLabel = new System.Windows.Forms.Label();
			this.NameTextbox = new System.Windows.Forms.TextBox();
			this.EditButton = new System.Windows.Forms.Button();
			this.RedLabel = new System.Windows.Forms.Label();
			this.BlueNUD = new System.Windows.Forms.NumericUpDown();
			this.GreenNUD = new System.Windows.Forms.NumericUpDown();
			this.RedNUD = new System.Windows.Forms.NumericUpDown();
			this.FavouriteCheckbox = new System.Windows.Forms.CheckBox();
			this.ChosenCheckbox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RedNUD)).BeginInit();
			this.SuspendLayout();
			// 
			// NameCombobox
			// 
			this.NameCombobox.BackColor = System.Drawing.SystemColors.Window;
			this.NameCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.NameCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NameCombobox.FormattingEnabled = true;
			this.NameCombobox.IntegralHeight = false;
			this.NameCombobox.Location = new System.Drawing.Point(416, 7);
			this.NameCombobox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.NameCombobox.MaxLength = 2100;
			this.NameCombobox.Name = "NameCombobox";
			this.NameCombobox.Size = new System.Drawing.Size(279, 26);
			this.NameCombobox.TabIndex = 34;
			this.NameCombobox.SelectedIndexChanged += new System.EventHandler(this.NameCombobox_SelectedIndexChanged);
			// 
			// TitleLabel
			// 
			this.TitleLabel.AutoSize = true;
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TitleLabel.Location = new System.Drawing.Point(14, 10);
			this.TitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(314, 18);
			this.TitleLabel.TabIndex = 33;
			this.TitleLabel.Text = "Select the name of the config you want to edit: ";
			// 
			// BlueLabel
			// 
			this.BlueLabel.AutoSize = true;
			this.BlueLabel.Enabled = false;
			this.BlueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.BlueLabel.Location = new System.Drawing.Point(48, 179);
			this.BlueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.BlueLabel.Name = "BlueLabel";
			this.BlueLabel.Size = new System.Drawing.Size(85, 18);
			this.BlueLabel.TabIndex = 32;
			this.BlueLabel.Text = "Blue Value: ";
			// 
			// GreenLabel
			// 
			this.GreenLabel.AutoSize = true;
			this.GreenLabel.Enabled = false;
			this.GreenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.GreenLabel.Location = new System.Drawing.Point(48, 135);
			this.GreenLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.GreenLabel.Name = "GreenLabel";
			this.GreenLabel.Size = new System.Drawing.Size(97, 18);
			this.GreenLabel.TabIndex = 31;
			this.GreenLabel.Text = "Green Value: ";
			// 
			// NameLabel
			// 
			this.NameLabel.AutoSize = true;
			this.NameLabel.Enabled = false;
			this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NameLabel.Location = new System.Drawing.Point(48, 50);
			this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(56, 18);
			this.NameLabel.TabIndex = 30;
			this.NameLabel.Text = "Name: ";
			// 
			// NameTextbox
			// 
			this.NameTextbox.Enabled = false;
			this.NameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NameTextbox.Location = new System.Drawing.Point(224, 48);
			this.NameTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.NameTextbox.MaxLength = 2100;
			this.NameTextbox.Name = "NameTextbox";
			this.NameTextbox.Size = new System.Drawing.Size(472, 22);
			this.NameTextbox.TabIndex = 29;
			// 
			// EditButton
			// 
			this.EditButton.Enabled = false;
			this.EditButton.FlatAppearance.BorderSize = 0;
			this.EditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.EditButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.EditButton.Location = new System.Drawing.Point(492, 159);
			this.EditButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.EditButton.Name = "EditButton";
			this.EditButton.Size = new System.Drawing.Size(159, 42);
			this.EditButton.TabIndex = 27;
			this.EditButton.Text = "Edit Element";
			this.EditButton.UseVisualStyleBackColor = true;
			this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
			this.EditButton.MouseEnter += new System.EventHandler(this.EditButton_MouseEnter);
			this.EditButton.MouseLeave += new System.EventHandler(this.EditButton_MouseLeave);
			// 
			// RedLabel
			// 
			this.RedLabel.AutoSize = true;
			this.RedLabel.Enabled = false;
			this.RedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.RedLabel.Location = new System.Drawing.Point(46, 92);
			this.RedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.RedLabel.Name = "RedLabel";
			this.RedLabel.Size = new System.Drawing.Size(83, 18);
			this.RedLabel.TabIndex = 35;
			this.RedLabel.Text = "Red Value: ";
			// 
			// BlueNUD
			// 
			this.BlueNUD.Enabled = false;
			this.BlueNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.BlueNUD.Location = new System.Drawing.Point(224, 179);
			this.BlueNUD.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.BlueNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.BlueNUD.Name = "BlueNUD";
			this.BlueNUD.Size = new System.Drawing.Size(167, 22);
			this.BlueNUD.TabIndex = 38;
			// 
			// GreenNUD
			// 
			this.GreenNUD.Enabled = false;
			this.GreenNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.GreenNUD.Location = new System.Drawing.Point(224, 135);
			this.GreenNUD.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.GreenNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.GreenNUD.Name = "GreenNUD";
			this.GreenNUD.Size = new System.Drawing.Size(167, 22);
			this.GreenNUD.TabIndex = 37;
			// 
			// RedNUD
			// 
			this.RedNUD.Enabled = false;
			this.RedNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.RedNUD.Location = new System.Drawing.Point(224, 92);
			this.RedNUD.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.RedNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.RedNUD.Name = "RedNUD";
			this.RedNUD.Size = new System.Drawing.Size(167, 22);
			this.RedNUD.TabIndex = 36;
			// 
			// FavouriteCheckbox
			// 
			this.FavouriteCheckbox.AutoSize = true;
			this.FavouriteCheckbox.Enabled = false;
			this.FavouriteCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FavouriteCheckbox.Location = new System.Drawing.Point(506, 94);
			this.FavouriteCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.FavouriteCheckbox.Name = "FavouriteCheckbox";
			this.FavouriteCheckbox.Size = new System.Drawing.Size(130, 20);
			this.FavouriteCheckbox.TabIndex = 39;
			this.FavouriteCheckbox.Text = "Favourite Config?";
			this.FavouriteCheckbox.UseVisualStyleBackColor = true;
			// 
			// ChosenCheckbox
			// 
			this.ChosenCheckbox.AutoSize = true;
			this.ChosenCheckbox.Enabled = false;
			this.ChosenCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ChosenCheckbox.Location = new System.Drawing.Point(506, 120);
			this.ChosenCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.ChosenCheckbox.Name = "ChosenCheckbox";
			this.ChosenCheckbox.Size = new System.Drawing.Size(135, 20);
			this.ChosenCheckbox.TabIndex = 41;
			this.ChosenCheckbox.Text = "Select this Config?";
			this.ChosenCheckbox.UseVisualStyleBackColor = true;
			// 
			// EditColorConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(723, 221);
			this.Controls.Add(this.ChosenCheckbox);
			this.Controls.Add(this.FavouriteCheckbox);
			this.Controls.Add(this.BlueNUD);
			this.Controls.Add(this.GreenNUD);
			this.Controls.Add(this.RedNUD);
			this.Controls.Add(this.RedLabel);
			this.Controls.Add(this.NameCombobox);
			this.Controls.Add(this.TitleLabel);
			this.Controls.Add(this.BlueLabel);
			this.Controls.Add(this.GreenLabel);
			this.Controls.Add(this.NameLabel);
			this.Controls.Add(this.NameTextbox);
			this.Controls.Add(this.EditButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "EditColorConfig";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "EditColorConfig";
			this.BackColorChanged += new System.EventHandler(this.EditColorConfig_BackColorChanged);
			((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RedNUD)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox NameCombobox;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label BlueLabel;
        private System.Windows.Forms.Label GreenLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox NameTextbox;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Label RedLabel;
        private System.Windows.Forms.NumericUpDown BlueNUD;
        private System.Windows.Forms.NumericUpDown GreenNUD;
        private System.Windows.Forms.NumericUpDown RedNUD;
        private System.Windows.Forms.CheckBox FavouriteCheckbox;
		private System.Windows.Forms.CheckBox ChosenCheckbox;
	}
}