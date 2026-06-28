
namespace PassGuard.GUI
{
    partial class AddContent
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
			this.AddButton = new System.Windows.Forms.Button();
			this.URLLabel = new System.Windows.Forms.Label();
			this.URLTextbox = new System.Windows.Forms.TextBox();
			this.NameTextbox = new System.Windows.Forms.TextBox();
			this.NameLabel = new System.Windows.Forms.Label();
			this.UsernameTextbox = new System.Windows.Forms.TextBox();
			this.UsernameLabel = new System.Windows.Forms.Label();
			this.PasswordTextbox = new System.Windows.Forms.TextBox();
			this.PassLabel = new System.Windows.Forms.Label();
			this.CategoryLabel = new System.Windows.Forms.Label();
			this.NotesLabel = new System.Windows.Forms.Label();
			this.NotesTextbox = new System.Windows.Forms.TextBox();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.PassVisibilityButton = new System.Windows.Forms.Button();
			this.ImportantCheckbox = new System.Windows.Forms.CheckBox();
			this.CategoryCombobox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// AddButton
			// 
			this.AddButton.FlatAppearance.BorderSize = 0;
			this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.AddButton.Location = new System.Drawing.Point(564, 365);
			this.AddButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(159, 42);
			this.AddButton.TabIndex = 0;
			this.AddButton.Text = "Add Element";
			this.AddButton.UseVisualStyleBackColor = true;
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			this.AddButton.MouseEnter += new System.EventHandler(this.AddButton_MouseEnter);
			this.AddButton.MouseLeave += new System.EventHandler(this.AddButton_MouseLeave);
			// 
			// URLLabel
			// 
			this.URLLabel.AutoSize = true;
			this.URLLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.URLLabel.Location = new System.Drawing.Point(75, 50);
			this.URLLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.URLLabel.Name = "URLLabel";
			this.URLLabel.Size = new System.Drawing.Size(112, 18);
			this.URLLabel.TabIndex = 1;
			this.URLLabel.Text = "URL (optional): ";
			// 
			// URLTextbox
			// 
			this.URLTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.URLTextbox.Location = new System.Drawing.Point(251, 46);
			this.URLTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.URLTextbox.MaxLength = 2100;
			this.URLTextbox.Name = "URLTextbox";
			this.URLTextbox.Size = new System.Drawing.Size(472, 22);
			this.URLTextbox.TabIndex = 2;
			// 
			// NameTextbox
			// 
			this.NameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NameTextbox.Location = new System.Drawing.Point(251, 87);
			this.NameTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.NameTextbox.MaxLength = 2100;
			this.NameTextbox.Name = "NameTextbox";
			this.NameTextbox.Size = new System.Drawing.Size(472, 22);
			this.NameTextbox.TabIndex = 4;
			// 
			// NameLabel
			// 
			this.NameLabel.AutoSize = true;
			this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NameLabel.Location = new System.Drawing.Point(75, 90);
			this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(56, 18);
			this.NameLabel.TabIndex = 3;
			this.NameLabel.Text = "Name: ";
			// 
			// UsernameTextbox
			// 
			this.UsernameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.UsernameTextbox.Location = new System.Drawing.Point(251, 129);
			this.UsernameTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.UsernameTextbox.MaxLength = 2100;
			this.UsernameTextbox.Name = "UsernameTextbox";
			this.UsernameTextbox.Size = new System.Drawing.Size(472, 22);
			this.UsernameTextbox.TabIndex = 6;
			// 
			// UsernameLabel
			// 
			this.UsernameLabel.AutoSize = true;
			this.UsernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.UsernameLabel.Location = new System.Drawing.Point(75, 133);
			this.UsernameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.UsernameLabel.Name = "UsernameLabel";
			this.UsernameLabel.Size = new System.Drawing.Size(114, 18);
			this.UsernameLabel.TabIndex = 5;
			this.UsernameLabel.Text = "Site Username: ";
			// 
			// PasswordTextbox
			// 
			this.PasswordTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.PasswordTextbox.Location = new System.Drawing.Point(251, 173);
			this.PasswordTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.PasswordTextbox.MaxLength = 2100;
			this.PasswordTextbox.Name = "PasswordTextbox";
			this.PasswordTextbox.Size = new System.Drawing.Size(435, 22);
			this.PasswordTextbox.TabIndex = 8;
			this.PasswordTextbox.UseSystemPasswordChar = true;
			// 
			// PassLabel
			// 
			this.PassLabel.AutoSize = true;
			this.PassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.PassLabel.Location = new System.Drawing.Point(75, 177);
			this.PassLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.PassLabel.Name = "PassLabel";
			this.PassLabel.Size = new System.Drawing.Size(112, 18);
			this.PassLabel.TabIndex = 7;
			this.PassLabel.Text = "Site Password: ";
			// 
			// CategoryLabel
			// 
			this.CategoryLabel.AutoSize = true;
			this.CategoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.CategoryLabel.Location = new System.Drawing.Point(75, 220);
			this.CategoryLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.CategoryLabel.Name = "CategoryLabel";
			this.CategoryLabel.Size = new System.Drawing.Size(145, 18);
			this.CategoryLabel.TabIndex = 9;
			this.CategoryLabel.Text = "Category (Optional): ";
			// 
			// NotesLabel
			// 
			this.NotesLabel.AutoSize = true;
			this.NotesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NotesLabel.Location = new System.Drawing.Point(75, 261);
			this.NotesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NotesLabel.Name = "NotesLabel";
			this.NotesLabel.Size = new System.Drawing.Size(125, 18);
			this.NotesLabel.TabIndex = 11;
			this.NotesLabel.Text = "Notes (Optional): ";
			// 
			// NotesTextbox
			// 
			this.NotesTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NotesTextbox.Location = new System.Drawing.Point(251, 261);
			this.NotesTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.NotesTextbox.MaxLength = 2100;
			this.NotesTextbox.Multiline = true;
			this.NotesTextbox.Name = "NotesTextbox";
			this.NotesTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.NotesTextbox.Size = new System.Drawing.Size(472, 93);
			this.NotesTextbox.TabIndex = 12;
			// 
			// TitleLabel
			// 
			this.TitleLabel.AutoSize = true;
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TitleLabel.Location = new System.Drawing.Point(16, 13);
			this.TitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(254, 18);
			this.TitleLabel.TabIndex = 13;
			this.TitleLabel.Text = "Insert the data for the new password: ";
			// 
			// PassVisibilityButton
			// 
			this.PassVisibilityButton.FlatAppearance.BorderSize = 0;
			this.PassVisibilityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PassVisibilityButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.PassVisibilityButton.Image = global::PassGuard.Properties.Resources.VisibilityOn24;
			this.PassVisibilityButton.Location = new System.Drawing.Point(694, 172);
			this.PassVisibilityButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.PassVisibilityButton.Name = "PassVisibilityButton";
			this.PassVisibilityButton.Size = new System.Drawing.Size(29, 29);
			this.PassVisibilityButton.TabIndex = 42;
			this.PassVisibilityButton.UseVisualStyleBackColor = true;
			this.PassVisibilityButton.Click += new System.EventHandler(this.PassVisibilityButton_Click);
			// 
			// ImportantCheckbox
			// 
			this.ImportantCheckbox.AutoSize = true;
			this.ImportantCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ImportantCheckbox.Location = new System.Drawing.Point(75, 376);
			this.ImportantCheckbox.Name = "ImportantCheckbox";
			this.ImportantCheckbox.Size = new System.Drawing.Size(225, 22);
			this.ImportantCheckbox.TabIndex = 43;
			this.ImportantCheckbox.Text = "Save as Important Password?";
			this.ImportantCheckbox.UseVisualStyleBackColor = true;
			// 
			// CategoryCombobox
			// 
			this.CategoryCombobox.FormattingEnabled = true;
			this.CategoryCombobox.IntegralHeight = false;
			this.CategoryCombobox.Location = new System.Drawing.Point(251, 220);
			this.CategoryCombobox.MaxLength = 2100;
			this.CategoryCombobox.Name = "CategoryCombobox";
			this.CategoryCombobox.Size = new System.Drawing.Size(472, 23);
			this.CategoryCombobox.TabIndex = 44;
			this.CategoryCombobox.Validating += new System.ComponentModel.CancelEventHandler(this.CategoryCombobox_Validating);
			// 
			// AddContent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(796, 434);
			this.Controls.Add(this.CategoryCombobox);
			this.Controls.Add(this.ImportantCheckbox);
			this.Controls.Add(this.PassVisibilityButton);
			this.Controls.Add(this.TitleLabel);
			this.Controls.Add(this.NotesTextbox);
			this.Controls.Add(this.NotesLabel);
			this.Controls.Add(this.CategoryLabel);
			this.Controls.Add(this.PasswordTextbox);
			this.Controls.Add(this.PassLabel);
			this.Controls.Add(this.UsernameTextbox);
			this.Controls.Add(this.UsernameLabel);
			this.Controls.Add(this.NameTextbox);
			this.Controls.Add(this.NameLabel);
			this.Controls.Add(this.URLTextbox);
			this.Controls.Add(this.URLLabel);
			this.Controls.Add(this.AddButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "AddContent";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PassGuard™";
			this.BackColorChanged += new System.EventHandler(this.AddContent_BackColorChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Label URLLabel;
        private System.Windows.Forms.TextBox URLTextbox;
        private System.Windows.Forms.TextBox NameTextbox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox UsernameTextbox;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.TextBox PasswordTextbox;
        private System.Windows.Forms.Label PassLabel;
        private System.Windows.Forms.Label CategoryLabel;
        private System.Windows.Forms.Label NotesLabel;
        private System.Windows.Forms.TextBox NotesTextbox;
        private System.Windows.Forms.Label TitleLabel;
		private System.Windows.Forms.Button PassVisibilityButton;
		private System.Windows.Forms.CheckBox ImportantCheckbox;
		private System.Windows.Forms.ComboBox CategoryCombobox;
	}
}