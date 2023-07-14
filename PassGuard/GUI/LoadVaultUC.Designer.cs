
namespace PassGuard.GUI
{
    partial class LoadVaultUC
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
			this.VaultEmailLabel = new System.Windows.Forms.Label();
			this.VaultEmailTextbox = new System.Windows.Forms.TextBox();
			this.VaultPassLabel = new System.Windows.Forms.Label();
			this.VaultPassTextbox = new System.Windows.Forms.TextBox();
			this.SecurityKeyLabel = new System.Windows.Forms.Label();
			this.SecurityKeyTextbox = new System.Windows.Forms.TextBox();
			this.SelectVaultPathButton = new System.Windows.Forms.Button();
			this.VaultPathLabel = new System.Windows.Forms.Label();
			this.VaultPathTextbox = new System.Windows.Forms.TextBox();
			this.LoadSavedSKButton = new System.Windows.Forms.Button();
			this.LoadVaultButton = new System.Windows.Forms.Button();
			this.LoadSavedEmailButton = new System.Windows.Forms.Button();
			this.SaveEmailButton = new System.Windows.Forms.Button();
			this.SaveSKButton = new System.Windows.Forms.Button();
			this.PassVisibilityButton = new System.Windows.Forms.Button();
			this.SKVisibilityButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// VaultEmailLabel
			// 
			this.VaultEmailLabel.AutoSize = true;
			this.VaultEmailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultEmailLabel.Location = new System.Drawing.Point(102, 92);
			this.VaultEmailLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.VaultEmailLabel.Name = "VaultEmailLabel";
			this.VaultEmailLabel.Size = new System.Drawing.Size(89, 18);
			this.VaultEmailLabel.TabIndex = 18;
			this.VaultEmailLabel.Text = "User Email: ";
			// 
			// VaultEmailTextbox
			// 
			this.VaultEmailTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultEmailTextbox.Location = new System.Drawing.Point(284, 89);
			this.VaultEmailTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.VaultEmailTextbox.MaxLength = 2100;
			this.VaultEmailTextbox.Name = "VaultEmailTextbox";
			this.VaultEmailTextbox.Size = new System.Drawing.Size(336, 24);
			this.VaultEmailTextbox.TabIndex = 17;
			// 
			// VaultPassLabel
			// 
			this.VaultPassLabel.AutoSize = true;
			this.VaultPassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultPassLabel.Location = new System.Drawing.Point(102, 179);
			this.VaultPassLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.VaultPassLabel.Name = "VaultPassLabel";
			this.VaultPassLabel.Size = new System.Drawing.Size(132, 18);
			this.VaultPassLabel.TabIndex = 14;
			this.VaultPassLabel.Text = "Vault´s Password: ";
			this.VaultPassLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// VaultPassTextbox
			// 
			this.VaultPassTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultPassTextbox.Location = new System.Drawing.Point(284, 175);
			this.VaultPassTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.VaultPassTextbox.MaxLength = 2100;
			this.VaultPassTextbox.Name = "VaultPassTextbox";
			this.VaultPassTextbox.Size = new System.Drawing.Size(621, 24);
			this.VaultPassTextbox.TabIndex = 13;
			this.VaultPassTextbox.UseSystemPasswordChar = true;
			// 
			// SecurityKeyLabel
			// 
			this.SecurityKeyLabel.AutoSize = true;
			this.SecurityKeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SecurityKeyLabel.Location = new System.Drawing.Point(102, 267);
			this.SecurityKeyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SecurityKeyLabel.Name = "SecurityKeyLabel";
			this.SecurityKeyLabel.Size = new System.Drawing.Size(132, 18);
			this.SecurityKeyLabel.TabIndex = 12;
			this.SecurityKeyLabel.Text = "Security Key (SK): ";
			// 
			// SecurityKeyTextbox
			// 
			this.SecurityKeyTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SecurityKeyTextbox.Location = new System.Drawing.Point(284, 263);
			this.SecurityKeyTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SecurityKeyTextbox.MaxLength = 2100;
			this.SecurityKeyTextbox.Name = "SecurityKeyTextbox";
			this.SecurityKeyTextbox.Size = new System.Drawing.Size(336, 24);
			this.SecurityKeyTextbox.TabIndex = 11;
			this.SecurityKeyTextbox.UseSystemPasswordChar = true;
			// 
			// SelectVaultPathButton
			// 
			this.SelectVaultPathButton.FlatAppearance.BorderSize = 0;
			this.SelectVaultPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SelectVaultPathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SelectVaultPathButton.Image = global::PassGuard.Properties.Resources.FolderIcon;
			this.SelectVaultPathButton.Location = new System.Drawing.Point(912, 347);
			this.SelectVaultPathButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SelectVaultPathButton.Name = "SelectVaultPathButton";
			this.SelectVaultPathButton.Size = new System.Drawing.Size(29, 29);
			this.SelectVaultPathButton.TabIndex = 21;
			this.SelectVaultPathButton.UseVisualStyleBackColor = true;
			this.SelectVaultPathButton.Click += new System.EventHandler(this.SelectVaultPathButton_Click);
			// 
			// VaultPathLabel
			// 
			this.VaultPathLabel.AutoSize = true;
			this.VaultPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultPathLabel.Location = new System.Drawing.Point(102, 352);
			this.VaultPathLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.VaultPathLabel.Name = "VaultPathLabel";
			this.VaultPathLabel.Size = new System.Drawing.Size(95, 18);
			this.VaultPathLabel.TabIndex = 20;
			this.VaultPathLabel.Text = "Vault´s Path: ";
			// 
			// VaultPathTextbox
			// 
			this.VaultPathTextbox.Enabled = false;
			this.VaultPathTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultPathTextbox.Location = new System.Drawing.Point(284, 348);
			this.VaultPathTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.VaultPathTextbox.MaxLength = 2100;
			this.VaultPathTextbox.Name = "VaultPathTextbox";
			this.VaultPathTextbox.Size = new System.Drawing.Size(621, 24);
			this.VaultPathTextbox.TabIndex = 19;
			// 
			// LoadSavedSKButton
			// 
			this.LoadSavedSKButton.FlatAppearance.BorderSize = 0;
			this.LoadSavedSKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.LoadSavedSKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.LoadSavedSKButton.Location = new System.Drawing.Point(661, 262);
			this.LoadSavedSKButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.LoadSavedSKButton.Name = "LoadSavedSKButton";
			this.LoadSavedSKButton.Size = new System.Drawing.Size(136, 29);
			this.LoadSavedSKButton.TabIndex = 22;
			this.LoadSavedSKButton.Text = "Load Saved SK";
			this.LoadSavedSKButton.UseVisualStyleBackColor = true;
			this.LoadSavedSKButton.Click += new System.EventHandler(this.LoadSavedSKButton_Click);
			this.LoadSavedSKButton.MouseEnter += new System.EventHandler(this.LoadSavedSKButton_MouseEnter);
			this.LoadSavedSKButton.MouseLeave += new System.EventHandler(this.LoadSavedSKButton_MouseLeave);
			// 
			// LoadVaultButton
			// 
			this.LoadVaultButton.FlatAppearance.BorderSize = 0;
			this.LoadVaultButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.LoadVaultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.LoadVaultButton.Location = new System.Drawing.Point(420, 426);
			this.LoadVaultButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.LoadVaultButton.Name = "LoadVaultButton";
			this.LoadVaultButton.Size = new System.Drawing.Size(205, 38);
			this.LoadVaultButton.TabIndex = 23;
			this.LoadVaultButton.Text = "Load Password Vault";
			this.LoadVaultButton.UseVisualStyleBackColor = true;
			this.LoadVaultButton.Click += new System.EventHandler(this.LoadVaultButton_Click);
			this.LoadVaultButton.MouseEnter += new System.EventHandler(this.LoadVaultButton_MouseEnter);
			this.LoadVaultButton.MouseLeave += new System.EventHandler(this.LoadVaultButton_MouseLeave);
			// 
			// LoadSavedEmailButton
			// 
			this.LoadSavedEmailButton.FlatAppearance.BorderSize = 0;
			this.LoadSavedEmailButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.LoadSavedEmailButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.LoadSavedEmailButton.Location = new System.Drawing.Point(628, 88);
			this.LoadSavedEmailButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.LoadSavedEmailButton.Name = "LoadSavedEmailButton";
			this.LoadSavedEmailButton.Size = new System.Drawing.Size(155, 29);
			this.LoadSavedEmailButton.TabIndex = 24;
			this.LoadSavedEmailButton.Text = "Load Saved Email";
			this.LoadSavedEmailButton.UseVisualStyleBackColor = true;
			this.LoadSavedEmailButton.Click += new System.EventHandler(this.LoadSavedEmailButton_Click);
			this.LoadSavedEmailButton.MouseEnter += new System.EventHandler(this.LoadSavedEmailButton_MouseEnter);
			this.LoadSavedEmailButton.MouseLeave += new System.EventHandler(this.LoadSavedEmailButton_MouseLeave);
			// 
			// SaveEmailButton
			// 
			this.SaveEmailButton.FlatAppearance.BorderSize = 0;
			this.SaveEmailButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SaveEmailButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SaveEmailButton.Location = new System.Drawing.Point(786, 88);
			this.SaveEmailButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SaveEmailButton.Name = "SaveEmailButton";
			this.SaveEmailButton.Size = new System.Drawing.Size(155, 29);
			this.SaveEmailButton.TabIndex = 26;
			this.SaveEmailButton.Text = "Save Email";
			this.SaveEmailButton.UseVisualStyleBackColor = true;
			this.SaveEmailButton.Click += new System.EventHandler(this.SaveEmailButton_Click);
			// 
			// SaveSKButton
			// 
			this.SaveSKButton.FlatAppearance.BorderSize = 0;
			this.SaveSKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SaveSKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SaveSKButton.Location = new System.Drawing.Point(805, 262);
			this.SaveSKButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SaveSKButton.Name = "SaveSKButton";
			this.SaveSKButton.Size = new System.Drawing.Size(136, 29);
			this.SaveSKButton.TabIndex = 25;
			this.SaveSKButton.Text = "Save SK";
			this.SaveSKButton.UseVisualStyleBackColor = true;
			this.SaveSKButton.Click += new System.EventHandler(this.SaveSKButton_Click);
			// 
			// PassVisibilityButton
			// 
			this.PassVisibilityButton.FlatAppearance.BorderSize = 0;
			this.PassVisibilityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PassVisibilityButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.PassVisibilityButton.Image = global::PassGuard.Properties.Resources.VisibilityOn24;
			this.PassVisibilityButton.Location = new System.Drawing.Point(912, 174);
			this.PassVisibilityButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.PassVisibilityButton.Name = "PassVisibilityButton";
			this.PassVisibilityButton.Size = new System.Drawing.Size(29, 29);
			this.PassVisibilityButton.TabIndex = 27;
			this.PassVisibilityButton.UseVisualStyleBackColor = true;
			this.PassVisibilityButton.Click += new System.EventHandler(this.PassVisibilityButton_Click);
			// 
			// SKVisibilityButton
			// 
			this.SKVisibilityButton.FlatAppearance.BorderSize = 0;
			this.SKVisibilityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SKVisibilityButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SKVisibilityButton.Image = global::PassGuard.Properties.Resources.VisibilityOn24;
			this.SKVisibilityButton.Location = new System.Drawing.Point(628, 262);
			this.SKVisibilityButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SKVisibilityButton.Name = "SKVisibilityButton";
			this.SKVisibilityButton.Size = new System.Drawing.Size(29, 29);
			this.SKVisibilityButton.TabIndex = 28;
			this.SKVisibilityButton.UseVisualStyleBackColor = true;
			this.SKVisibilityButton.Click += new System.EventHandler(this.SKVisibilityButton_Click);
			// 
			// LoadVaultUC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SKVisibilityButton);
			this.Controls.Add(this.PassVisibilityButton);
			this.Controls.Add(this.SaveEmailButton);
			this.Controls.Add(this.SaveSKButton);
			this.Controls.Add(this.LoadSavedEmailButton);
			this.Controls.Add(this.LoadVaultButton);
			this.Controls.Add(this.LoadSavedSKButton);
			this.Controls.Add(this.SelectVaultPathButton);
			this.Controls.Add(this.VaultPathLabel);
			this.Controls.Add(this.VaultPathTextbox);
			this.Controls.Add(this.VaultEmailLabel);
			this.Controls.Add(this.VaultEmailTextbox);
			this.Controls.Add(this.VaultPassLabel);
			this.Controls.Add(this.VaultPassTextbox);
			this.Controls.Add(this.SecurityKeyLabel);
			this.Controls.Add(this.SecurityKeyTextbox);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "LoadVaultUC";
			this.Size = new System.Drawing.Size(1050, 512);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label VaultEmailLabel;
        private System.Windows.Forms.TextBox VaultEmailTextbox;
        private System.Windows.Forms.Label VaultPassLabel;
        private System.Windows.Forms.TextBox VaultPassTextbox;
        private System.Windows.Forms.Label SecurityKeyLabel;
        private System.Windows.Forms.TextBox SecurityKeyTextbox;
        private System.Windows.Forms.Button SelectVaultPathButton;
        private System.Windows.Forms.Label VaultPathLabel;
        private System.Windows.Forms.TextBox VaultPathTextbox;
        private System.Windows.Forms.Button LoadSavedSKButton;
        private System.Windows.Forms.Button LoadVaultButton;
        private System.Windows.Forms.Button LoadSavedEmailButton;
        private System.Windows.Forms.Button SaveEmailButton;
        private System.Windows.Forms.Button SaveSKButton;
		private System.Windows.Forms.Button PassVisibilityButton;
		private System.Windows.Forms.Button SKVisibilityButton;
	}
}
