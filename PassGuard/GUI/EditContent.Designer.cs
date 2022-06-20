
namespace PassGuard.GUI
{
    partial class EditContent
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
            this.EditButton = new System.Windows.Forms.Button();
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
            this.TitleLabel = new System.Windows.Forms.Label();
            this.NameCombobox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // EditButton
            // 
            this.EditButton.Enabled = false;
            this.EditButton.FlatAppearance.BorderSize = 0;
            this.EditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditButton.Location = new System.Drawing.Point(273, 323);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(136, 36);
            this.EditButton.TabIndex = 1;
            this.EditButton.Text = "Edit Element";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // NotesTextbox
            // 
            this.NotesTextbox.Enabled = false;
            this.NotesTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotesTextbox.Location = new System.Drawing.Point(214, 228);
            this.NotesTextbox.Multiline = true;
            this.NotesTextbox.Name = "NotesTextbox";
            this.NotesTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NotesTextbox.Size = new System.Drawing.Size(405, 81);
            this.NotesTextbox.TabIndex = 24;
            // 
            // NotesLabel
            // 
            this.NotesLabel.AutoSize = true;
            this.NotesLabel.Enabled = false;
            this.NotesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotesLabel.Location = new System.Drawing.Point(63, 228);
            this.NotesLabel.Name = "NotesLabel";
            this.NotesLabel.Size = new System.Drawing.Size(125, 18);
            this.NotesLabel.TabIndex = 23;
            this.NotesLabel.Text = "Notes (Optional): ";
            // 
            // CategoryTextbox
            // 
            this.CategoryTextbox.Enabled = false;
            this.CategoryTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryTextbox.Location = new System.Drawing.Point(214, 190);
            this.CategoryTextbox.Name = "CategoryTextbox";
            this.CategoryTextbox.Size = new System.Drawing.Size(405, 22);
            this.CategoryTextbox.TabIndex = 22;
            // 
            // CategoryLabel
            // 
            this.CategoryLabel.AutoSize = true;
            this.CategoryLabel.Enabled = false;
            this.CategoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryLabel.Location = new System.Drawing.Point(63, 193);
            this.CategoryLabel.Name = "CategoryLabel";
            this.CategoryLabel.Size = new System.Drawing.Size(145, 18);
            this.CategoryLabel.TabIndex = 21;
            this.CategoryLabel.Text = "Category (Optional): ";
            // 
            // PasswordTextbox
            // 
            this.PasswordTextbox.Enabled = false;
            this.PasswordTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTextbox.Location = new System.Drawing.Point(214, 152);
            this.PasswordTextbox.Name = "PasswordTextbox";
            this.PasswordTextbox.PasswordChar = '*';
            this.PasswordTextbox.Size = new System.Drawing.Size(405, 22);
            this.PasswordTextbox.TabIndex = 20;
            // 
            // PassLabel
            // 
            this.PassLabel.AutoSize = true;
            this.PassLabel.Enabled = false;
            this.PassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassLabel.Location = new System.Drawing.Point(63, 155);
            this.PassLabel.Name = "PassLabel";
            this.PassLabel.Size = new System.Drawing.Size(112, 18);
            this.PassLabel.TabIndex = 19;
            this.PassLabel.Text = "Site Password: ";
            // 
            // UsernameTextbox
            // 
            this.UsernameTextbox.Enabled = false;
            this.UsernameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameTextbox.Location = new System.Drawing.Point(214, 114);
            this.UsernameTextbox.Name = "UsernameTextbox";
            this.UsernameTextbox.Size = new System.Drawing.Size(405, 22);
            this.UsernameTextbox.TabIndex = 18;
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Enabled = false;
            this.UsernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameLabel.Location = new System.Drawing.Point(63, 117);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(114, 18);
            this.UsernameLabel.TabIndex = 17;
            this.UsernameLabel.Text = "Site Username: ";
            // 
            // NameTextbox
            // 
            this.NameTextbox.Enabled = false;
            this.NameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameTextbox.Location = new System.Drawing.Point(214, 77);
            this.NameTextbox.Name = "NameTextbox";
            this.NameTextbox.Size = new System.Drawing.Size(405, 22);
            this.NameTextbox.TabIndex = 16;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Enabled = false;
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(63, 80);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(56, 18);
            this.NameLabel.TabIndex = 15;
            this.NameLabel.Text = "Name: ";
            // 
            // URLTextbox
            // 
            this.URLTextbox.Enabled = false;
            this.URLTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.URLTextbox.Location = new System.Drawing.Point(214, 42);
            this.URLTextbox.Name = "URLTextbox";
            this.URLTextbox.Size = new System.Drawing.Size(405, 22);
            this.URLTextbox.TabIndex = 14;
            // 
            // URLLabel
            // 
            this.URLLabel.AutoSize = true;
            this.URLLabel.Enabled = false;
            this.URLLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.URLLabel.Location = new System.Drawing.Point(63, 45);
            this.URLLabel.Name = "URLLabel";
            this.URLLabel.Size = new System.Drawing.Size(112, 18);
            this.URLLabel.TabIndex = 13;
            this.URLLabel.Text = "URL (optional): ";
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(339, 18);
            this.TitleLabel.TabIndex = 25;
            this.TitleLabel.Text = "Select the name of the password you want to edit: ";
            // 
            // NameCombobox
            // 
            this.NameCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NameCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameCombobox.FormattingEnabled = true;
            this.NameCombobox.Location = new System.Drawing.Point(357, 6);
            this.NameCombobox.Name = "NameCombobox";
            this.NameCombobox.Size = new System.Drawing.Size(262, 26);
            this.NameCombobox.TabIndex = 26;
            this.NameCombobox.SelectedIndexChanged += new System.EventHandler(this.NameCombobox_SelectedIndexChanged);
            // 
            // EditContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 376);
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
            this.Controls.Add(this.EditButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EditContent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PassGuard™";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button EditButton;
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
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.ComboBox NameCombobox;
    }
}