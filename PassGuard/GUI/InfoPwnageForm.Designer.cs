namespace PassGuard.GUI
{
	partial class InfoPwnageForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoPwnageForm));
			this.ContentRichTextbox = new System.Windows.Forms.RichTextBox();
			this.UnderstoodButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ContentRichTextbox
			// 
			this.ContentRichTextbox.BackColor = System.Drawing.SystemColors.Control;
			this.ContentRichTextbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ContentRichTextbox.Location = new System.Drawing.Point(12, 12);
			this.ContentRichTextbox.Name = "ContentRichTextbox";
			this.ContentRichTextbox.ReadOnly = true;
			this.ContentRichTextbox.Size = new System.Drawing.Size(547, 164);
			this.ContentRichTextbox.TabIndex = 27;
			this.ContentRichTextbox.Text = resources.GetString("ContentRichTextbox.Text");
			this.ContentRichTextbox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ContentRichTextbox_LinkClicked);
			// 
			// UnderstoodButton
			// 
			this.UnderstoodButton.FlatAppearance.BorderSize = 0;
			this.UnderstoodButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.UnderstoodButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.UnderstoodButton.Location = new System.Drawing.Point(229, 182);
			this.UnderstoodButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.UnderstoodButton.Name = "UnderstoodButton";
			this.UnderstoodButton.Size = new System.Drawing.Size(131, 38);
			this.UnderstoodButton.TabIndex = 26;
			this.UnderstoodButton.Text = "Understood";
			this.UnderstoodButton.UseVisualStyleBackColor = true;
			this.UnderstoodButton.Click += new System.EventHandler(this.UnderstoodButton_Click);
			this.UnderstoodButton.MouseEnter += new System.EventHandler(this.UnderstoodButton_MouseEnter);
			this.UnderstoodButton.MouseLeave += new System.EventHandler(this.UnderstoodButton_MouseLeave);
			// 
			// InfoPwnageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(571, 228);
			this.Controls.Add(this.ContentRichTextbox);
			this.Controls.Add(this.UnderstoodButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "InfoPwnageForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "InfoPwnageForm";
			this.BackColorChanged += new System.EventHandler(this.InfoPwnageForm_BackColorChanged);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox ContentRichTextbox;
		private System.Windows.Forms.Button UnderstoodButton;
	}
}