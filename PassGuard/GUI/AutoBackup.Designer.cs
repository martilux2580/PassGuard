
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
			this.NoteLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// SetupAutoBackupButton
			// 
			this.SetupAutoBackupButton.FlatAppearance.BorderSize = 0;
			this.SetupAutoBackupButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SetupAutoBackupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SetupAutoBackupButton.Location = new System.Drawing.Point(306, 232);
			this.SetupAutoBackupButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SetupAutoBackupButton.Name = "SetupAutoBackupButton";
			this.SetupAutoBackupButton.Size = new System.Drawing.Size(203, 42);
			this.SetupAutoBackupButton.TabIndex = 2;
			this.SetupAutoBackupButton.Text = "Setup AutoBackup";
			this.SetupAutoBackupButton.UseVisualStyleBackColor = true;
			this.SetupAutoBackupButton.Click += new System.EventHandler(this.SetupAutoBackupButton_Click);
			this.SetupAutoBackupButton.MouseEnter += new System.EventHandler(this.SetupAutoBackupButton_MouseEnter);
			this.SetupAutoBackupButton.MouseLeave += new System.EventHandler(this.SetupAutoBackupButton_MouseLeave);
			// 
			// ActivateBackupCheckbox
			// 
			this.ActivateBackupCheckbox.AutoSize = true;
			this.ActivateBackupCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ActivateBackupCheckbox.Location = new System.Drawing.Point(34, 22);
			this.ActivateBackupCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
			this.SelectVaultPathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SelectVaultPathButton.Image = global::PassGuard.Properties.Resources.FolderIcon;
			this.SelectVaultPathButton.Location = new System.Drawing.Point(700, 65);
			this.SelectVaultPathButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SelectVaultPathButton.Name = "SelectVaultPathButton";
			this.SelectVaultPathButton.Size = new System.Drawing.Size(29, 29);
			this.SelectVaultPathButton.TabIndex = 24;
			this.SelectVaultPathButton.UseVisualStyleBackColor = true;
			this.SelectVaultPathButton.Click += new System.EventHandler(this.SelectVaultPathButton_Click);
			// 
			// VaultPathLabel
			// 
			this.VaultPathLabel.AutoSize = true;
			this.VaultPathLabel.Enabled = false;
			this.VaultPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultPathLabel.Location = new System.Drawing.Point(65, 70);
			this.VaultPathLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.VaultPathLabel.Name = "VaultPathLabel";
			this.VaultPathLabel.Size = new System.Drawing.Size(206, 16);
			this.VaultPathLabel.TabIndex = 23;
			this.VaultPathLabel.Text = "Path of the Vault to be backed-up:";
			// 
			// VaultPathTextbox
			// 
			this.VaultPathTextbox.Enabled = false;
			this.VaultPathTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultPathTextbox.Location = new System.Drawing.Point(314, 67);
			this.VaultPathTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.VaultPathTextbox.MaxLength = 2100;
			this.VaultPathTextbox.Name = "VaultPathTextbox";
			this.VaultPathTextbox.ReadOnly = true;
			this.VaultPathTextbox.Size = new System.Drawing.Size(378, 22);
			this.VaultPathTextbox.TabIndex = 22;
			// 
			// SelectVaultBackupFilesPathButton
			// 
			this.SelectVaultBackupFilesPathButton.Enabled = false;
			this.SelectVaultBackupFilesPathButton.FlatAppearance.BorderSize = 0;
			this.SelectVaultBackupFilesPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SelectVaultBackupFilesPathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SelectVaultBackupFilesPathButton.Image = global::PassGuard.Properties.Resources.FolderIcon;
			this.SelectVaultBackupFilesPathButton.Location = new System.Drawing.Point(700, 115);
			this.SelectVaultBackupFilesPathButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SelectVaultBackupFilesPathButton.Name = "SelectVaultBackupFilesPathButton";
			this.SelectVaultBackupFilesPathButton.Size = new System.Drawing.Size(29, 29);
			this.SelectVaultBackupFilesPathButton.TabIndex = 27;
			this.SelectVaultBackupFilesPathButton.UseVisualStyleBackColor = true;
			this.SelectVaultBackupFilesPathButton.Click += new System.EventHandler(this.SelectVaultBackupFilesPathButton_Click);
			// 
			// BackupPathFilesTextbox
			// 
			this.BackupPathFilesTextbox.Enabled = false;
			this.BackupPathFilesTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.BackupPathFilesTextbox.Location = new System.Drawing.Point(314, 118);
			this.BackupPathFilesTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.BackupPathFilesTextbox.MaxLength = 2100;
			this.BackupPathFilesTextbox.Name = "BackupPathFilesTextbox";
			this.BackupPathFilesTextbox.ReadOnly = true;
			this.BackupPathFilesTextbox.Size = new System.Drawing.Size(378, 22);
			this.BackupPathFilesTextbox.TabIndex = 26;
			// 
			// BackupPathLabel
			// 
			this.BackupPathLabel.AutoSize = true;
			this.BackupPathLabel.Enabled = false;
			this.BackupPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.BackupPathLabel.Location = new System.Drawing.Point(65, 121);
			this.BackupPathLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.BackupPathLabel.Name = "BackupPathLabel";
			this.BackupPathLabel.Size = new System.Drawing.Size(157, 16);
			this.BackupPathLabel.TabIndex = 25;
			this.BackupPathLabel.Text = "Path for the Backup Files:";
			// 
			// FrequencyLabel
			// 
			this.FrequencyLabel.AutoSize = true;
			this.FrequencyLabel.Enabled = false;
			this.FrequencyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FrequencyLabel.Location = new System.Drawing.Point(65, 178);
			this.FrequencyLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.FrequencyLabel.Name = "FrequencyLabel";
			this.FrequencyLabel.Size = new System.Drawing.Size(126, 16);
			this.FrequencyLabel.TabIndex = 28;
			this.FrequencyLabel.Text = "Backup Frequency: ";
			// 
			// FrequencyCombobox
			// 
			this.FrequencyCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FrequencyCombobox.Enabled = false;
			this.FrequencyCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FrequencyCombobox.FormattingEnabled = true;
			this.FrequencyCombobox.IntegralHeight = false;
			this.FrequencyCombobox.Location = new System.Drawing.Point(314, 174);
			this.FrequencyCombobox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.FrequencyCombobox.MaxLength = 2100;
			this.FrequencyCombobox.Name = "FrequencyCombobox";
			this.FrequencyCombobox.Size = new System.Drawing.Size(378, 24);
			this.FrequencyCombobox.TabIndex = 29;
			// 
			// NoteLabel
			// 
			this.NoteLabel.AutoSize = true;
			this.NoteLabel.Location = new System.Drawing.Point(302, 28);
			this.NoteLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NoteLabel.Name = "NoteLabel";
			this.NoteLabel.Size = new System.Drawing.Size(449, 15);
			this.NoteLabel.TabIndex = 30;
			this.NoteLabel.Text = "Note: AutoBackup will execute and create backups only when PassGuard is running.";
			// 
			// AutoBackup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(796, 301);
			this.Controls.Add(this.NoteLabel);
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
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "AutoBackup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PassGuard™";
			this.BackColorChanged += new System.EventHandler(this.AutoBackup_BackColorChanged);
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
        private System.Windows.Forms.Label NoteLabel;
    }
}