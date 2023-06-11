
namespace PassGuard.GUI
{
    partial class AddColorConfig
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
			this.NameLabel = new System.Windows.Forms.Label();
			this.RedLabel = new System.Windows.Forms.Label();
			this.BlueLabel = new System.Windows.Forms.Label();
			this.GreenLabel = new System.Windows.Forms.Label();
			this.NameTextbox = new System.Windows.Forms.TextBox();
			this.RedNUD = new System.Windows.Forms.NumericUpDown();
			this.GreenNUD = new System.Windows.Forms.NumericUpDown();
			this.BlueNUD = new System.Windows.Forms.NumericUpDown();
			this.SendButton = new System.Windows.Forms.Button();
			this.FavouriteCheckbox = new System.Windows.Forms.CheckBox();
			this.NoteRGBLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.RedNUD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).BeginInit();
			this.SuspendLayout();
			// 
			// TitleLabel
			// 
			this.TitleLabel.AutoSize = true;
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TitleLabel.Location = new System.Drawing.Point(42, 28);
			this.TitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(278, 18);
			this.TitleLabel.TabIndex = 0;
			this.TitleLabel.Text = "Insert the data for the new outline colour: ";
			// 
			// NameLabel
			// 
			this.NameLabel.AutoSize = true;
			this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NameLabel.Location = new System.Drawing.Point(66, 80);
			this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(50, 16);
			this.NameLabel.TabIndex = 1;
			this.NameLabel.Text = "Name: ";
			// 
			// RedLabel
			// 
			this.RedLabel.AutoSize = true;
			this.RedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.RedLabel.Location = new System.Drawing.Point(66, 132);
			this.RedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.RedLabel.Name = "RedLabel";
			this.RedLabel.Size = new System.Drawing.Size(77, 16);
			this.RedLabel.TabIndex = 2;
			this.RedLabel.Text = "Red Value: ";
			// 
			// BlueLabel
			// 
			this.BlueLabel.AutoSize = true;
			this.BlueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.BlueLabel.Location = new System.Drawing.Point(66, 234);
			this.BlueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.BlueLabel.Name = "BlueLabel";
			this.BlueLabel.Size = new System.Drawing.Size(72, 16);
			this.BlueLabel.TabIndex = 4;
			this.BlueLabel.Text = "Blue Value";
			// 
			// GreenLabel
			// 
			this.GreenLabel.AutoSize = true;
			this.GreenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.GreenLabel.Location = new System.Drawing.Point(66, 182);
			this.GreenLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.GreenLabel.Name = "GreenLabel";
			this.GreenLabel.Size = new System.Drawing.Size(91, 16);
			this.GreenLabel.TabIndex = 3;
			this.GreenLabel.Text = "Green Value:  ";
			// 
			// NameTextbox
			// 
			this.NameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NameTextbox.Location = new System.Drawing.Point(230, 76);
			this.NameTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.NameTextbox.Name = "NameTextbox";
			this.NameTextbox.Size = new System.Drawing.Size(166, 22);
			this.NameTextbox.TabIndex = 5;
			// 
			// RedNUD
			// 
			this.RedNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.RedNUD.Location = new System.Drawing.Point(230, 129);
			this.RedNUD.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.RedNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.RedNUD.Name = "RedNUD";
			this.RedNUD.Size = new System.Drawing.Size(167, 22);
			this.RedNUD.TabIndex = 6;
			// 
			// GreenNUD
			// 
			this.GreenNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.GreenNUD.Location = new System.Drawing.Point(230, 180);
			this.GreenNUD.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.GreenNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.GreenNUD.Name = "GreenNUD";
			this.GreenNUD.Size = new System.Drawing.Size(167, 22);
			this.GreenNUD.TabIndex = 7;
			// 
			// BlueNUD
			// 
			this.BlueNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.BlueNUD.Location = new System.Drawing.Point(230, 232);
			this.BlueNUD.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.BlueNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.BlueNUD.Name = "BlueNUD";
			this.BlueNUD.Size = new System.Drawing.Size(167, 22);
			this.BlueNUD.TabIndex = 8;
			// 
			// SendButton
			// 
			this.SendButton.FlatAppearance.BorderSize = 0;
			this.SendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SendButton.Location = new System.Drawing.Point(161, 361);
			this.SendButton.Margin = new System.Windows.Forms.Padding(2);
			this.SendButton.Name = "SendButton";
			this.SendButton.Size = new System.Drawing.Size(115, 37);
			this.SendButton.TabIndex = 12;
			this.SendButton.Text = "Send";
			this.SendButton.UseVisualStyleBackColor = true;
			this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
			// 
			// FavouriteCheckbox
			// 
			this.FavouriteCheckbox.AutoSize = true;
			this.FavouriteCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FavouriteCheckbox.Location = new System.Drawing.Point(122, 277);
			this.FavouriteCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.FavouriteCheckbox.Name = "FavouriteCheckbox";
			this.FavouriteCheckbox.Size = new System.Drawing.Size(198, 20);
			this.FavouriteCheckbox.TabIndex = 13;
			this.FavouriteCheckbox.Text = "Mark this config as Favourite.";
			this.FavouriteCheckbox.UseVisualStyleBackColor = true;
			// 
			// NoteRGBLabel
			// 
			this.NoteRGBLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NoteRGBLabel.Location = new System.Drawing.Point(31, 313);
			this.NoteRGBLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.NoteRGBLabel.Name = "NoteRGBLabel";
			this.NoteRGBLabel.Size = new System.Drawing.Size(380, 30);
			this.NoteRGBLabel.TabIndex = 14;
			this.NoteRGBLabel.Text = "Note: Very light or dark RGB combinations may be subject to slight variations, so" +
    " that the colors entered are slightly different from those saved.";
			this.NoteRGBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// AddColorConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(444, 418);
			this.Controls.Add(this.NoteRGBLabel);
			this.Controls.Add(this.FavouriteCheckbox);
			this.Controls.Add(this.SendButton);
			this.Controls.Add(this.BlueNUD);
			this.Controls.Add(this.GreenNUD);
			this.Controls.Add(this.RedNUD);
			this.Controls.Add(this.NameTextbox);
			this.Controls.Add(this.BlueLabel);
			this.Controls.Add(this.GreenLabel);
			this.Controls.Add(this.RedLabel);
			this.Controls.Add(this.NameLabel);
			this.Controls.Add(this.TitleLabel);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "AddColorConfig";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AddColorConfig";
			((System.ComponentModel.ISupportInitialize)(this.RedNUD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label RedLabel;
        private System.Windows.Forms.Label BlueLabel;
        private System.Windows.Forms.Label GreenLabel;
        private System.Windows.Forms.TextBox NameTextbox;
        private System.Windows.Forms.NumericUpDown RedNUD;
        private System.Windows.Forms.NumericUpDown GreenNUD;
        private System.Windows.Forms.NumericUpDown BlueNUD;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.CheckBox FavouriteCheckbox;
		private System.Windows.Forms.Label NoteRGBLabel;
	}
}