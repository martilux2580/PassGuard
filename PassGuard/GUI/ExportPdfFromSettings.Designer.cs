namespace PassGuard.GUI
{
	partial class ExportPdfFromSettings
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SKVisibilityButton = new System.Windows.Forms.Button();
			this.PassVisibilityButton = new System.Windows.Forms.Button();
			this.SaveEmailButton = new System.Windows.Forms.Button();
			this.SaveSKButton = new System.Windows.Forms.Button();
			this.LoadSavedEmailButton = new System.Windows.Forms.Button();
			this.ExportPDFButton = new System.Windows.Forms.Button();
			this.LoadSavedSKButton = new System.Windows.Forms.Button();
			this.SelectVaultPathButton = new System.Windows.Forms.Button();
			this.VaultPathLabel = new System.Windows.Forms.Label();
			this.VaultPathTextbox = new System.Windows.Forms.TextBox();
			this.VaultEmailLabel = new System.Windows.Forms.Label();
			this.VaultEmailTextbox = new System.Windows.Forms.TextBox();
			this.VaultPassLabel = new System.Windows.Forms.Label();
			this.VaultPassTextbox = new System.Windows.Forms.TextBox();
			this.SecurityKeyLabel = new System.Windows.Forms.Label();
			this.SecurityKeyTextbox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// SKVisibilityButton
			// 
			this.SKVisibilityButton.FlatAppearance.BorderSize = 0;
			this.SKVisibilityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SKVisibilityButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SKVisibilityButton.Image = global::PassGuard.Properties.Resources.VisibilityOn24;
			this.SKVisibilityButton.Location = new System.Drawing.Point(632, 242);
			this.SKVisibilityButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SKVisibilityButton.Name = "SKVisibilityButton";
			this.SKVisibilityButton.Size = new System.Drawing.Size(29, 29);
			this.SKVisibilityButton.TabIndex = 44;
			this.SKVisibilityButton.UseVisualStyleBackColor = true;
			this.SKVisibilityButton.Click += new System.EventHandler(this.SKVisibilityButton_Click);
			// 
			// PassVisibilityButton
			// 
			this.PassVisibilityButton.FlatAppearance.BorderSize = 0;
			this.PassVisibilityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PassVisibilityButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.PassVisibilityButton.Image = global::PassGuard.Properties.Resources.VisibilityOn24;
			this.PassVisibilityButton.Location = new System.Drawing.Point(916, 154);
			this.PassVisibilityButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.PassVisibilityButton.Name = "PassVisibilityButton";
			this.PassVisibilityButton.Size = new System.Drawing.Size(29, 29);
			this.PassVisibilityButton.TabIndex = 43;
			this.PassVisibilityButton.UseVisualStyleBackColor = true;
			this.PassVisibilityButton.Click += new System.EventHandler(this.PassVisibilityButton_Click);
			// 
			// SaveEmailButton
			// 
			this.SaveEmailButton.FlatAppearance.BorderSize = 0;
			this.SaveEmailButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SaveEmailButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SaveEmailButton.Location = new System.Drawing.Point(790, 68);
			this.SaveEmailButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SaveEmailButton.Name = "SaveEmailButton";
			this.SaveEmailButton.Size = new System.Drawing.Size(155, 29);
			this.SaveEmailButton.TabIndex = 42;
			this.SaveEmailButton.Text = "Save Email";
			this.SaveEmailButton.UseVisualStyleBackColor = true;
			this.SaveEmailButton.Click += new System.EventHandler(this.SaveEmailButton_Click);
			this.SaveEmailButton.MouseEnter += new System.EventHandler(this.SaveEmailButton_MouseEnter);
			this.SaveEmailButton.MouseLeave += new System.EventHandler(this.SaveEmailButton_MouseLeave);
			// 
			// SaveSKButton
			// 
			this.SaveSKButton.FlatAppearance.BorderSize = 0;
			this.SaveSKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SaveSKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SaveSKButton.Location = new System.Drawing.Point(809, 242);
			this.SaveSKButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SaveSKButton.Name = "SaveSKButton";
			this.SaveSKButton.Size = new System.Drawing.Size(136, 29);
			this.SaveSKButton.TabIndex = 41;
			this.SaveSKButton.Text = "Save SK";
			this.SaveSKButton.UseVisualStyleBackColor = true;
			this.SaveSKButton.Click += new System.EventHandler(this.SaveSKButton_Click);
			this.SaveSKButton.MouseEnter += new System.EventHandler(this.SaveSKButton_MouseEnter);
			this.SaveSKButton.MouseLeave += new System.EventHandler(this.SaveSKButton_MouseLeave);
			// 
			// LoadSavedEmailButton
			// 
			this.LoadSavedEmailButton.FlatAppearance.BorderSize = 0;
			this.LoadSavedEmailButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.LoadSavedEmailButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.LoadSavedEmailButton.Location = new System.Drawing.Point(632, 68);
			this.LoadSavedEmailButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.LoadSavedEmailButton.Name = "LoadSavedEmailButton";
			this.LoadSavedEmailButton.Size = new System.Drawing.Size(155, 29);
			this.LoadSavedEmailButton.TabIndex = 40;
			this.LoadSavedEmailButton.Text = "Load Saved Email";
			this.LoadSavedEmailButton.UseVisualStyleBackColor = true;
			this.LoadSavedEmailButton.Click += new System.EventHandler(this.LoadSavedEmailButton_Click);
			this.LoadSavedEmailButton.MouseEnter += new System.EventHandler(this.LoadSavedEmailButton_MouseEnter);
			this.LoadSavedEmailButton.MouseLeave += new System.EventHandler(this.LoadSavedEmailButton_MouseLeave);
			// 
			// ExportPDFButton
			// 
			this.ExportPDFButton.FlatAppearance.BorderSize = 0;
			this.ExportPDFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ExportPDFButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ExportPDFButton.Location = new System.Drawing.Point(424, 406);
			this.ExportPDFButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.ExportPDFButton.Name = "ExportPDFButton";
			this.ExportPDFButton.Size = new System.Drawing.Size(205, 38);
			this.ExportPDFButton.TabIndex = 39;
			this.ExportPDFButton.Text = "Export Vault as PDF";
			this.ExportPDFButton.UseVisualStyleBackColor = true;
			this.ExportPDFButton.Click += new System.EventHandler(this.ExportPDFButton_Click);
			// 
			// LoadSavedSKButton
			// 
			this.LoadSavedSKButton.FlatAppearance.BorderSize = 0;
			this.LoadSavedSKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.LoadSavedSKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.LoadSavedSKButton.Location = new System.Drawing.Point(665, 242);
			this.LoadSavedSKButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.LoadSavedSKButton.Name = "LoadSavedSKButton";
			this.LoadSavedSKButton.Size = new System.Drawing.Size(136, 29);
			this.LoadSavedSKButton.TabIndex = 38;
			this.LoadSavedSKButton.Text = "Load Saved SK";
			this.LoadSavedSKButton.UseVisualStyleBackColor = true;
			this.LoadSavedSKButton.Click += new System.EventHandler(this.LoadSavedSKButton_Click);
			this.LoadSavedSKButton.MouseEnter += new System.EventHandler(this.LoadSavedSKButton_MouseEnter);
			this.LoadSavedSKButton.MouseLeave += new System.EventHandler(this.LoadSavedSKButton_MouseLeave);
			// 
			// SelectVaultPathButton
			// 
			this.SelectVaultPathButton.FlatAppearance.BorderSize = 0;
			this.SelectVaultPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SelectVaultPathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SelectVaultPathButton.Image = global::PassGuard.Properties.Resources.FolderIcon;
			this.SelectVaultPathButton.Location = new System.Drawing.Point(916, 327);
			this.SelectVaultPathButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SelectVaultPathButton.Name = "SelectVaultPathButton";
			this.SelectVaultPathButton.Size = new System.Drawing.Size(29, 29);
			this.SelectVaultPathButton.TabIndex = 37;
			this.SelectVaultPathButton.UseVisualStyleBackColor = true;
			this.SelectVaultPathButton.Click += new System.EventHandler(this.SelectVaultPathButton_Click);
			// 
			// VaultPathLabel
			// 
			this.VaultPathLabel.AutoSize = true;
			this.VaultPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultPathLabel.Location = new System.Drawing.Point(106, 332);
			this.VaultPathLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.VaultPathLabel.Name = "VaultPathLabel";
			this.VaultPathLabel.Size = new System.Drawing.Size(95, 18);
			this.VaultPathLabel.TabIndex = 36;
			this.VaultPathLabel.Text = "Vault´s Path: ";
			// 
			// VaultPathTextbox
			// 
			this.VaultPathTextbox.Enabled = false;
			this.VaultPathTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultPathTextbox.Location = new System.Drawing.Point(288, 328);
			this.VaultPathTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.VaultPathTextbox.MaxLength = 2100;
			this.VaultPathTextbox.Name = "VaultPathTextbox";
			this.VaultPathTextbox.Size = new System.Drawing.Size(621, 24);
			this.VaultPathTextbox.TabIndex = 35;
			// 
			// VaultEmailLabel
			// 
			this.VaultEmailLabel.AutoSize = true;
			this.VaultEmailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultEmailLabel.Location = new System.Drawing.Point(106, 72);
			this.VaultEmailLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.VaultEmailLabel.Name = "VaultEmailLabel";
			this.VaultEmailLabel.Size = new System.Drawing.Size(89, 18);
			this.VaultEmailLabel.TabIndex = 34;
			this.VaultEmailLabel.Text = "User Email: ";
			// 
			// VaultEmailTextbox
			// 
			this.VaultEmailTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultEmailTextbox.Location = new System.Drawing.Point(288, 69);
			this.VaultEmailTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.VaultEmailTextbox.MaxLength = 2100;
			this.VaultEmailTextbox.Name = "VaultEmailTextbox";
			this.VaultEmailTextbox.Size = new System.Drawing.Size(336, 24);
			this.VaultEmailTextbox.TabIndex = 33;
			// 
			// VaultPassLabel
			// 
			this.VaultPassLabel.AutoSize = true;
			this.VaultPassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultPassLabel.Location = new System.Drawing.Point(106, 159);
			this.VaultPassLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.VaultPassLabel.Name = "VaultPassLabel";
			this.VaultPassLabel.Size = new System.Drawing.Size(132, 18);
			this.VaultPassLabel.TabIndex = 32;
			this.VaultPassLabel.Text = "Vault´s Password: ";
			this.VaultPassLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// VaultPassTextbox
			// 
			this.VaultPassTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.VaultPassTextbox.Location = new System.Drawing.Point(288, 155);
			this.VaultPassTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.VaultPassTextbox.MaxLength = 2100;
			this.VaultPassTextbox.Name = "VaultPassTextbox";
			this.VaultPassTextbox.Size = new System.Drawing.Size(621, 24);
			this.VaultPassTextbox.TabIndex = 31;
			this.VaultPassTextbox.UseSystemPasswordChar = true;
			// 
			// SecurityKeyLabel
			// 
			this.SecurityKeyLabel.AutoSize = true;
			this.SecurityKeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SecurityKeyLabel.Location = new System.Drawing.Point(106, 247);
			this.SecurityKeyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SecurityKeyLabel.Name = "SecurityKeyLabel";
			this.SecurityKeyLabel.Size = new System.Drawing.Size(132, 18);
			this.SecurityKeyLabel.TabIndex = 30;
			this.SecurityKeyLabel.Text = "Security Key (SK): ";
			// 
			// SecurityKeyTextbox
			// 
			this.SecurityKeyTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SecurityKeyTextbox.Location = new System.Drawing.Point(288, 243);
			this.SecurityKeyTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SecurityKeyTextbox.MaxLength = 2100;
			this.SecurityKeyTextbox.Name = "SecurityKeyTextbox";
			this.SecurityKeyTextbox.Size = new System.Drawing.Size(336, 24);
			this.SecurityKeyTextbox.TabIndex = 29;
			this.SecurityKeyTextbox.UseSystemPasswordChar = true;
			// 
			// ExportPdfFromSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SKVisibilityButton);
			this.Controls.Add(this.PassVisibilityButton);
			this.Controls.Add(this.SaveEmailButton);
			this.Controls.Add(this.SaveSKButton);
			this.Controls.Add(this.LoadSavedEmailButton);
			this.Controls.Add(this.ExportPDFButton);
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
			this.Name = "ExportPdfFromSettings";
			this.Size = new System.Drawing.Size(1050, 512);
			this.BackColorChanged += new System.EventHandler(this.ExportPdfFromSettings_BackColorChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button SKVisibilityButton;
		private System.Windows.Forms.Button PassVisibilityButton;
		private System.Windows.Forms.Button SaveEmailButton;
		private System.Windows.Forms.Button SaveSKButton;
		private System.Windows.Forms.Button LoadSavedEmailButton;
		private System.Windows.Forms.Button ExportPDFButton;
		private System.Windows.Forms.Button LoadSavedSKButton;
		private System.Windows.Forms.Button SelectVaultPathButton;
		private System.Windows.Forms.Label VaultPathLabel;
		private System.Windows.Forms.TextBox VaultPathTextbox;
		private System.Windows.Forms.Label VaultEmailLabel;
		private System.Windows.Forms.TextBox VaultEmailTextbox;
		private System.Windows.Forms.Label VaultPassLabel;
		private System.Windows.Forms.TextBox VaultPassTextbox;
		private System.Windows.Forms.Label SecurityKeyLabel;
		private System.Windows.Forms.TextBox SecurityKeyTextbox;
	}
}
