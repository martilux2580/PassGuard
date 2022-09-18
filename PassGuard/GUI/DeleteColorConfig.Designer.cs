﻿
namespace PassGuard.GUI
{
    partial class DeleteColorConfig
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
            this.RedLabel = new System.Windows.Forms.Label();
            this.NameTextbox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.EnableDeleteAllCheckbox = new System.Windows.Forms.CheckBox();
            this.DeleteAllButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.BlueNUD = new System.Windows.Forms.NumericUpDown();
            this.GreenNUD = new System.Windows.Forms.NumericUpDown();
            this.RedNUD = new System.Windows.Forms.NumericUpDown();
            this.FavouriteCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // NameCombobox
            // 
            this.NameCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NameCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameCombobox.FormattingEnabled = true;
            this.NameCombobox.Location = new System.Drawing.Point(373, 6);
            this.NameCombobox.Name = "NameCombobox";
            this.NameCombobox.Size = new System.Drawing.Size(219, 26);
            this.NameCombobox.TabIndex = 50;
            this.NameCombobox.SelectedIndexChanged += new System.EventHandler(this.NameCombobox_SelectedIndexChanged);
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(355, 18);
            this.TitleLabel.TabIndex = 49;
            this.TitleLabel.Text = "Select the name of the password you want to delete: ";
            // 
            // BlueLabel
            // 
            this.BlueLabel.AutoSize = true;
            this.BlueLabel.Enabled = false;
            this.BlueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlueLabel.Location = new System.Drawing.Point(36, 158);
            this.BlueLabel.Name = "BlueLabel";
            this.BlueLabel.Size = new System.Drawing.Size(85, 18);
            this.BlueLabel.TabIndex = 48;
            this.BlueLabel.Text = "Blue Value: ";
            // 
            // GreenLabel
            // 
            this.GreenLabel.AutoSize = true;
            this.GreenLabel.Enabled = false;
            this.GreenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GreenLabel.Location = new System.Drawing.Point(36, 120);
            this.GreenLabel.Name = "GreenLabel";
            this.GreenLabel.Size = new System.Drawing.Size(93, 18);
            this.GreenLabel.TabIndex = 47;
            this.GreenLabel.Text = "Green Value:";
            // 
            // RedLabel
            // 
            this.RedLabel.AutoSize = true;
            this.RedLabel.Enabled = false;
            this.RedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RedLabel.Location = new System.Drawing.Point(36, 82);
            this.RedLabel.Name = "RedLabel";
            this.RedLabel.Size = new System.Drawing.Size(83, 18);
            this.RedLabel.TabIndex = 46;
            this.RedLabel.Text = "Red Value: ";
            // 
            // NameTextbox
            // 
            this.NameTextbox.Enabled = false;
            this.NameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameTextbox.Location = new System.Drawing.Point(187, 42);
            this.NameTextbox.Name = "NameTextbox";
            this.NameTextbox.Size = new System.Drawing.Size(405, 22);
            this.NameTextbox.TabIndex = 45;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Enabled = false;
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(36, 45);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(56, 18);
            this.NameLabel.TabIndex = 44;
            this.NameLabel.Text = "Name: ";
            // 
            // EnableDeleteAllCheckbox
            // 
            this.EnableDeleteAllCheckbox.AutoSize = true;
            this.EnableDeleteAllCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnableDeleteAllCheckbox.Location = new System.Drawing.Point(407, 138);
            this.EnableDeleteAllCheckbox.Name = "EnableDeleteAllCheckbox";
            this.EnableDeleteAllCheckbox.Size = new System.Drawing.Size(185, 22);
            this.EnableDeleteAllCheckbox.TabIndex = 43;
            this.EnableDeleteAllCheckbox.Text = "Enable Delete-All Button";
            this.EnableDeleteAllCheckbox.UseVisualStyleBackColor = true;
            this.EnableDeleteAllCheckbox.CheckedChanged += new System.EventHandler(this.EnableDeleteAllCheckbox_CheckedChanged);
            // 
            // DeleteAllButton
            // 
            this.DeleteAllButton.Enabled = false;
            this.DeleteAllButton.FlatAppearance.BorderSize = 0;
            this.DeleteAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteAllButton.Location = new System.Drawing.Point(15, 186);
            this.DeleteAllButton.Name = "DeleteAllButton";
            this.DeleteAllButton.Size = new System.Drawing.Size(145, 30);
            this.DeleteAllButton.TabIndex = 42;
            this.DeleteAllButton.Text = "Delete All Elements";
            this.DeleteAllButton.UseVisualStyleBackColor = true;
            this.DeleteAllButton.Click += new System.EventHandler(this.DeleteAllButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.FlatAppearance.BorderSize = 0;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteButton.Location = new System.Drawing.Point(412, 186);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(180, 30);
            this.DeleteButton.TabIndex = 41;
            this.DeleteButton.Text = "Delete Selected Element";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // BlueNUD
            // 
            this.BlueNUD.Enabled = false;
            this.BlueNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlueNUD.Location = new System.Drawing.Point(187, 158);
            this.BlueNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.BlueNUD.Name = "BlueNUD";
            this.BlueNUD.Size = new System.Drawing.Size(164, 22);
            this.BlueNUD.TabIndex = 53;
            // 
            // GreenNUD
            // 
            this.GreenNUD.Enabled = false;
            this.GreenNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GreenNUD.Location = new System.Drawing.Point(187, 120);
            this.GreenNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GreenNUD.Name = "GreenNUD";
            this.GreenNUD.Size = new System.Drawing.Size(164, 22);
            this.GreenNUD.TabIndex = 52;
            // 
            // RedNUD
            // 
            this.RedNUD.Enabled = false;
            this.RedNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RedNUD.Location = new System.Drawing.Point(187, 82);
            this.RedNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RedNUD.Name = "RedNUD";
            this.RedNUD.Size = new System.Drawing.Size(164, 22);
            this.RedNUD.TabIndex = 51;
            // 
            // FavouriteCheckbox
            // 
            this.FavouriteCheckbox.AutoSize = true;
            this.FavouriteCheckbox.Enabled = false;
            this.FavouriteCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FavouriteCheckbox.Location = new System.Drawing.Point(407, 102);
            this.FavouriteCheckbox.Name = "FavouriteCheckbox";
            this.FavouriteCheckbox.Size = new System.Drawing.Size(143, 22);
            this.FavouriteCheckbox.TabIndex = 54;
            this.FavouriteCheckbox.Text = "Favourite Config?";
            this.FavouriteCheckbox.UseVisualStyleBackColor = true;
            // 
            // DeleteColorConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 228);
            this.Controls.Add(this.FavouriteCheckbox);
            this.Controls.Add(this.BlueNUD);
            this.Controls.Add(this.GreenNUD);
            this.Controls.Add(this.RedNUD);
            this.Controls.Add(this.NameCombobox);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.BlueLabel);
            this.Controls.Add(this.GreenLabel);
            this.Controls.Add(this.RedLabel);
            this.Controls.Add(this.NameTextbox);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.EnableDeleteAllCheckbox);
            this.Controls.Add(this.DeleteAllButton);
            this.Controls.Add(this.DeleteButton);
            this.Enabled = false;
            this.Name = "DeleteColorConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeleteColorConfig";
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
        private System.Windows.Forms.Label RedLabel;
        private System.Windows.Forms.TextBox NameTextbox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.CheckBox EnableDeleteAllCheckbox;
        private System.Windows.Forms.Button DeleteAllButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.NumericUpDown BlueNUD;
        private System.Windows.Forms.NumericUpDown GreenNUD;
        private System.Windows.Forms.NumericUpDown RedNUD;
        private System.Windows.Forms.CheckBox FavouriteCheckbox;
    }
}