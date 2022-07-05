﻿
namespace PassGuard.GUI
{
    partial class CreateNewVaultUC
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
            this.VaultNameTextbox = new System.Windows.Forms.TextBox();
            this.VaultNameLabel = new System.Windows.Forms.Label();
            this.VaultPathLabel = new System.Windows.Forms.Label();
            this.VaultPathTextbox = new System.Windows.Forms.TextBox();
            this.SelectVaultPathButton = new System.Windows.Forms.Button();
            this.VaultPassLabel = new System.Windows.Forms.Label();
            this.VaultPassTextbox = new System.Windows.Forms.TextBox();
            this.ConfirmPassVaultLabel = new System.Windows.Forms.Label();
            this.ConfirmPassVaultTextbox = new System.Windows.Forms.TextBox();
            this.UserEmailLabel = new System.Windows.Forms.Label();
            this.VaultEmailTextbox = new System.Windows.Forms.TextBox();
            this.CreateNewVaultButton = new System.Windows.Forms.Button();
            this.SaveEmailCheckbox = new System.Windows.Forms.CheckBox();
            this.SaveEmailTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // VaultNameTextbox
            // 
            this.VaultNameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.VaultNameTextbox.Location = new System.Drawing.Point(348, 115);
            this.VaultNameTextbox.Name = "VaultNameTextbox";
            this.VaultNameTextbox.Size = new System.Drawing.Size(423, 24);
            this.VaultNameTextbox.TabIndex = 0;
            // 
            // VaultNameLabel
            // 
            this.VaultNameLabel.AutoSize = true;
            this.VaultNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VaultNameLabel.Location = new System.Drawing.Point(119, 118);
            this.VaultNameLabel.Name = "VaultNameLabel";
            this.VaultNameLabel.Size = new System.Drawing.Size(139, 18);
            this.VaultNameLabel.TabIndex = 1;
            this.VaultNameLabel.Text = "New Vault´s Name: ";
            // 
            // VaultPathLabel
            // 
            this.VaultPathLabel.AutoSize = true;
            this.VaultPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VaultPathLabel.Location = new System.Drawing.Point(119, 316);
            this.VaultPathLabel.Name = "VaultPathLabel";
            this.VaultPathLabel.Size = new System.Drawing.Size(129, 18);
            this.VaultPathLabel.TabIndex = 3;
            this.VaultPathLabel.Text = "New Vault´s Path: ";
            // 
            // VaultPathTextbox
            // 
            this.VaultPathTextbox.Enabled = false;
            this.VaultPathTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.VaultPathTextbox.Location = new System.Drawing.Point(348, 313);
            this.VaultPathTextbox.Name = "VaultPathTextbox";
            this.VaultPathTextbox.Size = new System.Drawing.Size(392, 24);
            this.VaultPathTextbox.TabIndex = 2;
            // 
            // SelectVaultPathButton
            // 
            this.SelectVaultPathButton.FlatAppearance.BorderSize = 0;
            this.SelectVaultPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectVaultPathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.SelectVaultPathButton.Location = new System.Drawing.Point(746, 312);
            this.SelectVaultPathButton.Name = "SelectVaultPathButton";
            this.SelectVaultPathButton.Size = new System.Drawing.Size(25, 25);
            this.SelectVaultPathButton.TabIndex = 4;
            this.SelectVaultPathButton.UseVisualStyleBackColor = true;
            this.SelectVaultPathButton.Click += new System.EventHandler(this.SelectVaultPathButton_Click);
            // 
            // VaultPassLabel
            // 
            this.VaultPassLabel.AutoSize = true;
            this.VaultPassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VaultPassLabel.Location = new System.Drawing.Point(119, 183);
            this.VaultPassLabel.Name = "VaultPassLabel";
            this.VaultPassLabel.Size = new System.Drawing.Size(166, 18);
            this.VaultPassLabel.TabIndex = 6;
            this.VaultPassLabel.Text = "New Vault´s Password: ";
            // 
            // VaultPassTextbox
            // 
            this.VaultPassTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.VaultPassTextbox.Location = new System.Drawing.Point(348, 180);
            this.VaultPassTextbox.Name = "VaultPassTextbox";
            this.VaultPassTextbox.PasswordChar = '*';
            this.VaultPassTextbox.Size = new System.Drawing.Size(423, 24);
            this.VaultPassTextbox.TabIndex = 5;
            // 
            // ConfirmPassVaultLabel
            // 
            this.ConfirmPassVaultLabel.AutoSize = true;
            this.ConfirmPassVaultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmPassVaultLabel.Location = new System.Drawing.Point(119, 249);
            this.ConfirmPassVaultLabel.Name = "ConfirmPassVaultLabel";
            this.ConfirmPassVaultLabel.Size = new System.Drawing.Size(223, 18);
            this.ConfirmPassVaultLabel.TabIndex = 8;
            this.ConfirmPassVaultLabel.Text = "Confirm New Vault´s Password: ";
            // 
            // ConfirmPassVaultTextbox
            // 
            this.ConfirmPassVaultTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.ConfirmPassVaultTextbox.Location = new System.Drawing.Point(348, 246);
            this.ConfirmPassVaultTextbox.Name = "ConfirmPassVaultTextbox";
            this.ConfirmPassVaultTextbox.PasswordChar = '*';
            this.ConfirmPassVaultTextbox.Size = new System.Drawing.Size(423, 24);
            this.ConfirmPassVaultTextbox.TabIndex = 7;
            // 
            // UserEmailLabel
            // 
            this.UserEmailLabel.AutoSize = true;
            this.UserEmailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserEmailLabel.Location = new System.Drawing.Point(119, 53);
            this.UserEmailLabel.Name = "UserEmailLabel";
            this.UserEmailLabel.Size = new System.Drawing.Size(89, 18);
            this.UserEmailLabel.TabIndex = 10;
            this.UserEmailLabel.Text = "User Email: ";
            // 
            // VaultEmailTextbox
            // 
            this.VaultEmailTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.VaultEmailTextbox.Location = new System.Drawing.Point(348, 50);
            this.VaultEmailTextbox.Name = "VaultEmailTextbox";
            this.VaultEmailTextbox.Size = new System.Drawing.Size(329, 24);
            this.VaultEmailTextbox.TabIndex = 9;
            // 
            // CreateNewVaultButton
            // 
            this.CreateNewVaultButton.FlatAppearance.BorderSize = 0;
            this.CreateNewVaultButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateNewVaultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.CreateNewVaultButton.Location = new System.Drawing.Point(327, 372);
            this.CreateNewVaultButton.Name = "CreateNewVaultButton";
            this.CreateNewVaultButton.Size = new System.Drawing.Size(237, 33);
            this.CreateNewVaultButton.TabIndex = 11;
            this.CreateNewVaultButton.Text = "Create New Password Vault";
            this.CreateNewVaultButton.UseVisualStyleBackColor = true;
            this.CreateNewVaultButton.Click += new System.EventHandler(this.CreateNewVaultButton_Click);
            this.CreateNewVaultButton.MouseEnter += new System.EventHandler(this.CreateNewVaultButton_MouseEnter);
            this.CreateNewVaultButton.MouseLeave += new System.EventHandler(this.CreateNewVaultButton_MouseLeave);
            // 
            // SaveEmailCheckbox
            // 
            this.SaveEmailCheckbox.AutoSize = true;
            this.SaveEmailCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveEmailCheckbox.Location = new System.Drawing.Point(683, 52);
            this.SaveEmailCheckbox.Name = "SaveEmailCheckbox";
            this.SaveEmailCheckbox.Size = new System.Drawing.Size(88, 19);
            this.SaveEmailCheckbox.TabIndex = 12;
            this.SaveEmailCheckbox.Text = "Save Email";
            this.SaveEmailCheckbox.UseVisualStyleBackColor = true;
            // 
            // SaveEmailTooltip
            // 
            this.SaveEmailTooltip.AutomaticDelay = 300;
            this.SaveEmailTooltip.AutoPopDelay = 5000;
            this.SaveEmailTooltip.InitialDelay = 300;
            this.SaveEmailTooltip.ReshowDelay = 60;
            // 
            // CreateNewVaultUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SaveEmailCheckbox);
            this.Controls.Add(this.CreateNewVaultButton);
            this.Controls.Add(this.UserEmailLabel);
            this.Controls.Add(this.VaultEmailTextbox);
            this.Controls.Add(this.ConfirmPassVaultLabel);
            this.Controls.Add(this.ConfirmPassVaultTextbox);
            this.Controls.Add(this.VaultPassLabel);
            this.Controls.Add(this.VaultPassTextbox);
            this.Controls.Add(this.SelectVaultPathButton);
            this.Controls.Add(this.VaultPathLabel);
            this.Controls.Add(this.VaultPathTextbox);
            this.Controls.Add(this.VaultNameLabel);
            this.Controls.Add(this.VaultNameTextbox);
            this.Name = "CreateNewVaultUC";
            this.Size = new System.Drawing.Size(900, 444);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox VaultNameTextbox;
        private System.Windows.Forms.Label VaultNameLabel;
        private System.Windows.Forms.Label VaultPathLabel;
        private System.Windows.Forms.TextBox VaultPathTextbox;
        private System.Windows.Forms.Button SelectVaultPathButton;
        private System.Windows.Forms.Label VaultPassLabel;
        private System.Windows.Forms.TextBox VaultPassTextbox;
        private System.Windows.Forms.Label ConfirmPassVaultLabel;
        private System.Windows.Forms.TextBox ConfirmPassVaultTextbox;
        private System.Windows.Forms.Label UserEmailLabel;
        private System.Windows.Forms.TextBox VaultEmailTextbox;
        private System.Windows.Forms.Button CreateNewVaultButton;
        private System.Windows.Forms.CheckBox SaveEmailCheckbox;
        private System.Windows.Forms.ToolTip SaveEmailTooltip;
    }
}