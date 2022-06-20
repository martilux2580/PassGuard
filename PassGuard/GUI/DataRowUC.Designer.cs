
namespace PassGuard.GUI
{
    partial class DataRowUC
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
            this.URLContent = new System.Windows.Forms.Button();
            this.NameContent = new System.Windows.Forms.Button();
            this.PassContent = new System.Windows.Forms.Button();
            this.UsernameContent = new System.Windows.Forms.Button();
            this.NotesContent = new System.Windows.Forms.Button();
            this.CategoryContent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // URLContent
            // 
            this.URLContent.FlatAppearance.BorderSize = 0;
            this.URLContent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.URLContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.URLContent.Location = new System.Drawing.Point(0, 0);
            this.URLContent.Name = "URLContent";
            this.URLContent.Size = new System.Drawing.Size(138, 33);
            this.URLContent.TabIndex = 0;
            this.URLContent.Text = "button1";
            this.URLContent.UseVisualStyleBackColor = true;
            this.URLContent.Click += new System.EventHandler(this.URLContent_Click);
            // 
            // NameContent
            // 
            this.NameContent.FlatAppearance.BorderSize = 0;
            this.NameContent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NameContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameContent.Location = new System.Drawing.Point(145, 0);
            this.NameContent.Name = "NameContent";
            this.NameContent.Size = new System.Drawing.Size(138, 33);
            this.NameContent.TabIndex = 1;
            this.NameContent.Text = "button2";
            this.NameContent.UseVisualStyleBackColor = true;
            this.NameContent.Click += new System.EventHandler(this.NameContent_Click);
            // 
            // PassContent
            // 
            this.PassContent.FlatAppearance.BorderSize = 0;
            this.PassContent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PassContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassContent.Location = new System.Drawing.Point(435, 0);
            this.PassContent.Name = "PassContent";
            this.PassContent.Size = new System.Drawing.Size(138, 33);
            this.PassContent.TabIndex = 3;
            this.PassContent.Text = "button3";
            this.PassContent.UseVisualStyleBackColor = true;
            this.PassContent.Click += new System.EventHandler(this.PassContent_Click);
            // 
            // UsernameContent
            // 
            this.UsernameContent.FlatAppearance.BorderSize = 0;
            this.UsernameContent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UsernameContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameContent.Location = new System.Drawing.Point(290, 0);
            this.UsernameContent.Name = "UsernameContent";
            this.UsernameContent.Size = new System.Drawing.Size(138, 33);
            this.UsernameContent.TabIndex = 2;
            this.UsernameContent.Text = "button4";
            this.UsernameContent.UseVisualStyleBackColor = true;
            this.UsernameContent.Click += new System.EventHandler(this.UsernameContent_Click);
            // 
            // NotesContent
            // 
            this.NotesContent.FlatAppearance.BorderSize = 0;
            this.NotesContent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NotesContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotesContent.Location = new System.Drawing.Point(725, 0);
            this.NotesContent.Name = "NotesContent";
            this.NotesContent.Size = new System.Drawing.Size(138, 33);
            this.NotesContent.TabIndex = 5;
            this.NotesContent.Text = "button5";
            this.NotesContent.UseVisualStyleBackColor = true;
            this.NotesContent.Click += new System.EventHandler(this.NotesContent_Click);
            // 
            // CategoryContent
            // 
            this.CategoryContent.FlatAppearance.BorderSize = 0;
            this.CategoryContent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CategoryContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryContent.Location = new System.Drawing.Point(580, 0);
            this.CategoryContent.Name = "CategoryContent";
            this.CategoryContent.Size = new System.Drawing.Size(138, 33);
            this.CategoryContent.TabIndex = 4;
            this.CategoryContent.Text = "button6";
            this.CategoryContent.UseVisualStyleBackColor = true;
            this.CategoryContent.Click += new System.EventHandler(this.CategoryContent_Click);
            // 
            // DataRowUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NotesContent);
            this.Controls.Add(this.CategoryContent);
            this.Controls.Add(this.PassContent);
            this.Controls.Add(this.UsernameContent);
            this.Controls.Add(this.NameContent);
            this.Controls.Add(this.URLContent);
            this.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.Name = "DataRowUC";
            this.Size = new System.Drawing.Size(864, 33);
            this.Load += new System.EventHandler(this.DataRowUC_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button URLContent;
        internal System.Windows.Forms.Button NameContent;
        internal System.Windows.Forms.Button PassContent;
        internal System.Windows.Forms.Button UsernameContent;
        internal System.Windows.Forms.Button NotesContent;
        internal System.Windows.Forms.Button CategoryContent;
    }
}
