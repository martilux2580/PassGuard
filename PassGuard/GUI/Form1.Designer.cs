
namespace PassGuard
{
    partial class mainWindow
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.LoadVaultButton = new System.Windows.Forms.Button();
            this.CreateVaultButton = new System.Windows.Forms.Button();
            this.LogoPanel = new System.Windows.Forms.Panel();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.DesignerLabel = new System.Windows.Forms.Label();
            this.LogoLabel = new System.Windows.Forms.Label();
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.ToolTipNewPassVault = new System.Windows.Forms.ToolTip(this.components);
            this.ToolTipLoadPassVault = new System.Windows.Forms.ToolTip(this.components);
            this.LeftPanel.SuspendLayout();
            this.MenuPanel.SuspendLayout();
            this.LogoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.treeView1);
            this.LeftPanel.Location = new System.Drawing.Point(1041, 338);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(391, 348);
            this.LeftPanel.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.Menu;
            this.treeView1.Location = new System.Drawing.Point(4, 182);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(384, 489);
            this.treeView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(405, 544);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // MenuPanel
            // 
            this.MenuPanel.Controls.Add(this.LoadVaultButton);
            this.MenuPanel.Controls.Add(this.CreateVaultButton);
            this.MenuPanel.Controls.Add(this.LogoPanel);
            this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuPanel.Location = new System.Drawing.Point(0, 0);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Size = new System.Drawing.Size(384, 698);
            this.MenuPanel.TabIndex = 2;
            // 
            // LoadVaultButton
            // 
            this.LoadVaultButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadVaultButton.FlatAppearance.BorderSize = 0;
            this.LoadVaultButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadVaultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadVaultButton.ForeColor = System.Drawing.Color.Black;
            this.LoadVaultButton.Location = new System.Drawing.Point(38, 270);
            this.LoadVaultButton.Name = "LoadVaultButton";
            this.LoadVaultButton.Size = new System.Drawing.Size(309, 59);
            this.LoadVaultButton.TabIndex = 5;
            this.LoadVaultButton.Text = "Load Password Vault";
            this.ToolTipNewPassVault.SetToolTip(this.LoadVaultButton, "Load passwords from a Password Vault.");
            this.LoadVaultButton.UseVisualStyleBackColor = true;
            this.LoadVaultButton.Click += new System.EventHandler(this.LoadVaultButton_Click);
            this.LoadVaultButton.MouseEnter += new System.EventHandler(this.LoadVaultButton_MouseEnter);
            this.LoadVaultButton.MouseLeave += new System.EventHandler(this.LoadVaultButton_MouseLeave);
            // 
            // CreateVaultButton
            // 
            this.CreateVaultButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CreateVaultButton.FlatAppearance.BorderSize = 0;
            this.CreateVaultButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateVaultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateVaultButton.ForeColor = System.Drawing.Color.Black;
            this.CreateVaultButton.Location = new System.Drawing.Point(37, 193);
            this.CreateVaultButton.Name = "CreateVaultButton";
            this.CreateVaultButton.Size = new System.Drawing.Size(309, 59);
            this.CreateVaultButton.TabIndex = 4;
            this.CreateVaultButton.Text = "New Password Vault";
            this.ToolTipNewPassVault.SetToolTip(this.CreateVaultButton, "Create a Safe Vault to Store your Passwords");
            this.CreateVaultButton.UseVisualStyleBackColor = true;
            this.CreateVaultButton.Click += new System.EventHandler(this.CreateVaultButton_Click);
            this.CreateVaultButton.MouseEnter += new System.EventHandler(this.CreateVaultButton_MouseEnter);
            this.CreateVaultButton.MouseLeave += new System.EventHandler(this.CreateVaultButton_MouseLeave);
            // 
            // LogoPanel
            // 
            this.LogoPanel.Controls.Add(this.LogoPictureBox);
            this.LogoPanel.Controls.Add(this.DesignerLabel);
            this.LogoPanel.Controls.Add(this.LogoLabel);
            this.LogoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.LogoPanel.Location = new System.Drawing.Point(0, 0);
            this.LogoPanel.Name = "LogoPanel";
            this.LogoPanel.Size = new System.Drawing.Size(384, 164);
            this.LogoPanel.TabIndex = 3;
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.Location = new System.Drawing.Point(128, 3);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(126, 103);
            this.LogoPictureBox.TabIndex = 3;
            this.LogoPictureBox.TabStop = false;
            // 
            // DesignerLabel
            // 
            this.DesignerLabel.AutoSize = true;
            this.DesignerLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DesignerLabel.Font = new System.Drawing.Font("Mongolian Baiti", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DesignerLabel.ForeColor = System.Drawing.Color.Black;
            this.DesignerLabel.Location = new System.Drawing.Point(91, 138);
            this.DesignerLabel.Name = "DesignerLabel";
            this.DesignerLabel.Size = new System.Drawing.Size(197, 18);
            this.DesignerLabel.TabIndex = 1;
            this.DesignerLabel.Text = "Designed by martilux2580";
            this.DesignerLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DesignerLabel_MouseClick);
            this.DesignerLabel.MouseEnter += new System.EventHandler(this.DesignerLabel_MouseEnter);
            this.DesignerLabel.MouseLeave += new System.EventHandler(this.DesignerLabel_MouseLeave);
            // 
            // LogoLabel
            // 
            this.LogoLabel.AutoSize = true;
            this.LogoLabel.Font = new System.Drawing.Font("Modern No. 20", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogoLabel.ForeColor = System.Drawing.Color.Black;
            this.LogoLabel.Location = new System.Drawing.Point(111, 104);
            this.LogoLabel.Name = "LogoLabel";
            this.LogoLabel.Size = new System.Drawing.Size(158, 34);
            this.LogoLabel.TabIndex = 0;
            this.LogoLabel.Text = "PassGuard";
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OptionsPanel.Location = new System.Drawing.Point(384, 0);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(1195, 164);
            this.OptionsPanel.TabIndex = 3;
            // 
            // ToolTipNewPassVault
            // 
            this.ToolTipNewPassVault.AutomaticDelay = 300;
            this.ToolTipNewPassVault.AutoPopDelay = 5000;
            this.ToolTipNewPassVault.BackColor = System.Drawing.SystemColors.GrayText;
            this.ToolTipNewPassVault.InitialDelay = 300;
            this.ToolTipNewPassVault.ReshowDelay = 60;
            // 
            // ToolTipLoadPassVault
            // 
            this.ToolTipLoadPassVault.AutomaticDelay = 300;
            this.ToolTipLoadPassVault.AutoPopDelay = 5000;
            this.ToolTipLoadPassVault.BackColor = System.Drawing.SystemColors.GrayText;
            this.ToolTipLoadPassVault.InitialDelay = 300;
            this.ToolTipLoadPassVault.ReshowDelay = 60;
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1579, 698);
            this.Controls.Add(this.OptionsPanel);
            this.Controls.Add(this.MenuPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LeftPanel);
            this.Name = "mainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PassGuard™";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LeftPanel.ResumeLayout(false);
            this.MenuPanel.ResumeLayout(false);
            this.LogoPanel.ResumeLayout(false);
            this.LogoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.Panel LogoPanel;
        private System.Windows.Forms.Label LogoLabel;
        private System.Windows.Forms.Label DesignerLabel;
        private System.Windows.Forms.PictureBox LogoPictureBox;
        private System.Windows.Forms.Panel OptionsPanel;
        private System.Windows.Forms.Button CreateVaultButton;
        private System.Windows.Forms.ToolTip ToolTipNewPassVault;
        private System.Windows.Forms.Button LoadVaultButton;
        private System.Windows.Forms.ToolTip ToolTipLoadPassVault;
    }
}

