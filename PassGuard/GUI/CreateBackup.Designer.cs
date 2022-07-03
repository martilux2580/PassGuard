
namespace PassGuard.GUI
{
    partial class CreateBackup
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
            this.SendButton = new System.Windows.Forms.Button();
            this.BackupPathLabel = new System.Windows.Forms.Label();
            this.SelectVaultBackupPathButton = new System.Windows.Forms.Button();
            this.VaultBackupPathTextbox = new System.Windows.Forms.TextBox();
            this.LoadSavedBackupPathButton = new System.Windows.Forms.Button();
            this.VaultPathTextbox = new System.Windows.Forms.TextBox();
            this.VaultPathLabel = new System.Windows.Forms.Label();
            this.SelectVaultPathButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SendButton
            // 
            this.SendButton.FlatAppearance.BorderSize = 0;
            this.SendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendButton.Location = new System.Drawing.Point(271, 110);
            this.SendButton.Margin = new System.Windows.Forms.Padding(2);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(116, 31);
            this.SendButton.TabIndex = 19;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            this.SendButton.MouseEnter += new System.EventHandler(this.SendButton_MouseEnter);
            this.SendButton.MouseLeave += new System.EventHandler(this.SendButton_MouseLeave);
            // 
            // BackupPathLabel
            // 
            this.BackupPathLabel.AutoSize = true;
            this.BackupPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackupPathLabel.Location = new System.Drawing.Point(12, 60);
            this.BackupPathLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BackupPathLabel.Name = "BackupPathLabel";
            this.BackupPathLabel.Size = new System.Drawing.Size(82, 15);
            this.BackupPathLabel.TabIndex = 10;
            this.BackupPathLabel.Text = "Backup Path: ";
            // 
            // SelectVaultBackupPathButton
            // 
            this.SelectVaultBackupPathButton.FlatAppearance.BorderSize = 0;
            this.SelectVaultBackupPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectVaultBackupPathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.SelectVaultBackupPathButton.Location = new System.Drawing.Point(364, 56);
            this.SelectVaultBackupPathButton.Name = "SelectVaultBackupPathButton";
            this.SelectVaultBackupPathButton.Size = new System.Drawing.Size(25, 25);
            this.SelectVaultBackupPathButton.TabIndex = 21;
            this.SelectVaultBackupPathButton.UseVisualStyleBackColor = true;
            this.SelectVaultBackupPathButton.Click += new System.EventHandler(this.SelectVaultBackupPathButton_Click);
            // 
            // VaultBackupPathTextbox
            // 
            this.VaultBackupPathTextbox.Enabled = false;
            this.VaultBackupPathTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.VaultBackupPathTextbox.Location = new System.Drawing.Point(99, 56);
            this.VaultBackupPathTextbox.Name = "VaultBackupPathTextbox";
            this.VaultBackupPathTextbox.Size = new System.Drawing.Size(259, 24);
            this.VaultBackupPathTextbox.TabIndex = 20;
            // 
            // LoadSavedBackupPathButton
            // 
            this.LoadSavedBackupPathButton.FlatAppearance.BorderSize = 0;
            this.LoadSavedBackupPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadSavedBackupPathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadSavedBackupPathButton.Location = new System.Drawing.Point(15, 110);
            this.LoadSavedBackupPathButton.Margin = new System.Windows.Forms.Padding(2);
            this.LoadSavedBackupPathButton.Name = "LoadSavedBackupPathButton";
            this.LoadSavedBackupPathButton.Size = new System.Drawing.Size(179, 31);
            this.LoadSavedBackupPathButton.TabIndex = 22;
            this.LoadSavedBackupPathButton.Text = "Load Saved AutoBackup Path";
            this.LoadSavedBackupPathButton.UseVisualStyleBackColor = true;
            this.LoadSavedBackupPathButton.Click += new System.EventHandler(this.LoadSavedBackupPathButton_Click);
            this.LoadSavedBackupPathButton.MouseEnter += new System.EventHandler(this.LoadSavedBackupPathButton_MouseEnter);
            this.LoadSavedBackupPathButton.MouseLeave += new System.EventHandler(this.LoadSavedBackupPathButton_MouseLeave);
            // 
            // VaultPathTextbox
            // 
            this.VaultPathTextbox.Enabled = false;
            this.VaultPathTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.VaultPathTextbox.Location = new System.Drawing.Point(99, 21);
            this.VaultPathTextbox.Name = "VaultPathTextbox";
            this.VaultPathTextbox.Size = new System.Drawing.Size(259, 24);
            this.VaultPathTextbox.TabIndex = 24;
            // 
            // VaultPathLabel
            // 
            this.VaultPathLabel.AutoSize = true;
            this.VaultPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VaultPathLabel.Location = new System.Drawing.Point(12, 25);
            this.VaultPathLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VaultPathLabel.Name = "VaultPathLabel";
            this.VaultPathLabel.Size = new System.Drawing.Size(68, 15);
            this.VaultPathLabel.TabIndex = 23;
            this.VaultPathLabel.Text = "Vault Path: ";
            // 
            // SelectVaultPathButton
            // 
            this.SelectVaultPathButton.FlatAppearance.BorderSize = 0;
            this.SelectVaultPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectVaultPathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.SelectVaultPathButton.Location = new System.Drawing.Point(364, 21);
            this.SelectVaultPathButton.Name = "SelectVaultPathButton";
            this.SelectVaultPathButton.Size = new System.Drawing.Size(25, 25);
            this.SelectVaultPathButton.TabIndex = 25;
            this.SelectVaultPathButton.UseVisualStyleBackColor = true;
            this.SelectVaultPathButton.Click += new System.EventHandler(this.SelectVaultPathButton_Click);
            // 
            // CreateBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 161);
            this.Controls.Add(this.SelectVaultPathButton);
            this.Controls.Add(this.VaultPathTextbox);
            this.Controls.Add(this.VaultPathLabel);
            this.Controls.Add(this.LoadSavedBackupPathButton);
            this.Controls.Add(this.SelectVaultBackupPathButton);
            this.Controls.Add(this.VaultBackupPathTextbox);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.BackupPathLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CreateBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PassGuard™";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Label BackupPathLabel;
        private System.Windows.Forms.Button SelectVaultBackupPathButton;
        private System.Windows.Forms.TextBox VaultBackupPathTextbox;
        private System.Windows.Forms.Button LoadSavedBackupPathButton;
        private System.Windows.Forms.TextBox VaultPathTextbox;
        private System.Windows.Forms.Label VaultPathLabel;
        private System.Windows.Forms.Button SelectVaultPathButton;
    }
}