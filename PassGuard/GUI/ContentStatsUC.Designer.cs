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
			this.LoadingLabel = new System.Windows.Forms.Label();
			this.TextStatsTextbox = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// Histogram1Plotview
			// 
			this.Histogram1Plotview.Location = new System.Drawing.Point(3, 3);
			this.Histogram1Plotview.Name = "Histogram1Plotview";
			this.Histogram1Plotview.PanCursor = System.Windows.Forms.Cursors.Hand;
			this.Histogram1Plotview.Size = new System.Drawing.Size(469, 400);
			this.Histogram1Plotview.TabIndex = 0;
			this.Histogram1Plotview.Text = "plotView1";
			this.Histogram1Plotview.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
			this.Histogram1Plotview.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.Histogram1Plotview.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
			// 
			// Histogram2Plotview
			// 
			this.Histogram2Plotview.Location = new System.Drawing.Point(472, 3);
			this.Histogram2Plotview.Name = "Histogram2Plotview";
			this.Histogram2Plotview.PanCursor = System.Windows.Forms.Cursors.Hand;
			this.Histogram2Plotview.Size = new System.Drawing.Size(469, 400);
			this.Histogram2Plotview.TabIndex = 1;
			this.Histogram2Plotview.Text = "plotView2";
			this.Histogram2Plotview.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
			this.Histogram2Plotview.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.Histogram2Plotview.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
			// 
			// LoadingLabel
			// 
			this.LoadingLabel.AutoSize = true;
			this.LoadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.LoadingLabel.Location = new System.Drawing.Point(402, 206);
			this.LoadingLabel.Name = "LoadingLabel";
			this.LoadingLabel.Size = new System.Drawing.Size(140, 18);
			this.LoadingLabel.TabIndex = 4;
			this.LoadingLabel.Text = "Loading Graphics....";
			// 
			// TextStatsTextbox
			// 
			this.TextStatsTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextStatsTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TextStatsTextbox.Location = new System.Drawing.Point(3, 409);
			this.TextStatsTextbox.Name = "TextStatsTextbox";
			this.TextStatsTextbox.ReadOnly = true;
			this.TextStatsTextbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.TextStatsTextbox.Size = new System.Drawing.Size(938, 51);
			this.TextStatsTextbox.TabIndex = 5;
			this.TextStatsTextbox.Text = "";
			// 
			// ContentStatsUC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TextStatsTextbox);
			this.Controls.Add(this.LoadingLabel);
			this.Controls.Add(this.Histogram2Plotview);
			this.Controls.Add(this.Histogram1Plotview);
			this.Name = "ContentStatsUC";
			this.Size = new System.Drawing.Size(944, 463);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OxyPlot.WindowsForms.PlotView Histogram1Plotview;
		private OxyPlot.WindowsForms.PlotView Histogram2Plotview;
		private System.Windows.Forms.Label LoadingLabel;
		private System.Windows.Forms.RichTextBox TextStatsTextbox;
	}
}
