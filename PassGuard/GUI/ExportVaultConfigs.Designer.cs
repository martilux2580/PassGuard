namespace PassGuard.GUI
{
	partial class ExportVaultConfigs
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
			this.ExportButton = new System.Windows.Forms.Button();
			this.PdfRadioButton = new System.Windows.Forms.RadioButton();
			this.JsonRadioButton = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// TitleLabel
			// 
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TitleLabel.Location = new System.Drawing.Point(11, 9);
			this.TitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(434, 23);
			this.TitleLabel.TabIndex = 2;
			this.TitleLabel.Text = "Select how to want to export your configs: ";
			// 
			// ExportButton
			// 
			this.ExportButton.FlatAppearance.BorderSize = 0;
			this.ExportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ExportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ExportButton.Location = new System.Drawing.Point(126, 158);
			this.ExportButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.ExportButton.Name = "ExportButton";
			this.ExportButton.Size = new System.Drawing.Size(114, 39);
			this.ExportButton.TabIndex = 28;
			this.ExportButton.Text = "Export";
			this.ExportButton.UseVisualStyleBackColor = true;
			this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
			this.ExportButton.MouseEnter += new System.EventHandler(this.ExportButton_MouseEnter);
			this.ExportButton.MouseLeave += new System.EventHandler(this.ExportButton_MouseLeave);
			// 
			// PdfRadioButton
			// 
			this.PdfRadioButton.AutoSize = true;
			this.PdfRadioButton.Checked = true;
			this.PdfRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.PdfRadioButton.Location = new System.Drawing.Point(64, 54);
			this.PdfRadioButton.Name = "PdfRadioButton";
			this.PdfRadioButton.Size = new System.Drawing.Size(158, 21);
			this.PdfRadioButton.TabIndex = 29;
			this.PdfRadioButton.TabStop = true;
			this.PdfRadioButton.Text = "Export config as PDF";
			this.PdfRadioButton.UseVisualStyleBackColor = true;
			this.PdfRadioButton.CheckedChanged += new System.EventHandler(this.PdfRadioButton_CheckedChanged);
			// 
			// JsonRadioButton
			// 
			this.JsonRadioButton.AutoSize = true;
			this.JsonRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.JsonRadioButton.Location = new System.Drawing.Point(64, 100);
			this.JsonRadioButton.Name = "JsonRadioButton";
			this.JsonRadioButton.Size = new System.Drawing.Size(202, 21);
			this.JsonRadioButton.TabIndex = 30;
			this.JsonRadioButton.Text = "Export config as a JSON file";
			this.JsonRadioButton.UseVisualStyleBackColor = true;
			this.JsonRadioButton.CheckedChanged += new System.EventHandler(this.JsonRadioButton_CheckedChanged);
			// 
			// ExportVaultConfigs
			// 
			this.AcceptButton = this.ExportButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(361, 222);
			this.Controls.Add(this.JsonRadioButton);
			this.Controls.Add(this.PdfRadioButton);
			this.Controls.Add(this.ExportButton);
			this.Controls.Add(this.TitleLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ExportVaultConfigs";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ExportVaultConfigs";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label TitleLabel;
		private System.Windows.Forms.Button ExportButton;
		private System.Windows.Forms.RadioButton PdfRadioButton;
		private System.Windows.Forms.RadioButton JsonRadioButton;
	}
}