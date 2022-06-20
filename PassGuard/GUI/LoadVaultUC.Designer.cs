
namespace PassGuard.GUI
{
    partial class LoadVaultUC
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
            this.VaultEmailLabel = new System.Windows.Forms.Label();
            this.VaultEmailTextbox = new System.Windows.Forms.TextBox();
            this.VaultPassLabel = new System.Windows.Forms.Label();
            this.VaultPassTextbox = new System.Windows.Forms.TextBox();
            this.SecurityKeyLabel = new System.Windows.Forms.Label();
            this.SecurityKeyTextbox = new System.Windows.Forms.TextBox();
            this.SelectVaultPathButton = new System.Windows.Forms.Button();
            this.VaultPathLabel = new System.Windows.Forms.Label();
            this.VaultPathTextbox = new System.Windows.Forms.TextBox();
            this.LoadSavedSKButton = new System.Windows.Forms.Button();
            this.LoadVaultButton = new System.Windows.Forms.Button();
            this.LoadSavedEmailButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // VaultEmailLabel
            // 
            this.VaultEmailLabel.AutoSize = true;
            this.VaultEmailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VaultEmailLabel.Location = new System.Drawing.Point(124, 80);
            this.VaultEmailLabel.Name = "VaultEmailLabel";
            this.VaultEmailLabel.Size = new System.Drawing.Size(89, 18);
            this.VaultEmailLabel.TabIndex = 18;
            this.VaultEmailLabel.Text = "User Email: ";
            // 
            // VaultEmailTextbox
            // 
            this.VaultEmailTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.VaultEmailTextbox.Location = new System.Drawing.Point(353, 77);
            this.VaultEmailTextbox.Name = "VaultEmailTextbox";
            this.VaultEmailTextbox.Size = new System.Drawing.Size(284, 24);
            this.VaultEmailTextbox.TabIndex = 17;
            // 
            // VaultPassLabel
            // 
            this.VaultPassLabel.AutoSize = true;
            this.VaultPassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VaultPassLabel.Location = new System.Drawing.Point(124, 155);
            this.VaultPassLabel.Name = "VaultPassLabel";
            this.VaultPassLabel.Size = new System.Drawing.Size(132, 18);
            this.VaultPassLabel.TabIndex = 14;
            this.VaultPassLabel.Text = "Vault´s Password: ";
            this.VaultPassLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VaultPassTextbox
            // 
            this.VaultPassTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.VaultPassTextbox.Location = new System.Drawing.Point(353, 152);
            this.VaultPassTextbox.Name = "VaultPassTextbox";
            this.VaultPassTextbox.PasswordChar = '*';
            this.VaultPassTextbox.Size = new System.Drawing.Size(423, 24);
            this.VaultPassTextbox.TabIndex = 13;
            // 
            // SecurityKeyLabel
            // 
            this.SecurityKeyLabel.AutoSize = true;
            this.SecurityKeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecurityKeyLabel.Location = new System.Drawing.Point(124, 231);
            this.SecurityKeyLabel.Name = "SecurityKeyLabel";
            this.SecurityKeyLabel.Size = new System.Drawing.Size(132, 18);
            this.SecurityKeyLabel.TabIndex = 12;
            this.SecurityKeyLabel.Text = "Security Key (SK): ";
            // 
            // SecurityKeyTextbox
            // 
            this.SecurityKeyTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.SecurityKeyTextbox.Location = new System.Drawing.Point(353, 228);
            this.SecurityKeyTextbox.Name = "SecurityKeyTextbox";
            this.SecurityKeyTextbox.PasswordChar = '*';
            this.SecurityKeyTextbox.Size = new System.Drawing.Size(284, 24);
            this.SecurityKeyTextbox.TabIndex = 11;
            // 
            // SelectVaultPathButton
            // 
            this.SelectVaultPathButton.FlatAppearance.BorderSize = 0;
            this.SelectVaultPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectVaultPathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.SelectVaultPathButton.Location = new System.Drawing.Point(751, 301);
            this.SelectVaultPathButton.Name = "SelectVaultPathButton";
            this.SelectVaultPathButton.Size = new System.Drawing.Size(25, 25);
            this.SelectVaultPathButton.TabIndex = 21;
            this.SelectVaultPathButton.UseVisualStyleBackColor = true;
            this.SelectVaultPathButton.Click += new System.EventHandler(this.SelectVaultPathButton_Click);
            // 
            // VaultPathLabel
            // 
            this.VaultPathLabel.AutoSize = true;
            this.VaultPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VaultPathLabel.Location = new System.Drawing.Point(124, 305);
            this.VaultPathLabel.Name = "VaultPathLabel";
            this.VaultPathLabel.Size = new System.Drawing.Size(95, 18);
            this.VaultPathLabel.TabIndex = 20;
            this.VaultPathLabel.Text = "Vault´s Path: ";
            // 
            // VaultPathTextbox
            // 
            this.VaultPathTextbox.Enabled = false;
            this.VaultPathTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.VaultPathTextbox.Location = new System.Drawing.Point(353, 302);
            this.VaultPathTextbox.Name = "VaultPathTextbox";
            this.VaultPathTextbox.Size = new System.Drawing.Size(392, 24);
            this.VaultPathTextbox.TabIndex = 19;
            // 
            // LoadSavedSKButton
            // 
            this.LoadSavedSKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadSavedSKButton.Location = new System.Drawing.Point(643, 227);
            this.LoadSavedSKButton.Name = "LoadSavedSKButton";
            this.LoadSavedSKButton.Size = new System.Drawing.Size(133, 25);
            this.LoadSavedSKButton.TabIndex = 22;
            this.LoadSavedSKButton.Text = "Load Saved SK";
            this.LoadSavedSKButton.UseVisualStyleBackColor = true;
            this.LoadSavedSKButton.Click += new System.EventHandler(this.LoadSavedSKButton_Click);
            this.LoadSavedSKButton.MouseEnter += new System.EventHandler(this.LoadSavedSKButton_MouseEnter);
            this.LoadSavedSKButton.MouseLeave += new System.EventHandler(this.LoadSavedSKButton_MouseLeave);
            // 
            // LoadVaultButton
            // 
            this.LoadVaultButton.FlatAppearance.BorderSize = 0;
            this.LoadVaultButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadVaultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.LoadVaultButton.Location = new System.Drawing.Point(353, 369);
            this.LoadVaultButton.Name = "LoadVaultButton";
            this.LoadVaultButton.Size = new System.Drawing.Size(176, 33);
            this.LoadVaultButton.TabIndex = 23;
            this.LoadVaultButton.Text = "Load Password Vault";
            this.LoadVaultButton.UseVisualStyleBackColor = true;
            this.LoadVaultButton.Click += new System.EventHandler(this.LoadVaultButton_Click);
            this.LoadVaultButton.MouseEnter += new System.EventHandler(this.LoadVaultButton_MouseEnter);
            this.LoadVaultButton.MouseLeave += new System.EventHandler(this.LoadVaultButton_MouseLeave);
            // 
            // LoadSavedEmailButton
            // 
            this.LoadSavedEmailButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadSavedEmailButton.Location = new System.Drawing.Point(643, 76);
            this.LoadSavedEmailButton.Name = "LoadSavedEmailButton";
            this.LoadSavedEmailButton.Size = new System.Drawing.Size(133, 25);
            this.LoadSavedEmailButton.TabIndex = 24;
            this.LoadSavedEmailButton.Text = "Load Saved Email";
            this.LoadSavedEmailButton.UseVisualStyleBackColor = true;
            this.LoadSavedEmailButton.Click += new System.EventHandler(this.LoadSavedEmailButton_Click);
            this.LoadSavedEmailButton.MouseEnter += new System.EventHandler(this.LoadSavedEmailButton_MouseEnter);
            this.LoadSavedEmailButton.MouseLeave += new System.EventHandler(this.LoadSavedEmailButton_MouseLeave);
            // 
            // LoadVaultUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LoadSavedEmailButton);
            this.Controls.Add(this.LoadVaultButton);
            this.Controls.Add(this.LoadSavedSKButton);
            this.Controls.Add(this.SelectVaultPathButton);
            this.Controls.Add(this.VaultPathLabel);
            this.Controls.Add(this.VaultPathTextbox);
            this.Controls.Add(this.VaultEmailLabel);
            this.Controls.Add(this.VaultEmailTextbox);
            this.Controls.Add(this.VaultPassLabel);
            this.Controls.Add(this.VaultPassTextbox);
            this.Controls.Add(this.SecurityKeyLabel);
            this.Controls.Add(this.SecurityKeyTextbox);
            this.Name = "LoadVaultUC";
            this.Size = new System.Drawing.Size(900, 444);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label VaultEmailLabel;
        private System.Windows.Forms.TextBox VaultEmailTextbox;
        private System.Windows.Forms.Label VaultPassLabel;
        private System.Windows.Forms.TextBox VaultPassTextbox;
        private System.Windows.Forms.Label SecurityKeyLabel;
        private System.Windows.Forms.TextBox SecurityKeyTextbox;
        private System.Windows.Forms.Button SelectVaultPathButton;
        private System.Windows.Forms.Label VaultPathLabel;
        private System.Windows.Forms.TextBox VaultPathTextbox;
        private System.Windows.Forms.Button LoadSavedSKButton;
        private System.Windows.Forms.Button LoadVaultButton;
        private System.Windows.Forms.Button LoadSavedEmailButton;
    }
}
