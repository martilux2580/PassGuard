namespace PassGuard.GUI
{
	partial class VaultStats
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
			this.UnderstoodButton = new System.Windows.Forms.Button();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.StatTypeCombobox = new System.Windows.Forms.ComboBox();
			this.StatsPanel = new System.Windows.Forms.Panel();
			this.ResetButton = new System.Windows.Forms.Button();
			this.SearchButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// UnderstoodButton
			// 
			this.UnderstoodButton.FlatAppearance.BorderSize = 0;
			this.UnderstoodButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.UnderstoodButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.UnderstoodButton.Location = new System.Drawing.Point(507, 533);
			this.UnderstoodButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.UnderstoodButton.Name = "UnderstoodButton";
			this.UnderstoodButton.Size = new System.Drawing.Size(130, 38);
			this.UnderstoodButton.TabIndex = 25;
			this.UnderstoodButton.Text = "Understood";
			this.UnderstoodButton.UseVisualStyleBackColor = true;
			this.UnderstoodButton.Click += new System.EventHandler(this.UnderstoodButton_Click);
			this.UnderstoodButton.MouseEnter += new System.EventHandler(this.UnderstoodButton_MouseEnter);
			this.UnderstoodButton.MouseLeave += new System.EventHandler(this.UnderstoodButton_MouseLeave);
			// 
			// TitleLabel
			// 
			this.TitleLabel.AutoSize = true;
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TitleLabel.Location = new System.Drawing.Point(30, 22);
			this.TitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(311, 18);
			this.TitleLabel.TabIndex = 26;
			this.TitleLabel.Text = "Select the type of statistics you want to show: ";
			// 
			// StatTypeCombobox
			// 
			this.StatTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.StatTypeCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.StatTypeCombobox.FormattingEnabled = true;
			this.StatTypeCombobox.Items.AddRange(new object[] {
            "",
            "Content Properties",
            "Security Properties"});
			this.StatTypeCombobox.Location = new System.Drawing.Point(361, 19);
			this.StatTypeCombobox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.StatTypeCombobox.MaxLength = 2100;
			this.StatTypeCombobox.Name = "StatTypeCombobox";
			this.StatTypeCombobox.Size = new System.Drawing.Size(473, 26);
			this.StatTypeCombobox.TabIndex = 35;
			this.StatTypeCombobox.TextChanged += new System.EventHandler(this.StatTypeCombobox_TextChanged);
			// 
			// StatsPanel
			// 
			this.StatsPanel.Location = new System.Drawing.Point(12, 64);
			this.StatsPanel.Name = "StatsPanel";
			this.StatsPanel.Size = new System.Drawing.Size(1144, 463);
			this.StatsPanel.TabIndex = 36;
			// 
			// ResetButton
			// 
			this.ResetButton.Enabled = false;
			this.ResetButton.FlatAppearance.BorderSize = 0;
			this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ResetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ResetButton.Location = new System.Drawing.Point(876, 16);
			this.ResetButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.ResetButton.Name = "ResetButton";
			this.ResetButton.Size = new System.Drawing.Size(119, 31);
			this.ResetButton.TabIndex = 38;
			this.ResetButton.Text = "Reset";
			this.ResetButton.UseVisualStyleBackColor = true;
			this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
			this.ResetButton.MouseEnter += new System.EventHandler(this.ResetButton_MouseEnter);
			this.ResetButton.MouseLeave += new System.EventHandler(this.ResetButton_MouseLeave);
			// 
			// SearchButton
			// 
			this.SearchButton.Enabled = false;
			this.SearchButton.FlatAppearance.BorderSize = 0;
			this.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SearchButton.Location = new System.Drawing.Point(1037, 16);
			this.SearchButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SearchButton.Name = "SearchButton";
			this.SearchButton.Size = new System.Drawing.Size(119, 31);
			this.SearchButton.TabIndex = 37;
			this.SearchButton.Text = "Search";
			this.SearchButton.UseVisualStyleBackColor = true;
			this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
			this.SearchButton.MouseEnter += new System.EventHandler(this.SearchButton_MouseEnter);
			this.SearchButton.MouseLeave += new System.EventHandler(this.SearchButton_MouseLeave);
			// 
			// VaultStats
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1168, 578);
			this.Controls.Add(this.ResetButton);
			this.Controls.Add(this.SearchButton);
			this.Controls.Add(this.StatsPanel);
			this.Controls.Add(this.StatTypeCombobox);
			this.Controls.Add(this.TitleLabel);
			this.Controls.Add(this.UnderstoodButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "VaultStats";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "VaultStats";
			this.BackColorChanged += new System.EventHandler(this.VaultStats_BackColorChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button UnderstoodButton;
		private System.Windows.Forms.Label TitleLabel;
		private System.Windows.Forms.ComboBox StatTypeCombobox;
		private System.Windows.Forms.Panel StatsPanel;
		private System.Windows.Forms.Button ResetButton;
		private System.Windows.Forms.Button SearchButton;
	}
}