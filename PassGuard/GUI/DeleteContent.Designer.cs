
namespace PassGuard.GUI
{
    partial class DeleteContent
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
			this.DeleteButton = new System.Windows.Forms.Button();
			this.DeleteAllButton = new System.Windows.Forms.Button();
			this.EnableDeleteAllCheckbox = new System.Windows.Forms.CheckBox();
			this.NameCombobox = new System.Windows.Forms.ComboBox();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.NotesTextbox = new System.Windows.Forms.TextBox();
			this.NotesLabel = new System.Windows.Forms.Label();
			this.CategoryTextbox = new System.Windows.Forms.TextBox();
			this.CategoryLabel = new System.Windows.Forms.Label();
			this.PasswordTextbox = new System.Windows.Forms.TextBox();
			this.PassLabel = new System.Windows.Forms.Label();
			this.UsernameTextbox = new System.Windows.Forms.TextBox();
			this.UsernameLabel = new System.Windows.Forms.Label();
			this.NameTextbox = new System.Windows.Forms.TextBox();
			this.NameLabel = new System.Windows.Forms.Label();
			this.URLTextbox = new System.Windows.Forms.TextBox();
			this.URLLabel = new System.Windows.Forms.Label();
			this.PassVisibilityButton = new System.Windows.Forms.Button();
			this.ImportantCheckbox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// DeleteButton
			// 
			this.DeleteButton.FlatAppearance.BorderSize = 0;
			this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.DeleteButton.Location = new System.Drawing.Point(521, 370);
			this.DeleteButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(217, 42);
			this.DeleteButton.TabIndex = 2;
			this.DeleteButton.Text = "Delete Selected Element";
			this.DeleteButton.UseVisualStyleBackColor = true;
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			this.DeleteButton.MouseEnter += new System.EventHandler(this.DeleteButton_MouseEnter);
			this.DeleteButton.MouseLeave += new System.EventHandler(this.DeleteButton_MouseLeave);
			// 
			// DeleteAllButton
			// 
			this.DeleteAllButton.Enabled = false;
			this.DeleteAllButton.FlatAppearance.BorderSize = 0;
			this.DeleteAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DeleteAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.DeleteAllButton.Location = new System.Drawing.Point(266, 370);
			this.DeleteAllButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.DeleteAllButton.Name = "DeleteAllButton";
			this.DeleteAllButton.Size = new System.Drawing.Size(181, 42);
			this.DeleteAllButton.TabIndex = 3;
			this.DeleteAllButton.Text = "Delete All Elements";
			this.DeleteAllButton.UseVisualStyleBackColor = true;
			this.DeleteAllButton.Click += new System.EventHandler(this.DeleteAllButton_Click);
			this.DeleteAllButton.MouseEnter += new System.EventHandler(this.DeleteAllButton_MouseEnter);
			this.DeleteAllButton.MouseLeave += new System.EventHandler(this.DeleteAllButton_MouseLeave);
			// 
			// EnableDeleteAllCheckbox
			// 
			this.EnableDeleteAllCheckbox.AutoSize = true;
			this.EnableDeleteAllCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.EnableDeleteAllCheckbox.Location = new System.Drawing.Point(34, 381);
			this.EnableDeleteAllCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.EnableDeleteAllCheckbox.Name = "EnableDeleteAllCheckbox";
			this.EnableDeleteAllCheckbox.Size = new System.Drawing.Size(185, 22);
			this.EnableDeleteAllCheckbox.TabIndex = 4;
			this.EnableDeleteAllCheckbox.Text = "Enable Delete-All Button";
			this.EnableDeleteAllCheckbox.UseVisualStyleBackColor = true;
			this.EnableDeleteAllCheckbox.CheckedChanged += new System.EventHandler(this.EnableDeleteAllCheckbox_CheckedChanged);
			// 
			// NameCombobox
			// 
			this.NameCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.NameCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NameCombobox.FormattingEnabled = true;
			this.NameCombobox.Location = new System.Drawing.Point(451, 7);
			this.NameCombobox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.NameCombobox.MaxLength = 2100;
			this.NameCombobox.Name = "NameCombobox";
			this.NameCombobox.Size = new System.Drawing.Size(286, 26);
			this.NameCombobox.TabIndex = 40;
			this.NameCombobox.SelectedIndexChanged += new System.EventHandler(this.NameCombobox_SelectedIndexChanged);
			// 
			// TitleLabel
			// 
			this.TitleLabel.AutoSize = true;
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TitleLabel.Location = new System.Drawing.Point(30, 10);
			this.TitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(355, 18);
			this.TitleLabel.TabIndex = 39;
			this.TitleLabel.Text = "Select the name of the password you want to delete: ";
			// 
			// NotesTextbox
			// 
			this.NotesTextbox.Enabled = false;
			this.NotesTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NotesTextbox.Location = new System.Drawing.Point(266, 263);
			this.NotesTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.NotesTextbox.MaxLength = 2100;
			this.NotesTextbox.Multiline = true;
			this.NotesTextbox.Name = "NotesTextbox";
			this.NotesTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.NotesTextbox.Size = new System.Drawing.Size(472, 49);
			this.NotesTextbox.TabIndex = 38;
			// 
			// NotesLabel
			// 
			this.NotesLabel.AutoSize = true;
			this.NotesLabel.Enabled = false;
			this.NotesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NotesLabel.Location = new System.Drawing.Point(90, 263);
			this.NotesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NotesLabel.Name = "NotesLabel";
			this.NotesLabel.Size = new System.Drawing.Size(125, 18);
			this.NotesLabel.TabIndex = 37;
			this.NotesLabel.Text = "Notes (Optional): ";
			// 
			// CategoryTextbox
			// 
			this.CategoryTextbox.Enabled = false;
			this.CategoryTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.CategoryTextbox.Location = new System.Drawing.Point(266, 219);
			this.CategoryTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CategoryTextbox.MaxLength = 2100;
			this.CategoryTextbox.Name = "CategoryTextbox";
			this.CategoryTextbox.Size = new System.Drawing.Size(472, 22);
			this.CategoryTextbox.TabIndex = 36;
			// 
			// CategoryLabel
			// 
			this.CategoryLabel.AutoSize = true;
			this.CategoryLabel.Enabled = false;
			this.CategoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.CategoryLabel.Location = new System.Drawing.Point(90, 223);
			this.CategoryLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.CategoryLabel.Name = "CategoryLabel";
			this.CategoryLabel.Size = new System.Drawing.Size(145, 18);
			this.CategoryLabel.TabIndex = 35;
			this.CategoryLabel.Text = "Category (Optional): ";
			// 
			// PasswordTextbox
			// 
			this.PasswordTextbox.Enabled = false;
			this.PasswordTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.PasswordTextbox.Location = new System.Drawing.Point(266, 175);
			this.PasswordTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.PasswordTextbox.MaxLength = 2100;
			this.PasswordTextbox.Name = "PasswordTextbox";
			this.PasswordTextbox.Size = new System.Drawing.Size(434, 22);
			this.PasswordTextbox.TabIndex = 34;
			this.PasswordTextbox.UseSystemPasswordChar = true;
			// 
			// PassLabel
			// 
			this.PassLabel.AutoSize = true;
			this.PassLabel.Enabled = false;
			this.PassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.PassLabel.Location = new System.Drawing.Point(90, 179);
			this.PassLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.PassLabel.Name = "PassLabel";
			this.PassLabel.Size = new System.Drawing.Size(112, 18);
			this.PassLabel.TabIndex = 33;
			this.PassLabel.Text = "Site Password: ";
			// 
			// UsernameTextbox
			// 
			this.UsernameTextbox.Enabled = false;
			this.UsernameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.UsernameTextbox.Location = new System.Drawing.Point(266, 132);
			this.UsernameTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.UsernameTextbox.MaxLength = 2100;
			this.UsernameTextbox.Name = "UsernameTextbox";
			this.UsernameTextbox.Size = new System.Drawing.Size(472, 22);
			this.UsernameTextbox.TabIndex = 32;
			// 
			// UsernameLabel
			// 
			this.UsernameLabel.AutoSize = true;
			this.UsernameLabel.Enabled = false;
			this.UsernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.UsernameLabel.Location = new System.Drawing.Point(90, 135);
			this.UsernameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.UsernameLabel.Name = "UsernameLabel";
			this.UsernameLabel.Size = new System.Drawing.Size(114, 18);
			this.UsernameLabel.TabIndex = 31;
			this.UsernameLabel.Text = "Site Username: ";
			// 
			// NameTextbox
			// 
			this.NameTextbox.Enabled = false;
			this.NameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NameTextbox.Location = new System.Drawing.Point(266, 89);
			this.NameTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.NameTextbox.MaxLength = 2100;
			this.NameTextbox.Name = "NameTextbox";
			this.NameTextbox.Size = new System.Drawing.Size(472, 22);
			this.NameTextbox.TabIndex = 30;
			// 
			// NameLabel
			// 
			this.NameLabel.AutoSize = true;
			this.NameLabel.Enabled = false;
			this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NameLabel.Location = new System.Drawing.Point(90, 92);
			this.NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(56, 18);
			this.NameLabel.TabIndex = 29;
			this.NameLabel.Text = "Name: ";
			// 
			// URLTextbox
			// 
			this.URLTextbox.Enabled = false;
			this.URLTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.URLTextbox.Location = new System.Drawing.Point(266, 48);
			this.URLTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.URLTextbox.MaxLength = 2100;
			this.URLTextbox.Name = "URLTextbox";
			this.URLTextbox.Size = new System.Drawing.Size(472, 22);
			this.URLTextbox.TabIndex = 28;
			// 
			// URLLabel
			// 
			this.URLLabel.AutoSize = true;
			this.URLLabel.Enabled = false;
			this.URLLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.URLLabel.Location = new System.Drawing.Point(90, 52);
			this.URLLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.URLLabel.Name = "URLLabel";
			this.URLLabel.Size = new System.Drawing.Size(112, 18);
			this.URLLabel.TabIndex = 27;
			this.URLLabel.Text = "URL (optional): ";
			// 
			// PassVisibilityButton
			// 
			this.PassVisibilityButton.FlatAppearance.BorderSize = 0;
			this.PassVisibilityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PassVisibilityButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.PassVisibilityButton.Image = global::PassGuard.Properties.Resources.VisibilityOn24;
			this.PassVisibilityButton.Location = new System.Drawing.Point(708, 174);
			this.PassVisibilityButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.PassVisibilityButton.Name = "PassVisibilityButton";
			this.PassVisibilityButton.Size = new System.Drawing.Size(29, 29);
			this.PassVisibilityButton.TabIndex = 41;
			this.PassVisibilityButton.UseVisualStyleBackColor = true;
			this.PassVisibilityButton.Click += new System.EventHandler(this.PassVisibilityButton_Click);
			// 
			// ImportantCheckbox
			// 
			this.ImportantCheckbox.AutoSize = true;
			this.ImportantCheckbox.Enabled = false;
			this.ImportantCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ImportantCheckbox.Location = new System.Drawing.Point(288, 333);
			this.ImportantCheckbox.Name = "ImportantCheckbox";
			this.ImportantCheckbox.Size = new System.Drawing.Size(225, 22);
			this.ImportantCheckbox.TabIndex = 45;
			this.ImportantCheckbox.Text = "Save as Important Password?";
			this.ImportantCheckbox.UseVisualStyleBackColor = true;
			// 
			// DeleteContent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(796, 434);
			this.Controls.Add(this.ImportantCheckbox);
			this.Controls.Add(this.PassVisibilityButton);
			this.Controls.Add(this.NameCombobox);
			this.Controls.Add(this.TitleLabel);
			this.Controls.Add(this.NotesTextbox);
			this.Controls.Add(this.NotesLabel);
			this.Controls.Add(this.CategoryTextbox);
			this.Controls.Add(this.CategoryLabel);
			this.Controls.Add(this.PasswordTextbox);
			this.Controls.Add(this.PassLabel);
			this.Controls.Add(this.UsernameTextbox);
			this.Controls.Add(this.UsernameLabel);
			this.Controls.Add(this.NameTextbox);
			this.Controls.Add(this.NameLabel);
			this.Controls.Add(this.URLTextbox);
			this.Controls.Add(this.URLLabel);
			this.Controls.Add(this.EnableDeleteAllCheckbox);
			this.Controls.Add(this.DeleteAllButton);
			this.Controls.Add(this.DeleteButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "DeleteContent";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PassGuard™";
			this.BackColorChanged += new System.EventHandler(this.DeleteContent_BackColorChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button DeleteAllButton;
        private System.Windows.Forms.CheckBox EnableDeleteAllCheckbox;
        private System.Windows.Forms.ComboBox NameCombobox;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.TextBox NotesTextbox;
        private System.Windows.Forms.Label NotesLabel;
        private System.Windows.Forms.TextBox CategoryTextbox;
        private System.Windows.Forms.Label CategoryLabel;
        private System.Windows.Forms.TextBox PasswordTextbox;
        private System.Windows.Forms.Label PassLabel;
        private System.Windows.Forms.TextBox UsernameTextbox;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.TextBox NameTextbox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox URLTextbox;
        private System.Windows.Forms.Label URLLabel;
		private System.Windows.Forms.Button PassVisibilityButton;
		private System.Windows.Forms.CheckBox ImportantCheckbox;
	}
}