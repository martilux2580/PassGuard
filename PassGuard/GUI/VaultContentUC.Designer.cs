
namespace PassGuard.GUI
{
    partial class VaultContentUC
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
            this.ContentFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.HeaderTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.NotesButton = new System.Windows.Forms.Button();
            this.CategoryButton = new System.Windows.Forms.Button();
            this.PassButton = new System.Windows.Forms.Button();
            this.UsernameButton = new System.Windows.Forms.Button();
            this.NameButton = new System.Windows.Forms.Button();
            this.URLButton = new System.Windows.Forms.Button();
            this.HeaderTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentFlowLayoutPanel
            // 
            this.ContentFlowLayoutPanel.AutoScroll = true;
            this.ContentFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContentFlowLayoutPanel.Location = new System.Drawing.Point(3, 42);
            this.ContentFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.ContentFlowLayoutPanel.Name = "ContentFlowLayoutPanel";
            this.ContentFlowLayoutPanel.Size = new System.Drawing.Size(889, 356);
            this.ContentFlowLayoutPanel.TabIndex = 0;
            // 
            // HeaderTableLayoutPanel
            // 
            this.HeaderTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.HeaderTableLayoutPanel.ColumnCount = 7;
            this.HeaderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.38F));
            this.HeaderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.38F));
            this.HeaderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.38F));
            this.HeaderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.38F));
            this.HeaderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.38F));
            this.HeaderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.38F));
            this.HeaderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.72F));
            this.HeaderTableLayoutPanel.Controls.Add(this.NotesButton, 5, 0);
            this.HeaderTableLayoutPanel.Controls.Add(this.CategoryButton, 4, 0);
            this.HeaderTableLayoutPanel.Controls.Add(this.PassButton, 3, 0);
            this.HeaderTableLayoutPanel.Controls.Add(this.UsernameButton, 2, 0);
            this.HeaderTableLayoutPanel.Controls.Add(this.NameButton, 1, 0);
            this.HeaderTableLayoutPanel.Controls.Add(this.URLButton, 0, 0);
            this.HeaderTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.HeaderTableLayoutPanel.Name = "HeaderTableLayoutPanel";
            this.HeaderTableLayoutPanel.RowCount = 1;
            this.HeaderTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.HeaderTableLayoutPanel.Size = new System.Drawing.Size(889, 41);
            this.HeaderTableLayoutPanel.TabIndex = 1;
            // 
            // NotesButton
            // 
            this.NotesButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotesButton.FlatAppearance.BorderSize = 0;
            this.NotesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NotesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotesButton.Location = new System.Drawing.Point(729, 4);
            this.NotesButton.Name = "NotesButton";
            this.NotesButton.Size = new System.Drawing.Size(138, 33);
            this.NotesButton.TabIndex = 5;
            this.NotesButton.Text = "Notes";
            this.NotesButton.UseVisualStyleBackColor = true;
            this.NotesButton.Click += new System.EventHandler(this.NotesButton_Click);
            // 
            // CategoryButton
            // 
            this.CategoryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CategoryButton.FlatAppearance.BorderSize = 0;
            this.CategoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CategoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryButton.Location = new System.Drawing.Point(584, 4);
            this.CategoryButton.Name = "CategoryButton";
            this.CategoryButton.Size = new System.Drawing.Size(138, 33);
            this.CategoryButton.TabIndex = 4;
            this.CategoryButton.Text = "Category";
            this.CategoryButton.UseVisualStyleBackColor = true;
            this.CategoryButton.Click += new System.EventHandler(this.CategoryButton_Click);
            // 
            // PassButton
            // 
            this.PassButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PassButton.FlatAppearance.BorderSize = 0;
            this.PassButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PassButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassButton.Location = new System.Drawing.Point(439, 4);
            this.PassButton.Name = "PassButton";
            this.PassButton.Size = new System.Drawing.Size(138, 33);
            this.PassButton.TabIndex = 3;
            this.PassButton.Text = "Site Password";
            this.PassButton.UseVisualStyleBackColor = true;
            this.PassButton.Click += new System.EventHandler(this.PassButton_Click);
            // 
            // UsernameButton
            // 
            this.UsernameButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsernameButton.FlatAppearance.BorderSize = 0;
            this.UsernameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UsernameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameButton.Location = new System.Drawing.Point(294, 4);
            this.UsernameButton.Name = "UsernameButton";
            this.UsernameButton.Size = new System.Drawing.Size(138, 33);
            this.UsernameButton.TabIndex = 2;
            this.UsernameButton.Text = "Site Username";
            this.UsernameButton.UseVisualStyleBackColor = true;
            this.UsernameButton.Click += new System.EventHandler(this.UsernameButton_Click);
            // 
            // NameButton
            // 
            this.NameButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameButton.FlatAppearance.BorderSize = 0;
            this.NameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameButton.Location = new System.Drawing.Point(149, 4);
            this.NameButton.Name = "NameButton";
            this.NameButton.Size = new System.Drawing.Size(138, 33);
            this.NameButton.TabIndex = 1;
            this.NameButton.Text = "Name";
            this.NameButton.UseVisualStyleBackColor = true;
            this.NameButton.Click += new System.EventHandler(this.NameButton_Click);
            // 
            // URLButton
            // 
            this.URLButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.URLButton.FlatAppearance.BorderSize = 0;
            this.URLButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.URLButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.URLButton.Location = new System.Drawing.Point(4, 4);
            this.URLButton.Name = "URLButton";
            this.URLButton.Size = new System.Drawing.Size(138, 33);
            this.URLButton.TabIndex = 0;
            this.URLButton.Text = "URL";
            this.URLButton.UseVisualStyleBackColor = true;
            this.URLButton.Click += new System.EventHandler(this.URLButton_Click);
            // 
            // VaultContentUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HeaderTableLayoutPanel);
            this.Controls.Add(this.ContentFlowLayoutPanel);
            this.Name = "VaultContentUC";
            this.Size = new System.Drawing.Size(895, 440);
            this.HeaderTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ContentFlowLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel HeaderTableLayoutPanel;
        private System.Windows.Forms.Button URLButton;
        private System.Windows.Forms.Button NotesButton;
        private System.Windows.Forms.Button CategoryButton;
        private System.Windows.Forms.Button PassButton;
        private System.Windows.Forms.Button UsernameButton;
        private System.Windows.Forms.Button NameButton;
    }
}
