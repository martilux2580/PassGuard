
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
            this.SendRGBButton = new System.Windows.Forms.Button();
            this.RGBWebToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.RedNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // RGBTitleLabel
            // 
            this.RGBTitleLabel.AutoSize = true;
            this.RGBTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGBTitleLabel.Location = new System.Drawing.Point(9, 19);
            this.RGBTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RGBTitleLabel.Name = "RGBTitleLabel";
            this.RGBTitleLabel.Size = new System.Drawing.Size(287, 17);
            this.RGBTitleLabel.TabIndex = 0;
            this.RGBTitleLabel.Text = "Enter the RGB values of the desired colour: ";
            // 
            // RLabel
            // 
            this.RLabel.AutoSize = true;
            this.RLabel.Location = new System.Drawing.Point(30, 48);
            this.RLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RLabel.Name = "RLabel";
            this.RLabel.Size = new System.Drawing.Size(21, 13);
            this.RLabel.TabIndex = 1;
            this.RLabel.Text = "R: ";
            // 
            // RedNUD
            // 
            this.RedNUD.Location = new System.Drawing.Point(74, 46);
            this.RedNUD.Margin = new System.Windows.Forms.Padding(2);
            this.RedNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RedNUD.Name = "RedNUD";
            this.RedNUD.Size = new System.Drawing.Size(90, 20);
            this.RedNUD.TabIndex = 2;
            // 
            // GreenNUD
            // 
            this.GreenNUD.Location = new System.Drawing.Point(74, 74);
            this.GreenNUD.Margin = new System.Windows.Forms.Padding(2);
            this.GreenNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GreenNUD.Name = "GreenNUD";
            this.GreenNUD.Size = new System.Drawing.Size(90, 20);
            this.GreenNUD.TabIndex = 4;
            // 
            // GLabel
            // 
            this.GLabel.AutoSize = true;
            this.GLabel.Location = new System.Drawing.Point(30, 76);
            this.GLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.GLabel.Name = "GLabel";
            this.GLabel.Size = new System.Drawing.Size(24, 13);
            this.GLabel.TabIndex = 3;
            this.GLabel.Text = "G:  ";
            // 
            // BlueNUD
            // 
            this.BlueNUD.Location = new System.Drawing.Point(74, 103);
            this.BlueNUD.Margin = new System.Windows.Forms.Padding(2);
            this.BlueNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.BlueNUD.Name = "BlueNUD";
            this.BlueNUD.Size = new System.Drawing.Size(90, 20);
            this.BlueNUD.TabIndex = 6;
            // 
            // BLabel
            // 
            this.BLabel.AutoSize = true;
            this.BLabel.Location = new System.Drawing.Point(30, 106);
            this.BLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BLabel.Name = "BLabel";
            this.BLabel.Size = new System.Drawing.Size(20, 13);
            this.BLabel.TabIndex = 5;
            this.BLabel.Text = "B: ";
            // 
            // WebHelpRGB
            // 
            this.WebHelpRGB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WebHelpRGB.FlatAppearance.BorderSize = 0;
            this.WebHelpRGB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WebHelpRGB.Location = new System.Drawing.Point(357, 6);
            this.WebHelpRGB.Margin = new System.Windows.Forms.Padding(2);
            this.WebHelpRGB.Name = "WebHelpRGB";
            this.WebHelpRGB.Size = new System.Drawing.Size(40, 44);
            this.WebHelpRGB.TabIndex = 7;
            this.RGBWebToolTip.SetToolTip(this.WebHelpRGB, "Choose a colour from this webpage, take its RGB values and copy them here :)");
            this.WebHelpRGB.UseVisualStyleBackColor = true;
            this.WebHelpRGB.Click += new System.EventHandler(this.WebHelpRGB_Click);
            // 
            // NoteRGBLabel
            // 
            this.NoteRGBLabel.AutoSize = true;
            this.NoteRGBLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoteRGBLabel.Location = new System.Drawing.Point(19, 139);
            this.NoteRGBLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NoteRGBLabel.Name = "NoteRGBLabel";
            this.NoteRGBLabel.Size = new System.Drawing.Size(380, 13);
            this.NoteRGBLabel.TabIndex = 8;
            this.NoteRGBLabel.Text = "Note: RGB combinations in which all 3 values are less than 32 are not available.";
            // 
            // SendRGBButton
            // 
            this.SendRGBButton.FlatAppearance.BorderSize = 0;
            this.SendRGBButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendRGBButton.Location = new System.Drawing.Point(321, 98);
            this.SendRGBButton.Margin = new System.Windows.Forms.Padding(2);
            this.SendRGBButton.Name = "SendRGBButton";
            this.SendRGBButton.Size = new System.Drawing.Size(76, 24);
            this.SendRGBButton.TabIndex = 9;
            this.SendRGBButton.Text = "Send";
            this.SendRGBButton.UseVisualStyleBackColor = true;
            this.SendRGBButton.Click += new System.EventHandler(this.SendRGBButton_Click);
            this.SendRGBButton.MouseEnter += new System.EventHandler(this.SendRGBButton_MouseEnter);
            this.SendRGBButton.MouseLeave += new System.EventHandler(this.SendRGBButton_MouseLeave);
            // 
            // RGBWebToolTip
            // 
            this.RGBWebToolTip.AutomaticDelay = 300;
            this.RGBWebToolTip.AutoPopDelay = 5000;
            this.RGBWebToolTip.InitialDelay = 300;
            this.RGBWebToolTip.ReshowDelay = 60;
            // 
            // AskRGBforSettings
            // 
            this.AcceptButton = this.SendRGBButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 161);
            this.Controls.Add(this.SendRGBButton);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label RGBTitleLabel;
        private System.Windows.Forms.Label RLabel;
        private System.Windows.Forms.NumericUpDown RedNUD;
        private System.Windows.Forms.NumericUpDown GreenNUD;
        private System.Windows.Forms.Label GLabel;
        private System.Windows.Forms.NumericUpDown BlueNUD;
        private System.Windows.Forms.Label BLabel;
        private System.Windows.Forms.Button WebHelpRGB;
        private System.Windows.Forms.Label NoteRGBLabel;
        private System.Windows.Forms.Button SendRGBButton;
        private System.Windows.Forms.ToolTip RGBWebToolTip;
    }
}