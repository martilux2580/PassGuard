﻿
namespace PassGuard.GUI
{
    partial class AutoBackup
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
            this.SetupAutoBackupButton = new System.Windows.Forms.Button();
            this.ActivateBackupCheckbox = new System.Windows.Forms.CheckBox();
            this.SelectVaultPathButton = new System.Windows.Forms.Button();
            this.VaultPathLabel = new System.Windows.Forms.Label();
            this.VaultPathTextbox = new System.Windows.Forms.TextBox();
            this.SelectVaultBackupFilesPathButton = new System.Windows.Forms.Button();
            this.BackupPathFilesTextbox = new System.Windows.Forms.TextBox();
            this.BackupPathLabel = new System.Windows.Forms.Label();
            this.FrequencyLabel = new System.Windows.Forms.Label();
            this.FrequencyCombobox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // SetupAutoBackupButton
            // 
            this.SetupAutoBackupButton.Enabled = false;
            this.SetupAutoBackupButton.FlatAppearance.BorderSize = 0;
            this.SetupAutoBackupButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SetupAutoBackupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetupAutoBackupButton.Location = new System.Drawing.Point(262, 202);
            this.SetupAutoBackupButton.Name = "SetupAutoBackupButton";
            this.SetupAutoBackupButton.Size = new System.Drawing.Size(174, 36);
            this.SetupAutoBackupButton.TabIndex = 2;
            this.SetupAutoBackupButton.Text = "Setup AutoBackup";
            this.SetupAutoBackupButton.UseVisualStyleBackColor = true;
            this.SetupAutoBackupButton.Click += new System.EventHandler(this.SetupAutoBackupButton_Click);
            // 
            // ActivateBackupCheckbox
            // 
            this.ActivateBackupCheckbox.AutoSize = true;
            this.ActivateBackupCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivateBackupCheckbox.Location = new System.Drawing.Point(29, 19);
            this.ActivateBackupCheckbox.Name = "ActivateBackupCheckbox";
            this.ActivateBackupCheckbox.Size = new System.Drawing.Size(166, 22);
            this.ActivateBackupCheckbox.TabIndex = 3;
            this.ActivateBackupCheckbox.Text = "Activate AutoBackup:";
            this.ActivateBackupCheckbox.UseVisualStyleBackColor = true;
            this.ActivateBackupCheckbox.CheckedChanged += new System.EventHandler(this.ActivateBackupCheckbox_CheckedChanged);
            // 
            // SelectVaultPathButton
            // 
            this.SelectVaultPathButton.Enabled = false;
            this.SelectVaultPathButton.FlatAppearance.BorderSize = 0;
            this.SelectVaultPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectVaultPathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.SelectVaultPathButton.Location = new System.Drawing.Point(600, 56);
            this.SelectVaultPathButton.Name = "SelectVaultPathButton";
            this.SelectVaultPathButton.Size = new System.Drawing.Size(25, 25);
            this.SelectVaultPathButton.TabIndex = 24;
            this.SelectVaultPathButton.UseVisualStyleBackColor = true;
            this.SelectVaultPathButton.Click += new System.EventHandler(this.SelectVaultPathButton_Click);
            // 
            // VaultPathLabel
            // 
            this.VaultPathLabel.AutoSize = true;
            this.VaultPathLabel.Enabled = false;
            this.VaultPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VaultPathLabel.Location = new System.Drawing.Point(56, 61);
            this.VaultPathLabel.Name = "VaultPathLabel";
            this.VaultPathLabel.Size = new System.Drawing.Size(207, 16);
            this.VaultPathLabel.TabIndex = 23;
            this.VaultPathLabel.Text = "Path of the Vault to be backed-up:";
            // 
            // VaultPathTextbox
            // 
            this.VaultPathTextbox.Enabled = false;
            this.VaultPathTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VaultPathTextbox.Location = new System.Drawing.Point(269, 58);
            this.VaultPathTextbox.Name = "VaultPathTextbox";
            this.VaultPathTextbox.Size = new System.Drawing.Size(325, 22);
            this.VaultPathTextbox.TabIndex = 22;
            // 
            // SelectVaultBackupFilesPathButton
            // 
            this.SelectVaultBackupFilesPathButton.Enabled = false;
            this.SelectVaultBackupFilesPathButton.FlatAppearance.BorderSize = 0;
            this.SelectVaultBackupFilesPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectVaultBackupFilesPathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.SelectVaultBackupFilesPathButton.Location = new System.Drawing.Point(600, 100);
            this.SelectVaultBackupFilesPathButton.Name = "SelectVaultBackupFilesPathButton";
            this.SelectVaultBackupFilesPathButton.Size = new System.Drawing.Size(25, 25);
            this.SelectVaultBackupFilesPathButton.TabIndex = 27;
            this.SelectVaultBackupFilesPathButton.UseVisualStyleBackColor = true;
            this.SelectVaultBackupFilesPathButton.Click += new System.EventHandler(this.SelectVaultBackupFilesPathButton_Click);
            // 
            // BackupPathFilesTextbox
            // 
            this.BackupPathFilesTextbox.Enabled = false;
            this.BackupPathFilesTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackupPathFilesTextbox.Location = new System.Drawing.Point(269, 102);
            this.BackupPathFilesTextbox.Name = "BackupPathFilesTextbox";
            this.BackupPathFilesTextbox.Size = new System.Drawing.Size(325, 22);
            this.BackupPathFilesTextbox.TabIndex = 26;
            // 
            // BackupPathLabel
            // 
            this.BackupPathLabel.AutoSize = true;
            this.BackupPathLabel.Enabled = false;
            this.BackupPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackupPathLabel.Location = new System.Drawing.Point(56, 105);
            this.BackupPathLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BackupPathLabel.Name = "BackupPathLabel";
            this.BackupPathLabel.Size = new System.Drawing.Size(158, 16);
            this.BackupPathLabel.TabIndex = 25;
            this.BackupPathLabel.Text = "Path for the Backup Files:";
            // 
            // FrequencyLabel
            // 
            this.FrequencyLabel.AutoSize = true;
            this.FrequencyLabel.Enabled = false;
            this.FrequencyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrequencyLabel.Location = new System.Drawing.Point(56, 154);
            this.FrequencyLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FrequencyLabel.Name = "FrequencyLabel";
            this.FrequencyLabel.Size = new System.Drawing.Size(127, 16);
            this.FrequencyLabel.TabIndex = 28;
            this.FrequencyLabel.Text = "Backup Frequency: ";
            // 
            // FrequencyCombobox
            // 
            this.FrequencyCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FrequencyCombobox.Enabled = false;
            this.FrequencyCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrequencyCombobox.FormattingEnabled = true;
            this.FrequencyCombobox.Location = new System.Drawing.Point(269, 151);
            this.FrequencyCombobox.Name = "FrequencyCombobox";
            this.FrequencyCombobox.Size = new System.Drawing.Size(325, 24);
            this.FrequencyCombobox.TabIndex = 29;
            // 
            // AutoBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 261);
            this.Controls.Add(this.FrequencyCombobox);
            this.Controls.Add(this.FrequencyLabel);
            this.Controls.Add(this.SelectVaultBackupFilesPathButton);
            this.Controls.Add(this.BackupPathFilesTextbox);
            this.Controls.Add(this.BackupPathLabel);
            this.Controls.Add(this.SelectVaultPathButton);
            this.Controls.Add(this.VaultPathLabel);
            this.Controls.Add(this.VaultPathTextbox);
            this.Controls.Add(this.ActivateBackupCheckbox);
            this.Controls.Add(this.SetupAutoBackupButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AutoBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PassGuard™";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SetupAutoBackupButton;
        private System.Windows.Forms.CheckBox ActivateBackupCheckbox;
        private System.Windows.Forms.Button SelectVaultPathButton;
        private System.Windows.Forms.Label VaultPathLabel;
        private System.Windows.Forms.TextBox VaultPathTextbox;
        private System.Windows.Forms.Button SelectVaultBackupFilesPathButton;
        private System.Windows.Forms.TextBox BackupPathFilesTextbox;
        private System.Windows.Forms.Label BackupPathLabel;
        private System.Windows.Forms.Label FrequencyLabel;
        private System.Windows.Forms.ComboBox FrequencyCombobox;
    }
}