
namespace PassGuard.GUI
{
    partial class AskRGBforSettings
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			this.RGBTitleLabel = new System.Windows.Forms.Label();
			this.RLabel = new System.Windows.Forms.Label();
			this.RedNUD = new System.Windows.Forms.NumericUpDown();
			this.GreenNUD = new System.Windows.Forms.NumericUpDown();
			this.GLabel = new System.Windows.Forms.Label();
			this.BlueNUD = new System.Windows.Forms.NumericUpDown();
			this.BLabel = new System.Windows.Forms.Label();
			this.WebHelpRGB = new System.Windows.Forms.Button();
			this.RGBWebToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SendButton = new System.Windows.Forms.Button();
			this.RedHeaderButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.EditButton = new System.Windows.Forms.Button();
			this.DeleteButton = new System.Windows.Forms.Button();
			this.AddButton = new System.Windows.Forms.Button();
			this.NameCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.normalOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ascendingOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.descendingOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ColourContentDGV = new System.Windows.Forms.DataGridView();
			this.ConfigNameColumn = new System.Windows.Forms.DataGridViewButtonColumn();
			this.RedColumn = new System.Windows.Forms.DataGridViewButtonColumn();
			this.GreenColumn = new System.Windows.Forms.DataGridViewButtonColumn();
			this.BlueColumn = new System.Windows.Forms.DataGridViewButtonColumn();
			this.ViewerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ChosenConfigColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.FavouriteColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.DeleteRowColumn = new System.Windows.Forms.DataGridViewButtonColumn();
			((System.ComponentModel.ISupportInitialize)(this.RedNUD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).BeginInit();
			this.NameCMS.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ColourContentDGV)).BeginInit();
			this.SuspendLayout();
			// 
			// RGBTitleLabel
			// 
			this.RGBTitleLabel.AutoSize = true;
			this.RGBTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.RGBTitleLabel.Location = new System.Drawing.Point(10, 23);
			this.RGBTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.RGBTitleLabel.Name = "RGBTitleLabel";
			this.RGBTitleLabel.Size = new System.Drawing.Size(350, 16);
			this.RGBTitleLabel.TabIndex = 0;
			this.RGBTitleLabel.Text = "Select the one colour you prefer and click the Send Button.";
			// 
			// RLabel
			// 
			this.RLabel.AutoSize = true;
			this.RLabel.Enabled = false;
			this.RLabel.Location = new System.Drawing.Point(248, 523);
			this.RLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.RLabel.Name = "RLabel";
			this.RLabel.Size = new System.Drawing.Size(20, 15);
			this.RLabel.TabIndex = 1;
			this.RLabel.Text = "R: ";
			// 
			// RedNUD
			// 
			this.RedNUD.Enabled = false;
			this.RedNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.RedNUD.Location = new System.Drawing.Point(299, 520);
			this.RedNUD.Margin = new System.Windows.Forms.Padding(2);
			this.RedNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.RedNUD.Name = "RedNUD";
			this.RedNUD.Size = new System.Drawing.Size(105, 25);
			this.RedNUD.TabIndex = 2;
			// 
			// GreenNUD
			// 
			this.GreenNUD.Enabled = false;
			this.GreenNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.GreenNUD.Location = new System.Drawing.Point(490, 520);
			this.GreenNUD.Margin = new System.Windows.Forms.Padding(2);
			this.GreenNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.GreenNUD.Name = "GreenNUD";
			this.GreenNUD.Size = new System.Drawing.Size(105, 25);
			this.GreenNUD.TabIndex = 4;
			// 
			// GLabel
			// 
			this.GLabel.AutoSize = true;
			this.GLabel.Enabled = false;
			this.GLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.GLabel.Location = new System.Drawing.Point(439, 523);
			this.GLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.GLabel.Name = "GLabel";
			this.GLabel.Size = new System.Drawing.Size(28, 17);
			this.GLabel.TabIndex = 3;
			this.GLabel.Text = "G:  ";
			// 
			// BlueNUD
			// 
			this.BlueNUD.Enabled = false;
			this.BlueNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.BlueNUD.Location = new System.Drawing.Point(683, 520);
			this.BlueNUD.Margin = new System.Windows.Forms.Padding(2);
			this.BlueNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.BlueNUD.Name = "BlueNUD";
			this.BlueNUD.Size = new System.Drawing.Size(105, 25);
			this.BlueNUD.TabIndex = 6;
			// 
			// BLabel
			// 
			this.BLabel.AutoSize = true;
			this.BLabel.Enabled = false;
			this.BLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.BLabel.Location = new System.Drawing.Point(632, 524);
			this.BLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.BLabel.Name = "BLabel";
			this.BLabel.Size = new System.Drawing.Size(22, 17);
			this.BLabel.TabIndex = 5;
			this.BLabel.Text = "B: ";
			// 
			// WebHelpRGB
			// 
			this.WebHelpRGB.Cursor = System.Windows.Forms.Cursors.Hand;
			this.WebHelpRGB.FlatAppearance.BorderSize = 0;
			this.WebHelpRGB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.WebHelpRGB.Image = global::PassGuard.Properties.Resources.Help32;
			this.WebHelpRGB.Location = new System.Drawing.Point(908, 8);
			this.WebHelpRGB.Margin = new System.Windows.Forms.Padding(2);
			this.WebHelpRGB.Name = "WebHelpRGB";
			this.WebHelpRGB.Size = new System.Drawing.Size(47, 51);
			this.WebHelpRGB.TabIndex = 7;
			this.RGBWebToolTip.SetToolTip(this.WebHelpRGB, "Choose a colour from this webpage, take its RGB values and copy them here :)");
			this.WebHelpRGB.UseVisualStyleBackColor = true;
			this.WebHelpRGB.Click += new System.EventHandler(this.WebHelpRGB_Click);
			// 
			// RGBWebToolTip
			// 
			this.RGBWebToolTip.AutomaticDelay = 300;
			this.RGBWebToolTip.AutoPopDelay = 5000;
			this.RGBWebToolTip.InitialDelay = 300;
			this.RGBWebToolTip.ReshowDelay = 60;
			// 
			// SendButton
			// 
			this.SendButton.FlatAppearance.BorderSize = 0;
			this.SendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SendButton.Location = new System.Drawing.Point(840, 513);
			this.SendButton.Margin = new System.Windows.Forms.Padding(2);
			this.SendButton.Name = "SendButton";
			this.SendButton.Size = new System.Drawing.Size(115, 37);
			this.SendButton.TabIndex = 11;
			this.SendButton.Text = "Send";
			this.SendButton.UseVisualStyleBackColor = true;
			this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
			// 
			// RedHeaderButton
			// 
			this.RedHeaderButton.FlatAppearance.BorderSize = 0;
			this.RedHeaderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.RedHeaderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.RedHeaderButton.Location = new System.Drawing.Point(119, 4);
			this.RedHeaderButton.Name = "RedHeaderButton";
			this.RedHeaderButton.Size = new System.Drawing.Size(108, 33);
			this.RedHeaderButton.TabIndex = 8;
			this.RedHeaderButton.Text = "Red";
			this.RedHeaderButton.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Enabled = false;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(47, 521);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 16);
			this.label1.TabIndex = 15;
			this.label1.Text = "Final Selected Values --->";
			// 
			// EditButton
			// 
			this.EditButton.FlatAppearance.BorderSize = 0;
			this.EditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.EditButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.EditButton.Location = new System.Drawing.Point(841, 271);
			this.EditButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.EditButton.Name = "EditButton";
			this.EditButton.Size = new System.Drawing.Size(114, 39);
			this.EditButton.TabIndex = 16;
			this.EditButton.Text = "Edit Config";
			this.EditButton.UseVisualStyleBackColor = true;
			this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
			// 
			// DeleteButton
			// 
			this.DeleteButton.FlatAppearance.BorderSize = 0;
			this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.DeleteButton.Location = new System.Drawing.Point(841, 443);
			this.DeleteButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(114, 39);
			this.DeleteButton.TabIndex = 17;
			this.DeleteButton.Text = "Delete Config";
			this.DeleteButton.UseVisualStyleBackColor = true;
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// AddButton
			// 
			this.AddButton.FlatAppearance.BorderSize = 0;
			this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.AddButton.Location = new System.Drawing.Point(842, 113);
			this.AddButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(114, 39);
			this.AddButton.TabIndex = 18;
			this.AddButton.Text = "Add Config";
			this.AddButton.UseVisualStyleBackColor = true;
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// NameCMS
			// 
			this.NameCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalOrderToolStripMenuItem,
            this.ascendingOrderToolStripMenuItem,
            this.descendingOrderToolStripMenuItem});
			this.NameCMS.Name = "NameCMS";
			this.NameCMS.Size = new System.Drawing.Size(170, 70);
			// 
			// normalOrderToolStripMenuItem
			// 
			this.normalOrderToolStripMenuItem.Checked = true;
			this.normalOrderToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.normalOrderToolStripMenuItem.Name = "normalOrderToolStripMenuItem";
			this.normalOrderToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.normalOrderToolStripMenuItem.Text = "Normal Order";
			this.normalOrderToolStripMenuItem.Click += new System.EventHandler(this.normalOrderToolStripMenuItem_Click);
			// 
			// ascendingOrderToolStripMenuItem
			// 
			this.ascendingOrderToolStripMenuItem.Name = "ascendingOrderToolStripMenuItem";
			this.ascendingOrderToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.ascendingOrderToolStripMenuItem.Text = "Ascending Order";
			this.ascendingOrderToolStripMenuItem.Click += new System.EventHandler(this.ascendingOrderToolStripMenuItem_Click);
			// 
			// descendingOrderToolStripMenuItem
			// 
			this.descendingOrderToolStripMenuItem.Name = "descendingOrderToolStripMenuItem";
			this.descendingOrderToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.descendingOrderToolStripMenuItem.Text = "Descending Order";
			this.descendingOrderToolStripMenuItem.Click += new System.EventHandler(this.descendingOrderToolStripMenuItem_Click);
			// 
			// ColourContentDGV
			// 
			this.ColourContentDGV.AllowUserToAddRows = false;
			this.ColourContentDGV.AllowUserToDeleteRows = false;
			this.ColourContentDGV.AllowUserToResizeColumns = false;
			this.ColourContentDGV.AllowUserToResizeRows = false;
			this.ColourContentDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.ColourContentDGV.BackgroundColor = System.Drawing.SystemColors.Control;
			this.ColourContentDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.ColourContentDGV.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			this.ColourContentDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.ColourContentDGV.ColumnHeadersHeight = 47;
			this.ColourContentDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.ColourContentDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ConfigNameColumn,
            this.RedColumn,
            this.GreenColumn,
            this.BlueColumn,
            this.ViewerColumn,
            this.ChosenConfigColumn,
            this.FavouriteColumn,
            this.DeleteRowColumn});
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.ButtonShadow;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.ColourContentDGV.DefaultCellStyle = dataGridViewCellStyle9;
			this.ColourContentDGV.EnableHeadersVisualStyles = false;
			this.ColourContentDGV.GridColor = System.Drawing.SystemColors.InfoText;
			this.ColourContentDGV.Location = new System.Drawing.Point(19, 67);
			this.ColourContentDGV.Name = "ColourContentDGV";
			this.ColourContentDGV.RowHeadersVisible = false;
			this.ColourContentDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.ColourContentDGV.RowTemplate.Height = 38;
			this.ColourContentDGV.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.ColourContentDGV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ColourContentDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.ColourContentDGV.Size = new System.Drawing.Size(801, 415);
			this.ColourContentDGV.TabIndex = 19;
			this.ColourContentDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ColourContentDGV_CellContentClick);
			// 
			// ConfigNameColumn
			// 
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			this.ConfigNameColumn.DefaultCellStyle = dataGridViewCellStyle2;
			this.ConfigNameColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ConfigNameColumn.HeaderText = "ConfigName";
			this.ConfigNameColumn.Name = "ConfigNameColumn";
			this.ConfigNameColumn.ReadOnly = true;
			this.ConfigNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// RedColumn
			// 
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
			this.RedColumn.DefaultCellStyle = dataGridViewCellStyle3;
			this.RedColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.RedColumn.HeaderText = "Red";
			this.RedColumn.Name = "RedColumn";
			this.RedColumn.ReadOnly = true;
			this.RedColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// GreenColumn
			// 
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
			this.GreenColumn.DefaultCellStyle = dataGridViewCellStyle4;
			this.GreenColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.GreenColumn.HeaderText = "Green";
			this.GreenColumn.Name = "GreenColumn";
			this.GreenColumn.ReadOnly = true;
			this.GreenColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// BlueColumn
			// 
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
			this.BlueColumn.DefaultCellStyle = dataGridViewCellStyle5;
			this.BlueColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BlueColumn.HeaderText = "Blue";
			this.BlueColumn.Name = "BlueColumn";
			this.BlueColumn.ReadOnly = true;
			this.BlueColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// ViewerColumn
			// 
			this.ViewerColumn.HeaderText = "Viewer";
			this.ViewerColumn.Name = "ViewerColumn";
			this.ViewerColumn.ReadOnly = true;
			this.ViewerColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.ViewerColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ChosenConfigColumn
			// 
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.NullValue = false;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
			this.ChosenConfigColumn.DefaultCellStyle = dataGridViewCellStyle6;
			this.ChosenConfigColumn.HeaderText = "ChosenConfig";
			this.ChosenConfigColumn.Name = "ChosenConfigColumn";
			this.ChosenConfigColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// FavouriteColumn
			// 
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.NullValue = false;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
			this.FavouriteColumn.DefaultCellStyle = dataGridViewCellStyle7;
			this.FavouriteColumn.HeaderText = "Favourite";
			this.FavouriteColumn.Name = "FavouriteColumn";
			this.FavouriteColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// DeleteRowColumn
			// 
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
			this.DeleteRowColumn.DefaultCellStyle = dataGridViewCellStyle8;
			this.DeleteRowColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DeleteRowColumn.HeaderText = "Delete Row";
			this.DeleteRowColumn.Name = "DeleteRowColumn";
			this.DeleteRowColumn.ReadOnly = true;
			this.DeleteRowColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.DeleteRowColumn.Text = "Delete";
			this.DeleteRowColumn.UseColumnTextForButtonValue = true;
			// 
			// AskRGBforSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(968, 583);
			this.Controls.Add(this.ColourContentDGV);
			this.Controls.Add(this.AddButton);
			this.Controls.Add(this.DeleteButton);
			this.Controls.Add(this.EditButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.SendButton);
			this.Controls.Add(this.WebHelpRGB);
			this.Controls.Add(this.BlueNUD);
			this.Controls.Add(this.BLabel);
			this.Controls.Add(this.GreenNUD);
			this.Controls.Add(this.GLabel);
			this.Controls.Add(this.RedNUD);
			this.Controls.Add(this.RLabel);
			this.Controls.Add(this.RGBTitleLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.Name = "AskRGBforSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PassGuard™";
			this.Load += new System.EventHandler(this.AskRGBforSettings_Load);
			this.BackColorChanged += new System.EventHandler(this.AskRGBforSettings_BackColorChanged);
			((System.ComponentModel.ISupportInitialize)(this.RedNUD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).EndInit();
			this.NameCMS.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ColourContentDGV)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label RGBTitleLabel;
        private System.Windows.Forms.Label RLabel;
        private System.Windows.Forms.Label GLabel;
        private System.Windows.Forms.Label BLabel;
        private System.Windows.Forms.Button WebHelpRGB;
        private System.Windows.Forms.ToolTip RGBWebToolTip;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Button RedHeaderButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.ContextMenuStrip NameCMS;
        private System.Windows.Forms.ToolStripMenuItem normalOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ascendingOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descendingOrderToolStripMenuItem;
        internal System.Windows.Forms.NumericUpDown RedNUD;
        internal System.Windows.Forms.NumericUpDown GreenNUD;
        internal System.Windows.Forms.NumericUpDown BlueNUD;
		private System.Windows.Forms.DataGridView ColourContentDGV;
		private System.Windows.Forms.DataGridViewButtonColumn ConfigNameColumn;
		private System.Windows.Forms.DataGridViewButtonColumn RedColumn;
		private System.Windows.Forms.DataGridViewButtonColumn GreenColumn;
		private System.Windows.Forms.DataGridViewButtonColumn BlueColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ViewerColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ChosenConfigColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn FavouriteColumn;
		private System.Windows.Forms.DataGridViewButtonColumn DeleteRowColumn;
	}
}