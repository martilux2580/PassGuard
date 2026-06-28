namespace PassGuard.GUI
{
	partial class ContentStatsUC
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Histogram1Plotview = new OxyPlot.WindowsForms.PlotView();
			this.Histogram2Plotview = new OxyPlot.WindowsForms.PlotView();
			this.H2InfoLabel = new System.Windows.Forms.Label();
			this.DownloadData2Button = new System.Windows.Forms.Button();
			this.DownloadData1Button = new System.Windows.Forms.Button();
			this.TextStatsLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Histogram1Plotview
			// 
			this.Histogram1Plotview.Location = new System.Drawing.Point(3, 3);
			this.Histogram1Plotview.Name = "Histogram1Plotview";
			this.Histogram1Plotview.PanCursor = System.Windows.Forms.Cursors.Hand;
			this.Histogram1Plotview.Size = new System.Drawing.Size(550, 400);
			this.Histogram1Plotview.TabIndex = 0;
			this.Histogram1Plotview.Text = "plotView1";
			this.Histogram1Plotview.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
			this.Histogram1Plotview.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.Histogram1Plotview.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
			// 
			// Histogram2Plotview
			// 
			this.Histogram2Plotview.Location = new System.Drawing.Point(591, 3);
			this.Histogram2Plotview.Name = "Histogram2Plotview";
			this.Histogram2Plotview.PanCursor = System.Windows.Forms.Cursors.Hand;
			this.Histogram2Plotview.Size = new System.Drawing.Size(550, 389);
			this.Histogram2Plotview.TabIndex = 1;
			this.Histogram2Plotview.Text = "plotView2";
			this.Histogram2Plotview.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
			this.Histogram2Plotview.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.Histogram2Plotview.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
			// 
			// H2InfoLabel
			// 
			this.H2InfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.H2InfoLabel.Location = new System.Drawing.Point(591, 387);
			this.H2InfoLabel.Name = "H2InfoLabel";
			this.H2InfoLabel.Size = new System.Drawing.Size(550, 16);
			this.H2InfoLabel.TabIndex = 14;
			this.H2InfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// DownloadData2Button
			// 
			this.DownloadData2Button.FlatAppearance.BorderSize = 0;
			this.DownloadData2Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DownloadData2Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.DownloadData2Button.Location = new System.Drawing.Point(977, 409);
			this.DownloadData2Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.DownloadData2Button.Name = "DownloadData2Button";
			this.DownloadData2Button.Size = new System.Drawing.Size(163, 51);
			this.DownloadData2Button.TabIndex = 18;
			this.DownloadData2Button.UseVisualStyleBackColor = true;
			this.DownloadData2Button.Click += new System.EventHandler(this.DownloadData2Button_Click);
			this.DownloadData2Button.MouseEnter += new System.EventHandler(this.DownloadData2Button_MouseEnter);
			this.DownloadData2Button.MouseLeave += new System.EventHandler(this.DownloadData2Button_MouseLeave);
			// 
			// DownloadData1Button
			// 
			this.DownloadData1Button.FlatAppearance.BorderSize = 0;
			this.DownloadData1Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DownloadData1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.DownloadData1Button.Location = new System.Drawing.Point(3, 409);
			this.DownloadData1Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.DownloadData1Button.Name = "DownloadData1Button";
			this.DownloadData1Button.Size = new System.Drawing.Size(163, 51);
			this.DownloadData1Button.TabIndex = 17;
			this.DownloadData1Button.UseVisualStyleBackColor = true;
			this.DownloadData1Button.Click += new System.EventHandler(this.DownloadData1Button_Click);
			this.DownloadData1Button.MouseEnter += new System.EventHandler(this.DownloadData1Button_MouseEnter);
			this.DownloadData1Button.MouseLeave += new System.EventHandler(this.DownloadData1Button_MouseLeave);
			// 
			// TextStatsLabel
			// 
			this.TextStatsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TextStatsLabel.Location = new System.Drawing.Point(173, 409);
			this.TextStatsLabel.Name = "TextStatsLabel";
			this.TextStatsLabel.Size = new System.Drawing.Size(798, 51);
			this.TextStatsLabel.TabIndex = 16;
			this.TextStatsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ContentStatsUC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.DownloadData2Button);
			this.Controls.Add(this.DownloadData1Button);
			this.Controls.Add(this.TextStatsLabel);
			this.Controls.Add(this.H2InfoLabel);
			this.Controls.Add(this.Histogram2Plotview);
			this.Controls.Add(this.Histogram1Plotview);
			this.Name = "ContentStatsUC";
			this.Size = new System.Drawing.Size(1144, 463);
			this.ResumeLayout(false);

		}

		#endregion

		private OxyPlot.WindowsForms.PlotView Histogram1Plotview;
		private OxyPlot.WindowsForms.PlotView Histogram2Plotview;
		private System.Windows.Forms.Label H2InfoLabel;
		private System.Windows.Forms.Button DownloadData2Button;
		private System.Windows.Forms.Button DownloadData1Button;
		private System.Windows.Forms.Label TextStatsLabel;
	}
}
