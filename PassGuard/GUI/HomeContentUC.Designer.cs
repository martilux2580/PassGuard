
namespace PassGuard.GUI
{
    partial class HomeContentUC
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.TimePanel = new System.Windows.Forms.Panel();
            this.TimePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TimeLabel
            // 
            this.TimeLabel.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.Location = new System.Drawing.Point(3, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(1195, 93);
            this.TimeLabel.TabIndex = 0;
            this.TimeLabel.Text = "Loading Time....";
            this.TimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DateLabel
            // 
            this.DateLabel.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLabel.Location = new System.Drawing.Point(6, 107);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(1195, 89);
            this.DateLabel.TabIndex = 1;
            this.DateLabel.Text = "Loading Date....";
            this.DateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // TimePanel
            // 
            this.TimePanel.AutoSize = true;
            this.TimePanel.Controls.Add(this.DateLabel);
            this.TimePanel.Controls.Add(this.TimeLabel);
            this.TimePanel.Location = new System.Drawing.Point(0, 171);
            this.TimePanel.Name = "TimePanel";
            this.TimePanel.Size = new System.Drawing.Size(1204, 196);
            this.TimePanel.TabIndex = 2;
            // 
            // HomeContentUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.TimePanel);
            this.Name = "HomeContentUC";
            this.Size = new System.Drawing.Size(1153, 534);
            this.Load += new System.EventHandler(this.HomeContentUC_Load);
            this.TimePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.Panel TimePanel;
    }
}
