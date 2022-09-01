
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
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.AppVersionLabel = new System.Windows.Forms.Label();
            this.CreateQuickPassButton = new System.Windows.Forms.Button();
            this.LoadVaultButton = new System.Windows.Forms.Button();
            this.CreateVaultButton = new System.Windows.Forms.Button();
            this.LogoPanel = new System.Windows.Forms.Panel();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.DesignerLabel = new System.Windows.Forms.Label();
            this.LogoLabel = new System.Windows.Forms.Label();
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.SettingButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.ToolTipNewPassVault = new System.Windows.Forms.ToolTip(this.components);
            this.ToolTipLoadPassVault = new System.Windows.Forms.ToolTip(this.components);
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.SettingsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SettingsCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TitleSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.SettingsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.changeThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeComplemenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.createABackupOfYourVaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureAnAutoBackupOfAVaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveChangesClosePassGuardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPanel.SuspendLayout();
            this.LogoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.OptionsPanel.SuspendLayout();
            this.SettingsCMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuPanel
            // 
            this.MenuPanel.Controls.Add(this.AppVersionLabel);
            this.MenuPanel.Controls.Add(this.CreateQuickPassButton);
            this.MenuPanel.Controls.Add(this.LoadVaultButton);
            this.MenuPanel.Controls.Add(this.CreateVaultButton);
            this.MenuPanel.Controls.Add(this.LogoPanel);
            this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuPanel.Location = new System.Drawing.Point(0, 0);
            this.MenuPanel.Margin = new System.Windows.Forms.Padding(2);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Size = new System.Drawing.Size(288, 574);
            this.MenuPanel.TabIndex = 2;
            // 
            // AppVersionLabel
            // 
            this.AppVersionLabel.AutoSize = true;
            this.AppVersionLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.AppVersionLabel.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppVersionLabel.ForeColor = System.Drawing.Color.Black;
            this.AppVersionLabel.Location = new System.Drawing.Point(9, 550);
            this.AppVersionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AppVersionLabel.Name = "AppVersionLabel";
            this.AppVersionLabel.Size = new System.Drawing.Size(58, 16);
            this.AppVersionLabel.TabIndex = 4;
            this.AppVersionLabel.Text = "          ";
            // 
            // CreateQuickPassButton
            // 
            this.CreateQuickPassButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CreateQuickPassButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.CreateQuickPassButton.FlatAppearance.BorderSize = 0;
            this.CreateQuickPassButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateQuickPassButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateQuickPassButton.ForeColor = System.Drawing.Color.Black;
            this.CreateQuickPassButton.Location = new System.Drawing.Point(0, 295);
            this.CreateQuickPassButton.Margin = new System.Windows.Forms.Padding(2);
            this.CreateQuickPassButton.Name = "CreateQuickPassButton";
            this.CreateQuickPassButton.Size = new System.Drawing.Size(288, 81);
            this.CreateQuickPassButton.TabIndex = 6;
            this.CreateQuickPassButton.Text = "Create Quick Password";
            this.ToolTipNewPassVault.SetToolTip(this.CreateQuickPassButton, "Create a quick + safe password.");
            this.CreateQuickPassButton.UseVisualStyleBackColor = true;
            this.CreateQuickPassButton.Click += new System.EventHandler(this.CreateQuickPassButton_Click);
            this.CreateQuickPassButton.MouseEnter += new System.EventHandler(this.CreateQuickPassButton_MouseEnter);
            this.CreateQuickPassButton.MouseLeave += new System.EventHandler(this.CreateQuickPassButton_MouseLeave);
            // 
            // LoadVaultButton
            // 
            this.LoadVaultButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadVaultButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.LoadVaultButton.FlatAppearance.BorderSize = 0;
            this.LoadVaultButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadVaultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadVaultButton.ForeColor = System.Drawing.Color.Black;
            this.LoadVaultButton.Location = new System.Drawing.Point(0, 214);
            this.LoadVaultButton.Margin = new System.Windows.Forms.Padding(2);
            this.LoadVaultButton.Name = "LoadVaultButton";
            this.LoadVaultButton.Size = new System.Drawing.Size(288, 81);
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
            this.CreateVaultButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.CreateVaultButton.FlatAppearance.BorderSize = 0;
            this.CreateVaultButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateVaultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateVaultButton.ForeColor = System.Drawing.Color.Black;
            this.CreateVaultButton.Location = new System.Drawing.Point(0, 133);
            this.CreateVaultButton.Margin = new System.Windows.Forms.Padding(2);
            this.CreateVaultButton.Name = "CreateVaultButton";
            this.CreateVaultButton.Size = new System.Drawing.Size(288, 81);
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
            this.LogoPanel.Margin = new System.Windows.Forms.Padding(2);
            this.LogoPanel.Name = "LogoPanel";
            this.LogoPanel.Size = new System.Drawing.Size(288, 133);
            this.LogoPanel.TabIndex = 3;
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LogoPictureBox.Location = new System.Drawing.Point(96, 2);
            this.LogoPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(94, 84);
            this.LogoPictureBox.TabIndex = 3;
            this.LogoPictureBox.TabStop = false;
            this.LogoPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LogoPictureBox_MouseClick);
            // 
            // DesignerLabel
            // 
            this.DesignerLabel.AutoSize = true;
            this.DesignerLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DesignerLabel.Font = new System.Drawing.Font("Mongolian Baiti", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DesignerLabel.ForeColor = System.Drawing.Color.Black;
            this.DesignerLabel.Location = new System.Drawing.Point(68, 112);
            this.DesignerLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DesignerLabel.Name = "DesignerLabel";
            this.DesignerLabel.Size = new System.Drawing.Size(157, 15);
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
            this.LogoLabel.Location = new System.Drawing.Point(83, 84);
            this.LogoLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LogoLabel.Name = "LogoLabel";
            this.LogoLabel.Size = new System.Drawing.Size(130, 29);
            this.LogoLabel.TabIndex = 0;
            this.LogoLabel.Text = "PassGuard";
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.Controls.Add(this.SettingButton);
            this.OptionsPanel.Controls.Add(this.TitleLabel);
            this.OptionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OptionsPanel.Location = new System.Drawing.Point(288, 0);
            this.OptionsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(898, 133);
            this.OptionsPanel.TabIndex = 3;
            // 
            // SettingButton
            // 
            this.SettingButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SettingButton.FlatAppearance.BorderSize = 0;
            this.SettingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingButton.Location = new System.Drawing.Point(792, 32);
            this.SettingButton.Margin = new System.Windows.Forms.Padding(2);
            this.SettingButton.Name = "SettingButton";
            this.SettingButton.Size = new System.Drawing.Size(63, 68);
            this.SettingButton.TabIndex = 2;
            this.SettingButton.UseVisualStyleBackColor = true;
            this.SettingButton.Click += new System.EventHandler(this.SettingButton_Click);
            // 
            // TitleLabel
            // 
            this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleLabel.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.ForeColor = System.Drawing.SystemColors.Desktop;
            this.TitleLabel.Location = new System.Drawing.Point(0, 0);
            this.TitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(898, 133);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "HOME";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // ContentPanel
            // 
            this.ContentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(288, 133);
            this.ContentPanel.Margin = new System.Windows.Forms.Padding(2);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(898, 441);
            this.ContentPanel.TabIndex = 4;
            // 
            // SettingsToolTip
            // 
            this.SettingsToolTip.AutomaticDelay = 300;
            this.SettingsToolTip.AutoPopDelay = 5000;
            this.SettingsToolTip.InitialDelay = 300;
            this.SettingsToolTip.ReshowDelay = 60;
            // 
            // SettingsCMS
            // 
            this.SettingsCMS.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.SettingsCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TitleSettingsToolStripMenuItem,
            this.SettingsToolStripSeparator,
            this.changeThemeToolStripMenuItem,
            this.changeComplemenToolStripMenuItem,
            this.toolStripSeparator1,
            this.createABackupOfYourVaultToolStripMenuItem,
            this.configureAnAutoBackupOfAVaultToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveChangesClosePassGuardToolStripMenuItem});
            this.SettingsCMS.Name = "SettingsCMS";
            this.SettingsCMS.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SettingsCMS.ShowImageMargin = false;
            this.SettingsCMS.Size = new System.Drawing.Size(246, 153);
            // 
            // TitleSettingsToolStripMenuItem
            // 
            this.TitleSettingsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TitleSettingsToolStripMenuItem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TitleSettingsToolStripMenuItem.Enabled = false;
            this.TitleSettingsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleSettingsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.MenuText;
            this.TitleSettingsToolStripMenuItem.Name = "TitleSettingsToolStripMenuItem";
            this.TitleSettingsToolStripMenuItem.ReadOnly = true;
            this.TitleSettingsToolStripMenuItem.Size = new System.Drawing.Size(210, 19);
            this.TitleSettingsToolStripMenuItem.Text = "SETTINGS";
            this.TitleSettingsToolStripMenuItem.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SettingsToolStripSeparator
            // 
            this.SettingsToolStripSeparator.Name = "SettingsToolStripSeparator";
            this.SettingsToolStripSeparator.Size = new System.Drawing.Size(242, 6);
            // 
            // changeThemeToolStripMenuItem
            // 
            this.changeThemeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightToolStripMenuItem,
            this.darkToolStripMenuItem});
            this.changeThemeToolStripMenuItem.Name = "changeThemeToolStripMenuItem";
            this.changeThemeToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.changeThemeToolStripMenuItem.Text = "Change Theme";
            // 
            // lightToolStripMenuItem
            // 
            this.lightToolStripMenuItem.Checked = true;
            this.lightToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            this.lightToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.lightToolStripMenuItem.Text = "Light";
            this.lightToolStripMenuItem.Click += new System.EventHandler(this.lightToolStripMenuItem_Click);
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.darkToolStripMenuItem.Text = "Dark";
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.darkToolStripMenuItem_Click);
            // 
            // changeComplemenToolStripMenuItem
            // 
            this.changeComplemenToolStripMenuItem.Name = "changeComplemenToolStripMenuItem";
            this.changeComplemenToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.changeComplemenToolStripMenuItem.Text = "Change Outline Colours";
            this.changeComplemenToolStripMenuItem.Click += new System.EventHandler(this.changeComplemenToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(242, 6);
            // 
            // createABackupOfYourVaultToolStripMenuItem
            // 
            this.createABackupOfYourVaultToolStripMenuItem.Name = "createABackupOfYourVaultToolStripMenuItem";
            this.createABackupOfYourVaultToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.createABackupOfYourVaultToolStripMenuItem.Text = "Create a Backup of your Vault";
            this.createABackupOfYourVaultToolStripMenuItem.Click += new System.EventHandler(this.createABackupOfYourVaultToolStripMenuItem_Click);
            // 
            // configureAnAutoBackupOfAVaultToolStripMenuItem
            // 
            this.configureAnAutoBackupOfAVaultToolStripMenuItem.Name = "configureAnAutoBackupOfAVaultToolStripMenuItem";
            this.configureAnAutoBackupOfAVaultToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.configureAnAutoBackupOfAVaultToolStripMenuItem.Text = "Configure an AutoBackup of a Vault";
            this.configureAnAutoBackupOfAVaultToolStripMenuItem.Click += new System.EventHandler(this.configureAnAutoBackupOfAVaultToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(242, 6);
            // 
            // saveChangesClosePassGuardToolStripMenuItem
            // 
            this.saveChangesClosePassGuardToolStripMenuItem.Name = "saveChangesClosePassGuardToolStripMenuItem";
            this.saveChangesClosePassGuardToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.saveChangesClosePassGuardToolStripMenuItem.Text = "Save Changes + Close PassGuard";
            this.saveChangesClosePassGuardToolStripMenuItem.Click += new System.EventHandler(this.saveChangesClosePassGuardToolStripMenuItem_Click);
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1186, 574);
            this.Controls.Add(this.ContentPanel);
            this.Controls.Add(this.OptionsPanel);
            this.Controls.Add(this.MenuPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1202, 613);
            this.Name = "mainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PassGuard™";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainWindow_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MenuPanel.ResumeLayout(false);
            this.MenuPanel.PerformLayout();
            this.LogoPanel.ResumeLayout(false);
            this.LogoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.OptionsPanel.ResumeLayout(false);
            this.SettingsCMS.ResumeLayout(false);
            this.SettingsCMS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Panel ContentPanel;
        private System.Windows.Forms.ToolTip SettingsToolTip;
        private System.Windows.Forms.Button SettingButton;
        private System.Windows.Forms.ContextMenuStrip SettingsCMS;
        private System.Windows.Forms.ToolStripTextBox TitleSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator SettingsToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem changeThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeComplemenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem createABackupOfYourVaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveChangesClosePassGuardToolStripMenuItem;
        private System.Windows.Forms.Button CreateQuickPassButton;
        private System.Windows.Forms.ToolStripMenuItem configureAnAutoBackupOfAVaultToolStripMenuItem;
        private System.Windows.Forms.Label AppVersionLabel;
    }
}

