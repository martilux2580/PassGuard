
namespace PassGuard.GUI
{
    partial class OutlineColorDataRowUC
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
            this.NameTextbox = new System.Windows.Forms.TextBox();
            this.RedNUD = new System.Windows.Forms.NumericUpDown();
            this.BlueNUD = new System.Windows.Forms.NumericUpDown();
            this.GreenNUD = new System.Windows.Forms.NumericUpDown();
            this.ViewerPanel = new System.Windows.Forms.Panel();
            this.ChosenConfigCheckbox = new System.Windows.Forms.CheckBox();
            this.FavouriteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RedNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // NameTextbox
            // 
            this.NameTextbox.Enabled = false;
            this.NameTextbox.Location = new System.Drawing.Point(2, 5);
            this.NameTextbox.Name = "NameTextbox";
            this.NameTextbox.Size = new System.Drawing.Size(86, 20);
            this.NameTextbox.TabIndex = 0;
            // 
            // RedNUD
            // 
            this.RedNUD.BackColor = System.Drawing.SystemColors.Window;
            this.RedNUD.Enabled = false;
            this.RedNUD.Location = new System.Drawing.Point(95, 5);
            this.RedNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RedNUD.Name = "RedNUD";
            this.RedNUD.Size = new System.Drawing.Size(86, 20);
            this.RedNUD.TabIndex = 3;
            // 
            // BlueNUD
            // 
            this.BlueNUD.Enabled = false;
            this.BlueNUD.Location = new System.Drawing.Point(287, 5);
            this.BlueNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.BlueNUD.Name = "BlueNUD";
            this.BlueNUD.Size = new System.Drawing.Size(86, 20);
            this.BlueNUD.TabIndex = 4;
            // 
            // GreenNUD
            // 
            this.GreenNUD.Enabled = false;
            this.GreenNUD.Location = new System.Drawing.Point(191, 6);
            this.GreenNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GreenNUD.Name = "GreenNUD";
            this.GreenNUD.Size = new System.Drawing.Size(86, 20);
            this.GreenNUD.TabIndex = 5;
            // 
            // ViewerPanel
            // 
            this.ViewerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewerPanel.Enabled = false;
            this.ViewerPanel.Location = new System.Drawing.Point(383, 5);
            this.ViewerPanel.Name = "ViewerPanel";
            this.ViewerPanel.Size = new System.Drawing.Size(86, 20);
            this.ViewerPanel.TabIndex = 6;
            // 
            // ChosenConfigCheckbox
            // 
            this.ChosenConfigCheckbox.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChosenConfigCheckbox.AutoSize = true;
            this.ChosenConfigCheckbox.FlatAppearance.BorderSize = 0;
            this.ChosenConfigCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChosenConfigCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChosenConfigCheckbox.Location = new System.Drawing.Point(495, 3);
            this.ChosenConfigCheckbox.Name = "ChosenConfigCheckbox";
            this.ChosenConfigCheckbox.Size = new System.Drawing.Size(58, 23);
            this.ChosenConfigCheckbox.TabIndex = 7;
            this.ChosenConfigCheckbox.Text = "Disabled";
            this.ChosenConfigCheckbox.UseVisualStyleBackColor = true;
            this.ChosenConfigCheckbox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChosenConfigCheckbox_MouseClick);
            // 
            // FavouriteButton
            // 
            this.FavouriteButton.FlatAppearance.BorderSize = 0;
            this.FavouriteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FavouriteButton.Location = new System.Drawing.Point(593, 5);
            this.FavouriteButton.Name = "FavouriteButton";
            this.FavouriteButton.Size = new System.Drawing.Size(54, 20);
            this.FavouriteButton.TabIndex = 8;
            this.FavouriteButton.UseVisualStyleBackColor = true;
            this.FavouriteButton.Click += new System.EventHandler(this.FavouriteButton_Click);
            // 
            // OutlineColorDataRowUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FavouriteButton);
            this.Controls.Add(this.ChosenConfigCheckbox);
            this.Controls.Add(this.ViewerPanel);
            this.Controls.Add(this.GreenNUD);
            this.Controls.Add(this.BlueNUD);
            this.Controls.Add(this.RedNUD);
            this.Controls.Add(this.NameTextbox);
            this.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.Name = "OutlineColorDataRowUC";
            this.Size = new System.Drawing.Size(655, 31);
            ((System.ComponentModel.ISupportInitialize)(this.RedNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameTextbox;
        private System.Windows.Forms.NumericUpDown RedNUD;
        private System.Windows.Forms.NumericUpDown BlueNUD;
        private System.Windows.Forms.NumericUpDown GreenNUD;
        private System.Windows.Forms.Panel ViewerPanel;
        private System.Windows.Forms.CheckBox ChosenConfigCheckbox;
        private System.Windows.Forms.Button FavouriteButton;
    }
}
