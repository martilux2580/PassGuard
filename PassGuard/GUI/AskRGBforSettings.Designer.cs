
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
            this.RGBTitleLabel = new System.Windows.Forms.Label();
            this.RLabel = new System.Windows.Forms.Label();
            this.RedNUD = new System.Windows.Forms.NumericUpDown();
            this.GreenNUD = new System.Windows.Forms.NumericUpDown();
            this.GLabel = new System.Windows.Forms.Label();
            this.BlueNUD = new System.Windows.Forms.NumericUpDown();
            this.BLabel = new System.Windows.Forms.Label();
            this.WebHelpRGB = new System.Windows.Forms.Button();
            this.NoteRGBLabel = new System.Windows.Forms.Label();
            this.RGBWebToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SendButton = new System.Windows.Forms.Button();
            this.RedHeaderButton = new System.Windows.Forms.Button();
            this.ContentFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.FavouriteButton = new System.Windows.Forms.Button();
            this.ConfigNameButton = new System.Windows.Forms.Button();
            this.RedButton = new System.Windows.Forms.Button();
            this.GreenButton = new System.Windows.Forms.Button();
            this.BlueButton = new System.Windows.Forms.Button();
            this.ViewerButton = new System.Windows.Forms.Button();
            this.ChosenConfigButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.EditButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.NameCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.normalOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ascendingOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descendingOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.RedNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).BeginInit();
            this.HeaderPanel.SuspendLayout();
            this.NameCMS.SuspendLayout();
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
            this.RLabel.Location = new System.Drawing.Point(250, 500);
            this.RLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RLabel.Name = "RLabel";
            this.RLabel.Size = new System.Drawing.Size(20, 15);
            this.RLabel.TabIndex = 1;
            this.RLabel.Text = "R: ";
            // 
            // RedNUD
            // 
            this.RedNUD.Enabled = false;
            this.RedNUD.Location = new System.Drawing.Point(301, 497);
            this.RedNUD.Margin = new System.Windows.Forms.Padding(2);
            this.RedNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RedNUD.Name = "RedNUD";
            this.RedNUD.Size = new System.Drawing.Size(105, 23);
            this.RedNUD.TabIndex = 2;
            // 
            // GreenNUD
            // 
            this.GreenNUD.Enabled = false;
            this.GreenNUD.Location = new System.Drawing.Point(492, 497);
            this.GreenNUD.Margin = new System.Windows.Forms.Padding(2);
            this.GreenNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GreenNUD.Name = "GreenNUD";
            this.GreenNUD.Size = new System.Drawing.Size(105, 23);
            this.GreenNUD.TabIndex = 4;
            // 
            // GLabel
            // 
            this.GLabel.AutoSize = true;
            this.GLabel.Enabled = false;
            this.GLabel.Location = new System.Drawing.Point(441, 500);
            this.GLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.GLabel.Name = "GLabel";
            this.GLabel.Size = new System.Drawing.Size(24, 15);
            this.GLabel.TabIndex = 3;
            this.GLabel.Text = "G:  ";
            // 
            // BlueNUD
            // 
            this.BlueNUD.Enabled = false;
            this.BlueNUD.Location = new System.Drawing.Point(685, 497);
            this.BlueNUD.Margin = new System.Windows.Forms.Padding(2);
            this.BlueNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.BlueNUD.Name = "BlueNUD";
            this.BlueNUD.Size = new System.Drawing.Size(105, 23);
            this.BlueNUD.TabIndex = 6;
            // 
            // BLabel
            // 
            this.BLabel.AutoSize = true;
            this.BLabel.Enabled = false;
            this.BLabel.Location = new System.Drawing.Point(634, 501);
            this.BLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BLabel.Name = "BLabel";
            this.BLabel.Size = new System.Drawing.Size(20, 15);
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
            // NoteRGBLabel
            // 
            this.NoteRGBLabel.AutoSize = true;
            this.NoteRGBLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NoteRGBLabel.Location = new System.Drawing.Point(20, 542);
            this.NoteRGBLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NoteRGBLabel.Name = "NoteRGBLabel";
            this.NoteRGBLabel.Size = new System.Drawing.Size(380, 13);
            this.NoteRGBLabel.TabIndex = 8;
            this.NoteRGBLabel.Text = "Note: RGB combinations in which all 3 values are less than 32 are not available.";
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
            this.SendButton.Location = new System.Drawing.Point(839, 531);
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
            // ContentFlowLayoutPanel
            // 
            this.ContentFlowLayoutPanel.AutoScroll = true;
            this.ContentFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContentFlowLayoutPanel.Location = new System.Drawing.Point(19, 113);
            this.ContentFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.ContentFlowLayoutPanel.Name = "ContentFlowLayoutPanel";
            this.ContentFlowLayoutPanel.Size = new System.Drawing.Size(801, 369);
            this.ContentFlowLayoutPanel.TabIndex = 13;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HeaderPanel.Controls.Add(this.FavouriteButton);
            this.HeaderPanel.Controls.Add(this.ConfigNameButton);
            this.HeaderPanel.Controls.Add(this.RedButton);
            this.HeaderPanel.Controls.Add(this.GreenButton);
            this.HeaderPanel.Controls.Add(this.BlueButton);
            this.HeaderPanel.Controls.Add(this.ViewerButton);
            this.HeaderPanel.Controls.Add(this.ChosenConfigButton);
            this.HeaderPanel.Location = new System.Drawing.Point(19, 67);
            this.HeaderPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(801, 47);
            this.HeaderPanel.TabIndex = 14;
            // 
            // FavouriteButton
            // 
            this.FavouriteButton.FlatAppearance.BorderSize = 0;
            this.FavouriteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FavouriteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FavouriteButton.Location = new System.Drawing.Point(674, 3);
            this.FavouriteButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FavouriteButton.Name = "FavouriteButton";
            this.FavouriteButton.Size = new System.Drawing.Size(105, 38);
            this.FavouriteButton.TabIndex = 21;
            this.FavouriteButton.Text = "Favourite";
            this.FavouriteButton.UseVisualStyleBackColor = true;
            // 
            // ConfigNameButton
            // 
            this.ConfigNameButton.FlatAppearance.BorderSize = 0;
            this.ConfigNameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConfigNameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ConfigNameButton.Location = new System.Drawing.Point(4, 3);
            this.ConfigNameButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ConfigNameButton.Name = "ConfigNameButton";
            this.ConfigNameButton.Size = new System.Drawing.Size(105, 38);
            this.ConfigNameButton.TabIndex = 20;
            this.ConfigNameButton.Text = "ConfigName";
            this.ConfigNameButton.UseVisualStyleBackColor = true;
            this.ConfigNameButton.Click += new System.EventHandler(this.ConfigNameButton_Click);
            // 
            // RedButton
            // 
            this.RedButton.FlatAppearance.BorderSize = 0;
            this.RedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RedButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RedButton.Location = new System.Drawing.Point(112, 3);
            this.RedButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RedButton.Name = "RedButton";
            this.RedButton.Size = new System.Drawing.Size(105, 38);
            this.RedButton.TabIndex = 19;
            this.RedButton.Text = "Red";
            this.RedButton.UseVisualStyleBackColor = true;
            // 
            // GreenButton
            // 
            this.GreenButton.FlatAppearance.BorderSize = 0;
            this.GreenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GreenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GreenButton.Location = new System.Drawing.Point(224, 3);
            this.GreenButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GreenButton.Name = "GreenButton";
            this.GreenButton.Size = new System.Drawing.Size(105, 38);
            this.GreenButton.TabIndex = 18;
            this.GreenButton.Text = "Green";
            this.GreenButton.UseVisualStyleBackColor = true;
            // 
            // BlueButton
            // 
            this.BlueButton.FlatAppearance.BorderSize = 0;
            this.BlueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BlueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BlueButton.Location = new System.Drawing.Point(336, 3);
            this.BlueButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BlueButton.Name = "BlueButton";
            this.BlueButton.Size = new System.Drawing.Size(105, 38);
            this.BlueButton.TabIndex = 17;
            this.BlueButton.Text = "Blue";
            this.BlueButton.UseVisualStyleBackColor = true;
            // 
            // ViewerButton
            // 
            this.ViewerButton.FlatAppearance.BorderSize = 0;
            this.ViewerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ViewerButton.Location = new System.Drawing.Point(448, 3);
            this.ViewerButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ViewerButton.Name = "ViewerButton";
            this.ViewerButton.Size = new System.Drawing.Size(105, 38);
            this.ViewerButton.TabIndex = 16;
            this.ViewerButton.Text = "Viewer";
            this.ViewerButton.UseVisualStyleBackColor = true;
            // 
            // ChosenConfigButton
            // 
            this.ChosenConfigButton.FlatAppearance.BorderSize = 0;
            this.ChosenConfigButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChosenConfigButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ChosenConfigButton.Location = new System.Drawing.Point(560, 3);
            this.ChosenConfigButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ChosenConfigButton.Name = "ChosenConfigButton";
            this.ChosenConfigButton.Size = new System.Drawing.Size(107, 38);
            this.ChosenConfigButton.TabIndex = 15;
            this.ChosenConfigButton.Text = "ChosenConfig";
            this.ChosenConfigButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(49, 498);
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
            // AskRGBforSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 583);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HeaderPanel);
            this.Controls.Add(this.ContentFlowLayoutPanel);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.NoteRGBLabel);
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
            ((System.ComponentModel.ISupportInitialize)(this.RedNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).EndInit();
            this.HeaderPanel.ResumeLayout(false);
            this.NameCMS.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label RGBTitleLabel;
        private System.Windows.Forms.Label RLabel;
        private System.Windows.Forms.Label GLabel;
        private System.Windows.Forms.Label BLabel;
        private System.Windows.Forms.Button WebHelpRGB;
        private System.Windows.Forms.Label NoteRGBLabel;
        private System.Windows.Forms.ToolTip RGBWebToolTip;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Button RedHeaderButton;
        private System.Windows.Forms.FlowLayoutPanel ContentFlowLayoutPanel;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Button BlueButton;
        private System.Windows.Forms.Button ViewerButton;
        private System.Windows.Forms.Button ChosenConfigButton;
        private System.Windows.Forms.Button ConfigNameButton;
        private System.Windows.Forms.Button RedButton;
        private System.Windows.Forms.Button GreenButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.ContextMenuStrip NameCMS;
        private System.Windows.Forms.ToolStripMenuItem normalOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ascendingOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descendingOrderToolStripMenuItem;
        private System.Windows.Forms.Button FavouriteButton;
        internal System.Windows.Forms.NumericUpDown RedNUD;
        internal System.Windows.Forms.NumericUpDown GreenNUD;
        internal System.Windows.Forms.NumericUpDown BlueNUD;
    }
}