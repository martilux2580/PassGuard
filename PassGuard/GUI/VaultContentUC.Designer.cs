
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			this.VaultContentDGV = new System.Windows.Forms.DataGridView();
			this.URLColumn = new System.Windows.Forms.DataGridViewButtonColumn();
			this.NameColumn = new System.Windows.Forms.DataGridViewButtonColumn();
			this.SiteUsernameColumn = new System.Windows.Forms.DataGridViewButtonColumn();
			this.PasswordColumn = new System.Windows.Forms.DataGridViewButtonColumn();
			this.CategoryColumn = new System.Windows.Forms.DataGridViewButtonColumn();
			this.NotesColumn = new System.Windows.Forms.DataGridViewButtonColumn();
			this.ImportantColumn = new System.Windows.Forms.DataGridViewButtonColumn();
			this.DeleteRowColumn = new System.Windows.Forms.DataGridViewButtonColumn();
			this.AddButton = new System.Windows.Forms.Button();
			this.EditButton = new System.Windows.Forms.Button();
			this.HelpButton = new System.Windows.Forms.Button();
			this.DeleteButton = new System.Windows.Forms.Button();
			this.URLCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.URLCMSSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.URLNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.URLAscendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.URLDescendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.NameCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.NameCMSSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.NameNormalCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.NameAscendingCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.NameDescendingCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.UsernameCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.UsernameCMSSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.UsernameNormalCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.UsernameAscendingCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.UsernameDescendingCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.CategoryCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CategoryCMSSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.CategoryNormalCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.CategoryAscendingCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.CategoryDescendingCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.NotesCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.NotesCMSSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.NotesNormalCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.NotesAscendingCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.NotesDescendingCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.ExportAsPdfButton = new System.Windows.Forms.Button();
			this.ImportantCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ImportantCMSSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.ImportantNormalCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.ImportantAscendingCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.ImportantDescendingCMS = new System.Windows.Forms.ToolStripMenuItem();
			this.SearchButton = new System.Windows.Forms.Button();
			this.SearchTextbox = new System.Windows.Forms.TextBox();
			this.ResetButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.VaultContentDGV)).BeginInit();
			this.URLCMS.SuspendLayout();
			this.NameCMS.SuspendLayout();
			this.UsernameCMS.SuspendLayout();
			this.CategoryCMS.SuspendLayout();
			this.NotesCMS.SuspendLayout();
			this.ImportantCMS.SuspendLayout();
			this.SuspendLayout();
			// 
			// VaultContentDGV
			// 
			this.VaultContentDGV.AllowUserToAddRows = false;
			this.VaultContentDGV.AllowUserToDeleteRows = false;
			this.VaultContentDGV.AllowUserToResizeColumns = false;
			this.VaultContentDGV.AllowUserToResizeRows = false;
			this.VaultContentDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.VaultContentDGV.BackgroundColor = System.Drawing.SystemColors.Control;
			this.VaultContentDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.VaultContentDGV.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			this.VaultContentDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
			this.VaultContentDGV.ColumnHeadersHeight = 47;
			this.VaultContentDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.VaultContentDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.URLColumn,
            this.NameColumn,
            this.SiteUsernameColumn,
            this.PasswordColumn,
            this.CategoryColumn,
            this.NotesColumn,
            this.ImportantColumn,
            this.DeleteRowColumn});
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.Red;
			dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.VaultContentDGV.DefaultCellStyle = dataGridViewCellStyle17;
			this.VaultContentDGV.EnableHeadersVisualStyles = false;
			this.VaultContentDGV.GridColor = System.Drawing.SystemColors.InfoText;
			this.VaultContentDGV.Location = new System.Drawing.Point(4, 3);
			this.VaultContentDGV.Name = "VaultContentDGV";
			this.VaultContentDGV.RowHeadersVisible = false;
			this.VaultContentDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
			this.VaultContentDGV.RowsDefaultCellStyle = dataGridViewCellStyle18;
			this.VaultContentDGV.RowTemplate.Height = 38;
			this.VaultContentDGV.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.VaultContentDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.VaultContentDGV.Size = new System.Drawing.Size(1040, 414);
			this.VaultContentDGV.TabIndex = 0;
			this.VaultContentDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.VaultContentDGV_CellContentClick);
			this.VaultContentDGV.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.VaultContentDGV_ColumnHeaderMouseClick);
			// 
			// URLColumn
			// 
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.URLColumn.DefaultCellStyle = dataGridViewCellStyle14;
			this.URLColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.URLColumn.HeaderText = "URL";
			this.URLColumn.Name = "URLColumn";
			this.URLColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.URLColumn.Text = "";
			// 
			// NameColumn
			// 
			this.NameColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.NameColumn.HeaderText = "Name";
			this.NameColumn.Name = "NameColumn";
			this.NameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.NameColumn.Text = "";
			// 
			// SiteUsernameColumn
			// 
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.SiteUsernameColumn.DefaultCellStyle = dataGridViewCellStyle15;
			this.SiteUsernameColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SiteUsernameColumn.HeaderText = "Site Username";
			this.SiteUsernameColumn.Name = "SiteUsernameColumn";
			this.SiteUsernameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.SiteUsernameColumn.Text = "";
			// 
			// PasswordColumn
			// 
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.PasswordColumn.DefaultCellStyle = dataGridViewCellStyle16;
			this.PasswordColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PasswordColumn.HeaderText = "Password";
			this.PasswordColumn.Name = "PasswordColumn";
			this.PasswordColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// CategoryColumn
			// 
			this.CategoryColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CategoryColumn.HeaderText = "Category";
			this.CategoryColumn.Name = "CategoryColumn";
			this.CategoryColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// NotesColumn
			// 
			this.NotesColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.NotesColumn.HeaderText = "Notes";
			this.NotesColumn.Name = "NotesColumn";
			this.NotesColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// ImportantColumn
			// 
			this.ImportantColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ImportantColumn.HeaderText = "Important";
			this.ImportantColumn.Name = "ImportantColumn";
			this.ImportantColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// DeleteRowColumn
			// 
			this.DeleteRowColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DeleteRowColumn.HeaderText = "Delete Row";
			this.DeleteRowColumn.Name = "DeleteRowColumn";
			this.DeleteRowColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.DeleteRowColumn.Text = "Delete";
			this.DeleteRowColumn.UseColumnTextForButtonValue = true;
			// 
			// AddButton
			// 
			this.AddButton.FlatAppearance.BorderSize = 0;
			this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.AddButton.Location = new System.Drawing.Point(4, 466);
			this.AddButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(158, 37);
			this.AddButton.TabIndex = 2;
			this.AddButton.Text = "Add";
			this.AddButton.UseVisualStyleBackColor = true;
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			this.AddButton.MouseEnter += new System.EventHandler(this.AddButton_MouseEnter);
			this.AddButton.MouseLeave += new System.EventHandler(this.AddButton_MouseLeave);
			// 
			// EditButton
			// 
			this.EditButton.FlatAppearance.BorderSize = 0;
			this.EditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.EditButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.EditButton.Location = new System.Drawing.Point(223, 466);
			this.EditButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.EditButton.Name = "EditButton";
			this.EditButton.Size = new System.Drawing.Size(158, 37);
			this.EditButton.TabIndex = 3;
			this.EditButton.Text = "Edit";
			this.EditButton.UseVisualStyleBackColor = true;
			this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
			this.EditButton.MouseEnter += new System.EventHandler(this.EditButton_MouseEnter);
			this.EditButton.MouseLeave += new System.EventHandler(this.EditButton_MouseLeave);
			// 
			// HelpButton
			// 
			this.HelpButton.FlatAppearance.BorderSize = 0;
			this.HelpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.HelpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.HelpButton.Location = new System.Drawing.Point(883, 466);
			this.HelpButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.HelpButton.Name = "HelpButton";
			this.HelpButton.Size = new System.Drawing.Size(158, 37);
			this.HelpButton.TabIndex = 5;
			this.HelpButton.Text = "Help";
			this.HelpButton.UseVisualStyleBackColor = true;
			this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
			this.HelpButton.MouseEnter += new System.EventHandler(this.HelpButton_MouseEnter);
			this.HelpButton.MouseLeave += new System.EventHandler(this.HelpButton_MouseLeave);
			// 
			// DeleteButton
			// 
			this.DeleteButton.FlatAppearance.BorderSize = 0;
			this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.DeleteButton.Location = new System.Drawing.Point(443, 466);
			this.DeleteButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(158, 37);
			this.DeleteButton.TabIndex = 4;
			this.DeleteButton.Text = "Delete";
			this.DeleteButton.UseVisualStyleBackColor = true;
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			this.DeleteButton.MouseEnter += new System.EventHandler(this.DeleteButton_MouseEnter);
			this.DeleteButton.MouseLeave += new System.EventHandler(this.DeleteButton_MouseLeave);
			// 
			// URLCMS
			// 
			this.URLCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.URLCMSSeparator,
            this.URLNormalToolStripMenuItem,
            this.URLAscendingToolStripMenuItem,
            this.URLDescendingToolStripMenuItem});
			this.URLCMS.Name = "URLCMS";
			this.URLCMS.Size = new System.Drawing.Size(170, 76);
			// 
			// URLCMSSeparator
			// 
			this.URLCMSSeparator.Name = "URLCMSSeparator";
			this.URLCMSSeparator.Size = new System.Drawing.Size(166, 6);
			// 
			// URLNormalToolStripMenuItem
			// 
			this.URLNormalToolStripMenuItem.Checked = true;
			this.URLNormalToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.URLNormalToolStripMenuItem.Name = "URLNormalToolStripMenuItem";
			this.URLNormalToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.URLNormalToolStripMenuItem.Text = "Normal Order";
			this.URLNormalToolStripMenuItem.Click += new System.EventHandler(this.URLNormalToolStripMenuItem_Click);
			// 
			// URLAscendingToolStripMenuItem
			// 
			this.URLAscendingToolStripMenuItem.Name = "URLAscendingToolStripMenuItem";
			this.URLAscendingToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.URLAscendingToolStripMenuItem.Text = "Ascending Order";
			this.URLAscendingToolStripMenuItem.Click += new System.EventHandler(this.URLAscendingToolStripMenuItem_Click);
			// 
			// URLDescendingToolStripMenuItem
			// 
			this.URLDescendingToolStripMenuItem.Name = "URLDescendingToolStripMenuItem";
			this.URLDescendingToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.URLDescendingToolStripMenuItem.Text = "Descending Order";
			this.URLDescendingToolStripMenuItem.Click += new System.EventHandler(this.URLDescendingToolStripMenuItem_Click);
			// 
			// NameCMS
			// 
			this.NameCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NameCMSSeparator,
            this.NameNormalCMS,
            this.NameAscendingCMS,
            this.NameDescendingCMS});
			this.NameCMS.Name = "contextMenuStrip1";
			this.NameCMS.Size = new System.Drawing.Size(170, 76);
			// 
			// NameCMSSeparator
			// 
			this.NameCMSSeparator.Name = "NameCMSSeparator";
			this.NameCMSSeparator.Size = new System.Drawing.Size(166, 6);
			// 
			// NameNormalCMS
			// 
			this.NameNormalCMS.Checked = true;
			this.NameNormalCMS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.NameNormalCMS.Name = "NameNormalCMS";
			this.NameNormalCMS.Size = new System.Drawing.Size(169, 22);
			this.NameNormalCMS.Text = "Normal Order";
			this.NameNormalCMS.Click += new System.EventHandler(this.NameNormalCMS_Click);
			// 
			// NameAscendingCMS
			// 
			this.NameAscendingCMS.Name = "NameAscendingCMS";
			this.NameAscendingCMS.Size = new System.Drawing.Size(169, 22);
			this.NameAscendingCMS.Text = "Ascending Order";
			this.NameAscendingCMS.Click += new System.EventHandler(this.NameAscendingCMS_Click);
			// 
			// NameDescendingCMS
			// 
			this.NameDescendingCMS.Name = "NameDescendingCMS";
			this.NameDescendingCMS.Size = new System.Drawing.Size(169, 22);
			this.NameDescendingCMS.Text = "Descending Order";
			this.NameDescendingCMS.Click += new System.EventHandler(this.NameDescendingCMS_Click);
			// 
			// UsernameCMS
			// 
			this.UsernameCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UsernameCMSSeparator,
            this.UsernameNormalCMS,
            this.UsernameAscendingCMS,
            this.UsernameDescendingCMS});
			this.UsernameCMS.Name = "contextMenuStrip1";
			this.UsernameCMS.Size = new System.Drawing.Size(191, 76);
			// 
			// UsernameCMSSeparator
			// 
			this.UsernameCMSSeparator.Name = "UsernameCMSSeparator";
			this.UsernameCMSSeparator.Size = new System.Drawing.Size(187, 6);
			// 
			// UsernameNormalCMS
			// 
			this.UsernameNormalCMS.Checked = true;
			this.UsernameNormalCMS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.UsernameNormalCMS.Name = "UsernameNormalCMS";
			this.UsernameNormalCMS.Size = new System.Drawing.Size(190, 22);
			this.UsernameNormalCMS.Text = "Normal Order";
			this.UsernameNormalCMS.Click += new System.EventHandler(this.UsernameNormalCMS_Click);
			// 
			// UsernameAscendingCMS
			// 
			this.UsernameAscendingCMS.Name = "UsernameAscendingCMS";
			this.UsernameAscendingCMS.Size = new System.Drawing.Size(190, 22);
			this.UsernameAscendingCMS.Text = "Ascending Order";
			this.UsernameAscendingCMS.Click += new System.EventHandler(this.UsernameAscendingCMS_Click);
			// 
			// UsernameDescendingCMS
			// 
			this.UsernameDescendingCMS.Name = "UsernameDescendingCMS";
			this.UsernameDescendingCMS.Size = new System.Drawing.Size(190, 22);
			this.UsernameDescendingCMS.Text = "Descending Order       ";
			this.UsernameDescendingCMS.Click += new System.EventHandler(this.UsernameDescendingCMS_Click);
			// 
			// CategoryCMS
			// 
			this.CategoryCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CategoryCMSSeparator,
            this.CategoryNormalCMS,
            this.CategoryAscendingCMS,
            this.CategoryDescendingCMS});
			this.CategoryCMS.Name = "contextMenuStrip1";
			this.CategoryCMS.Size = new System.Drawing.Size(191, 76);
			// 
			// CategoryCMSSeparator
			// 
			this.CategoryCMSSeparator.Name = "CategoryCMSSeparator";
			this.CategoryCMSSeparator.Size = new System.Drawing.Size(187, 6);
			// 
			// CategoryNormalCMS
			// 
			this.CategoryNormalCMS.Checked = true;
			this.CategoryNormalCMS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CategoryNormalCMS.Name = "CategoryNormalCMS";
			this.CategoryNormalCMS.Size = new System.Drawing.Size(190, 22);
			this.CategoryNormalCMS.Text = "Normal Order";
			this.CategoryNormalCMS.Click += new System.EventHandler(this.CategoryNormalCMS_Click);
			// 
			// CategoryAscendingCMS
			// 
			this.CategoryAscendingCMS.Name = "CategoryAscendingCMS";
			this.CategoryAscendingCMS.Size = new System.Drawing.Size(190, 22);
			this.CategoryAscendingCMS.Text = "Ascending Order";
			this.CategoryAscendingCMS.Click += new System.EventHandler(this.CategoryAscendingCMS_Click);
			// 
			// CategoryDescendingCMS
			// 
			this.CategoryDescendingCMS.Name = "CategoryDescendingCMS";
			this.CategoryDescendingCMS.Size = new System.Drawing.Size(190, 22);
			this.CategoryDescendingCMS.Text = "Descending Order       ";
			this.CategoryDescendingCMS.Click += new System.EventHandler(this.CategoryDescendingCMS_Click);
			// 
			// NotesCMS
			// 
			this.NotesCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NotesCMSSeparator,
            this.NotesNormalCMS,
            this.NotesAscendingCMS,
            this.NotesDescendingCMS});
			this.NotesCMS.Name = "contextMenuStrip1";
			this.NotesCMS.Size = new System.Drawing.Size(170, 76);
			// 
			// NotesCMSSeparator
			// 
			this.NotesCMSSeparator.Name = "NotesCMSSeparator";
			this.NotesCMSSeparator.Size = new System.Drawing.Size(166, 6);
			// 
			// NotesNormalCMS
			// 
			this.NotesNormalCMS.Checked = true;
			this.NotesNormalCMS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.NotesNormalCMS.Name = "NotesNormalCMS";
			this.NotesNormalCMS.Size = new System.Drawing.Size(169, 22);
			this.NotesNormalCMS.Text = "Normal Order";
			this.NotesNormalCMS.Click += new System.EventHandler(this.NotesNormalCMS_Click);
			// 
			// NotesAscendingCMS
			// 
			this.NotesAscendingCMS.Name = "NotesAscendingCMS";
			this.NotesAscendingCMS.Size = new System.Drawing.Size(169, 22);
			this.NotesAscendingCMS.Text = "Ascending Order";
			this.NotesAscendingCMS.Click += new System.EventHandler(this.NotesAscendingCMS_Click);
			// 
			// NotesDescendingCMS
			// 
			this.NotesDescendingCMS.Name = "NotesDescendingCMS";
			this.NotesDescendingCMS.Size = new System.Drawing.Size(169, 22);
			this.NotesDescendingCMS.Text = "Descending Order";
			this.NotesDescendingCMS.Click += new System.EventHandler(this.NotesDescendingCMS_Click);
			// 
			// ExportAsPdfButton
			// 
			this.ExportAsPdfButton.FlatAppearance.BorderSize = 0;
			this.ExportAsPdfButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ExportAsPdfButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ExportAsPdfButton.Location = new System.Drawing.Point(660, 466);
			this.ExportAsPdfButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.ExportAsPdfButton.Name = "ExportAsPdfButton";
			this.ExportAsPdfButton.Size = new System.Drawing.Size(158, 37);
			this.ExportAsPdfButton.TabIndex = 6;
			this.ExportAsPdfButton.Text = "Export as PDF";
			this.ExportAsPdfButton.UseVisualStyleBackColor = true;
			this.ExportAsPdfButton.Click += new System.EventHandler(this.ExportAsPdfButton_Click);
			this.ExportAsPdfButton.MouseEnter += new System.EventHandler(this.ExportAsPdfButton_MouseEnter);
			this.ExportAsPdfButton.MouseLeave += new System.EventHandler(this.ExportAsPdfButton_MouseLeave);
			// 
			// ImportantCMS
			// 
			this.ImportantCMS.AutoSize = false;
			this.ImportantCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportantCMSSeparator,
            this.ImportantNormalCMS,
            this.ImportantAscendingCMS,
            this.ImportantDescendingCMS});
			this.ImportantCMS.Name = "contextMenuStrip1";
			this.ImportantCMS.Size = new System.Drawing.Size(191, 98);
			// 
			// ImportantCMSSeparator
			// 
			this.ImportantCMSSeparator.Name = "ImportantCMSSeparator";
			this.ImportantCMSSeparator.Size = new System.Drawing.Size(187, 6);
			// 
			// ImportantNormalCMS
			// 
			this.ImportantNormalCMS.AutoSize = false;
			this.ImportantNormalCMS.Checked = true;
			this.ImportantNormalCMS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ImportantNormalCMS.Name = "ImportantNormalCMS";
			this.ImportantNormalCMS.Size = new System.Drawing.Size(205, 22);
			this.ImportantNormalCMS.Text = "Normal Order";
			this.ImportantNormalCMS.Click += new System.EventHandler(this.ImportantNormalCMS_Click);
			// 
			// ImportantAscendingCMS
			// 
			this.ImportantAscendingCMS.AutoSize = false;
			this.ImportantAscendingCMS.Name = "ImportantAscendingCMS";
			this.ImportantAscendingCMS.Size = new System.Drawing.Size(205, 22);
			this.ImportantAscendingCMS.Text = "Ascending Order";
			this.ImportantAscendingCMS.Click += new System.EventHandler(this.ImportantAscendingCMS_Click);
			// 
			// ImportantDescendingCMS
			// 
			this.ImportantDescendingCMS.AutoSize = false;
			this.ImportantDescendingCMS.Name = "ImportantDescendingCMS";
			this.ImportantDescendingCMS.Size = new System.Drawing.Size(205, 22);
			this.ImportantDescendingCMS.Text = "Descending Order";
			this.ImportantDescendingCMS.Click += new System.EventHandler(this.ImportantDescendingCMS_Click);
			// 
			// SearchButton
			// 
			this.SearchButton.FlatAppearance.BorderSize = 0;
			this.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SearchButton.Location = new System.Drawing.Point(660, 423);
			this.SearchButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.SearchButton.Name = "SearchButton";
			this.SearchButton.Size = new System.Drawing.Size(158, 37);
			this.SearchButton.TabIndex = 9;
			this.SearchButton.Text = "Search";
			this.SearchButton.UseVisualStyleBackColor = true;
			this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
			this.SearchButton.MouseEnter += new System.EventHandler(this.SearchButton_MouseEnter);
			this.SearchButton.MouseLeave += new System.EventHandler(this.SearchButton_MouseLeave);
			// 
			// SearchTextbox
			// 
			this.SearchTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SearchTextbox.Location = new System.Drawing.Point(294, 423);
			this.SearchTextbox.Multiline = true;
			this.SearchTextbox.Name = "SearchTextbox";
			this.SearchTextbox.PlaceholderText = "Search for Names...";
			this.SearchTextbox.Size = new System.Drawing.Size(265, 37);
			this.SearchTextbox.TabIndex = 10;
			this.SearchTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// ResetButton
			// 
			this.ResetButton.FlatAppearance.BorderSize = 0;
			this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ResetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ResetButton.Location = new System.Drawing.Point(860, 423);
			this.ResetButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.ResetButton.Name = "ResetButton";
			this.ResetButton.Size = new System.Drawing.Size(158, 37);
			this.ResetButton.TabIndex = 11;
			this.ResetButton.Text = "Reset";
			this.ResetButton.UseVisualStyleBackColor = true;
			this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
			this.ResetButton.MouseEnter += new System.EventHandler(this.ResetButton_MouseEnter);
			this.ResetButton.MouseLeave += new System.EventHandler(this.ResetButton_MouseLeave);
			// 
			// VaultContentUC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ResetButton);
			this.Controls.Add(this.SearchTextbox);
			this.Controls.Add(this.SearchButton);
			this.Controls.Add(this.VaultContentDGV);
			this.Controls.Add(this.ExportAsPdfButton);
			this.Controls.Add(this.HelpButton);
			this.Controls.Add(this.DeleteButton);
			this.Controls.Add(this.EditButton);
			this.Controls.Add(this.AddButton);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "VaultContentUC";
			this.Size = new System.Drawing.Size(1044, 508);
			this.BackColorChanged += new System.EventHandler(this.VaultContentUC_BackColorChanged);
			((System.ComponentModel.ISupportInitialize)(this.VaultContentDGV)).EndInit();
			this.URLCMS.ResumeLayout(false);
			this.NameCMS.ResumeLayout(false);
			this.UsernameCMS.ResumeLayout(false);
			this.CategoryCMS.ResumeLayout(false);
			this.NotesCMS.ResumeLayout(false);
			this.ImportantCMS.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button HelpButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.ContextMenuStrip URLCMS;
        private System.Windows.Forms.ToolStripSeparator URLCMSSeparator;
        private System.Windows.Forms.ToolStripMenuItem URLNormalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem URLAscendingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem URLDescendingToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip NameCMS;
        private System.Windows.Forms.ToolStripSeparator NameCMSSeparator;
        private System.Windows.Forms.ToolStripMenuItem NameNormalCMS;
        private System.Windows.Forms.ToolStripMenuItem NameAscendingCMS;
        private System.Windows.Forms.ToolStripMenuItem NameDescendingCMS;
        private System.Windows.Forms.ContextMenuStrip UsernameCMS;
        private System.Windows.Forms.ToolStripSeparator UsernameCMSSeparator;
        private System.Windows.Forms.ToolStripMenuItem UsernameNormalCMS;
        private System.Windows.Forms.ToolStripMenuItem UsernameAscendingCMS;
        private System.Windows.Forms.ToolStripMenuItem UsernameDescendingCMS;
        private System.Windows.Forms.ContextMenuStrip CategoryCMS;
        private System.Windows.Forms.ToolStripSeparator CategoryCMSSeparator;
        private System.Windows.Forms.ToolStripMenuItem CategoryNormalCMS;
        private System.Windows.Forms.ToolStripMenuItem CategoryAscendingCMS;
        private System.Windows.Forms.ToolStripMenuItem CategoryDescendingCMS;
        private System.Windows.Forms.ContextMenuStrip NotesCMS;
        private System.Windows.Forms.ToolStripSeparator NotesCMSSeparator;
        private System.Windows.Forms.ToolStripMenuItem NotesNormalCMS;
        private System.Windows.Forms.ToolStripMenuItem NotesAscendingCMS;
        private System.Windows.Forms.ToolStripMenuItem NotesDescendingCMS;
        private System.Windows.Forms.Button ExportAsPdfButton;
		private System.Windows.Forms.ContextMenuStrip ImportantCMS;
		private System.Windows.Forms.ToolStripSeparator ImportantCMSSeparator;
		private System.Windows.Forms.ToolStripMenuItem ImportantNormalCMS;
		private System.Windows.Forms.ToolStripMenuItem ImportantAscendingCMS;
		private System.Windows.Forms.ToolStripMenuItem ImportantDescendingCMS;
		private System.Windows.Forms.DataGridView VaultContentDGV;
		private System.Windows.Forms.DataGridViewButtonColumn URLColumn;
		private System.Windows.Forms.DataGridViewButtonColumn NameColumn;
		private System.Windows.Forms.DataGridViewButtonColumn SiteUsernameColumn;
		private System.Windows.Forms.DataGridViewButtonColumn PasswordColumn;
		private System.Windows.Forms.DataGridViewButtonColumn CategoryColumn;
		private System.Windows.Forms.DataGridViewButtonColumn NotesColumn;
		private System.Windows.Forms.DataGridViewButtonColumn ImportantColumn;
		private System.Windows.Forms.DataGridViewButtonColumn DeleteRowColumn;
		private System.Windows.Forms.Button SearchButton;
		private System.Windows.Forms.TextBox SearchTextbox;
		private System.Windows.Forms.Button ResetButton;
	}
}
