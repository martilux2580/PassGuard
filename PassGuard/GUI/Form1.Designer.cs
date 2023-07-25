
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
			this.BottomPanel = new System.Windows.Forms.Panel();
			this.DesignerLabel = new System.Windows.Forms.Label();
			this.AppVersionLabel = new System.Windows.Forms.Label();
			this.CreateQuickPassButton = new System.Windows.Forms.Button();
			this.LoadVaultButton = new System.Windows.Forms.Button();
			this.CreateVaultButton = new System.Windows.Forms.Button();
			this.LogoPanel = new System.Windows.Forms.Panel();
			this.LogoPictureBox = new System.Windows.Forms.PictureBox();
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
			this.exportAVaultsContentAsPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportOutlineColoursAsPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.setPassguardToRunBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setPassguardToMinimizeToTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.saveChangesClosePassGuardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuPanel.SuspendLayout();
			this.BottomPanel.SuspendLayout();
			this.LogoPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
			this.OptionsPanel.SuspendLayout();
			this.SettingsCMS.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuPanel
			// 
			this.MenuPanel.Controls.Add(this.BottomPanel);
			this.MenuPanel.Controls.Add(this.CreateQuickPassButton);
			this.MenuPanel.Controls.Add(this.LoadVaultButton);
			this.MenuPanel.Controls.Add(this.CreateVaultButton);
			this.MenuPanel.Controls.Add(this.LogoPanel);
			this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.MenuPanel.Location = new System.Drawing.Point(0, 0);
			this.MenuPanel.Margin = new System.Windows.Forms.Padding(2);
			this.MenuPanel.Name = "MenuPanel";
			this.MenuPanel.Size = new System.Drawing.Size(336, 662);
			this.MenuPanel.TabIndex = 2;
			// 
			// BottomPanel
			// 
			this.BottomPanel.Controls.Add(this.DesignerLabel);
			this.BottomPanel.Controls.Add(this.AppVersionLabel);
			this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BottomPanel.Location = new System.Drawing.Point(0, 627);
			this.BottomPanel.Name = "BottomPanel";
			this.BottomPanel.Size = new System.Drawing.Size(336, 35);
			this.BottomPanel.TabIndex = 7;
			// 
			// DesignerLabel
			// 
			this.DesignerLabel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DesignerLabel.Dock = System.Windows.Forms.DockStyle.Right;
			this.DesignerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.DesignerLabel.ForeColor = System.Drawing.Color.Black;
			this.DesignerLabel.Location = new System.Drawing.Point(130, 0);
			this.DesignerLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.DesignerLabel.Name = "DesignerLabel";
			this.DesignerLabel.Size = new System.Drawing.Size(206, 35);
			this.DesignerLabel.TabIndex = 1;
			this.DesignerLabel.Text = "Designed by martilux2580";
			this.DesignerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DesignerLabel.UseCompatibleTextRendering = true;
			this.DesignerLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DesignerLabel_MouseClick);
			this.DesignerLabel.MouseEnter += new System.EventHandler(this.DesignerLabel_MouseEnter);
			this.DesignerLabel.MouseLeave += new System.EventHandler(this.DesignerLabel_MouseLeave);
			// 
			// AppVersionLabel
			// 
			this.AppVersionLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this.AppVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.AppVersionLabel.ForeColor = System.Drawing.Color.Black;
			this.AppVersionLabel.Location = new System.Drawing.Point(0, 0);
			this.AppVersionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.AppVersionLabel.Name = "AppVersionLabel";
			this.AppVersionLabel.Size = new System.Drawing.Size(95, 35);
			this.AppVersionLabel.TabIndex = 4;
			this.AppVersionLabel.Text = "          ";
			this.AppVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CreateQuickPassButton
			// 
			this.CreateQuickPassButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CreateQuickPassButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.CreateQuickPassButton.FlatAppearance.BorderSize = 0;
			this.CreateQuickPassButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CreateQuickPassButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.CreateQuickPassButton.ForeColor = System.Drawing.Color.Black;
			this.CreateQuickPassButton.Location = new System.Drawing.Point(0, 339);
			this.CreateQuickPassButton.Margin = new System.Windows.Forms.Padding(2);
			this.CreateQuickPassButton.Name = "CreateQuickPassButton";
			this.CreateQuickPassButton.Size = new System.Drawing.Size(336, 93);
			this.CreateQuickPassButton.TabIndex = 6;
			this.CreateQuickPassButton.Text = "Create Quick Password";
			this.ToolTipNewPassVault.SetToolTip(this.CreateQuickPassButton, "Create a quick + safe password.");
			this.CreateQuickPassButton.UseCompatibleTextRendering = true;
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
			this.LoadVaultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.LoadVaultButton.ForeColor = System.Drawing.Color.Black;
			this.LoadVaultButton.Location = new System.Drawing.Point(0, 246);
			this.LoadVaultButton.Margin = new System.Windows.Forms.Padding(2);
			this.LoadVaultButton.Name = "LoadVaultButton";
			this.LoadVaultButton.Size = new System.Drawing.Size(336, 93);
			this.LoadVaultButton.TabIndex = 5;
			this.LoadVaultButton.Text = "Load Password Vault";
			this.ToolTipNewPassVault.SetToolTip(this.LoadVaultButton, "Load passwords from a Password Vault.");
			this.LoadVaultButton.UseCompatibleTextRendering = true;
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
			this.CreateVaultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.CreateVaultButton.ForeColor = System.Drawing.Color.Black;
			this.CreateVaultButton.Location = new System.Drawing.Point(0, 153);
			this.CreateVaultButton.Margin = new System.Windows.Forms.Padding(2);
			this.CreateVaultButton.Name = "CreateVaultButton";
			this.CreateVaultButton.Size = new System.Drawing.Size(336, 93);
			this.CreateVaultButton.TabIndex = 4;
			this.CreateVaultButton.Text = "New Password Vault";
			this.ToolTipNewPassVault.SetToolTip(this.CreateVaultButton, "Create a Safe Vault to Store your Passwords");
			this.CreateVaultButton.UseCompatibleTextRendering = true;
			this.CreateVaultButton.UseVisualStyleBackColor = true;
			this.CreateVaultButton.Click += new System.EventHandler(this.CreateVaultButton_Click);
			this.CreateVaultButton.MouseEnter += new System.EventHandler(this.CreateVaultButton_MouseEnter);
			this.CreateVaultButton.MouseLeave += new System.EventHandler(this.CreateVaultButton_MouseLeave);
			// 
			// LogoPanel
			// 
			this.LogoPanel.Controls.Add(this.LogoPictureBox);
			this.LogoPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.LogoPanel.Location = new System.Drawing.Point(0, 0);
			this.LogoPanel.Margin = new System.Windows.Forms.Padding(2);
			this.LogoPanel.Name = "LogoPanel";
			this.LogoPanel.Size = new System.Drawing.Size(336, 153);
			this.LogoPanel.TabIndex = 3;
			// 
			// LogoPictureBox
			// 
			this.LogoPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.LogoPictureBox.Image = global::PassGuard.Properties.Resources.LogoAndTitle;
			this.LogoPictureBox.Location = new System.Drawing.Point(40, 3);
			this.LogoPictureBox.Margin = new System.Windows.Forms.Padding(2);
			this.LogoPictureBox.Name = "LogoPictureBox";
			this.LogoPictureBox.Size = new System.Drawing.Size(257, 148);
			this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.LogoPictureBox.TabIndex = 3;
			this.LogoPictureBox.TabStop = false;
			this.LogoPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LogoPictureBox_MouseClick);
			// 
			// OptionsPanel
			// 
			this.OptionsPanel.Controls.Add(this.SettingButton);
			this.OptionsPanel.Controls.Add(this.TitleLabel);
			this.OptionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.OptionsPanel.Location = new System.Drawing.Point(336, 0);
			this.OptionsPanel.Margin = new System.Windows.Forms.Padding(2);
			this.OptionsPanel.Name = "OptionsPanel";
			this.OptionsPanel.Size = new System.Drawing.Size(1048, 153);
			this.OptionsPanel.TabIndex = 3;
			// 
			// SettingButton
			// 
			this.SettingButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.SettingButton.FlatAppearance.BorderSize = 0;
			this.SettingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SettingButton.Image = global::PassGuard.Properties.Resources.Setting;
			this.SettingButton.Location = new System.Drawing.Point(924, 37);
			this.SettingButton.Margin = new System.Windows.Forms.Padding(2);
			this.SettingButton.Name = "SettingButton";
			this.SettingButton.Size = new System.Drawing.Size(74, 78);
			this.SettingButton.TabIndex = 2;
			this.SettingButton.UseVisualStyleBackColor = true;
			this.SettingButton.Click += new System.EventHandler(this.SettingButton_Click);
			// 
			// TitleLabel
			// 
			this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TitleLabel.ForeColor = System.Drawing.SystemColors.Desktop;
			this.TitleLabel.Location = new System.Drawing.Point(0, 0);
			this.TitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(1048, 153);
			this.TitleLabel.TabIndex = 0;
			this.TitleLabel.Text = "HOME";
			this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TitleLabel.UseCompatibleTextRendering = true;
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
			this.ContentPanel.Location = new System.Drawing.Point(336, 153);
			this.ContentPanel.Margin = new System.Windows.Forms.Padding(2);
			this.ContentPanel.Name = "ContentPanel";
			this.ContentPanel.Size = new System.Drawing.Size(1048, 509);
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
            this.exportAVaultsContentAsPDFToolStripMenuItem,
            this.exportOutlineColoursAsPDFToolStripMenuItem,
            this.toolStripSeparator3,
            this.setPassguardToRunBackgroundToolStripMenuItem,
            this.setPassguardToMinimizeToTrayToolStripMenuItem,
            this.toolStripSeparator4,
            this.saveChangesClosePassGuardToolStripMenuItem});
			this.SettingsCMS.Name = "SettingsCMS";
			this.SettingsCMS.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.SettingsCMS.ShowImageMargin = false;
			this.SettingsCMS.Size = new System.Drawing.Size(246, 253);
			// 
			// TitleSettingsToolStripMenuItem
			// 
			this.TitleSettingsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.TitleSettingsToolStripMenuItem.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TitleSettingsToolStripMenuItem.Enabled = false;
			this.TitleSettingsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
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
			this.changeComplemenToolStripMenuItem.Text = "Outline Colour Configuration";
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
			// exportAVaultsContentAsPDFToolStripMenuItem
			// 
			this.exportAVaultsContentAsPDFToolStripMenuItem.Name = "exportAVaultsContentAsPDFToolStripMenuItem";
			this.exportAVaultsContentAsPDFToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
			this.exportAVaultsContentAsPDFToolStripMenuItem.Text = "Export a Vault´s Content as PDF";
			this.exportAVaultsContentAsPDFToolStripMenuItem.Click += new System.EventHandler(this.exportAVaultsContentAsPDFToolStripMenuItem_Click);
			// 
			// exportOutlineColoursAsPDFToolStripMenuItem
			// 
			this.exportOutlineColoursAsPDFToolStripMenuItem.Name = "exportOutlineColoursAsPDFToolStripMenuItem";
			this.exportOutlineColoursAsPDFToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
			this.exportOutlineColoursAsPDFToolStripMenuItem.Text = "Export Outline Colours as PDF";
			this.exportOutlineColoursAsPDFToolStripMenuItem.Click += new System.EventHandler(this.exportOutlineColoursAsPDFToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(242, 6);
			// 
			// setPassguardToRunBackgroundToolStripMenuItem
			// 
			this.setPassguardToRunBackgroundToolStripMenuItem.Name = "setPassguardToRunBackgroundToolStripMenuItem";
			this.setPassguardToRunBackgroundToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
			this.setPassguardToRunBackgroundToolStripMenuItem.Text = "Set Passguard to run on startup";
			this.setPassguardToRunBackgroundToolStripMenuItem.Click += new System.EventHandler(this.setPassguardToRunBackgroundToolStripMenuItem_Click);
			// 
			// setPassguardToMinimizeToTrayToolStripMenuItem
			// 
			this.setPassguardToMinimizeToTrayToolStripMenuItem.Name = "setPassguardToMinimizeToTrayToolStripMenuItem";
			this.setPassguardToMinimizeToTrayToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
			this.setPassguardToMinimizeToTrayToolStripMenuItem.Text = "Set Passguard to minimize to tray";
			this.setPassguardToMinimizeToTrayToolStripMenuItem.Click += new System.EventHandler(this.setPassguardToMinimizeToTrayToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(242, 6);
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
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1384, 662);
			this.Controls.Add(this.ContentPanel);
			this.Controls.Add(this.OptionsPanel);
			this.Controls.Add(this.MenuPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(1400, 701);
			this.Name = "mainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PassGuard™";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainWindow_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Shown += new System.EventHandler(this.mainWindow_Shown);
			this.MenuPanel.ResumeLayout(false);
			this.BottomPanel.ResumeLayout(false);
			this.LogoPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
			this.OptionsPanel.ResumeLayout(false);
			this.SettingsCMS.ResumeLayout(false);
			this.SettingsCMS.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.Panel LogoPanel;
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
        private System.Windows.Forms.ToolStripMenuItem exportOutlineColoursAsPDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exportAVaultsContentAsPDFToolStripMenuItem;
		private System.Windows.Forms.Panel BottomPanel;
		private System.Windows.Forms.ToolStripMenuItem setPassguardToRunBackgroundToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem setPassguardToMinimizeToTrayToolStripMenuItem;
	}
}

